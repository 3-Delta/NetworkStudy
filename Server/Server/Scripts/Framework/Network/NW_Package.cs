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
    public short size;
    public short protoType;
    public short playerID; // serverID * 10000 + playerID

    public NW_PackageHead(short protoType, short size, short playerID)
    {
        this.protoType = protoType;
        this.size = size;
        this.playerID = playerID;
    }
    public byte[] Encode()
    {
        // 针对大小端设备统一进行字节顺序转换
        short t = IPAddress.HostToNetworkOrder(size);
        byte[] sizeBytes = BitConverter.GetBytes(t);
        t = IPAddress.HostToNetworkOrder(protoType);
        byte[] typeBytes = BitConverter.GetBytes(t);
        t = IPAddress.HostToNetworkOrder(playerID);
        byte[] playerBytes = BitConverter.GetBytes(t);

        int byteCount = NW_Def.PACKAGE_HEAD_SIZE;
        byte[] headBytes = new byte[byteCount];

        Buffer.BlockCopy(sizeBytes, 0, headBytes, 0, sizeBytes.Length);
        Buffer.BlockCopy(typeBytes, 0, headBytes, sizeof(short), typeBytes.Length);
        Buffer.BlockCopy(playerBytes, 0, headBytes, sizeof(short) + sizeof(short), playerBytes.Length);

        return headBytes;
    }
    public void Decode(byte[] bytes, int startIndex, int endIndex)
    {
        if (bytes != null && 0 <= startIndex && startIndex <= endIndex && endIndex < bytes.Length)
        {
            size = BitConverter.ToInt16(bytes, startIndex);
            size = IPAddress.NetworkToHostOrder(size);
            protoType = BitConverter.ToInt16(bytes, startIndex + sizeof(short));
            protoType = IPAddress.NetworkToHostOrder(protoType);
            playerID = BitConverter.ToInt16(bytes, startIndex + sizeof(short) + sizeof(short));
            playerID = IPAddress.NetworkToHostOrder(playerID);
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
    public byte[] Encode() { return bodyBytes; }
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

    public NW_Package(short protoType, byte[] bytes)
    {
        head = new NW_PackageHead(protoType, (short)bytes.Length, 1);
        body = new NW_PackageBody(bytes, 0, bytes.Length - 1);
        body.Decode(bytes, 0, bytes.Length - 1);
    }
    public void Clear() { }
    public byte[] Encode()
    {
        int bodySize = body.bodyBytes.Length < 0 ? 0 : body.bodyBytes.Length;
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