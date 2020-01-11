using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Sys_SyncEnemy : Sys_Sync
{
    // 数据层
    public Dictionary<uint, SyncEntityEnemy> enemies = new Dictionary<uint, SyncEntityEnemy>();
    // 控制层
    public Dictionary<uint, SyncEntityEnemyMono> enemyMonos = new Dictionary<uint, SyncEntityEnemyMono>();

    public void Add(uint id, SyncEntityEnemy enemy) { }
    public void Remove(uint id) { }
}