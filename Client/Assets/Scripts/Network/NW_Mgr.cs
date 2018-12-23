using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Google.Protobuf;

// https://www.jianshu.com/p/fa959d16eaed
public static class NW_Mgr
{
    public static NW_Transfer transfer { get; private set; } = new NW_Transfer();

    public static void Send<T>(EProtoType protoType, IMessage message)
    {

    }
    public static void Send(EProtoType protoType, byte[] bytes) { Send((ushort)protoType, bytes); }
    public static void Send(ushort protoType, byte[] bytes)
    {
        // transfer.Send(protoType, bytes);
    }

    public static void Receive()
    {
    }
}
