using System.Collections;
using System.Collections.Generic;
using System;

public class NW_Queue
{
    private Queue<NW_Package> packageQueue = new Queue<NW_Package>();

    public int Count => packageQueue.Count;

    public NW_Queue() { Clear(); }
    public void Clear() { packageQueue.Clear(); }
    public void Enqueue(NW_Package package)
    {
        lock (packageQueue) { packageQueue.Enqueue(package); }
    }
    public bool Dequeue(ref NW_Package package)
    {
        lock (packageQueue)
        {
            if (packageQueue.Count > 0)
            {
                package = packageQueue.Dequeue();
                return true;
            }
            else { return false; }
        }
    }
}
