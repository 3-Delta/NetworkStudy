using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

public class SyncCommand
{
    public uint sequence; // 操作指令序号
    public byte commandState; // 命令状态
    public SyncEntity entity; // 命令执行对象
    public CommandInput input; // 操作指令输入
    public CommandResult result; // 操作指令执行结果

    public void Exec() { /*获取操作结果*/ }
}