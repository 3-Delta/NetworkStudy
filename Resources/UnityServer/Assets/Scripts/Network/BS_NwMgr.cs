using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BS_NwMgr : BS_ManagerBase<BS_NwMgr>
{
    private List<NetworkConnection> connections = new List<NetworkConnection>();

    public override void OnInit()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnDisConnected);
        NetworkServer.RegisterHandler(MsgType.Error, OnError);
    }
    public bool Lunch()
    {
        bool ret = NetworkServer.Listen(Def.port);
        if (ret)
            Debug.LogError("服务器成功启动!");
        else
            Debug.LogError("服务器无法启动,端口为:" + Def.port);
        return ret;
    }
    public override void OnUpdate()
    {
    }
    public override void OnExit()
    {
        connections.ForEach(conn => {
            conn.Disconnect();
            conn.Dispose();
        });
        connections.Clear();
    }

    private void OnConnected(NetworkMessage msg)
    {
        connections.Add(msg.conn);
    }
    private void OnDisConnected(NetworkMessage msg)
    {
        connections.Remove(msg.conn);
    }
    private void OnError(NetworkMessage msg)
    {

    }
}
