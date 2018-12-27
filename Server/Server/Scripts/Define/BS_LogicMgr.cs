using System.Collections;
using System.Collections.Generic;

public class BS_LogicMgr : Singleton<BS_LogicMgr>
{
	public void OnInit()
	{
		BS_SystemMgr.Instance.OnInit();
        BS_ManagerMgr.Instance.OnInit();
    }
    public void OnReload() { BS_SystemMgr.Instance.OnReload(); }
    public void OnLogin() { BS_SystemMgr.Instance.OnLogin(); }
    public void OnLogout() { BS_SystemMgr.Instance.OnLogout(); }
    public void OnUpdate()
	{
		BS_SystemMgr.Instance.OnUpdate();
        BS_ManagerMgr.Instance.OnUpdate();
    }
    public void OnExit()
    {
        BS_SystemMgr.Instance.OnExit();
        BS_ManagerMgr.Instance.OnExit();
    }
}