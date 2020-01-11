using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

public enum ECommandState
{
    UnExected = 0, // 未执行
    HasExected = 1, // 已执行
    Execting = 2, // 正在执行
}

public enum EOp
{
    None = 0,
    // Player
    Walk,
    Run,
    Fire_Skill_1,
    Fire_Skill_2,
    Fire_Skill_3,

    // Enemy
    // Bullet
}

public class SyncEntity
{
    public bool enable;

    public Vector3 position;
    public Vector3 euler;
    public Vector3 scale;

    public EOp status; // 状态
}
