using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sys_Mail : BS_SystemBase<Sys_Mail>
{
    public void ReqReadMail()
    {
        Proto_Mail msg = new Proto_Mail();
        msg.id = 1;
        msg.senderName = "kaclok";
        msg.sendTime = 222;
        msg.content = new byte[100];

        BS_NwMgr.Instance.connection.Send(T_MsgType.reqReadMail, msg);
    }
}