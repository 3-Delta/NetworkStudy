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
        BS_LogicMgr.OnInit();
    }
    private void Update()
    {
        BS_LogicMgr.Update();
    }
    private void OnApplicationQuit()
    {
        BS_LogicMgr.OnExit();
    }
}
