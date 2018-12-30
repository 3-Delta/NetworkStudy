using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;

public class Sys_PlayerCollection : BS_SystemBase<Sys_PlayerCollection>
{
    private Map<ushort, Player> players = new Map<ushort, Player>();

    public override void OnInit()
    {
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.scLogin, RespLogin);
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.scLogout, RespLogout);
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.scKickOff, RespKickOff);
    }

    private void RespLogin(NW_Package package)
    {
        SCLogin sc = T_Protobuf.DeSerialize<SCLogin>(SCLogin.Parser, package.body);
    }
    private void RespLogout(NW_Package package)
    {
        SCLogout sc = T_Protobuf.DeSerialize<SCLogout>(SCLogout.Parser, package.body);
    }
    private void RespKickOff(NW_Package package)
    {
        SCKickOff sc = T_Protobuf.DeSerialize<SCKickOff>(SCKickOff.Parser, package.body);
    }
}