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

// Created by kaclok at 2017/06/10-14:01:00 Saturday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

namespace FSM
{
    // 代理定义
    public delegate void VoidDelegateVoid();
    public delegate void VoidDelegateState(BS_IState iState);
    public delegate void VoidVoidDelegateFloat(float deltaTime);

    public class BS_State : BS_IState
    {
        // 状态事件
        public event VoidDelegateState OnEnter;
        public event VoidDelegateState OnExit;
        public event VoidVoidDelegateFloat OnUpdate;

        public BS_IStateMachine machine { get; set; } = null;
        public string name { get; protected set; } = null;
        public float time { get; protected set; } = 0f;
        
        public BS_State(string name) 
        {
            this.name = name;
        }
        public virtual void Enter(BS_IState prev)
        {
            time = 0f;
            OnEnter?.Invoke(prev);
        }
        public virtual void Exit(BS_IState next)
        {
            OnExit?.Invoke(next);
            time = 0f;
        }
        public virtual void Update(float deltaTime)
        {
            time += deltaTime;
            OnUpdate?.Invoke(deltaTime);
        }

        // 状态转换
        public List<BS_ITransition> transitions { get; protected set; }
        public void AddTransition(BS_ITransition target)
        {
            transitions = transitions ?? new List<BS_ITransition>();
            if (target != null && !transitions.Contains(target)) { transitions.Add(target); }
        }
        public void RemoveTransition(BS_ITransition target)
        {
            if (transitions != null && transitions.Contains(target)) { transitions.Remove(target); }
        }
    }
}
