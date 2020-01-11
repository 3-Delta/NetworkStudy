using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

public abstract class CommandInput
{
    public EOp op; // 操作指令
    public List<object> opArgs; // 操作指令相关参数
    public float time; // 命令输入时间【服务器使用】
}
