using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_SystemBaseCallback
{
    // 是否收到服务器关于该系统的登录回包
    public bool isReceived { get; protected set; } = false;

    public virtual void OnInit() { isReceived = false; }
    public virtual void OnBeforeLogin() { }
    public virtual void OnLogin() { }
	public virtual void OnLogout() { isReceived = false; }
	public virtual void OnUpdate() { }
	public virtual void OnExit() { isReceived = false; }
}

public class BS_SystemBase<T> : BS_SystemBaseCallback where T : class, new()
{
    protected BS_SystemBase() { }
    public static T Instance { get { return Singleton<T>.Instance; } }
}
