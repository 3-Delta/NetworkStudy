using System.Collections;
using System.Collections.Generic;

public enum LC_EEventType : ushort
{
    OnConnectFailed = 0,
    OnConnectSuccess = 1,
    // 连接中断
    OnConnectLost = 2,
    OnSendFailed = 3,
}