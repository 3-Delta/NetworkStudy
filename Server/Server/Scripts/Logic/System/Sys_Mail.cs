using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;

public class Sys_Mail : BS_SystemBase<Sys_Mail>
{
    private Map<ulong, LC_Mail> mails = new Map<ulong, LC_Mail>();

    public override void OnInit()
    {
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.scReadMail, RespReadMail, true);
    }

    public void RespReadMail(NW_Package package)
    {
        SCReadMail sc = T_Protobuf.DeSerialize<SCReadMail>(SCReadMail.Parser, package.body);
    }
}