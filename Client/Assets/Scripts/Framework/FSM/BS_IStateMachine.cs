using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Net.Sockets;
using System.Diagnostics;
using System;
using System.IO;
using System.Net;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Created by kaclok at 2017/06/10-17:29:04 Saturday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

namespace FSM
{
    public interface BS_IStateMachine : BS_IName
    {
        Dictionary<string, BS_IState> states { get; }
        BS_IState currentState { get; }
        BS_IState defaultState { get; set; }
        string name { get; }
        // 转换
        BS_ITransition currentTransition { get; }
        bool IsInTransition{ get; }

        void Add(string name, BS_IState target);
        void Remove(string name);
        void Change(BS_IState target, bool force = false);
        void Change(string name, bool force = false);
        bool Has(string name);
        BS_IState Get(string name);
        void Reset();
    }
}
