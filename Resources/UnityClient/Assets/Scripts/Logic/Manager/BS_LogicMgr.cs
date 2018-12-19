using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BS_LogicMgr
{
	public static void OnInit()
	{
		BS_SystemMgr.Instance.OnInit();
        BS_ManagerMgr.Instance.OnInit();
    }
	public static void Update()
	{
		BS_SystemMgr.Instance.OnUpdate();
        BS_ManagerMgr.Instance.OnUpdate();
    }
    public static void OnExit()
    {
        BS_SystemMgr.Instance.OnExit();
        BS_ManagerMgr.Instance.OnExit();
    }
}