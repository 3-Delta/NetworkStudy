using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;

public class Sys_Mail : BS_SystemBase<Sys_Mail>
{
    public override void OnInit()
    {
        BS_EventManager<EProtoType>.Handle<NW_PackageBody>(EProtoType.scReadMail, RespReadMail, true);
    }
    public void ReqwReadMail()
    {
        CSReadMail cs = new CSReadMail();
        cs.MailID = 1;
        NW_Mgr.Send(EProtoType.csReadMail, cs);
    }
    public void RespReadMail(NW_PackageBody body)
    {
        SCReadMail sc = T_Protobuf.DeSerialize<SCReadMail>(SCReadMail.Parser, body);
    }
}