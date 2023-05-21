using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/*
 
// https://www.jianshu.com/p/167f0f59b505
public interface IDo<T>
{
    bool PreDo();
    T Do();
    void PostDo();
}

public abstract class CommandInput { }
public abstract class CommandResult { }
public enum ECommandState
{
    UnExected = 0, // 未执行
    HasExected = 1, // 已执行
    Execting = 2, // 正在执行
}
public class OpCommand
{
    public uint sequence; // 操作指令序号
    public byte commandState; // 命令状态
    public CommandInput input; // 操作指令输入
    public CommandResult result; // 操作指令执行结果
}

public class Entity { }
public class SyncEntity
{
    public SyncState currentState;
}
public class PackagePool<T>
{
    public T Get() { return default(T); }
}
public class SyncPackage
{

}
public class SyncProperty { }
public class SyncState
{
    public uint frame;
    public Entity entity;
    public List<SyncProperty> properties;

    public void Encode() { }
    public void Decode() { }
}

// https://www.jianshu.com/p/c1fb23afbabe
public class Sys_Sync : SystemBase<Sys_Sync>
{
    public static bool isServer = true;
    // 本地预表现，比如本地按键按下的时候立即执行， 而不是等待服务器回包之后执行 
    public static bool isLocalPredicte = true;

    private Queue<OpCommand> commands = new Queue<OpCommand>();

    public void OnFixedUpdate()
    {
        PreSimulate();
        if (isServer || isLocalPredicte)
        {
            OpCommand command = new OpCommand();
            command.input = GetCommandInput();
            ExecuteCommand(ref command); // 执行指令
            command.commandState |= (byte)ECommandState.HasExected;
            commands.Enqueue(command); // 已经执行过的指令，需要缓存， 见哈里斯如果是客户端
            // 需要将队列中的command传输给服务器
        }
        PostSimulate();
    }
    private void PreSimulate()
    { }
    private void PostSimulate()
    { }
    public CommandInput GetCommandInput() { return null; }
    public void ExecuteCommand(ref OpCommand command)
    {
        // 得到命令结果
        command.result = null;
    }
}

*/
