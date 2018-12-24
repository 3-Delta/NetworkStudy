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
    public static NW_Transfer transfer { get; private set; } = new NW_Transfer();

    public override void OnUpdate() { transfer?.Update(); }
    public static void Ping()
    {
        // 判断内外网
    }
    public static void Send(EProtoType protoType, IMessage message)
    {
        using (System.IO.MemoryStream strenm = new System.IO.MemoryStream())
        {
            message.WriteTo(strenm);
            Send(protoType, strenm.ToArray());
        }
    }
    public static void Send(EProtoType protoType, byte[] bytes) { Send((ushort)protoType, bytes); }
    public static void Send(ushort protoType, byte[] bytes) { transfer?.Send(protoType, bytes); }
}
