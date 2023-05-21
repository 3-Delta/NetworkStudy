using System.Collections.Generic;

public static class BS_SystemRegistry
{
	public readonly static List<ISystemBaseCallback> registry = new List<ISystemBaseCallback>()
	{
		Sys_Account.Instance,
		Sys_Login.Instance,
		Sys_ServerList.Instance,
		
		Sys_User.Instance,
        Sys_Mail.Instance,
	};
}
