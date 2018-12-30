using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

// 单个transfer,在服务器模式中，代表一个与服务器连接的客户端
public class NW_Transfer
{
    private Socket socket = null;
    public bool IsConnected { get { return socket != null && socket.Connected; } }
    private NW_Buffer buffer = new NW_Buffer();
    public NW_Queue receivedQueue { get; private set; } = new NW_Queue();
    public NW_Queue sendQueue { get; private set; } = new NW_Queue();
    
    private System.Threading.Thread receivedThread = null;
    private System.Threading.Thread sendThread = null;

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
    private void SendThreadUpdate()
    {
        while (true)
        {
            System.Threading.Thread.Sleep(30);

            NW_Package package = new NW_Package();
            if (sendQueue.Dequeue(ref package))
            {

            }
        }
    }
    public NW_Transfer(Socket socket)
    {
        this.socket = socket;
        receivedThread = new System.Threading.Thread(new System.Threading.ThreadStart(ReceivedThreadUpdate));
        receivedThread.Start();
        sendThread = new System.Threading.Thread(new System.Threading.ThreadStart(SendThreadUpdate));
        sendThread.Start();
    }
    public void BeginReceive()
    {
        socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedPackage), null);
    }
    private void OnReceivedPackage(IAsyncResult ar)
    {
        try
        {
            // 继续receive
            socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedPackage), null);
        }
        catch (System.Exception e)
        {
            Console.WriteLine("OnReceivedPackage Failed : " + e.ToString());
        }
    }

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
    #endregion
}