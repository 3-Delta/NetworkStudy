﻿using System.Collections;
using System.Collections.Generic;
using System;
using Google.Protobuf;
using System.IO;

public static class ProtobufUtils
{
    public static bool HasValue(int nullableEnum)
    {
        return nullableEnum != default(int);
    }

    public static T DeSerialize<T>(MessageParser parser, byte[] bytes)
    {
        T ret = default(T);
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        {
            IMessage message = parser.ParseFrom(bytes);
            ret = (T)message;
            return ret;
        }
    }

    public static byte[] Serialize(IMessage msg)
    {
        byte[] data;
        using (MemoryStream stream = new MemoryStream())
        {
            msg.WriteTo(stream);
            data = stream.ToArray();
        }

        return data;
    }

    public static void Serialize(IMessage msg, MemoryStream stream)
    {
        msg.WriteTo(stream);
    }

    public static T Deserialize<T>(IMessage msg, MessageParser parser) where T : class, IMessage
    {
        // todo 热更层类型  为什么 可以 as 成 框架层？？
        // 因为IMessage有对应的适配器
        T rlt = msg as T;
        return rlt;
    }

    public static bool TryDeserialize<T>(MessageParser parser, byte[] bytes, out T rlt) where T : class, IMessage
    {
        bool can = true;
        try
        {
            // todo 热更层类型  为什么 可以 as 成 框架层？？
            rlt = parser.ParseFrom(bytes) as T;
        }
        catch (Exception e)
        {
            rlt = default;
            can = false;
        }

        return can;
    }

    public static bool TryDeserialize<T>(MessageParser parser, ByteString bytes, out T rlt) where T : class, IMessage
    {
        bool can = true;
        try
        {
            rlt = parser.ParseFrom(bytes) as T;
        }
        catch (Exception e)
        {
            rlt = default;
            can = false;
        }

        return can;
    }

    public static bool TryDeserialize<T>(MessageParser parser, MemoryStream bytes, out T rlt) where T : class, IMessage
    {
        bool can = true;
        try
        {
            rlt = parser.ParseFrom(bytes) as T;
        }
        catch (Exception e)
        {
            rlt = default;
            can = false;
        }

        return can;
    }
}