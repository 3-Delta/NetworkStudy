﻿using System.Collections;
using System.Collections.Generic;

public class NW_Buffer
{
    public byte[] buffer;
    public int length;

    public NW_Buffer()
    {
        buffer = new byte[NW_Def.PACKAGE_BODY_MAX_SIZE];
        Clear();
    }
    public void Clear() { length = 0; }
}
