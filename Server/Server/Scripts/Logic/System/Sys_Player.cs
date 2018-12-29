using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;

public class Sys_Player : BS_SystemBase<Sys_Player>
{
    public override void OnInit()
    {
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.scLogin, RespLogin, true);
    }

    public void RespLogin(NW_Package package)
    {
        SCLogin sc = T_Protobuf.DeSerialize<SCLogin>(SCLogin.Parser, package.body);
    }
}