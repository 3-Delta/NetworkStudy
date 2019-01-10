using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 不能加入ManagerList
public class BS_DriveMgr : BS_ManagerBase<BS_DriveMgr>
{
    public class MgrTime
    {
        public List<BS_ManagerBaseCallback> list { get; set; } = new List<BS_ManagerBaseCallback>();
        public float lastTime { get; set; } = 0f;
        public uint frame { get; set; } = 0;

        public MgrTime() { }
    }
    public static Dictionary<uint, MgrTime> dict { get; private set; } = new Dictionary<uint, MgrTime>();
    public static Dictionary<BS_ManagerBaseCallback, bool> existDict { get; private set; } = new Dictionary<BS_ManagerBaseCallback, bool>();

    public void Add(uint frame, BS_ManagerBaseCallback mgr)
    {
        if (frame > 0 && mgr != null && !existDict.ContainsKey(mgr))
        {
            if (dict.ContainsKey(frame))
            {
                if (!dict[frame].list.Contains(mgr))
                {
                    dict[frame].list.Add(mgr);
                    dict[frame].lastTime = Time.time;
                    existDict[mgr] = true;
                }
            }
            else
            {
                dict[frame] = new MgrTime();
                dict[frame].list = new List<BS_ManagerBaseCallback>() { mgr};
                dict[frame].lastTime = Time.time;
                existDict[mgr] = true;
            }
        }
    }

    public override void OnFixedUpdate(float fixedDeltaTime)
    {
        foreach (var kvp in dict)
        {
            var t = dict[kvp.Key];
            if (++t.frame % kvp.Key == 0)
            {
                foreach (BS_ManagerBaseCallback mgr in t.list) { mgr.OnFixedUpdate(Time.time - t.lastTime); }
                t.frame = 0;
                t.lastTime = Time.time;
            }
        }
    }
}
