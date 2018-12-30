using System.Collections;
using System.Collections.Generic;
using System;

// 将来客户端和服务器公用
public enum LC_EProtoType : ushort
{
    csLogin,
    scLogin,
    csLogout,
    scLogout,

    scKickOff,

    csReadMail,
    scReadMail,
}