using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 在Game中统一驱动调用
public class BS_ControllerMgr : BS_ManagerBase<BS_ControllerMgr>
{
	public override void OnInit() 
	{
        BS_ControllerList.list.ForEach(controller => { controller.OnInit(); });
	}
	public override void OnUpdate()
	{
        BS_ControllerList.list.ForEach(controller => { controller.OnUpdate(); });
	}
	public override void OnExit() 
	{
        BS_ControllerList.list.ForEach(controller => { controller.OnExit(); });
	}
}