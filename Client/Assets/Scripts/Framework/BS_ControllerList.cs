using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class BS_ControllerList
{
    public readonly static List<BS_ManagerBaseCallback> list = new List<BS_ManagerBaseCallback>()
    {
        Ct_Mail.Instance,
    };
}
