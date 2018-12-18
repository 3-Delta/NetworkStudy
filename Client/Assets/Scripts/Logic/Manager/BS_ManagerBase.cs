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

// Path: Assets/Scripts/Base/BS_ManagerBase.cs.
// SvnVersion: -1.
// Author: kaclok created 2018/12/16 15:35:58 Sunday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

public class BS_ManagerBaseCallback
{
    public virtual void OnInit() { }
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
}

public class BS_ManagerBase<T> : BS_ManagerBaseCallback where T : class, new()
{
    protected BS_ManagerBase() { }
    public static T Instance { get { return Singleton<T>.Instance; } }
}