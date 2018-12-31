using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class NW_Buffer
{
    public NW_Package package;
    public int realLength;
    // 缓冲区尽量大，数据接收丢失
    public byte[] buffer { get; set; } = new byte[NW_Def.PACKAGE_MAX_SIZE * 2];

    public NW_Buffer()
    {
        package.Clear();
        this.Clear();
    }
    public void Clear() { realLength = 0; }
}
