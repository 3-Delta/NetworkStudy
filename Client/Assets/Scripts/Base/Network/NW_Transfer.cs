using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

// 单个transfer, 将来可能多个transfer协作
public class NW_Transfer
{

    private Socket socket = null;
    public bool IsConnected { get { return socket != null && socket.Connected; } }
    public NW_Queue receivedQueue { get; private set; } = new NW_Queue();
    private NW_Buffer buffer = new NW_Buffer();

    public NW_Transfer(Socket socket)
    {
        this.socket = socket;
    }
    public void OnExit() { socket?.Close(); }

    #region // Connect
    public void Connect(string ip, int port, System.Action callback = null) { Connect(IPAddress.Parse(ip), port, callback); }
    public void Connect(IPAddress ip, int port, System.Action callback = null) { Connect(new IPEndPoint(ip, port), callback); }
    public void Connect(IPEndPoint ipe, System.Action callback = null)
    {
        socket = socket ?? BS_T_Network.BuildSocket4TCP(ipe.AddressFamily);
        if (!IsConnected)
        {
            try
            {
                socket.BeginConnect(ipe, new AsyncCallback(OnConnected), null);
            }
            catch (Exception e)
            {
                DisConnect();
                UnityEngine.Debug.Log("Connect Failed : " + e.Message);
            }
        }
    }
    public void DisConnect()
    {
        socket?.Close();
        socket = null;
    }
    #endregion

    #region // OnTransfer
    private void OnConnected(IAsyncResult ar)
    {
        try
        {
            // 建立连接
            socket.EndConnect(ar);
            // 收发数据
            buffer.Clear();
            //socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedHead), buffer);
            socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedPackage), buffer);
        }
        catch (Exception e)
        {
            DisConnect();
            UnityEngine.Debug.Log("OnConnected Failed : " + e.Message);
        }
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
                DisConnect();
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
            DisConnect();
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
                DisConnect();
                UnityEngine.Debug.Log("OnReceivedHead");
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
            DisConnect();
            UnityEngine.Debug.Log("OnReceivedHead Failed : " + e.ToString());
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
                DisConnect();
                UnityEngine.Debug.Log("OnReceivedBody");
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
            DisConnect();
            UnityEngine.Debug.Log("OnReceivedBody Failed : " + e.ToString());
        }
    }
    #endregion

    #region // 收发数据
    public void Send(short protoType, byte[] bytes)
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
                    DisConnect();
                    Debug.LogError("Send Failed : " + protoType.ToString() + " " + e.ToString());
                }
            }
        }
    }
    private void OnSend(IAsyncResult ar)
    {
        try { socket.EndSend(ar); }
        catch (System.Exception e)
        {
            DisConnect();
            Debug.LogError("EndSend Failed : " + e.ToString());
        }
    }
    public void Update()
    {
        if (receivedQueue.Count > 0)
        {
            NW_Package package = new NW_Package();
            if (receivedQueue.Dequeue(ref package))
            {
                BS_EventManager<LC_EProtoType>.Trigger<NW_Package>((LC_EProtoType)package.head.protoType, package);
            }
        }
    }
    #endregion
}