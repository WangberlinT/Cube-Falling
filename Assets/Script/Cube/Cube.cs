using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 所有方块的父类
 */
public abstract class Cube
{
    //是否处于陷落状态，默认不是
    protected bool isFalling = false;
    //是否可以触发连锁，默认不可以
    protected bool chainAble = false;
    //站立时间，默认无限
    protected int standTime = int.MaxValue;
    //坠落延迟：从旁边的方块触发其坠落到其真正发生坠落的时间，默认1s
    protected float delay = 1f;
    //生成的方块
    protected GameObject cube;
    //初始位置，所有方块的初始位置和属性在World类中记录
    protected Vector3 initPos;
    //Cube 所在世界
    protected World world;

    
    public Cube(Vector3 pos)
    {
        initPos = pos;
    }

    public GameObject GetCubeObject()
    {
        return cube;
    }

    public Vector3 GetInitPosition()
    {
        return initPos;
    }

    public bool GetIsFalling()
    {
        return isFalling;
    }

    public bool GetChainable()
    {
        return chainAble;
    }

    public int GetStandTime()
    {
        return standTime;
    }

    public float GetDelay()
    {
        return delay;
    }
    /*
     * 当前方块触发坠落，子类重写此方法
     * 云方块的FallDown是破碎消失
     * 除了云方块，其他方块在世界中触发FallAround事件
     */
    public abstract void FallDown();
    /*
     * 销毁方块的接口
     */
    public abstract void Disappear();
}
