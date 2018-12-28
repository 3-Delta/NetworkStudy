using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;

// https://www.jianshu.com/p/fa959d16eaed
public class NW_Mgr : BS_ManagerBase<NW_Mgr>
{
    private System.Threading.Thread thread = null;

    // playerID:NW_Transfer
    // private Dictionary<ulong, NW_Transfer> clients = new Dictionary<ulong, NW_Transfer>();

    public NW_Queue queue { get; private set; } = new NW_Queue();

    public System.Action onListenedCallback;
    public System.Action onAcceptedCallback;
    public System.Action onConnectedCallback;
    public System.Action onDisConnectedCallback;
    public System.Action onLostCallback;

    private Socket socket;
    private NW_Buffer buffer = new NW_Buffer();
    public int listenCount { get; set; } = 100;
    public bool IsConnected
    {
        get { return socket != null && socket.Connected; }
    }

    public override void OnInit()
    {
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnAccepted, OnAccepted);
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnConnected, OnConnected);
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnDisConnected, OnDisConnected);
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnLost, OnLost);

        Listen(NW_Def.IPv4, NW_Def.PORT, listenCount);

        // 线程处理
        thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadUpdate));
        thread.Start();
    }

    private void ThreadUpdate()
    {
        while (true)
        {
            System.Threading.Thread.Sleep(30);

            NW_Package package = new NW_Package();
            if (queue.Dequeue(ref package))
            {
                BS_EventManager<LC_EProtoType>.Trigger<NW_Package>((LC_EProtoType)package.head.protoType, package);
            }        
        }
    }

    #region // Listen
    public void Listen(string ip, int port, int listenCount, System.Action callback = null) { Listen(IPAddress.Parse(ip), port, listenCount, callback); }
    public void Listen(IPAddress ip, int port, int listenCount, System.Action callback = null) { Listen(new IPEndPoint(ip, port), listenCount, callback); }
    public void Listen(IPEndPoint ipe, int listenCount, System.Action callback = null)
    {
        socket = socket ?? T_Network.BuildSocket4TCP(ipe.AddressFamily);
        onListenedCallback = callback;
        try
        {
            socket.Bind(ipe);
            socket.Listen(listenCount);
            // socket.BeginAccept(new System.AsyncCallback(OnListen), null);
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
            NW_Buffer buffer = new NW_Buffer(client);
            client.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedPackage), buffer);
            client.BeginSend
        }
        catch (Exception e)
        {
            Console.WriteLine("Connect Failed : " + e.Message);
        }

        // 继续监听其他客户端socket
        socket.BeginAccept(new System.AsyncCallback(OnListen), null);
        onListenedCallback?.Invoke();
    }
    private void OnReceivedPackage(IAsyncResult ar)
    {
        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = buffer.socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read < 1)
            {
                BS_EventManager<LC_EEventType>.Trigger(LC_EEventType.OnConnectLost);
                Console.WriteLine("Connect Lost : " + errCode.ToString());
                return;
            }

            // 暂时将包头存储到body中，开始接受body的时候正式转移到head中
            buffer.length += read;
            // 包头必须读满
            if (buffer.length < NW_Def.PACKAGE_HEAD_SIZE)
            {
                buffer.socket.BeginReceive(buffer.buffer, buffer.length, NW_Def.PACKAGE_HEAD_SIZE - buffer.length, SocketFlags.None, new AsyncCallback(OnReceivedPackage), buffer);
            }
            else if (buffer.length >= NW_Def.PACKAGE_HEAD_SIZE)
            {
                // 处理包头
                buffer.package.head.Decode(buffer.buffer);
                // 清0开始接收body
                buffer.Clear()
            }
            else
            {
                // 处理包头
                package.head.Decode(buffer.buffer);
                // 清0开始接收body
                buffer.Clear();
                package.socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_BODY_MAX_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedPackage), package);
            }

            buffer.socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedPackage), buffer.socket);
        }
        catch (System.Exception e)
        {
            Console.WriteLine("OnReceive Failed : " + e.ToString());
        }
    }
    #endregion

    #region // 回调
    private void OnAccepted() { }
    private void OnConnected() { }
    private void OnDisConnected() { }
    private void OnLost() { }
    #endregion

    #region // 收发数据包
    public void Send(LC_EProtoType protoType, IMessage message)
    {
        using (System.IO.MemoryStream strenm = new System.IO.MemoryStream())
        {
            message.WriteTo(strenm);
            Send(protoType, strenm.ToArray());
        }
    }
    public void Send(LC_EProtoType protoType, byte[] bytes) { Send((ushort)protoType, bytes); }
    public void Send(ushort protoType, byte[] bytes) { }
    #endregion
}
