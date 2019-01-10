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

// Created by kaclok at 2017/06/11-14:22:17 Sunday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

/// <summary>
/// ×´Ì¬¹ý¶É
/// </summary>
namespace FSM
{
    public delegate bool BoolTransitionBool();
    public delegate float FloatTransitionBool();

    public class BS_Transition : BS_ITransition
    {
        public string name { get; private set; }
        public BS_IState from { get; private set; }
        public BS_IState to { get; private set; }

        public event FloatTransitionBool transitionProgress;
        public event BoolTransitionBool transitionBegin;

        public BS_Transition(string name, BS_IState from = null, BS_IState to = null)
        {
            this.name = name;
            this.from = from;
            this.to = to;
        }
        public virtual float OnTransitionProgress()
        {
            float prg = 1f;
            if (transitionProgress != null) { prg = transitionProgress(); }
            return prg;
        }
        public virtual void Exit()
        {

        }
        public virtual void Enter()
        {

        }
        public virtual bool ShouldBegin()
        {
            bool ret = false;
            if (transitionBegin != null) { ret = transitionBegin(); }
            // if (ret) { Enter(); }
            return ret;
        }
        public virtual bool ShouleEnd()
        {
            bool ret = OnTransitionProgress() >= 1f;
            // if (ret) { Exit(); }
            return ret;
        }
    }
}
