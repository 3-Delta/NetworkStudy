using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        Settings();
        RegisterCallback();
        BS_LogicMgr.Instance.OnInit();
    }
    private void Update()
    {
        BS_LogicMgr.Instance.OnUpdate();
    }
    private void FixedUpdate()
    {
        BS_LogicMgr.Instance.OnFixedUpdate(Time.fixedDeltaTime);
    }
    private void OnApplicationQuit()
    {
        BS_LogicMgr.Instance.OnExit();
        Application.Quit();
    }

    private void Settings()
    {
        Application.runInBackground = true;
    }
    private void RegisterCallback()
    {
    }
}
