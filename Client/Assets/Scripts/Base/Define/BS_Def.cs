using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Def
{
    public const string URL = "www.kaclok.com";
    public const string IP = "127.0.0.1";
    public const int PORT = 20086;

    public const ushort PACKAGE_HEAD_SIZE = sizeof(ushort) + sizeof(ushort);
    public const int PACKAGE_BODY_MAX_SIZE = 1024 * 512;
}
