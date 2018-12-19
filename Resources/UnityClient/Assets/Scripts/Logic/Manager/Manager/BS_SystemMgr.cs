using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 在Game中统一驱动调用
public class BS_SystemMgr : BS_SystemBase<BS_SystemMgr>
{
	public override void OnInit() 
	{
        BS_SystemList.systemList.ForEach(system => { system.OnInit(); });
	}
    public override void OnBeforeLogin()
    {
        BS_SystemList.systemList.ForEach(system => { system.OnBeforeLogin(); });
    }
    public override void OnLogin() 
	{
        BS_SystemList.systemList.ForEach(system => { system.OnLogin(); });
	}
	public override void OnLogout() 
	{
        BS_SystemList.systemList.ForEach(system => { system.OnLogout(); });
	}
	public override void OnUpdate()
	{
        BS_SystemList.systemList.ForEach(system => { system.OnUpdate(); });
	}
	public override void OnExit() 
	{
        BS_SystemList.systemList.ForEach(system => { system.OnExit(); });
	}
}