﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEventType : ushort
{
    OnConnectFailed = 0,
    OnConnectSuccess = 1,
    // 连接中断
    OnConnectLost = 2,
    OnSendFailed = 3,
}