using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 管理World中的怪物
 */
public class MonsterManager
{
    private World world;
    //保存着Monster的记录信息
    private Dictionary<Vector3, MonsterData> monsterDatas = new Dictionary<Vector3, MonsterData>();
    //保存着EnermySubject
    private Dictionary<Vector3, MonsterSubject> monsters = new Dictionary<Vector3, MonsterSubject>();
    private bool hasMonster;

    public MonsterManager(World world)
    {
        hasMonster = false;
        this.world = world;
    }

    /*
     * 添加一个Monster的实体和记录
     */
    public void AddMonster(MonsterData data)
    {
        //TODO: 处理异常
        monsterDatas.Add(data.GetPos(), data);
        EnermyFactory(data);
    }
    public void DeleteMonster(Vector3 pos)
    {
        Debug.Log("Delete: " + pos);
        if (monsterDatas.ContainsKey(pos))
        {
            MonsterData m = monsterDatas[pos];
            monsterDatas.Remove(pos);
            monsters[pos].Delete();
            monsters.Remove(pos);
        }
        
    }

    public List<MonsterData> GetMonsterDatas()
    {
        List<MonsterData> list = new List<MonsterData>();
        list.AddRange(monsterDatas.Values);
        return list;
    }

    public void LoadMonsterDatas(List<MonsterData> datas)
    {
        
        if (datas != null && datas.Count != 0)
        {
            hasMonster = true;
            Debug.Log("Data: " + datas.Count);
            foreach (MonsterData m in datas)
            {
                monsterDatas.Add(m.GetPos(), m);
            }
        }

    }
    public void GenerateEnermys()
    {
        if (hasMonster)
        {
            Debug.Log("monsterDatas size: "+monsterDatas.Values.Count);
            foreach (MonsterData m in monsterDatas.Values)
            {
                EnermyFactory(m);
            }
        }
    }

    public void DestroyMonsters()
    {
        if(monsters.Values.Count != 0)
        {
            foreach (MonsterSubject e in monsters.Values)
            {
                e.Delete();
            }
            monsters.Clear();
        }
        
    }

    public void AddEnermySubject(MonsterSubject e)
    {
        monsters.Add(e.GetPosition(), e);
    }

    public void DeleteMonsterSubject(MonsterSubject e)
    {
        foreach(KeyValuePair<Vector3,MonsterSubject> s in monsters)
        {
            if(e == s.Value)
                monsters.Remove(s.Key);
        }
            
    }

    private void EnermyFactory(MonsterData data)
    {
        Debug.Log("Enermy generate!" + data.GetPos());
        Monster tmp;
        if (data.GetMonsterType() == MonsterType.Breaker)
        {
            tmp = new Breaker(data.GetPos(), world);
            tmp.GetMonster().AddComponent<MonsterUpdate>();
            tmp.GetMonster().GetComponent<MonsterUpdate>().SetMonster(tmp);
        }
    }

    //通过pos查看是否有相同pos 的Monster
    public bool FindConflict(Vector3 pos)
    {
        foreach(MonsterSubject s in monsters.Values)
        {
            if(pos.Equals(s.GetPosition()))
            {
                return true;
            }
        }
        return false;
    }

    
}
