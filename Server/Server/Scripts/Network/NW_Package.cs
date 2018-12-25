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
    // 2字节,控制单个包体大小[不包括包头]
    public ushort size { get; private set; }
    public ushort protoType { get; private set; }

    public NW_PackageHead(ushort protoType, ushort size)
    {
        this.protoType = protoType;
        this.size = size;
    }
    public byte[] Encode()
    {
        // 针对大小端设备统一进行字节顺序转换
        byte[] sizeBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(size));
        byte[] typeBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protoType));
        int byteCount = NW_Def.PACKAGE_HEAD_SIZE;
        byte[] headBytes = new byte[byteCount];
        Buffer.BlockCopy(sizeBytes, 0, headBytes, 0, sizeBytes.Length);
        Buffer.BlockCopy(typeBytes, 0, headBytes, sizeof(ushort), typeBytes.Length);
        return headBytes;
    }
    public void Decode(byte[] bytes, int startIndex = 0)
    {
        if (bytes != null)
        {
            size = BitConverter.ToUInt16(bytes, startIndex);
            size = (ushort)IPAddress.NetworkToHostOrder(size);
            protoType = BitConverter.ToUInt16(bytes, startIndex + sizeof(ushort));
            protoType = (ushort)IPAddress.NetworkToHostOrder(protoType);
        }
    }
}

public struct NW_PackageBody
{
    // bodyBytes的长度不定，但是存在一个上限值，如果超过上限值，如何处理？
    public byte[] bodyBytes { get; private set; }

    // public NW_PackageBody(byte[] bytes) { Decode(bytes, 0); }
    public void Clear() { }
    public byte[] Encode() { return bodyBytes; }
    public void Decode(byte[] bytes, int startIndex = 0)
    {
        if (bytes != null && bytes.Length > startIndex)
        {
            int bodySize = bytes.Length - startIndex;
            bodyBytes = new byte[bodySize];
            Buffer.BlockCopy(bytes, startIndex, bodyBytes, 0, bodySize);
        }
    }
}

public class NW_Package
{
    public NW_PackageHead head { get; private set; }
    public NW_PackageBody body { get; private set; }
    public Socket socket { get; set; }

    public NW_Package(ushort protoType, byte[] bytes)
    {
        head = new NW_PackageHead(protoType, (ushort)bytes.Length);
        body = new NW_PackageBody();
        body.Decode(bytes, 0);
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
        head.Decode(bytes, 0);
        body.Decode(bytes, NW_Def.PACKAGE_HEAD_SIZE);
    }
}