using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;

public class Sys_Player : BS_SystemBase<Sys_Player>
{
    public short playerID { get; private set; } = 127; 

    public override void OnInit()
    {
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.scLogin, RespLogin);
        BS_EventManager<LC_EProtoType>.Handle<NW_Package>(LC_EProtoType.scLogout, RespLogout);
    }

    public void ReqLogin()
    {
        UnityEngine.Debug.LogError("ReqLogin");
        CSLogin cs = new CSLogin();
        cs.PlayerID = 123;
        NW_Mgr.Instance.Send(LC_EProtoType.csLogin, cs);
    }
    private void RespLogin(NW_Package package)
    {
        UnityEngine.Debug.LogError("RespLogin");
        SCLogin sc = BS_T_Protobuf.DeSerialize<SCLogin>(SCLogin.Parser, package.body.bodyBytes);
    }

    private void ReqLogout(NW_Package package)
    {
    }
    private void RespLogout(NW_Package package)
    {
        SCLogout sc = BS_T_Protobuf.DeSerialize<SCLogout>(SCLogout.Parser, package.body.bodyBytes);
    }
}