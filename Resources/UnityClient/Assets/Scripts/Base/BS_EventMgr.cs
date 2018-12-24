using System;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 多参数消息传递
/// 同一个事件，如果同时注册不同类型的监听函数，也就是参数个数不相同的监听函数，这里可以注册，但是trigger的时候类型as强转会失败。
/// 会有一个问题就是注册失败的话，会一直存在不会销毁。
/// </summary>

// public delegate void BS_EventsAction(params object[] args);
// 泛型相比可变参数优势：可以减少box操作。
public delegate void BS_EventAction();
public delegate void BS_EventAction<T>(T arg);
public delegate void BS_EventAction<T1, T2>(T1 arg1, T2 arg2);
public delegate void BS_EventAction<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
public delegate void BS_EventAction<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

public static class BS_EventHelper
{
    private static Dictionary<BS_EventType, Delegate> messageTable = null;
    private static void TryCreateEventsList() { messageTable = messageTable ?? new Dictionary<BS_EventType, Delegate>(); }
    private static bool PreAdd(BS_EventType eventType, Delegate action)
    {
        if (action == null) { return false; }
        TryCreateEventsList();
        bool canAdd = true;
        if (!messageTable.ContainsKey(eventType))
        {
            messageTable.Add(eventType, null);
        }

        Delegate d = messageTable[eventType];
        if (d != null)
        {
            if (d.GetType() != action.GetType())
            {
                canAdd = false;
            }
        }

        return canAdd;
    }

    private static void PostAdd(BS_EventType eventType, Delegate action)
    {
        if (action == null)
        {
            return;
        }
    }

    private static bool PreRemove(BS_EventType eventType, Delegate action)
    {
        if (messageTable == null || action == null) { return false; }
        bool canRemove = true;
        if (messageTable.ContainsKey(eventType))
        {
            Delegate d = messageTable[eventType];
            canRemove = d != null;
        }
        else { canRemove = false; }
        return canRemove;
    }

    private static void PostRemove(BS_EventType eventType)
    {
        if (messageTable == null) { return; }
        if (messageTable.ContainsKey(eventType))
        {
            if (messageTable[eventType] == null)
            {
                messageTable.Remove(eventType);
            }
        }
    }

    private static bool PreTrigger(BS_EventType eventType)
    {
        if (messageTable == null) { return false; }
        return messageTable.ContainsKey(eventType) && messageTable[eventType] != null;
    }

    // add
    public static void Add(BS_EventType eventType, BS_EventAction handler)
    {
        if (PreAdd(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction)messageTable[eventType] + handler;
        }
        PostAdd(eventType, handler);
    }

    public static void Add<T>(BS_EventType eventType, BS_EventAction<T> handler)
    {
        if (PreAdd(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction<T>)messageTable[eventType] + handler;
        }
        PostAdd(eventType, handler);
    }

    public static void Add<T1, T2>(BS_EventType eventType, BS_EventAction<T1, T2> handler)
    {
        if (PreAdd(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction<T1, T2>)messageTable[eventType] + handler;
        }
        PostAdd(eventType, handler);
    }

    public static void Add<T1, T2, T3>(BS_EventType eventType, BS_EventAction<T1, T2, T3> handler)
    {
        if (PreAdd(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction<T1, T2, T3>)messageTable[eventType] + handler;
        }
        PostAdd(eventType, handler);
    }

    public static void Add<T1, T2, T3, T4>(BS_EventType eventType, BS_EventAction<T1, T2, T3, T4> handler)
    {
        if (PreAdd(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction<T1, T2, T3, T4>)messageTable[eventType] + handler;
        }
        PostAdd(eventType, handler);
    }

    // remove
    public static void Remove(BS_EventType eventType, BS_EventAction handler)
    {
        if (PreRemove(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction)messageTable[eventType] - handler;
        }
        PostRemove(eventType);
    }

    public static void Remove<T>(BS_EventType eventType, BS_EventAction<T> handler)
    {
        if (PreRemove(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction<T>)messageTable[eventType] - handler;
        }
        PostRemove(eventType);
    }

