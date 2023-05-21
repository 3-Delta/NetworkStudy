using System;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;
using UnityEngine;

// 单个transfer, 将来可能多个transfer协作
public class NW_Transfer {
    private Socket socket = null;

    public enum EConnectStatus {
        Ready, // 准备连接
        Connecting, // 连接中

        Connected, // 连接成功
    }

    public enum EDisconnectReason {
        Nil,

#region 本机主动系列
        // 主动断开系列
        Initiative, // 本机主动断开

        ConnectFail,

        SendBeginFail,
        SendEndFail,

        ReceiveBeginFail,
        ReceiveHeadFail,
        ReceiveBodyFail,
#endregion

#region 远端主动系列
        // 被动断开系列
        Passive, // 远程主动断开
#endregion

    }

    public EConnectStatus connectStatus = EConnectStatus.Ready;

    public NW_PackageQueue packageQueue { get; private set; } = new NW_PackageQueue();
    public NW_MessageQueue messageQueue { get; private set; } = new NW_MessageQueue();

    private NW_Buffer buffer = new NW_Buffer();

    public NW_Transfer(Socket socket) {
        this.socket = socket;
    }

    public void OnExit() {
        //this.DisConnect();
    }

#region // Connect
    public void Connect(string ip, int port, System.Action callback = null) {
        this.Connect(IPAddress.Parse(ip), port, callback);
    }

    public void Connect(IPAddress ip, int port, System.Action callback = null) {
        this.Connect(new IPEndPoint(ip, port), callback);
    }

    public void Connect(IPEndPoint ipe, System.Action callback = null) {
        if (this.connectStatus != EConnectStatus.Ready) {
            return;
        }

        this.connectStatus = EConnectStatus.Connecting;
        this.socket = this.socket ?? BS_T_Network.BuildSocket4TCP(ipe.AddressFamily);
        if (socket != null && !socket.Connected) {
            try {
                this.socket.BeginConnect(ipe, new AsyncCallback(this.OnConnected), null);
            }
            catch (Exception e) {
                this.DisConnect(EDisconnectReason.ConnectFail);
                UnityEngine.Debug.Log("Connect Failed : " + e.Message);
            }
        }
    }

    // 不允许DisConnect之后立即Connect, 因为此时OnReceive的线程在DisConnect之后还在工作还在继续读取缓冲区数据
    // 参数表示手动关闭还是自动关闭
    public void DisConnect(EDisconnectReason disReason = EDisconnectReason.Initiative /*缺省主动关闭*/) {
        Debug.LogError($"DisConnect disReason: {disReason.ToString()}, connectStatus: {connectStatus.ToString()}");

        if (this.connectStatus == EConnectStatus.Ready) {
            return;
        }

        Debug.LogError("DisConnect");
        try {
            // 是否选ShutDown(Both);
            socket.Close();
        }
        catch (Exception ex) {
            Debug.LogError($"DisConnect {ex.Message}");
        }
        finally {
            this.connectStatus = EConnectStatus.Ready;
            socket = null;
        }
    }
#endregion

#region // OnTransfer
    private void OnConnected(IAsyncResult ar) {
        try {
            // 建立连接
            this.socket.EndConnect(ar);
            // 收发数据
            this.buffer.Clear();

            this.connectStatus = EConnectStatus.Connected;
            this.messageQueue.Clear();
            this.packageQueue.Clear();

            Debug.LogError("OnConnected ip");
            socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedHead), buffer);
        }
        catch (Exception e) {
            this.DisConnect(EDisconnectReason.ReceiveBeginFail);
            UnityEngine.Debug.Log("OnConnected Failed : " + e.Message);
        }
    }

    private void OnReceivedHead(IAsyncResult ar) {
        if (connectStatus != EConnectStatus.Connected) {
            return;
        }

        Debug.LogError("OnReceivedHead-> ThreadId:" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try {
            SocketError errCode = SocketError.Success;
            // server主动断开，这里IsConnected是true，经过下面的EndReceive，EndReceive就成了false
            int read = this.socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read <= 0) {
                this.DisConnect(EDisconnectReason.Passive);
                return;
            }

            // 暂时将包头存储到buffer中，开始接受body的时候正式转移到head中
            buffer.realLength += read;
            // 包头必须读满
            if (buffer.realLength < NW_Def.PACKAGE_HEAD_SIZE) {
                this.socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(this.OnReceivedHead), buffer);
            }
            else {
                // 处理包头
                buffer.package.head.Decode(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE - 1);
                this.socket.BeginReceive(buffer.buffer, buffer.realLength, buffer.package.head.size - (buffer.realLength - NW_Def.PACKAGE_HEAD_SIZE), SocketFlags.None, new AsyncCallback(this.OnReceivedBody), buffer);
            }
        }
        catch (Exception e) {
            this.DisConnect(EDisconnectReason.ReceiveHeadFail);
            UnityEngine.Debug.Log("OnReceivedHead Failed : " + e.Message);
        }
    }

    private void OnReceivedBody(IAsyncResult ar) {
        if (connectStatus != EConnectStatus.Connected) {
            return;
        }

        Debug.LogError("OnReceivedBody-> ThreadId:" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try {
            SocketError errCode = SocketError.Success;
            int read = this.socket.EndReceive(ar, out errCode);
            // 断开连接
            if (read <= 0) {
                this.DisConnect(EDisconnectReason.Passive);
                return;
            }

            buffer.realLength += read;
            // 消息体不满足长度
            if (buffer.realLength < buffer.package.head.size + NW_Def.PACKAGE_HEAD_SIZE) {
                this.socket.BeginReceive(buffer.buffer, buffer.realLength, buffer.package.head.size - (buffer.realLength - NW_Def.PACKAGE_HEAD_SIZE), SocketFlags.None, new AsyncCallback(this.OnReceivedBody), buffer);
            }
            else {
                // 接收大一个包，判断该包有没有后续序列包
                buffer.package.body.Decode(buffer.buffer, NW_Def.PACKAGE_HEAD_SIZE, NW_Def.PACKAGE_HEAD_SIZE + buffer.package.head.size - 1);
                this.packageQueue.Enqueue(buffer.package);

                if (buffer.package.head.segmentIndex == buffer.package.head.segmentCount - 1) {
                    NW_ReceiveMessage receiveMessage = new NW_ReceiveMessage();
                    receiveMessage.protoType = buffer.package.head.protoType;
                    receiveMessage.bytes = this.packageQueue.Combine().ToArray();

                    this.packageQueue.Clear();
                    this.messageQueue.Enqueue(receiveMessage);
                }

                // 保存已接收的数据
                int remainLength = buffer.realLength - buffer.package.head.size - NW_Def.PACKAGE_HEAD_SIZE;
                Buffer.BlockCopy(buffer.buffer, buffer.package.head.size, buffer.buffer, 0, remainLength);
                buffer.realLength = remainLength;
                this.socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(this.OnReceivedHead), buffer);
            }
        }
        catch (Exception e) {
            this.DisConnect(EDisconnectReason.ReceiveBodyFail);
            UnityEngine.Debug.Log("OnReceivedBody Failed : " + e.Message);
        }
    }
