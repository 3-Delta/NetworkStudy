using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// https://www.jianshu.com/p/f8e9c3f49424
public class Proto_Mail : MessageBase
{
    public ulong id;
    public uint sendTime;
    public string senderName;
    public byte[] content;

    public override void Serialize(NetworkWriter writer)
    {
        base.Serialize(writer);
        writer.Write(id);
        writer.Write(sendTime);
        writer.Write(senderName);
        // 数组类型记录长度
        writer.WriteBytesAndSize(content, content.Length);
    }
    public override void Deserialize(NetworkReader reader)
    {
        base.Deserialize(reader);
        id = reader.ReadUInt64();
        sendTime = reader.ReadUInt32();
        senderName = reader.ReadString();

        // 读取write写入的bytes的长度
        int count = reader.ReadUInt16();
        content = reader.ReadBytes(count);
    }
}
