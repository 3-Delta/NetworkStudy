using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    private static T instance = null;

    protected Singleton() { }
    public static T Instance
    {
        get
        {
            instance = instance ?? new T();
            return instance;
        }
    }

}
