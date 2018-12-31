using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public static class App
{
    public enum EAppState
    {
        Running,
        Reload, // 热更新表格
        Exit,
    }

    public static EAppState currentState = EAppState.Running;

    public static void Init()
    {
        currentState = EAppState.Running;
        BS_LogicMgr.Instance.OnInit();

        BS_EventManager<BS_EEventType>.Handle(BS_EEventType.OnReload, () =>
        {
            BS_LogicMgr.Instance.OnReload();
        });
    }

    public static void Update()
    {
        while (currentState != EAppState.Exit)
        {
            BS_LogicMgr.Instance.OnUpdate();
            Thread.Sleep(25);
        }
    }
}
