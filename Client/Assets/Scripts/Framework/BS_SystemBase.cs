public interface ISystemBaseCallback {
    ulong RoleId { get; set; } // 各个系统持有roleId
    
    void OnInit();
    void OnExit();
    void OnHandleEvents(bool toRegister); // 事件监听

    void OnLoadINI(ulong roleId); // 从Persistent或者其他文件加载客户端本机的数据
    void OnFenced(); // 登录首包全部接收

    void OnLogin(ulong roleId); // 断线重连成功之后不会调用OnLogin，只有玩家首次登录 或者 主动切换角色然后会走重新登录流程，才会OnLogin
    void OnLogout(); // 断线重连开始之后不会调用OnLogout，只有玩家主动切换角色才会OnLogout

    void OnBeginReconnect(); // 开始断线重连
    void OnEndReconnect(bool result); // 结束断线重连，参数是重连成功还是失败
}

public interface ISystemUpdateCallback : ISystemBaseCallback {
    void OnUpdate();
}

public interface ISystemNetTransfer {
    NW_Transfer nwTransfer { get; set; } // 各个系统持有nwTransfer
}

public class SystemBase : ISystemBaseCallback {
    public ulong RoleId { get; set; } // 各个系统持有roleId

    public virtual void OnInit() { }
    public virtual void OnExit() { }
    public virtual void OnHandleEvents(bool toRegister) { }

    public virtual void OnLoadINI(ulong roleId) { }
    public virtual void OnFenced() { }

    public virtual void OnLogin(ulong roleId) { }
    public virtual void OnLogout() { }

    public virtual void OnBeginReconnect() { }
    public virtual void OnEndReconnect(bool result) { }
}

public class SystemBase<T> : SystemBase where T : class, new() {
    protected SystemBase() { }

    public static T Instance {
        get { return Singleton<T>.Instance; }
    }
}
