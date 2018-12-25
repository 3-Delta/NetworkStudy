using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

// 单个transfer, 将来可能多个transfer协作
public class NW_Transfer
{
    private Socket socket;
    private System.Action listenedCallback;
    private NW_Buffer buffer = new NW_Buffer();

    // 各个客户端连接的数据包都在该队列中
    public NW_Queue queue { get; private set; } = new NW_Queue();
    public int listenCount { get; set; } = 100;

    public bool IsConnected
    {
        get { return socket != null && socket.Connected; }
    }

    public NW_Transfer()
    {
        listenedCallback = null;
        socket = null;
    }

    #region // Listen
    public void Listen(string ip, int port, int listenCount, System.Action callback) { Listen(IPAddress.Parse(ip), port, listenCount, callback); }
    public void Listen(IPAddress ip, int port, int listenCount, System.Action callback) { Listen(new IPEndPoint(ip, port), listenCount, callback); }
    public void Listen(IPEndPoint ipe, int listenCount, System.Action callback)
    {
        socket = socket ?? T_Network.BuildSocket4TCP(ipe.AddressFamily);
        listenedCallback = callback;
        try
        {
            socket.Bind(ipe);
            socket.Listen(listenCount);
            socket.BeginAccept(new System.AsyncCallback(OnListen), null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Listen Failed : " + e.Message);
        }
    }
    #endregion

    #region // OnTransfer
    private void OnListen(IAsyncResult ar)
    {
        try
        {
            Socket client = socket.EndAccept(ar);
            NW_Package package = new NW_Package();
            // 终于理解为什么服务器的数据包中需要一个socket引用了。
            package.socket = client;
            client.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveHead), package);
        }
        catch (Exception e)
        {
            Console.WriteLine("Connect Failed : " + e.Message);
        }

        // 继续监听其他客户端socket
        socket.BeginAccept(new System.AsyncCallback(OnListen), null);
        listenedCallback?.Invoke();
    }
    private void OnReceiveHead(IAsyncResult ar)
    {
        NW_Package package = (NW_Package)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = package.socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read < 1)
            {
                BS_EventManager<EEventType>.Trigger(EEventType.OnConnectLost);
                Debug.LogError("Connect Lost : " + errCode.ToString());
                return;
            }

            // 暂时将包头存储到body中，开始接受body的时候正式转移到head中
            buffer.length += read;
            // 包头必须读满
            if (buffer.length < NW_Def.PACKAGE_HEAD_SIZE)
            {
                package.socket.BeginReceive(buffer.buffer, buffer.length, NW_Def.PACKAGE_HEAD_SIZE - buffer.length, SocketFlags.None, new AsyncCallback(OnReceiveHead), package);
            }
            else
            {
                // 处理包头
                package.head.Decode(buffer.buffer);
                // 清0开始接收body
                buffer.Clear();
                package.socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_BODY_MAX_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveBody), package);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("OnReceive Failed : " + e.ToString());
        }
    }

    private void OnReceiveBody(IAsyncResult ar)
    {
        NW_Package package = (NW_Package)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = package.socket.EndReceive(ar, out errCode);
            // 断开连接
            if (read < 1)
            {
                BS_EventManager<EEventType>.Trigger(EEventType.OnConnectLost);
                Debug.LogError("Connect Lost : " + errCode.ToString());
                return;
            }

            buffer.length += read;
            // 消息体满足长度
            if (buffer.length < package.head.size)
            {
                package.socket.BeginReceive(buffer.buffer, buffer.length, package.head.size - buffer.length, SocketFlags.None, new AsyncCallback(OnReceiveBody), package);
            }
            else
            {
                // 入队
                package.body.Decode(buffer.buffer, package.head.size);
                queue.Enqueue(package);

                buffer.Clear();
                package.socket.BeginReceive(package.body.bodyBytes, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveHead), package);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("OnReceive Failed : " + e.ToString());
        }
    }
    #endregion

    #region // 收发数据
    public void Send(ushort protoType, byte[] bytes)
    {
        if (IsConnected)
        {
            if (bytes.Length <= NW_Def.PACKAGE_BODY_MAX_SIZE)
            {
                NW_Package package = new NW_Package(protoType, bytes);
                byte[] packageBytes = package.Encode();
                try
                {
                    socket.BeginSend(packageBytes, 0, packageBytes.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
                }
                catch (System.Exception e)
                {
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
            Console.WriteLine("EndSend Failed : " + e.ToString());
        }
    }
    public void Update()
    {
        if (queue.Count > 0)
        {
            NW_Package package = new NW_Package();
            if (queue.Dequeue(ref package))
            {
                BS_EventManager<EProtoType>.Trigger<NW_PackageBody>((EProtoType)package.head.protoType, package.body);
            }
        }
    }
    #endregion
}