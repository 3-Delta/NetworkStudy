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

// Created by kaclok at 2017/06/10-17:28:48 Saturday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.
// https://study.163.com/course/courseLearn.htm?courseId=1003000008#/learn/video?lessonId=1003441078&courseId=1003000008

namespace FSM
{
    public interface BS_IName
    {
        string name { get; }
    }
}
