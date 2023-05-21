using System.Collections;
using System.Collections.Generic;
using System;
using Google.Protobuf;
using ProtobufNet;

public class Sys_Mail : SystemBase<Sys_Mail> {
    public class MailEntry {
        public ulong mailID { get; private set; } = 0;
    }

    private Map<ulong, MailEntry> mails = new Map<ulong, MailEntry>();

    public override void OnInit() {
        NWDelegateService.Handle<SCReadMail>((ushort)LC_EProtoType.csReadMail, (ushort)LC_EProtoType.scReadMail, RespReadMail, SCReadMail.Parser, true);
    }

    public void ReqReadMail() {
        UnityEngine.Debug.LogError("ReqReadMail");
        CSReadMail cs = new CSReadMail();
        cs.MailID = 1;
        const string CONTENT = @"Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本

Socket.Close 方法
参考


定义
命名空间:
System.Net.Sockets
程序集:
System.dll
关闭 Socket 连接并释放所有关联的资源。

重载
Close()	
关闭 Socket 连接并释放所有关联的资源。

Close(Int32)	
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

Close()
关闭 Socket 连接并释放所有关联的资源。

C#

复制
public void Close ();
示例
下面的代码示例关闭了一个 Socket。

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用方法之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLingerSocket选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将套接字选项设置为DontLinger，请创建一个LingerOption、将启用的属性设置为true，并将该属性设置为LingerTime所需的超时false时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
Close(Int32)
关闭 Socket 连接并释放所有与指定超时关联的资源，以允许要发送的数据排队。

C#

复制
public void Close (int timeout);
参数
timeout
Int32
等待最多 timeout 毫秒以发送剩余的数据，然后关闭套接字。

示例
下面的代码示例演示如何关闭 。Socket

C#

复制
try
{
    aSocket.Shutdown(SocketShutdown.Both);
}
finally
{
    aSocket.Close();
}
注解
该方法 Close 关闭远程主机连接，并释放与该 Socket连接关联的所有托管和非托管资源。 关闭后，属性 Connected 设置为 false。

对于面向连接的协议，建议在调用之前调用ShutdownClose。 这可确保在连接套接字上发送和接收所有数据，然后再将其关闭。

如果需要在没有第一次调用的情况下进行呼叫CloseShutdown，可以通过设置DontLinger选项false并指定非零超时间隔，确保将排队传输的数据发送到传出传输。 Close 然后，将阻止此数据发送，或直到指定的超时过期。 如果设置为DontLingerfalse并指定零超时间隔，Close则释放连接，并自动放弃传出排队的数据。

 备注

若要将 DontLinger 套接字选项 false设置为，请创建一个 LingerOption，将启用的属性设置为 true，并将该属性设置为 LingerTime 所需的超时时间段。 将此 LingerOption 与套接字选项一 DontLinger 起使用以调用 SetSocketOption 该方法。

 备注

当你在应用程序中启用网络跟踪后，此成员将输出跟踪信息。 有关详细信息，请参阅.NET Framework中的网络跟踪。

另请参阅
Shutdown(SocketShutdown)
DontLinger
适用于
.NET 7 和其他版本
";
        cs.MailContent = CONTENT;
        NW_Mgr.Instance.Send(LC_EProtoType.csReadMail, cs);
    }

    public void RespReadMail(SCReadMail msg) {
        var res = msg as SCReadMail;
        UnityEngine.Debug.LogError("RespReadMail" + res.MailID + " " + res.MailContent.Length + " \n" + res.MailContent);
    }
}
