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

// Created by kaclok at 2017/06/10-17:19:24 Saturday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

namespace FSM
{
    public class BS_StateMachine : BS_State, BS_IStateMachine
    {
        public Dictionary<string, BS_IState> states { get; protected set; } = new Dictionary<string, BS_IState>();
        public BS_IState currentState { get; protected set; } = null;
        public BS_IState defaultState { get; set; } = null;

        // 是否处于转换过程中
        public bool IsInTransition { get; protected set; } = false;
        public BS_ITransition currentTransition { get; protected set; } = null;

        public BS_StateMachine(string name, BS_IState defaultState) : base(name)
        {
            this.currentState = defaultState;
            this.defaultState = defaultState;
        }
        public void Add(string name, BS_IState target)
        {
            if (!Has(name))
            {
                states.Add(name, target);
                target.machine = this;
            }
        }
        public void Remove(string name)
        {
            if (!Equal(name, currentState.name) && Has(name))
            {
                states[name].machine = null;
                states.Remove(name);
            }
        }
        public void Reset() { currentState = defaultState; }
        public void Change(BS_IState target, bool force = false)
        {
            if (target != null)
            {
                if (!Equal(target.name, currentState.name) || force)
                {
                    currentState?.Exit(target);
                    target?.Enter(currentState);
                }
                currentState = target;
            }
        }
        public void Change(string name, bool force = false) { Change(Get(name), force); }
        public BS_IState Get(string name)
        {
            BS_IState ret = null;
            if (states != null)
            {
                if (!states.TryGetValue(name, out ret)) { ret = null; }
            }
            return ret;
        }
        public static bool Equal(string left, string right)
        {
            // 值类型的GetHashCode()就是值
            return left.Equals(right);
        }
        public bool Has(string name) { return Get(name) != null; }

        // 状态相关
        public override void Enter(BS_IState prev)
        {
            base.Enter(prev);
            currentState?.Enter(prev);
        }
        public override void Exit(BS_IState next)
        {
            base.Exit(next);
            currentState?.Enter(next);
        }
        // 整个状态机的驱动从这里开始
        public override void Update(float deltaTime)
        {
            if (IsInTransition)
            {
                if (!currentTransition.ShouleEnd()) { return; }
                else
                {
                    DoTransition(currentTransition);
                    IsInTransition = false;
                    // 是否需要这个return
                    return;
                }
            }

            base.Update(deltaTime);
            currentState?.Update(deltaTime);

            foreach (BS_ITransition t in currentState.transitions)
            {
                if (t.ShouldBegin())
                {
                    IsInTransition = true;
                    currentTransition = t;
                    return;
                }
            }
        }
        public virtual void DoTransition(BS_ITransition transition)
        {
            currentState?.Exit(transition.to);
            currentState = transition.to;
            currentState?.Enter(transition.from);
        }
    }
}
