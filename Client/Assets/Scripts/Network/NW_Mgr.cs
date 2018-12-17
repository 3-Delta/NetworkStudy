using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public static class NW_Mgr
{
    private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private static NW_Queue queue = new NW_Queue();

    public static bool IsConnected
    {
        get
        {
            return socket != null && socket.Connected;
        }
    }

    public static bool Connect(string ip, int port)
    {
        if (!IsConnected)
        {
            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
                socket.BeginConnect(ipe, new AsyncCallback(OnConnectCallback), socket);
            }
            catch (Exception e)
            {
                BS_EventHelper.Trigger(BS_EventType.OnConnectFailed);
                Debug.LogError("Connect Failed on Connect: " + e.ToString());
                return false;
            }
        }
        return true;
    }

    private static void OnConnectCallback(IAsyncResult ar)
    {
        Socket argSocket = (Socket)ar.AsyncState;
        try
        {
            argSocket.EndConnect(ar);
            NW_Package package = new NW_Package();
            package.socket = argSocket;
            argSocket.BeginReceive(package.body.bodyBytes, 0, (int)NW_PackageHead.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveHeadCallback), package);
        }
        catch (System.Exception e)
        {
            BS_EventHelper.Trigger(BS_EventType.OnConnectFailed);
            Debug.LogError("Connect Failed on OnConnectCallback: " + e.ToString());
        }
    }

    private static void OnReceiveHeadCallback(IAsyncResult ar)
    {
        NW_Package argPackage = (NW_Package)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = argPackage.socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read < 1)
            {
                BS_EventHelper.Trigger(BS_EventType.OnConnectLost);
                Debug.LogError("Connect Lost on OnReceiveHeadCallback: " + errCode.ToString());
                return;
            }

            argPackage.body.bodySize += read;
            // 包头必须读满
            if (argPackage.body.bodySize < (int)NW_PackageHead.PACKAGE_HEAD_SIZE)
            {
                argPackage.socket.BeginReceive(argPackage.body.bodyBytes, argPackage.body.bodySize, (int)NW_PackageHead.PACKAGE_HEAD_SIZE - argPackage.body.bodySize, SocketFlags.None, new AsyncCallback(OnReceiveHeadCallback), argPackage);
            }
            else
            {
                // 处理包头
                argPackage.head.Decode(argPackage.body.bodyBytes);
                // 清0开始接收body
                argPackage.body.ResetSize();
                argPackage.socket.BeginReceive(argPackage.body.bodyBytes, 0, (int)NW_PackageBody.PACKAGE_BODY_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveBodyCallback), argPackage);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Connect Failed on OnReceiveHeadCallback: " + e.ToString());
        }
    }

    private static void OnReceiveBodyCallback(IAsyncResult ar)
    {
        NW_Package argPackage = (NW_Package)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = argPackage.socket.EndReceive(ar, out errCode);
            // 断开连接
            if (read < 1)
            {
                BS_EventHelper.Trigger(BS_EventType.OnConnectLost);
                Debug.LogError("Connect Lost on OnReceiveBodyCallback: " + errCode.ToString());
                return;
            }

            argPackage.body.bodySize += read;
            // 消息体满足长度
            if (argPackage.body.bodySize < argPackage.head.size)
            {
                argPackage.socket.BeginReceive(argPackage.body.bodyBytes, argPackage.body.bodySize, (int)NW_PackageBody.PACKAGE_BODY_SIZE - argPackage.body.bodySize, SocketFlags.None, new AsyncCallback(OnReceiveBodyCallback), argPackage);
            }
            else
            {
                queue.Enqueue(argPackage);
                argPackage.body.ResetSize();
                argPackage.socket.BeginReceive(argPackage.body.bodyBytes, 0, (int)NW_PackageHead.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceiveHeadCallback), argPackage);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Connect Failed on OnReceiveBodyCallback: " + e.ToString());
        }
    }

    public static void Send()
    {

    }

    public static void Receive()
    {
    }
}
