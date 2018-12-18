﻿using System.Collections;
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
    // 2字节,控制单个包体大小
    public ushort size;
    public ushort protoType;
    public const ushort PACKAGE_HEAD_SIZE = sizeof(ushort) + sizeof(ushort);

    public byte[] Encode()
    {
        // 针对大小端设备统一进行字节顺序转换
        byte[] sizeBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(size));
        byte[] typeBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(protoType));
        int byteCount = PACKAGE_HEAD_SIZE;
        byte[] headBytes = new byte[byteCount];
        Array.Copy(sizeBytes, 0, headBytes, 0, sizeBytes.Length);
        Array.Copy(typeBytes, 0, headBytes, sizeof(ushort), typeBytes.Length);
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
    public const ushort PACKAGE_BODY_SIZE = ushort.MaxValue;
    public byte[] bodyBytes;
    public int bodySize;

    public void ResetSize()
    {
        bodyBytes = null;
        bodySize = 0;
    }
    public byte[] Encode() { return bodyBytes; }
    public void Decode(byte[] bytes, int startIndex = 0)
    {
        if (bytes != null && bytes.Length > startIndex)
        {
            bodySize = (ushort)(bytes.Length - startIndex);
            bodyBytes = new byte[bodySize];
            Array.Copy(bytes, 0, bodyBytes, 0, bodySize);
        }
        else { ResetSize(); }
    }
}

public struct NW_Package
{
    public NW_PackageHead head;
    public NW_PackageBody body;
    public Socket socket;

    public NW_Package(NW_PackageHead head, NW_PackageBody body, Socket socket = null)
    {
        this.head = head;
        this.body = body;
        this.socket = socket;
    }
    public void Reset()
    {
        socket = null;
    }
    public byte[] Encode()
    {
        int bodySize = body.bodySize < 0 ? 0 : body.bodySize;
        byte[] totalBytes = new byte[NW_PackageHead.PACKAGE_HEAD_SIZE + bodySize];
        byte[] headBytes = head.Encode();
        byte[] bodyBytes = body.Encode();
        Array.Copy(headBytes, 0, totalBytes, 0, headBytes.Length);
        if (bodySize > 0)
        {
            Array.Copy(bodyBytes, 0, totalBytes, headBytes.Length, bodySize);
        }
        return totalBytes;
    }

    public void Decode(byte[] bytes)
    {
        head.Decode(bytes, 0);
        body.Decode(bytes, NW_PackageHead.PACKAGE_HEAD_SIZE);
    }
    public void Decode(MemoryStream stream) { Decode(stream?.ToArray()); }
}