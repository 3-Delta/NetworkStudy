using System.Collections;
using System.Collections.Generic;
using System;
using Google.Protobuf;

public static class T_Protobuf
{
    public static bool HasValue(int nullableEnum) { return nullableEnum != default(int); }
    public static T DeSerialize<T>(MessageParser parser, NW_PackageBody body)
    {
        T ret = default(T);
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        {
            IMessage message = parser.ParseFrom(body.bodyBytes);
            return ret;
        } 
    }
}