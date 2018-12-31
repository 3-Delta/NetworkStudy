using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class NW_Buffer
{
    public NW_Package package = new NW_Package();
    public int realLength { get; set; } = 0;
    // 缓冲区尽量大，数据接收丢失
    public byte[] buffer { get; set; } = new byte[NW_Def.PACKAGE_MAX_SIZE * 2];

    public NW_Buffer()
    {
        this.Clear();
    }
    public void Clear()
    {
        realLength = 0;
        package.Clear();
    }
}
