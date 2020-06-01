using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster: MonsterSubject
{
    protected bool isPaused = false;    // 是否禁用
    protected bool isDead = false;      //是否死亡
    protected bool isMoving = false;   // 正在移动
    protected bool isRotating = false; // 正在旋转
    protected float timestamp;          //旋转时间戳

    protected int faceIndex=0;           //朝向下标 0为向前，1为向右，2为向后，3为向左
    protected float moveSpeed;         // 移动速度
    protected float traceDepth;        // 仇恨范围
    protected int health;              // 生命
    protected int fallRange;           // 陷落范围
    protected Vector3 fallCenter;          // 陷落中心
    protected GameObject monster;       //怪物实例
    protected Animator monsterAni;      // 怪物动画实例
    protected Vector3 destination;         // 怪物行走的目的地
    public Monster(Vector3 pos)
    {
        destination = pos;
    }
    public bool GetPaused()
    {
        return isPaused;
    }
    public void SetPaused(bool pause)
    {
        isPaused = pause;
    }
    public bool GetDead()
    {
        return isDead;
    }
    public void SetDead(bool dead)
    {
        isDead = dead;
    }
    public bool GetMoving()
    {
        return isMoving;
    }
    public void SetMoving(bool moving)
    {
        isMoving = moving;
    }
    public bool GetRotating()
    {
        return isRotating;
    }
    public void SetRotating(bool rotating)
    {
        isRotating = rotating;
    }
    public int GetFace()
    {
        return faceIndex;
    }
    public void SetFace(int dir)
    {
        faceIndex = dir;
    }
    public float GetTime()
    {
        return timestamp;
    }
    public void SetTime(float time)
    {
        timestamp = time;
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
    
    public int GetFallRange()
    {
        return fallRange;
    }
    public void SetFallRange(int r)
    {
        fallRange = r;
    }

    public Vector3 GetFallCenter()
    {
        return fallCenter;
    }
    public void SetFallCenter(Vector3 c)
    {
        fallCenter = c;
    }

    public Vector3 GetPosition()
    {
        return monster.transform.localPosition;
    }

    public GameObject GetMonster()
    {
        return monster;
    }

    public Vector3 GetDestination()
    {
        return destination;
    }
    public void SetDestination(Vector3 pos)
    {
        this.destination = pos;
    }

    public Vector3 GetFallPosition()
    {
        return GetFallCenter();
    }

    public Animator GetAnimator()
    {
        return monsterAni;
    }
    //死亡行为
    public abstract void DeadAction();
    // 追踪行为
    public abstract void TraceAction();
    // 平常行为
    public abstract void NormalAction();
    // 随机方向
    public Vector3 RandomDirection(int i)
    {
        switch (i)
        {
            case 0:
                return new Vector3(0, 0, 1);
                //向前
            case 1:
                return new Vector3(1, 0, 0);
            case 2:
                return new Vector3(0, 0, -1);
            case 3:
                return new Vector3(-1, 0, 0);
            default:
                return new Vector3(0, 0, 0);
        }
    }
    // 旋转等待时间
    public void RotatePause(int waitTime)
    {
        //monster.transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
        if (1-GetTime() < 1/(waitTime+1))
        {
            SetRotating(false);
            SetMoving(true);
            SetTime(0);
        }
        //Debug.Log(GetTime());
        monster.transform.rotation = Quaternion.Euler(new Vector3(0, 90*(GetFace()),0));
        SetTime(GetTime() + (float)1/(waitTime+1));
    }
    public abstract void OnDie();

    public abstract void OnCreate();

    public abstract void Delete();
}
