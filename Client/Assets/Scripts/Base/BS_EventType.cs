using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BS_EventType : ushort
{
    OnConnectFailed = 0,
    OnConnectSuccess = 1,
    // 连接中断
    OnConnectLost = 2,
    OnSendFailed = 3,
}