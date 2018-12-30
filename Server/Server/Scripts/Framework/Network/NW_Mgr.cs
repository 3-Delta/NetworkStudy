using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;

// https://github.com/Headbangeerr/ForestWar/blob/master/GameServer/GameServer/Servers/Client.cs
// https://www.jianshu.com/p/fa959d16eaed
public class NW_Mgr : BS_ManagerBase<NW_Mgr>
{
    private Socket socket;

    public override void OnInit()
    {
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnAccepted, OnAccepted);
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnConnected, OnConnected);
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnDisConnected, OnDisConnected);
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnLost, OnLost);

        Listen(NW_Def.IPv4, NW_Def.PORT, NW_Def.ListenMax);
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
            Socket client = socket.EndAccept(ar);
            NW_Transfer transfer = new NW_Transfer(client);
            transfer.BeginReceive();
        }
        catch (Exception e)
        {
            Console.WriteLine("Connect Failed : " + e.Message);
        }

        // 继续监听其他客户端socket
        socket.BeginAccept(new System.AsyncCallback(OnAccepted), null);
    }
    private void OnAccepted() { }
    private void OnConnected() { }
    private void OnDisConnected() { }
    private void OnLost() { }
    #endregion

    #region // 收发数据包
    public void Send(ushort playerID, LC_EProtoType protoType, IMessage message)
    {
        using (System.IO.MemoryStream strenm = new System.IO.MemoryStream())
        {
            message.WriteTo(strenm);
            Send(playerID, protoType, strenm.ToArray());
        }
    }
    public void Send(ushort playerID, LC_EProtoType protoType, byte[] bytes) { Send(playerID, (ushort)protoType, bytes); }
    public void Send(ushort playerID, ushort protoType, byte[] bytes)
    {

    }
    #endregion

    #region // transfers
    public Dictionary<ushort, NW_Transfer> transfers { get; private set; } = new Dictionary<ushort, NW_Transfer>();

    public bool Has(ushort playerID) { return transfers.ContainsKey(playerID); }
    public bool TryGet(ushort playerID, out NW_Transfer transfer)
    {
        transfer = null;
        if (Has(playerID)) { transfer = transfers[playerID]; }
        return transfer != null;
    }
    public void Add(ushort playerID, NW_Transfer transfer) { if (!Has(playerID)) { transfers.Add(playerID, transfer); } }
    public void Remove(ushort playerID) { if (Has(playerID)) { transfers.Remove(playerID); } }
    public void Clear() { transfers.Clear(); }
    #endregion
}
