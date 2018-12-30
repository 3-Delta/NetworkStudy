using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

// Path: Assets/Scripts/Base/BS_ManagerList.cs.
// SvnVersion: -1.
// Author: kaclok created 2018/12/16 15:38:22 Sunday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

public static class BS_ManagerList
{
    // BS_ManagerMgr��BS_DriveMgr���ܷ���
    public readonly static List<BS_ManagerBaseCallback> list = new List<BS_ManagerBaseCallback>()
    {
        NW_Mgr.Instance,
        Mgr_DB.Instance,
        Mgr_GM.Instance,
        Mgr_Redis.Instance,
    };
}