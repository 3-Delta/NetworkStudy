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

    public NW_Package Combine()
    {
        lock (packageQueue)
        {
            NW_Package pkg = new NW_Package();
            int i = 0;
            List<byte> ls = new List<byte>();
            while (this.packageQueue.Count > 0)
            {
                var one = this.packageQueue.Dequeue();
                if (i == 0)
                {
                    pkg.head.protoType = one.head.protoType;
                    pkg.head.randomKey = one.head.randomKey;
                    pkg.head.playerID = one.head.playerID;
                }

                pkg.head.size += one.head.size;
                if (one.body.bodyBytes != null)
                {
                    ls.AddRange(one.body.bodyBytes);
                }
                ++i;
            }

            pkg.body.bodyBytes = ls.ToArray();
            return pkg;
        }
    }
}
