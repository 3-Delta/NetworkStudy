using System.Collections;
using System.Collections.Generic;
using System;
using Google.Protobuf;

public class NW_Queue<T> {
    protected Queue<T> queue = new Queue<T>();

    public int Count => this.queue.Count;

    public NW_Queue() {
        Clear();
    }

    public void Clear() {
        this.queue.Clear();
    }

    public void Enqueue(T package) {
        lock (this.queue) {
            this.queue.Enqueue(package);
        }
    }

    public bool Dequeue(out T package) {
        package = default;
        lock (this.queue) {
            if (this.queue.Count > 0) {
                package = this.queue.Dequeue();
                return true;
            }
            else {
                return false;
            }
        }
    }
}

public class NW_PackageQueue : NW_Queue<NW_Package> {
    public List<byte> Combine() {
        lock (this.queue) {
            int i = 0;
            List<byte> ls = new List<byte>();
            while (this.queue.Count > 0) {
                var one = this.queue.Dequeue();
                if (one.body.bodyBytes != null) {
                    ls.AddRange(one.body.bodyBytes);
                }

                ++i;
            }

            return ls;
        }
    }
}

public class NW_MessageQueue : NW_Queue<NW_ReceiveMessage> { }
