using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Sys_Mail : BS_SystemBase<Sys_Mail>
{
    public override void OnInit()
    {
        NetworkServer.RegisterHandler(T_MsgType.reqReadMail, OnReqReadMail);
    }

    private void OnReqReadMail(NetworkMessage msg)
    {
        Proto_Mail package = new Proto_Mail();
        package.Deserialize(msg.reader);

        Debug.LogWarning("OnReqReadMail");
        Debug.LogWarning("id:" + package.id);
        Debug.LogWarning("senderName:" + package.senderName);
        Debug.LogWarning("sendTime:" + package.sendTime);
        Debug.LogWarning("contentLength:" + package.content.Length);
    }
}