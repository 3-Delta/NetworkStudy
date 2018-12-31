using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Google.Protobuf;

// https://www.jianshu.com/p/fa959d16eaed
public class NW_Mgr : BS_ManagerBase<NW_Mgr>
{
    private NW_Transfer transfer = new NW_Transfer(null);

    public override void OnInit() { }
    public override void OnUpdate() { transfer?.Update(); }

    public void Connect(string ip, int port, System.Action callback = null) { transfer?.Connect(ip, port, callback); }
    public void Send(LC_EProtoType protoType, IMessage message)
    {
        using (System.IO.MemoryStream strenm = new System.IO.MemoryStream())
        {
            message.WriteTo(strenm);
            Send(protoType, strenm.ToArray());
        }
    }
    private void Send(LC_EProtoType protoType, byte[] bytes) { Send((short)protoType, bytes); }
    private void Send(short protoType, byte[] bytes) { transfer?.Send(protoType, bytes); }
}
