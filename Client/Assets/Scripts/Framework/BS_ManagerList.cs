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

// Path: Assets/Scripts/Base/BS_ManagerList.cs.
// SvnVersion: -1.
// Author: kaclok created 2018/12/16 15:38:22 Sunday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

public static class BS_ManagerList
{
    // BS_ManagerMgr和BS_DriveMgr不能放入
    public readonly static List<BS_ManagerBaseCallback> list = new List<BS_ManagerBaseCallback>()
    {
        NW_Mgr.Instance,
        BS_AIMgr.Instance,
        BS_CameraMgr.Instance,
        BS_FxMgr.Instance,
        BS_QualityMgr.Instance,
        BS_ResourceMgr.Instance,
        BS_SceneMgr.Instance,
        BS_TimeMgr.Instance,
        BS_UIMgr.Instance,
    };
}