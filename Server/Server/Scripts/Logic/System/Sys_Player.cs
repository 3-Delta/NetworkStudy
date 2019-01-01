using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;

public class Sys_Player : BS_SystemBase<Sys_Player>
{
    private Map<ushort, Player> players = new Map<ushort, Player>();

    public override void OnInit()
    {
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.csLogin, OnReqLogin);
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.csLogout, OnReqLogout);
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.scKickOff, OnRespKickOff);
    }

    private void OnReqLogin(NW_Package package)
    {
        CSLogin cs = BS_T_Protobuf.DeSerialize<CSLogin>(CSLogin.Parser, package.body.bodyBytes);
        Console.WriteLine("OnReqLogin : " + cs.PlayerID);

        SCLogin sc = new SCLogin();
        sc.PlayerID = 1277;
        NW_Mgr.Instance.Send(package.head.playerID, LC_EProtoType.scLogin, sc);
    }
    private void OnReqLogout(NW_Package package)
    {
        CSLogout sc = BS_T_Protobuf.DeSerialize<CSLogout>(CSLogout.Parser, package.body.bodyBytes);
    }
    private void OnRespKickOff(NW_Package package)
    {
        SCKickOff sc = BS_T_Protobuf.DeSerialize<SCKickOff>(SCKickOff.Parser, package.body.bodyBytes);
    }
}