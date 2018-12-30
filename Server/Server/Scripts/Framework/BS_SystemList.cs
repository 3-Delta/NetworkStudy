using System.Collections;
using System.Collections.Generic;

public static class BS_SystemList
{
	public readonly static List<BS_SystemBaseCallback> list = new List<BS_SystemBaseCallback>()
	{
        Sys_Mail.Instance,
	};
}
