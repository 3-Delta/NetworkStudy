using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

public static class App
{
    public enum TState
    {
        Normal,
        Reload,
        Exit,
    }

    public static TState currentState = TState.Normal;

    public static void Init()
    {
        currentState = TState.Normal;
        BS_LogicMgr.Instance.OnInit();

        BS_EventManager<BS_EEventType>.Handle(BS_EEventType.OnReload, () =>
        {
            BS_LogicMgr.Instance.OnReload();
        });
    }

    public static void Update()
    {
        while (currentState != TState.Exit)
        {
            BS_LogicMgr.Instance.OnUpdate();
            Thread.Sleep(25);
            Console.WriteLine("Update");
        }
    }
}
