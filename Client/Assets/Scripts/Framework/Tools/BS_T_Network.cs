using System.Collections;
using System.Collections.Generic;
using System;
using Google.Protobuf;
using System.Net.Sockets;
using System.Net.NetworkInformation;

public static class BS_T_Network
{
    public static bool IsIPv6(AddressFamily addressFamily) { return addressFamily == AddressFamily.InterNetworkV6; }
    public static Socket BuildSocket4TCP(AddressFamily addressFamily) { return new Socket(addressFamily, SocketType.Stream, ProtocolType.Tcp); }
    public static string GetMacAddress()
    {
        string ret = "00-00-00-00-00-00";
        try
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                ret = BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes());
                break;
            }
        }
        catch (Exception) { }
        return ret;
    }
}
