using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;
using Google.Protobuf;

public class Sys_Player : BS_SystemBase<Sys_Player>
{
    private Map<ushort, Player> players = new Map<ushort, Player>();

    public override void OnInit()
    {
        NWDelegateService.Handle<NW_ReceiveMessage>(0, (ushort)LC_EProtoType.csLogin, OnReqLogin, CSLogin.Parser, true);
        NWDelegateService.Handle<NW_ReceiveMessage>(0, (ushort)LC_EProtoType.csLogout, OnReqLogin, CSLogout.Parser, true);
        NWDelegateService.Handle<NW_ReceiveMessage>(0, (ushort)LC_EProtoType.scKickOff, OnReqLogin, SCKickOff.Parser, true);
    }

    private void OnReqLogin(NW_ReceiveMessage message)
    {
        CSLogin cs = message.message as CSLogin;
        Console.WriteLine("OnReqLogin : " + cs.PlayerID);

        SCLogin sc = new SCLogin();
        sc.PlayerID = 1277;
        NW_Mgr.Instance.Send(message.playerId, LC_EProtoType.scLogin, sc);
    }
    private void OnReqLogout(NW_ReceiveMessage message)
    {
        Console.WriteLine("OnReqLogout : ");
    }
    private void OnRespKickOff(NW_ReceiveMessage message)
    {
    }
}