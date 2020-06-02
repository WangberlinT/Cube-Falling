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
        //设置其为暂停状态
        EnermyFactory(data,true);
    }
    public void DeleteMonster(Vector3 pos)
    {
        //Debug.Log("Delete: " + pos);
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
            //Debug.Log("Data: " + datas.Count);
            foreach (MonsterData m in datas)
            {
                monsterDatas.Add(m.GetPos(), m);
            }
        }

    }
    public void GenerateEnermys()
    {
        Debug.Log(GameManager.GetGameMode());
        if (hasMonster)
        {
            Debug.Log("monsterDatas size: "+monsterDatas.Values.Count);
            foreach (MonsterData m in monsterDatas.Values)
            {
                if (GameManager.GetGameMode() == GameMode.SinglePlayer)
                    EnermyFactory(m, false);
                else if (GameManager.GetGameMode() == GameMode.EditMode)
                    EnermyFactory(m, true);
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

        List<Vector3> key = new List<Vector3>(monsters.Keys);
        for (int i =0;i<monsters.Count;i++)
        {
            if (e == monsters[key[i]])
                monsters.Remove(key[i]);
        }
            
    }

    private void EnermyFactory(MonsterData data,bool isPause)
    {
        Debug.Log("Enermy generate!" + data.GetPos());
        Monster tmp;
        if (data.GetMonsterType() == MonsterType.Breaker)
        {
            tmp = new Breaker(data.GetPos(), world);
            tmp.GetMonster().AddComponent<MonsterUpdate>();
            tmp.GetMonster().GetComponent<MonsterUpdate>().SetMonster(tmp);
            tmp.SetPaused(isPause);
        }
    }

    //通过pos查看是否有相同pos 的Monster
    public bool FindConflict(Vector3 pos)
    {
        foreach(MonsterSubject s in monsters.Values)
        {
            if(pos.Equals(s.GetDestination()))
            {
                return true;
            }
        }
        return false;
    }

    
}
