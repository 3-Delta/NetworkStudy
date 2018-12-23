using GM.Src.Src;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using GM.Src.Network;

// https://www.jianshu.com/p/f8e9c3f49424
public class Proto_GM : MessageBase
{
    public string cmd;
    // public List<object> args = new List<object>();

    public override void Serialize(NetworkWriter writer)
    {
        base.Serialize(writer);
        writer.Write(cmd);
        //foreach (object o in args)
        //{
        //    Utils.Write(writer, o);
        //}
    }
    public override void Deserialize(NetworkReader reader)
    {
        base.Deserialize(reader);
        cmd = reader.ReadString();
    }
}