#endregion

#region // 收发数据
    public void Send(ushort protoType, IMessage message) {
        Debug.LogError("Send-> ThreadId:" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());

        if (connectStatus != EConnectStatus.Connected) {
            return;
        }

        byte[] messageBytes = ProtobufUtils.Serialize(message);
        int packageCount = Mathf.CeilToInt(1f * messageBytes.Length / NW_Def.PACKAGE_BODY_MAX_SIZE);
        Debug.LogError("Send内容长度-> " + messageBytes.Length.ToString() + "  分块：" + packageCount.ToString());
        if (packageCount > byte.MaxValue) {
            Debug.LogError($"{protoType.ToString()} 数据包太大，即使切分之后还是过大");
            return;
        }
        else {
            byte[] packageBytes;
            if (packageCount <= 1) {
                NW_Package package = new NW_Package(protoType, messageBytes, 0, (ushort)messageBytes.Length, 0, 1);
                packageBytes = package.Encode();

                try {
                    this.socket.BeginSend(packageBytes, 0, packageBytes.Length, SocketFlags.None, new AsyncCallback(this.OnSend), null);
                }
                catch (System.Exception e) {
                    this.DisConnect(EDisconnectReason.SendBeginFail);
                    Debug.LogError("Send Failed : " + protoType.ToString() + " " + e.ToString());
                }
            }
            else {
                int i = 0;
                while (i < packageCount) {
                    int beginIndex = i * NW_Def.PACKAGE_BODY_MAX_SIZE;
                    ushort packageLength = NW_Def.PACKAGE_BODY_MAX_SIZE;
                    if (i == packageCount - 1) {
                        packageLength = (ushort)(messageBytes.Length - beginIndex);
                    }

                    NW_Package package = new NW_Package(protoType, messageBytes, beginIndex, packageLength, (byte)(i++), (byte)packageCount);
                    packageBytes = package.Encode();

                    try {
                        this.socket.BeginSend(packageBytes, 0, packageBytes.Length, SocketFlags.None, new AsyncCallback(this.OnSend), null);
                    }
                    catch (System.Exception e) {
                        this.DisConnect(EDisconnectReason.SendBeginFail);
                        Debug.LogError("Send Failed : " + protoType.ToString() + " " + e.ToString());
                    }
                }
            }
        }
    }

    private void OnSend(IAsyncResult ar) {
        try {
            this.socket.EndSend(ar);
        }
        catch (System.Exception e) {
            this.DisConnect(EDisconnectReason.SendEndFail);
            Debug.LogError("EndSend Failed : " + e.ToString());
        }
    }

    public void Update() {
        if (this.messageQueue.Count > 0) {
            if (this.messageQueue.Dequeue(out NW_ReceiveMessage message)) {
                NWDelegateService.Fire(message.protoType, message);
            }
        }
    }
#endregion

}
