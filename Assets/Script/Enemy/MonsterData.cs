using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData 
{
    private float[] position = new float[3];
    private MonsterType type;

    public MonsterData(Monster m)
    {
        type = MonsterToMonsterType(m);
        Vector3 pos = m.GetPosition();
        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;
    }

    public MonsterData(Vector3 pos, MonsterType type)
    {
        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;
        this.type = type;
    }

    public MonsterType GetMonsterType()
    {
        return type;
    }

    public static MonsterType MonsterToMonsterType(Monster m)
    {
        if (m is Breaker)
            return MonsterType.Breaker;
        else
            throw new System.Exception("No such monster type");
    }

    public Vector3 GetPos()
    {
        return new Vector3(position[0], position[1], position[2]);
    }
}

public enum MonsterType
{
    Breaker,
    //Record number of Monster
    LENGTH
}
