using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

// Path: Assets/Scripts/Base/BS_ManagerMgr.cs.
// SvnVersion: -1.
// Author: kaclok created 2018/12/16 15:35:16 Sunday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

public class BS_ManagerMgr : BS_ManagerBase<BS_ManagerMgr>
{
    public override void OnInit()
    {
        BS_ManagerList.list.ForEach(mgr => { mgr.OnInit(); });
    }
    public override void OnUpdate()
    {
        BS_ManagerList.list.ForEach(mgr => { mgr.OnUpdate(); });
    }
    public override void OnExit()
    {
        BS_ManagerList.list.ForEach(mgr => { mgr.OnExit(); });
    }
}