using Google.Protobuf;

// https://www.jianshu.com/p/fa959d16eaed
public class NW_Mgr : BS_ManagerBase<NW_Mgr> {
    private NW_Transfer transfer = new NW_Transfer(null);

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
        byte[] bytes = BS_T_Protobuf.Serialize(message);
        if (bytes != null) {
            this.Send(protoType, bytes);
        }
    }
    private void Send(LC_EProtoType protoType, byte[] bytes) {
        this.Send((short)protoType, bytes);
    }
    private void Send(short protoType, byte[] bytes) {
        this.transfer.Send(protoType, bytes);
    }
}
