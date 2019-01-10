using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

// 单个transfer,在服务器模式中，代表一个与服务器连接的客户端
public class NW_Transfer
{
    public Socket socket = null;
    public bool IsConnected { get { return socket != null && socket.Connected; } }
    public NW_Queue receivedQueue { get; private set; } = new NW_Queue();
    public NW_Queue sendQueue { get; private set; } = new NW_Queue();
    
    private System.Threading.Thread receivedThread = null;

    private void ReceivedThreadUpdate()
    {
        while (true)
        {
            System.Threading.Thread.Sleep(30);

            NW_Package package = new NW_Package();
            if (receivedQueue.Dequeue(ref package))
            {
                BS_EventManager<LC_EProtoType>.Trigger<NW_Package>((LC_EProtoType)package.head.protoType, package);
            }
        }
    }
    public NW_Transfer(Socket socket)
    {
        this.socket = socket;
        receivedThread = new System.Threading.Thread(new System.Threading.ThreadStart(ReceivedThreadUpdate));
        receivedThread.Start();
    }
    public void OnExit()
    {
        receivedThread?.Abort();
        DisConnect();
    }
    public void DisConnect()
    {
        socket?.Close();
        socket = null;
    }
    public void BeginReceive()
    {
        NW_Buffer buffer = new NW_Buffer();
        // Receive系列函数，接收的size <= 我们预期的
        // socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedHead), buffer);
        socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedPackage), buffer);
    }
    private void OnReceivedPackage(IAsyncResult ar)
    {
        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read < 0)
            {
                BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
                Console.WriteLine("OnReceivedPackage");
                return;
            }
            buffer.realLength += read;
            if (buffer.realLength < NW_Def.PACKAGE_HEAD_SIZE)
            {
                socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(OnReceivedPackage), buffer);
            }
            else
            {
                buffer.package.head.Decode(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE - 1);
                if (buffer.realLength < buffer.package.head.size + NW_Def.PACKAGE_HEAD_SIZE)
                {
                    socket.BeginReceive(buffer.buffer, buffer.realLength, buffer.package.head.size - (buffer.realLength - NW_Def.PACKAGE_HEAD_SIZE), SocketFlags.None, new AsyncCallback(OnReceivedBody), buffer);
                }
                else
                {
                    buffer.package.body.Decode(buffer.buffer, NW_Def.PACKAGE_HEAD_SIZE, NW_Def.PACKAGE_HEAD_SIZE + buffer.package.head.size - 1);
                    receivedQueue.Enqueue(buffer.package);

                    // 保存已接收的数据
                    int remainLength = buffer.realLength - buffer.package.head.size - NW_Def.PACKAGE_HEAD_SIZE;
                    Buffer.BlockCopy(buffer.buffer, buffer.package.head.size, buffer.buffer, 0, remainLength);
                    buffer.realLength = remainLength;
                    socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(OnReceivedPackage), buffer);
                }
            }
        }
        catch (Exception e)
        {
            BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
            Console.WriteLine("OnReceivedPackage Failed : " + e.ToString());
        }
    }
    private void OnReceivedHead(IAsyncResult ar)
    {
        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read < 0)
            {
                BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
                Console.WriteLine("OnReceivedHead");
                return;
            }

            // 暂时将包头存储到buffer中，开始接受body的时候正式转移到head中
            buffer.realLength += read;
            // 包头必须读满
            if (buffer.realLength < NW_Def.PACKAGE_HEAD_SIZE)
            {
                socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(OnReceivedHead), buffer);
            }
            else
            {
                // 处理包头
                buffer.package.head.Decode(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE - 1);
                socket.BeginReceive(buffer.buffer, buffer.realLength, buffer.package.head.size - (buffer.realLength - NW_Def.PACKAGE_HEAD_SIZE), SocketFlags.None, new AsyncCallback(OnReceivedBody), buffer);
            }
        }
        catch (Exception e)
        {
            BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
            Console.WriteLine("OnReceivedHead Failed : " + e.ToString());
        }
    }
    private void OnReceivedBody(IAsyncResult ar)
    {
        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = socket.EndReceive(ar, out errCode);
            // 断开连接
            if (read < 0)
            {
                BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
                Console.WriteLine("OnReceivedBody");
                return;
            }

            buffer.realLength += read;
            // 消息体不满足长度
            if (buffer.realLength < buffer.package.head.size + NW_Def.PACKAGE_HEAD_SIZE)
            {
                socket.BeginReceive(buffer.buffer, buffer.realLength, buffer.package.head.size - (buffer.realLength - NW_Def.PACKAGE_HEAD_SIZE), SocketFlags.None, new AsyncCallback(OnReceivedBody), buffer);
            }
            else
            {
                // 入队
                buffer.package.body.Decode(buffer.buffer, NW_Def.PACKAGE_HEAD_SIZE, NW_Def.PACKAGE_HEAD_SIZE + buffer.package.head.size - 1);
                TryProcessPackage(buffer);
                receivedQueue.Enqueue(buffer.package);

                // 保存已接收的数据
                int remainLength = buffer.realLength - buffer.package.head.size - NW_Def.PACKAGE_HEAD_SIZE;
                Buffer.BlockCopy(buffer.buffer, buffer.package.head.size, buffer.buffer, 0, remainLength);
                buffer.realLength = remainLength;
                socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(OnReceivedHead), buffer);
            }
        }
        catch (Exception e)
        {
            BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
            Console.WriteLine("OnReceivedBody Failed : " + e.ToString());
        }
    }
    private void TryProcessPackage(NW_Buffer buffer)
    {
        if (buffer != null)
        {
            if ((LC_EProtoType)buffer.package.head.protoType == LC_EProtoType.csLogin)
            {
                NW_Mgr.Instance.clients.Add(buffer.package.head.playerID, this);
                NW_Mgr.Instance.transfers.Add(this, buffer.package.head.playerID);
            }
            else if ((LC_EProtoType)buffer.package.head.protoType == LC_EProtoType.csLogout)
            {
                NW_Mgr.Instance.clients.Remove(buffer.package.head.playerID);
                NW_Mgr.Instance.transfers.Remove(this);
            }
        }
    }

   #region // 收发数据
    public void Send(short protoType, byte[] bytes)
    {
        if (!IsConnected) { return; }
        {
            if (bytes.Length <= NW_Def.PACKAGE_BODY_MAX_SIZE)
            {
                NW_Package package = new NW_Package(protoType, bytes);
                byte[] packageBytes = package.Encode();
                try
                {
                    Console.WriteLine("Server Network Send Message To Client... " + (LC_EProtoType)protoType + " " + socket.GetHashCode());
                    socket.BeginSend(packageBytes, 0, packageBytes.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                }
                catch (System.Exception e)
                {
                    BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
                    Console.WriteLine("Send Failed : " + e.ToString());
                }
            }
        }
    }
    private void OnSend(IAsyncResult ar)
    {
        try { socket.EndSend(ar); }
        catch (System.Exception e)
        {
            BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
            Console.WriteLine("EndSend Failed : " + e.ToString());
        }
    }
    #endregion
}