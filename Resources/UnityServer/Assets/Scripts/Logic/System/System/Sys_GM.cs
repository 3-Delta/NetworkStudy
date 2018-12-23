using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Sys_GM : BS_SystemBase<Sys_GM>
{
    public override void OnInit()
    {
        NetworkServer.RegisterHandler(T_MsgType.reqGM, OnGM);
    }

    private void OnGM(NetworkMessage msg)
    {
        Proto_GM package = new Proto_GM();
        package.Deserialize(msg.reader);

        Debug.LogWarning("OnReqReadMail");
        Debug.LogWarning("cmd:" + package.cmd);
    }
}