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

// Created by kaclok at 2017/06/11-14:20:25 Sunday on pc: KACLOK.
// Copyright@nullgame`s testgame. All rights reserved.

/// <summary>
/// ״̬���ɽӿ�
/// </summary>
namespace FSM
{
    public interface BS_ITransition : BS_IName
    {
        string name { get; }
        BS_IState from { get;}
        BS_IState to { get; }

        
        // ����ת������һЩ��ʱ�ԵĲ���������һ������
        float OnTransitionProgress();
        // ����ת��
        void Enter();
        // �˳�ת��
        void Exit();
        // �ܷ�ʼ����
        bool ShouldBegin();
        // �ܷ��������
        bool ShouleEnd();
    }
}
