using System.Collections;
using System.Collections.Generic;
using System;
using Google.Protobuf;

public static class BS_T_Protobuf
{
    public static bool HasValue(int nullableEnum) { return nullableEnum != default(int); }
    public static T DeSerialize<T>(MessageParser parser, byte[] bytes) { return BS_T_Object.DeSerialize<T>(parser, bytes); }
    public static byte[] Serialize(IMessage message) { return BS_T_Object.Serialize(message); }
}