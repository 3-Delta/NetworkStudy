using Google.Protobuf;

// https://www.jianshu.com/p/fa959d16eaed
public class NW_Mgr : BS_ManagerBase<NW_Mgr> {
    public NW_Transfer transfer { get; private set; } = new NW_Transfer(null);

    public override void OnInit() {
    }
    public override void OnUpdate() {
        this.transfer?.Update();
    }
    public override void OnExit() {
        this.Disconnect();
    }

    public void Disconnect() {
        this.transfer.DisConnect();
    }

    public void Connect(string ip, int port, System.Action callback = null) {
        this.transfer.Connect(ip, port, callback);
    }
    public void Send(LC_EProtoType protoType, IMessage message) {
        this.Send((ushort)protoType, message);
    }
    private void Send(ushort protoType, IMessage message) {
        this.transfer.Send(protoType, message);
    }
}
