using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using Google.Protobuf;

public struct NW_SendMessage {
    public ushort protoType;
    public IMessage message;
}

public struct NW_ReceiveMessage {
    public ushort playerId;
    public ushort protoType;
    public byte[] bytes;

    public IMessage message;
}
