using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 管理World中的怪物
 */
public class MonsterManager
{
    //保存着Monster的记录信息
    private Dictionary<Vector3, MonsterData> monsterDatas = new Dictionary<Vector3, MonsterData>();
    //保存着EnermySubject
    private Dictionary<Vector3, EnermySubject> enermys = new Dictionary<Vector3, EnermySubject>();
    private bool hasMonster;

    public MonsterManager()
    {
        hasMonster = false;
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
            enermys[pos].Delete();
            enermys.Remove(pos);
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
        if(enermys.Values.Count != 0)
        {
            foreach (EnermySubject e in enermys.Values)
            {
                e.Delete();
            }
            enermys.Clear();
        }
        
    }

    public void AddEnermySubject(EnermySubject e)
    {
        enermys.Add(e.GetPosition(), e);
    }

    public void DeleteEnermySubject(EnermySubject e)
    {
        enermys.Remove(e.GetPosition());
    }

    private void EnermyFactory(MonsterData data)
    {
        Debug.Log("Enermy generate!" + data.GetPos());
        Monster tmp;
        if (data.GetMonsterType() == MonsterType.Breaker)
        {
            tmp = new Breaker(data.GetPos(), World.GetInstance());
            tmp.GetMonster().AddComponent<MonsterUpdate>();
            tmp.GetMonster().GetComponent<MonsterUpdate>().SetMonster(tmp);
        }
    }

    
}
