using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 所有方块的父类
 */
public class Cube
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

    
    public Cube(Vector3 pos)
    {
        initPos = pos;
    }

    public Vector3 GetInitPosition()
    {
        return initPos;
    }

    /*
     * 当前方块触发坠落，子类重写此方法
     * 云方块的FallDown是破碎消失
     */
    public void FallDown()
    {
        //Empty
    }
    /*
     * 当前方块触发周边方块坠落，子类重写此方法
     */
    protected void FallAround()
    {
        //Empty
    }
}
