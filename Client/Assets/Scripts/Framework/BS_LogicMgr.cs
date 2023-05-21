using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_LogicMgr : Singleton<BS_LogicMgr> {
    public void OnInit() {
        SystemMgr.Instance.Register(BS_SystemRegistry.registry);
        SystemMgr.Instance.OnInit();

        BS_ManagerMgr.Instance.OnInit();
        BS_ControllerMgr.Instance.OnInit();
    }

    public void OnLogin(ulong roleId) {
        SystemMgr.Instance.OnLogin(roleId);
    }

    public void OnLogout() {
        SystemMgr.Instance.OnLogout();
    }

    public void OnUpdate() {
        SystemMgr.Instance.OnUpdate();
        BS_ManagerMgr.Instance.OnUpdate();
        BS_ControllerMgr.Instance.OnUpdate();
    }

    public void OnFixedUpdate(float fixedDeltaTime) {
        BS_ManagerMgr.Instance.OnFixedUpdate(fixedDeltaTime);
    }

    public void OnExit() {
        SystemMgr.Instance.OnExit();
        BS_ManagerMgr.Instance.OnExit();
        BS_ControllerMgr.Instance.OnExit();
    }
}
