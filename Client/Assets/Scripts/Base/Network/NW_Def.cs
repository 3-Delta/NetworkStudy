using System.Collections;
using System.Collections.Generic;
using System;

public class NW_Def
{
    public const string URL = "www.kaclok.com";
    public const string IPv4 = "127.0.0.1";
    public const string IPv6 = "1030::C9B4:FF12:48AA:1A2B";
    public const int PORT = 20086;

    public const ushort PACKAGE_HEAD_SIZE = sizeof(ushort) + sizeof(ushort);
    public const int PACKAGE_BODY_MAX_SIZE = 1024 * 8;
}