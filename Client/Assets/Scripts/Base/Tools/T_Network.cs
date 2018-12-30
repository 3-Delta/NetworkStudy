using System.Collections;
using System.Collections.Generic;
using System;
using Google.Protobuf;
using System.Net.Sockets;

public static class T_Network
{
    public static bool IsIPv6(AddressFamily addressFamily) { return addressFamily == AddressFamily.InterNetworkV6; }
    public static Socket BuildSocket4TCP(AddressFamily addressFamily) { return new Socket(addressFamily, SocketType.Stream, ProtocolType.Tcp); }
}
