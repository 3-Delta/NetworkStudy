using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;
using Google.Protobuf;

public class Sys_Mail : BS_SystemBase<Sys_Mail>
{
    private Map<ulong, LC_Mail> mails = new Map<ulong, LC_Mail>();

    public override void OnInit()
    {
        NWDelegateService.Handle<NW_ReceiveMessage>(0, (ushort)LC_EProtoType.csReadMail, OnReqReadMail, CSReadMail.Parser, true);
    }

    public void OnReqReadMail(NW_ReceiveMessage message)
    {
        CSReadMail cs = message.message as CSReadMail;
        SCReadMail sc = new SCReadMail();
        sc.MailID = 12578;
        sc.MailContent = "Server: |||" + cs.MailContent;
        NW_Mgr.Instance.Send(message.playerId, LC_EProtoType.scReadMail, sc);
    }
}