using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BS_NwMgr : BS_ManagerBase<BS_NwMgr>
{
    public NetworkClient client { get; private set; }
    public NetworkConnection connection { get; private set; }

    public override void OnInit()
    {
        client = new NetworkClient();
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MsgType.Disconnect, OnDisConnected);
        client.RegisterHandler(MsgType.Error, OnError);
    }
    public void Lunch()
    {
        if (connection == null || !connection.isConnected)
        {
            client.Connect(Def.ip, Def.port);
        }
    }
    public override void OnUpdate()
    {

    }
    public override void OnExit()
    {
        connection?.Disconnect();
        connection?.Dispose();
    }

    private void OnConnected(NetworkMessage msg)
    {
        Debug.LogError("OnConnected");
        connection = msg.conn;
    }
    private void OnDisConnected(NetworkMessage msg)
    {
        connection = null;
    }
    private void OnError(NetworkMessage msg)
    {

    }
}
