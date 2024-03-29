﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;

// 单个transfer,在服务器模式中，代表一个与服务器连接的客户端
public class NW_Transfer
{
    public Socket socket = null;
    public bool IsConnected
    {
        get
        {
            return socket != null && socket.Connected;
        }
    }

    public NW_PackageQueue packageQueue { get; private set; } = new NW_PackageQueue();
    public NW_MessageQueue messageQueue { get; private set; } = new NW_MessageQueue();

    public NW_MessageQueue sendQueue { get; private set; } = new NW_MessageQueue();

    private System.Threading.Thread receivedThread = null;


    private void ReceivedThreadUpdate()
    {
        while (true)
        {
            System.Threading.Thread.Sleep(30);

            while (this.messageQueue.Count > 0)
            {
                if (this.messageQueue.Dequeue(out NW_ReceiveMessage message))
                {
                    NWDelegateService.Fire(message.protoType, message);
                }
            }
        }
    }
    public NW_Transfer(Socket socket)
    {
        this.socket = socket;
        receivedThread = new System.Threading.Thread(new System.Threading.ThreadStart(ReceivedThreadUpdate));
        receivedThread.Start();
    }
    public void OnExit()
    {
        receivedThread?.Abort();
        DisConnect();
    }
    public void DisConnect()
    {
        if (!IsConnected)
        {
            return;
        }

        try
        {
            socket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DisConnect {ex.Message}");
        }

        socket = null;
    }
    public void BeginReceive()
    {
        NW_Buffer buffer = new NW_Buffer();
        // Receive系列函数，接收的size <= 我们预期的
        socket.BeginReceive(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE, SocketFlags.None, new AsyncCallback(OnReceivedHead), buffer);
    }

