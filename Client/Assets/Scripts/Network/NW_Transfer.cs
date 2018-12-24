using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

// 单个transfer, 将来可能多个transfer协作
public class NW_Transfer
{
    private Socket socket;
    private System.Action connectedCallback;
    private NW_Buffer buffer = new NW_Buffer();
    public NW_Queue queue { get; private set; } = new NW_Queue();

    public bool IsConnected
    {
        get { return socket != null && socket.Connected; }
    }

    public NW_Transfer()
    {
        connectedCallback = null;
    }

    #region // Connect
    public void Connect(string ip, int port, System.Action callback) { Connect(IPAddress.Parse(ip), port, callback); }
    public void Connect(IPAddress ip, int port, System.Action callback) { Connect(new IPEndPoint(ip, port), callback); }
    public void Connect(IPEndPoint ipe, System.Action callback)
    {
        // IPv4, 将来如何处理IPv4和IPv6同时的情况???
        socket = socket ?? T_Network.BuildSocket4TCP(ipe.AddressFamily);
        if (!IsConnected)
        {
            connectedCallback = callback;
            try
            {
                socket.BeginConnect(ipe, new AsyncCallback(OnConnected), null);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("Connect Failed : " + e.Message);
            }
        }
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
            NW_Package package = new NW_Package();
            socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveHead), package);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("Connect Failed : " + e.Message);
        }

        connectedCallback?.Invoke();
    }
    private void OnReceiveHead(IAsyncResult ar)
    {
        NW_Package package = (NW_Package)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read < 1)
            {
                BS_EventManager<BS_EventType>.Trigger(BS_EventType.OnConnectLost);
                Debug.LogError("Connect Lost : " + errCode.ToString());
                return;
            }

            // 暂时将包头存储到body中，开始接受body的时候正式转移到head中
            buffer.length += read;
            // 包头必须读满
            if (buffer.length < NW_Def.PACKAGE_HEAD_SIZE)
            {
                socket.BeginReceive(buffer.buffer, buffer.length, NW_Def.PACKAGE_HEAD_SIZE - buffer.length, SocketFlags.None, new AsyncCallback(OnReceiveHead), package);
            }
            else
            {
                // 处理包头
                package.head.Decode(buffer.buffer);
                // 清0开始接收body
                buffer.Clear();
                socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_BODY_MAX_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveBody), package);
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
            int read = socket.EndReceive(ar, out errCode);
            // 断开连接
            if (read < 1)
            {
                BS_EventManager<BS_EventType>.Trigger(BS_EventType.OnConnectLost);
                Debug.LogError("Connect Lost : " + errCode.ToString());
                return;
            }

            buffer.length += read;
            // 消息体满足长度
            if (buffer.length < package.head.size)
            {
                socket.BeginReceive(buffer.buffer, buffer.length, package.head.size - buffer.length, SocketFlags.None, new AsyncCallback(OnReceiveBody), package);
            }
            else
            {
                // 入队
                package.body.Decode(buffer.buffer, package.head.size);
                queue.Enqueue(package);

                buffer.Clear();
                socket.BeginReceive(package.body.bodyBytes, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveHead), package);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("OnReceive Failed : " + e.ToString());
        }
    }
    #endregion

    #region // DisConnect
    public void DicConnect()
    {
        socket?.Close();
        connectedCallback = null;
    }
    #endregion

    #region // 收发数据
    public void Send(ushort protoType, byte[] bytes)
    {
        if (IsConnected)
        {
            NW_Package package = new NW_Package(protoType, bytes);
            byte[] packageBytes = package.Encode();
            try
            {
                socket.BeginSend(packageBytes, 0, packageBytes.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Send Failed : " + e.ToString());
            }
        }
    }
    private void OnSend(IAsyncResult ar)
    {
        try { socket.EndSend(ar); }
        catch (System.Exception e)
        {
            Debug.LogError("EndSend Failed : " + e.ToString());
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