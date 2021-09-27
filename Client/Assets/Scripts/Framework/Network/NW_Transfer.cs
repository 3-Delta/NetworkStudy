using System;
using System.Net;
using System.Net.Sockets;

using UnityEngine;

// 单个transfer, 将来可能多个transfer协作
public class NW_Transfer {

    private Socket socket = null;
    public bool IsConnected { get { return this.socket != null && this.socket.Connected; } }
    public NW_Queue receivedQueue { get; private set; } = new NW_Queue();
    private NW_Buffer buffer = new NW_Buffer();

    public NW_Transfer(Socket socket) {
        this.socket = socket;
    }
    public void OnExit() {
        //this.DisConnect();
    }


    #region // Connect
    public void Connect(string ip, int port, System.Action callback = null) { this.Connect(IPAddress.Parse(ip), port, callback); }
    public void Connect(IPAddress ip, int port, System.Action callback = null) { this.Connect(new IPEndPoint(ip, port), callback); }
    public void Connect(IPEndPoint ipe, System.Action callback = null) {
        this.socket = this.socket ?? BS_T_Network.BuildSocket4TCP(ipe.AddressFamily);
        if (!this.IsConnected) {
            try {
                this.socket.BeginConnect(ipe, new AsyncCallback(this.OnConnected), null);
            }
            catch (Exception e) {
                this.DisConnect();
                UnityEngine.Debug.Log("Connect Failed : " + e.Message);
            }
        }
    }
    public void DisConnect() {
        this.socket?.Close();
        this.socket = null;
    }
    #endregion

    #region // OnTransfer
    private void OnConnected(IAsyncResult ar) {
        try {
            // 建立连接
            this.socket.EndConnect(ar);
            // 收发数据
            this.buffer.Clear();
            //socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedHead), buffer);
            this.socket.BeginReceive(this.buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(this.OnReceivedPackage), this.buffer);
        }
        catch (Exception e) {
            this.DisConnect();
            UnityEngine.Debug.Log("OnConnected Failed : " + e.Message);
        }
    }
    private void OnReceivedPackage(IAsyncResult ar) {
        Debug.LogError("OnReceivedPackage: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try {
            SocketError errCode = SocketError.Success;
            int read = this.socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read <= 0) {
                this.DisConnect();
                return;
            }
            buffer.realLength += read;
            if (buffer.realLength < NW_Def.PACKAGE_HEAD_SIZE) {
                this.socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(this.OnReceivedPackage), buffer);
            }
            else {
                buffer.package.head.Decode(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE - 1);
                if (buffer.realLength < buffer.package.head.size + NW_Def.PACKAGE_HEAD_SIZE) {
                    this.socket.BeginReceive(buffer.buffer, buffer.realLength, buffer.package.head.size - (buffer.realLength - NW_Def.PACKAGE_HEAD_SIZE), SocketFlags.None, new AsyncCallback(this.OnReceivedBody), buffer);
                }
                else {
                    buffer.package.body.Decode(buffer.buffer, NW_Def.PACKAGE_HEAD_SIZE, NW_Def.PACKAGE_HEAD_SIZE + buffer.package.head.size - 1);
                    this.receivedQueue.Enqueue(buffer.package);

                    // 保存已接收的数据
                    int remainLength = buffer.realLength - buffer.package.head.size - NW_Def.PACKAGE_HEAD_SIZE;
                    Buffer.BlockCopy(buffer.buffer, buffer.package.head.size, buffer.buffer, 0, remainLength);
                    buffer.realLength = remainLength;
                    this.socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(this.OnReceivedPackage), buffer);
                }
            }
        }
        catch (Exception e) {
            this.DisConnect();
            Console.WriteLine("OnReceivedPackage Failed : " + e.ToString());
        }
    }
    private void OnReceivedHead(IAsyncResult ar) {
        Debug.LogError("OnReceivedHead: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try {
            SocketError errCode = SocketError.Success;
            int read = this.socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read <= 0) {
                this.DisConnect();
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
            this.DisConnect();
            UnityEngine.Debug.Log("OnReceivedHead Failed : " + e.ToString());
        }
    }

    private void OnReceivedBody(IAsyncResult ar) {
        Debug.LogError("OnReceivedBody: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try {
            SocketError errCode = SocketError.Success;
            int read = this.socket.EndReceive(ar, out errCode);
            // 断开连接
            if (read <= 0) {
                this.DisConnect();
                return;
            }

            buffer.realLength += read;
            // 消息体不满足长度
            if (buffer.realLength < buffer.package.head.size + NW_Def.PACKAGE_HEAD_SIZE) {
                this.socket.BeginReceive(buffer.buffer, buffer.realLength, buffer.package.head.size - (buffer.realLength - NW_Def.PACKAGE_HEAD_SIZE), SocketFlags.None, new AsyncCallback(this.OnReceivedBody), buffer);
            }
            else {
                // 入队
                buffer.package.body.Decode(buffer.buffer, NW_Def.PACKAGE_HEAD_SIZE, NW_Def.PACKAGE_HEAD_SIZE + buffer.package.head.size - 1);
                this.receivedQueue.Enqueue(buffer.package);

                // 保存已接收的数据
                int remainLength = buffer.realLength - buffer.package.head.size - NW_Def.PACKAGE_HEAD_SIZE;
                Buffer.BlockCopy(buffer.buffer, buffer.package.head.size, buffer.buffer, 0, remainLength);
                buffer.realLength = remainLength;
                this.socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(this.OnReceivedHead), buffer);
            }
        }
        catch (Exception e) {
            this.DisConnect();
            UnityEngine.Debug.Log("OnReceivedBody Failed : " + e.ToString());
        }
    }
    #endregion

    #region // 收发数据
    public void Send(short protoType, byte[] bytes) {
        Debug.LogError("Send: " + System.Threading.Thread.CurrentThread.ManagedThreadId);

        if (this.IsConnected) {
            if (bytes.Length <= NW_Def.PACKAGE_BODY_MAX_SIZE) {
                NW_Package package = new NW_Package(protoType, bytes);
                byte[] packageBytes = package.Encode();
                try {
                    this.socket.BeginSend(packageBytes, 0, packageBytes.Length, SocketFlags.None, new AsyncCallback(this.OnSend), null);
                }
                catch (System.Exception e) {
                    this.DisConnect();
                    Debug.LogError("Send Failed : " + protoType.ToString() + " " + e.ToString());
                }
            }
        }
    }
    private void OnSend(IAsyncResult ar) {
        try { this.socket.EndSend(ar); }
        catch (System.Exception e) {
            this.DisConnect();
            Debug.LogError("EndSend Failed : " + e.ToString());
        }
    }
    public void Update() {
        if (this.receivedQueue.Count > 0) {
            NW_Package package = new NW_Package();
            if (this.receivedQueue.Dequeue(ref package)) {
                BS_EventManager<LC_EProtoType>.Trigger<NW_Package>((LC_EProtoType)package.head.protoType, package);
            }
        }
    }
    #endregion
}