    public static void Remove<T1, T2>(BS_EventType eventType, BS_EventAction<T1, T2> handler)
    {
        if (PreRemove(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction<T1, T2>)messageTable[eventType] - handler;
        }
        PostRemove(eventType);
    }

    public static void Remove<T1, T2, T3>(BS_EventType eventType, BS_EventAction<T1, T2, T3> handler)
    {
        if (PreRemove(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction<T1, T2, T3>)messageTable[eventType] - handler;
        }
        PostRemove(eventType);
    }

    public static void Remove<T1, T2, T3, T4>(BS_EventType eventType, BS_EventAction<T1, T2, T3, T4> handler)
    {
        if (PreRemove(eventType, handler))
        {
            messageTable[eventType] = (BS_EventAction<T1, T2, T3, T4>)messageTable[eventType] - handler;
        }
        PostRemove(eventType);
    }

    // handle
    public static void Handle(BS_EventType eventType, BS_EventAction handler, bool toBeAdd = true)
    {
        if (toBeAdd) { Add(eventType, handler); }
        else { Remove(eventType, handler); }
    }

    public static void Handle<T>(BS_EventType eventType, BS_EventAction<T> handler, bool toBeAdd = true)
    {
        if (toBeAdd) { Add<T>(eventType, handler); }
        else { Remove<T>(eventType, handler); }
    }

    public static void Handle<T1, T2>(BS_EventType eventType, BS_EventAction<T1, T2> handler, bool toBeAdd = true)
    {
        if (toBeAdd) { Add<T1, T2>(eventType, handler); }
        else { Remove<T1, T2>(eventType, handler); }
    }

    public static void Handle<T1, T2, T3>(BS_EventType eventType, BS_EventAction<T1, T2, T3> handler, bool toBeAdd = true)
    {
        if (toBeAdd) { Add<T1, T2, T3>(eventType, handler); }
        else { Remove<T1, T2, T3>(eventType, handler); }
    }

    public static void Handle<T1, T2, T3, T4>(BS_EventType eventType, BS_EventAction<T1, T2, T3, T4> handler, bool toBeAdd = true)
    {
        if (toBeAdd) { Add<T1, T2, T3, T4>(eventType, handler); }
        else { Remove<T1, T2, T3, T4>(eventType, handler); }
    }

    // trigger
    public static void Trigger(BS_EventType eventType)
    {
        if (PreTrigger(eventType))
        {
            BS_EventAction handler = messageTable[eventType] as BS_EventAction;
            // as运算符有可能强转失败，返回null
            if (handler != null)
            {
                handler();
            }
        }
    }

    public static void Trigger<T>(BS_EventType eventType, T arg)
    {
        if (PreTrigger(eventType))
        {
            BS_EventAction<T> handler = messageTable[eventType] as BS_EventAction<T>;
            // as运算符有可能强转失败，返回null
            if (handler != null)
            {
                handler(arg);
            }
        }
    }

    public static void Trigger<T1, T2>(BS_EventType eventType, T1 arg1, T2 arg2)
    {
        if (PreTrigger(eventType))
        {
            BS_EventAction<T1, T2> handler = messageTable[eventType] as BS_EventAction<T1, T2>;
            if (handler != null)
            {
                handler(arg1, arg2);
            }
        }
    }

    public static void Trigger<T1, T2, T3>(BS_EventType eventType, T1 arg1, T2 arg2, T3 arg3)
    {
        if (PreTrigger(eventType))
        {
            BS_EventAction<T1, T2, T3> handler = messageTable[eventType] as BS_EventAction<T1, T2, T3>;
            if (handler != null)
            {
                handler(arg1, arg2, arg3);
            }
        }
    }

    public static void Trigger<T1, T2, T3, T4>(BS_EventType eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        if (PreTrigger(eventType))
        {
            BS_EventAction<T1, T2, T3, T4> handler = messageTable[eventType] as BS_EventAction<T1, T2, T3, T4>;
            if (handler != null)
            {
                handler(arg1, arg2, arg3, arg4);
            }
        }
    }
}
