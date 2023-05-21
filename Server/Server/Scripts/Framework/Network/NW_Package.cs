using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using System.Net.Sockets;

/// <summary>
/// 之前存在一个疑问：就是为什么只是针对于NW_PackageHead的size和protoType进行大小端的转换，而不对于NW_PackageBody的bodyBytes进行大小端的转换呢？
/// 首先需要知道大小端其实是对于大于一个字节的数据类型的内部字节顺序的问题。那么后面的因为都是byte，都是一个字节，所以就不需要进行大小端的转换。
/// </summary>
public struct NW_PackageHead
{
    // 2字节,控制单个包体大小[不包括包头], 最大32768字节
    public ushort size;
    public ushort protoType;
    public ushort randomKey; // 随机加密某些协议
    public ushort playerID; // 玩家id
    public byte segmentIndex; // 超过协议总大小之后切分的块下标
    public byte segmentCount; // 超过协议总大小之后切分的总块数

    public NW_PackageHead(ushort protoType, ushort size, ushort randomKey, ushort playerId, byte segmentIndex = 0, byte segmentCount = 1)
    {
        this.protoType = protoType;
        this.size = size;
        this.randomKey = randomKey;
        this.playerID = playerId;

        this.segmentIndex = segmentIndex;
        this.segmentCount = segmentCount;
    }

    public byte[] Encode()
    {
        // 针对大小端设备统一进行字节顺序转换
        ushort t = (ushort)IPAddress.HostToNetworkOrder((short)size);
        byte[] sizeBytes = BitConverter.GetBytes(t);
        t = (ushort)IPAddress.HostToNetworkOrder((short)protoType);
        byte[] typeBytes = BitConverter.GetBytes(t);
        t = (ushort)IPAddress.HostToNetworkOrder((short)randomKey);
        byte[] keyBytes = BitConverter.GetBytes(t);
        t = (ushort)IPAddress.HostToNetworkOrder((short)this.playerID);
        byte[] playerBytes = BitConverter.GetBytes(t);

        int byteCount = NW_Def.PACKAGE_HEAD_SIZE;
        byte[] headBytes = new byte[byteCount];

        Buffer.BlockCopy(sizeBytes, 0, headBytes, 0, sizeBytes.Length);
        Buffer.BlockCopy(typeBytes, 0, headBytes, sizeof(short), typeBytes.Length);
        Buffer.BlockCopy(keyBytes, 0, headBytes, sizeof(short) + sizeof(short), keyBytes.Length);
        Buffer.BlockCopy(playerBytes, 0, headBytes, sizeof(short) + sizeof(short) + sizeof(short), playerBytes.Length);

        headBytes[sizeof(short) + sizeof(short) + sizeof(short) + sizeof(short)] = segmentIndex;
        headBytes[sizeof(short) + sizeof(short) + sizeof(short) + sizeof(short) + sizeof(byte)] = segmentCount;

        return headBytes;
    }

    public void Decode(byte[] bytes, int startIndex, int endIndex)
    {
        if (bytes != null && 0 <= startIndex && startIndex <= endIndex && endIndex < bytes.Length && bytes.Length >= NW_Def.PACKAGE_HEAD_SIZE)
        {
            size = BitConverter.ToUInt16(bytes, startIndex);
            size = (ushort)IPAddress.NetworkToHostOrder((short)size);

            protoType = BitConverter.ToUInt16(bytes, startIndex + sizeof(short));
            protoType = (ushort)IPAddress.NetworkToHostOrder((short)protoType);

            randomKey = BitConverter.ToUInt16(bytes, startIndex + sizeof(short) + sizeof(short));
            randomKey = (ushort)IPAddress.NetworkToHostOrder((short)randomKey);

            this.playerID = BitConverter.ToUInt16(bytes, startIndex + sizeof(short) + sizeof(short) + sizeof(short));
            this.playerID = (ushort)IPAddress.NetworkToHostOrder((short)playerID);

            segmentIndex = bytes[startIndex + sizeof(short) + sizeof(short) + sizeof(short) + sizeof(short)];
            segmentCount = bytes[startIndex + sizeof(short) + sizeof(short) + sizeof(short) + sizeof(short) + sizeof(byte)];
        }
    }
}

public struct NW_PackageBody
{
    // bodyBytes的长度不定，但是存在一个上限值，如果超过上限值，如何处理？
    public byte[] bodyBytes;

    public NW_PackageBody(byte[] bytes, int startIndex, int endIndex)
    {
        bodyBytes = null;
        Decode(bytes, startIndex, endIndex);
    }

    public byte[] Encode()
    {
        return bodyBytes;
    }

    public void Decode(byte[] bytes, int startIndex, int endIndex)
    {
        if (bytes != null && 0 <= startIndex && startIndex <= endIndex && endIndex < bytes.Length)
        {
            int bodySize = endIndex - startIndex + 1;
            bodyBytes = new byte[bodySize];
            Buffer.BlockCopy(bytes, startIndex, bodyBytes, 0, bodySize);
        }
    }
}

public struct NW_Package
{
    public NW_PackageHead head;
    public NW_PackageBody body;

    public NW_Package(ushort protoType, byte[] bytes, int beginIndex, ushort length, byte segmentIndex = 0, byte segmentCount = 1)
    {
        head = new NW_PackageHead(protoType, length, 0, 1, segmentIndex, segmentCount);
        body = new NW_PackageBody(bytes, beginIndex, beginIndex + length - 1);
    }

    public void Clear() { }

    public byte[] Encode()
    {
        int bodySize = body.bodyBytes.Length <= 0 ? 0 : body.bodyBytes.Length;
        byte[] totalBytes = new byte[NW_Def.PACKAGE_HEAD_SIZE + bodySize];
        byte[] headBytes = head.Encode();
        byte[] bodyBytes = body.Encode();
        Buffer.BlockCopy(headBytes, 0, totalBytes, 0, headBytes.Length);
        if (bodySize > 0)
        {
            Buffer.BlockCopy(bodyBytes, 0, totalBytes, headBytes.Length, bodySize);
        }

        return totalBytes;
    }

    public void Decode(byte[] bytes)
    {
        head.Decode(bytes, 0, NW_Def.PACKAGE_HEAD_SIZE - 1);
        body.Decode(bytes, NW_Def.PACKAGE_HEAD_SIZE, bytes.Length - 1);
    }
}
