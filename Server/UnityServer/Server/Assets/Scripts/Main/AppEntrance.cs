using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEntrance : MonoBehaviour
{
    private void Awake()
    {
        Load();
    }
    private void Load()
    {
        GameObject go = GameObject.Find("App");
        if (go == null)
        {
            go = new GameObject("App", typeof(App));
            if (go != null) { GameObject.DontDestroyOnLoad(go); }
        }
    }
}
