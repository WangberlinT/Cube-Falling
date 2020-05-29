using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster: EnermySubject
{
    protected float moveSpeed = 0.01f;    // 移动速度
    protected float traceDepth = 5;    // 仇恨范围
    protected int health = 1;          // 生命
    protected GameObject monster;       //怪物实例
    protected Vector3 position;
    public Monster(Vector3 pos)
    {
        position = pos;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetTraceDepth()
    {
        return traceDepth;
    }
    public int GetHealth()
    {
        return health;
    }
    public void SetHealth(int h)
    {
        health = h;
    }
    public GameObject GetMonster()
    {
        return monster;
    }
    public Vector3 GetPosition()
    {
        return position;
    }
    public void SetPosition(Vector3 pos)
    {
        this.position = pos;
    }
    //死亡行为
    public abstract void DeadAction();
    // 追踪行为
    public abstract void TraceAction();
    // 平常行为
    public abstract void NormalAction();
    public Vector3 RandomDirection(int i)
    {
        switch (i)
        {
            case 0:
                return new Vector3(1, 0, 0);
            case 1:
                return new Vector3(-1, 0, 0);
            case 2:
                return new Vector3(0, 0, 1);
            case 3:
                return new Vector3(0, 0, -1);
            default:
                return new Vector3(0, 0, 0);
        }
    }

    public abstract void OnDie();

    public abstract void OnCreate();

    public abstract void Delete();
}
