using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BS_AIMgr : BS_ManagerBase<BS_AIMgr>
{
    public override void OnInit()
    {
        BS_DriveMgr.Instance.Add(15, this);
    }
    public override void OnFixedUpdate(float fixedDeltaTime)
    {
    }
}