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
    public static NW_Transfer transfer { get; private set; } = new NW_Transfer();

    public override void OnInit()
    {
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnAccepted, OnAccepted);
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnConnected, OnAccepted);
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnDisConnected, OnAccepted);
        BS_EventManager<BS_EProtoType>.Add(BS_EProtoType.OnLost, OnAccepted);
    }
    public override void OnUpdate() { transfer?.Update(); }

    public static void Send(LC_EProtoType protoType, IMessage message)
    {
        using (System.IO.MemoryStream strenm = new System.IO.MemoryStream())
        {
            message.WriteTo(strenm);
            Send(protoType, strenm.ToArray());
        }
    }
    public static void Send(LC_EProtoType protoType, byte[] bytes) { Send((ushort)protoType, bytes); }
    public static void Send(ushort protoType, byte[] bytes) { transfer?.Send(protoType, bytes); }
}
