using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;

public class Sys_Mail : BS_SystemBase<Sys_Mail>
{
    private Map<ulong, LC_Mail> mails = new Map<ulong, LC_Mail>();

    public override void OnInit()
    {
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.csReadMail, OnReqReadMail, true);
    }

    public void OnReqReadMail(NW_Package package)
    {
        CSReadMail cs = BS_T_Protobuf.DeSerialize<CSReadMail>(CSReadMail.Parser, package.body.bodyBytes);
        SCReadMail sc = new SCReadMail();
        sc.MailID = 12578;
        NW_Mgr.Instance.Send(package.head.playerID, LC_EProtoType.scReadMail, sc);
    }
}