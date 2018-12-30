using System.Collections;
using System.Collections.Generic;
using System;
using ProtobufNet;

public class Player
{
    public ushort playerID { get; private set; } = 0;
    public string name { get; set; } = null;
}