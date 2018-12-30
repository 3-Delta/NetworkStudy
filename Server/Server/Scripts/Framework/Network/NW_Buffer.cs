using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class NW_Buffer
{
    public int realLength { get; set; } = 0;
    public byte[] buffer { get; set; } = null;

    public NW_Buffer()
    {
        this.buffer = new byte[NW_Def.PACKAGE_BODY_MAX_SIZE + NW_Def.PACKAGE_HEAD_SIZE];
        this.Clear();
    }
    public void Clear() { realLength = 0; }
}