    private void OnReceivedHead(IAsyncResult ar)
    {
        if (!IsConnected)
        {
            return;
        }

        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = socket.EndReceive(ar, out errCode);
            // 丢失连接
            if (read <= 0)
            {
                BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
                return;
            }

            // 暂时将包头存储到buffer中，开始接受body的时候正式转移到head中
            buffer.realLength += read;
            // 包头必须读满
            if (buffer.realLength < NW_Def.PACKAGE_HEAD_SIZE)
            {
                socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(OnReceivedHead), buffer);
            }
            else
            {
                // 处理包头
                buffer.package.head.Decode(buffer.buffer, 0, NW_Def.PACKAGE_HEAD_SIZE - 1);
                socket.BeginReceive(buffer.buffer, buffer.realLength, buffer.package.head.size - (buffer.realLength - NW_Def.PACKAGE_HEAD_SIZE), SocketFlags.None, new AsyncCallback(OnReceivedBody), buffer);
            }
        }
        catch (Exception e)
        {
            BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
            Console.WriteLine("OnReceivedHead Failed : " + e.ToString());
        }
    }
    private void OnReceivedBody(IAsyncResult ar)
    {
        if (!IsConnected)
        {
            return;
        }

        NW_Buffer buffer = (NW_Buffer)ar.AsyncState;
        try
        {
            SocketError errCode = SocketError.Success;
            int read = socket.EndReceive(ar, out errCode);
            // 断开连接
            if (read <= 0)
            {
                BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
                return;
            }

            buffer.realLength += read;
            // 消息体不满足长度
            if (buffer.realLength < buffer.package.head.size + NW_Def.PACKAGE_HEAD_SIZE)
            {
                socket.BeginReceive(buffer.buffer, buffer.realLength, buffer.package.head.size - (buffer.realLength - NW_Def.PACKAGE_HEAD_SIZE), SocketFlags.None, new AsyncCallback(OnReceivedBody), buffer);
            }
            else
            {
                // 接收到一个包，判断该包有没有后续序列包
                buffer.package.body.Decode(buffer.buffer, NW_Def.PACKAGE_HEAD_SIZE, NW_Def.PACKAGE_HEAD_SIZE + buffer.package.head.size - 1);
                this.packageQueue.Enqueue(buffer.package);

                if (buffer.package.head.segmentIndex == buffer.package.head.segmentCount - 1)
                {
                    NW_ReceiveMessage receiveMessage = new NW_ReceiveMessage();
                    receiveMessage.playerId = buffer.package.head.playerID;
                    receiveMessage.protoType = buffer.package.head.protoType;
                    receiveMessage.bytes = this.packageQueue.Combine().ToArray();

                    TryProcessClient(buffer.package.head.protoType, buffer.package.head.playerID);
                    this.packageQueue.Clear();
                    this.messageQueue.Enqueue(receiveMessage);
                }

                // 保存已接收的数据
                int remainLength = buffer.realLength - buffer.package.head.size - NW_Def.PACKAGE_HEAD_SIZE;
                Buffer.BlockCopy(buffer.buffer, buffer.package.head.size, buffer.buffer, 0, remainLength);
                buffer.realLength = remainLength;
                this.socket.BeginReceive(buffer.buffer, buffer.realLength, NW_Def.PACKAGE_HEAD_SIZE - buffer.realLength, SocketFlags.None, new AsyncCallback(this.OnReceivedHead), buffer);
            }
        }
        catch (Exception e)
        {
            BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
            Console.WriteLine("OnReceivedBody Failed : " + e.ToString());
        }
    }
    private void TryProcessClient(ushort protoType, ushort playerId)
    {
        if ((LC_EProtoType)protoType == LC_EProtoType.csLogin)
        {
            NW_Mgr.Instance.clients.Add(playerId, this);
            NW_Mgr.Instance.transfers.Add(this, playerId);
        }
        else if ((LC_EProtoType)protoType == LC_EProtoType.csLogout)
        {
            NW_Mgr.Instance.clients.Remove(playerId);
            NW_Mgr.Instance.transfers.Remove(this);
        }
    }

    #region // 收发数据
    public void Send(ushort protoType, IMessage message)
    {
        if (this.IsConnected)
        {
            byte[] messageBytes = ProtobufUtils.Serialize(message);
            int packageCount = (int)Math.Ceiling(1f * messageBytes.Length / NW_Def.PACKAGE_BODY_MAX_SIZE);
            if (packageCount > byte.MaxValue)
            {
                Console.WriteLine($"{protoType.ToString()} 数据包太大，即使切分之后还是过大");
                return;
            }
            else
            {
                Console.WriteLine("Server Network Send Message To Client... " + (LC_EProtoType)protoType + " " + socket.GetHashCode());
                byte[] packageBytes;
                if (packageCount <= 1)
                {
                    NW_Package package = new NW_Package(protoType, messageBytes, 0, (ushort)messageBytes.Length, 0, 1);
                    packageBytes = package.Encode();

                    try
                    {
                        this.socket.BeginSend(packageBytes, 0, packageBytes.Length, SocketFlags.None, new AsyncCallback(this.OnSend), null);
                    }
                    catch (System.Exception e)
                    {
                        BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
                        Console.WriteLine("Send Failed : " + e.ToString());
                    }
                }
                else
                {
                    int i = 0;
                    while (i < packageCount)
                    {
                        int beginIndex = i * NW_Def.PACKAGE_BODY_MAX_SIZE;
                        ushort packageLength = NW_Def.PACKAGE_BODY_MAX_SIZE;
                        if (i == packageCount - 1)
                        {
                            packageLength = (ushort)(messageBytes.Length - beginIndex);
                        }

                        NW_Package package = new NW_Package(protoType, messageBytes, beginIndex, packageLength, (byte)(i++), (byte)packageCount);
                        packageBytes = package.Encode();

                        try
                        {
                            Console.WriteLine("Server Network Send Message To Client... " + (LC_EProtoType)protoType + " " + socket.GetHashCode() + $" {i}/{packageCount}");
                            this.socket.BeginSend(packageBytes, 0, packageBytes.Length, SocketFlags.None, new AsyncCallback(this.OnSend), null);
                        }
                        catch (System.Exception e)
                        {
                            BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
                            Console.WriteLine("Send Failed : " + e.ToString());
                        }
                    }
                }
            }
        }
    }
    private void OnSend(IAsyncResult ar)
    {
        try { socket.EndSend(ar); }
        catch (System.Exception e)
        {
            BS_EventManager<BS_EEventType>.Trigger<NW_Transfer>(BS_EEventType.OnConnectLost, this);
            Console.WriteLine("EndSend Failed : " + e.ToString());
        }
    }
    #endregion
}