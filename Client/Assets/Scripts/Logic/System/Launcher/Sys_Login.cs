using ProtobufNet;

public class Sys_Login : SystemBase<Sys_Login> {
    public ushort playerID { get; private set; } = 127;

    public override void OnHandleEvents(bool toRegister) {
        NWDelegateService.Handle<SCLogin>((ushort)LC_EProtoType.csLogin, (ushort)LC_EProtoType.scLogin, RespLogin, SCLogin.Parser, toRegister);
        NWDelegateService.Handle<SCLogout>((ushort)LC_EProtoType.csLogout, (ushort)LC_EProtoType.scLogout, RespLogout, SCLogout.Parser, toRegister);

        void _onConnectStatusChanged(NW_Transfer transfer, NW_Transfer.EConnectStatus oldStatus, NW_Transfer.EConnectStatus newStatus) {
            if (newStatus == NW_Transfer.EConnectStatus.Ready) {
                if (transfer.disconnectReason != NW_Transfer.EDisconnectReason.Initiative) {
                    // server把client踢掉了
                    _OnLogout(false);
                }
            }
        }

        if (toRegister) {
            NW_Mgr.Instance.transfer.onConnectStatusChanged += _onConnectStatusChanged;
        }
        else {
            NW_Mgr.Instance.transfer.onConnectStatusChanged -= _onConnectStatusChanged;
        }
    }

    public void Connect() {
        NW_Mgr.Instance.Connect(NW_Def.IPv4, NW_Def.PORT);
    }

    public void Disconnect() {
        NW_Mgr.Instance.Disconnect();
    }

    public void ReqLogin() {
        UnityEngine.Debug.LogError("ReqLogin");
        CSLogin cs = new CSLogin();
        cs.PlayerID = 123;
        NW_Mgr.Instance.Send(LC_EProtoType.csLogin, cs);
    }

    private void RespLogin(SCLogin message) {
        UnityEngine.Debug.LogError("RespLogin");

        SystemMgr.Instance.OnLogin(this.playerID);
        SystemMgr.Instance.SetTransfer(NW_Mgr.Instance.transfer);
    }

    public void ReqLogout() {
        UnityEngine.Debug.LogError("ReqLogout");
        CSLogout cs = new CSLogout();
        cs.PlayerID = 123;
        NW_Mgr.Instance.Send(LC_EProtoType.csLogout, cs);
    }

    private void RespLogout(SCLogout message) {
        UnityEngine.Debug.LogError("RespLogout");

        _OnLogout(true);
    }

    private void _OnLogout(bool initiative) {
        SystemMgr.Instance.OnLogout();
        this.Disconnect();
    }
}
