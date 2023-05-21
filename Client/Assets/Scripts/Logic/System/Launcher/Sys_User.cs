using System.Collections;
using System.Collections.Generic;
using System;
using Google.Protobuf;
using ProtobufNet;

public class Sys_User : SystemBase<Sys_User> {
    public ushort playerID { get; private set; } = 127;

    public override void OnInit() {
        NWDelegateService.Handle((ushort)LC_EProtoType.csLogin, (ushort)LC_EProtoType.scLogin, RespLogin, SCLogin.Parser, true);
        NWDelegateService.Handle((ushort)LC_EProtoType.csLogout, (ushort)LC_EProtoType.scLogout, RespLogout, SCLogout.Parser, true);
    }

    public void ReqLogin() {
        UnityEngine.Debug.LogError("ReqLogin");
        CSLogin cs = new CSLogin();
        cs.PlayerID = 123;
        NW_Mgr.Instance.Send(LC_EProtoType.csLogin, cs);
    }

    private void RespLogin(IMessage msg) {
        UnityEngine.Debug.LogError("RespLogin");
    }

    private void ReqLogout(NW_Package package) { }

    private void RespLogout(IMessage msg) {
        UnityEngine.Debug.LogError("RespLogout");
    }
}
