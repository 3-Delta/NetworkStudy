﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;
using System.IO;

// https://github.com/Headbangeerr/ForestWar/blob/master/GameServer/GameServer/Servers/Client.cs
// https://www.jianshu.com/p/fa959d16eaed
public class NW_Mgr : BS_ManagerBase<NW_Mgr>
{
    private Socket socket;
    public Map<ushort, NW_Transfer> clients { get; private set; } = new Map<ushort, NW_Transfer>();
    public Map<NW_Transfer, ushort> transfers { get; private set; } = new Map<NW_Transfer, ushort>();

    public override void OnInit()
    {
        BS_EventManager<BS_EEventType>.Add<NW_Transfer>(BS_EEventType.OnConnectSuccess, OnConnectSuccess);
        BS_EventManager<BS_EEventType>.Add<NW_Transfer>(BS_EEventType.OnConnectFailed, OnConnectFailed);
        BS_EventManager<BS_EEventType>.Add<NW_Transfer>(BS_EEventType.OnConnectLost, OnConnectLost);

        Listen(NW_Def.IPv4, NW_Def.PORT, NW_Def.ListenMax);
    }

    public override void OnExit()
    {
        foreach (var kvp in transfers.dict) { kvp.Key.OnExit(); }
        transfers.Clear();
        clients.Clear();
        socket?.Close();
        socket = null;
    }

    #region // 连接
    public void Listen(string ip, int port, int listenCount) { Listen(IPAddress.Parse(ip), port, listenCount); }
    public void Listen(IPAddress ip, int port, int listenCount) { Listen(new IPEndPoint(ip, port), listenCount); }
    public void Listen(IPEndPoint ipe, int listenCount)
    {
        socket = BS_T_Network.BuildSocket4TCP(ipe.AddressFamily);
        try
        {
            socket.Bind(ipe);
            socket.Listen(listenCount);
            BS_Mgr_Log.LogError("Server Network Launch Success! Start Listenning...");
            socket.BeginAccept(new System.AsyncCallback(OnAccepted), null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Listen Failed : " + e.Message);
        }
    }
    #endregion

    #region // 回调
    private void OnAccepted(IAsyncResult ar)
    {
        try
        {
            // 取得客户端连接
            Socket client = socket.EndAccept(ar);
            Console.WriteLine("Server Network Accepted Client! Start Receiving... " + client.GetHashCode());
            new NW_Transfer(client).BeginReceive();
        }
        catch (Exception e)
        {
            Console.WriteLine("Connect Failed : " + e.Message);
        }

        // 继续监听其他客户端socket
        socket.BeginAccept(new System.AsyncCallback(OnAccepted), null);
    }
    private void OnConnectSuccess(NW_Transfer transfer)
    {

    }
    private void OnConnectFailed(NW_Transfer transfer)
    {

    }
    private void OnConnectLost(NW_Transfer transfer)
    {
        Console.WriteLine("OnConnectLost --> transfer " + transfer.ToString());
        ushort playerID = transfers[transfer];
        clients.Remove(playerID);
        transfers.Remove(transfer);
        transfer.DisConnect();
    }
    #endregion

    #region // 发数据包
    public void Send(ushort playerID, LC_EProtoType protoType, IMessage message)
    {
        if (playerID == ushort.MaxValue)
        {
            foreach (var kvp in clients.dict)
            {
                kvp.Value.Send((ushort)protoType, message);
            }
        }
        else
        {
            Send(playerID, (ushort)protoType, message);
        }
    }
    private void Send(ushort playerID, ushort protoType, IMessage message)
    {
        var client = clients[playerID];
        if (client != null)
        {
            client.Send(protoType, message);
        }
    }
    #endregion
}
