using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct stTypeID
{
    //public ECharacterType type; // 类型
    public uint id; // id
}

// buff叠加
// buff互斥【不能同时添加】
// buff清除
// buff免疫【对目标无效】
// buff替换【新的bugg替换老的正在生效或者还未生效的buff】

public class CSVBuff
{
    public uint id;
    public float delayTime; // buff施放之后间隔多长时间开始起效
    public float duration;  // buff生效的时长
    public int maxStack; // 同类型buff最大叠加
}

public class Buff
{
    public float startTime; // buff施放时间
    public CSVBuff csv; // buff配置数据
}

public class BuffBase
{
    public stTypeID from; // buff施放者
    public List<Buff> buffs; // 施放者施放的buff
    public List<stTypeID> tos; // 受buff影响者

    public void Add(Buff buff) { }
    public void Add(stTypeID ti) { }
    public void Remove(Buff buff) { }
    public void Remove(stTypeID ti) { }
    public void Replace(Buff old, Buff newBuff) { }
    public void Add(stTypeID old, stTypeID newTi) { }
    public void Update() { }
}
