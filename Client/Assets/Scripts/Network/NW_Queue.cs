using System.Collections;
using System.Collections.Generic;
using System;

public class NW_Queue
{
    private static readonly object lockFlag = new object();
    private Queue<NW_Package> packageQueue = new Queue<NW_Package>();

    public NW_Queue()
    {
        Clear();
    }

    public int Count
    {
        get
        {
            return packageQueue.Count;
        }
    }

    public void Clear()
    {
        packageQueue.Clear();
    }

    public void Enqueue(NW_Package target)
    {
        if (target != null)
        {
            lock (lockFlag)
            {
                packageQueue.Enqueue(target);
            }
        }
    }

    public NW_Package Dequeue()
    {
        lock (lockFlag)
        {
            if (packageQueue.Count > 0)
            {
                return packageQueue.Dequeue();
            }
            else
            {
                return null;
            }
        }
    }
}
