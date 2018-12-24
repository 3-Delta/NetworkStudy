using System.Collections;
using System.Collections.Generic;

public class BS_SystemBaseCallback
{
	public virtual void OnInit() {}
    public virtual void OnBeforeLogin() { }
    public virtual void OnLogin() {}
	public virtual void OnLogout() {}
	public virtual void OnUpdate() {}
	public virtual void OnExit() {}
}

public class BS_SystemBase<T> : BS_SystemBaseCallback where T : class, new()
{
    protected BS_SystemBase() { }
    public static T Instance { get { return Singleton<T>.Instance; } }
}
