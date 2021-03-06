﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breaker : Monster
{

    public World thisWorld;     //World 对象
    // for Debug is public
    private Vector3 tar;        // 目标方向
    private Cube[,,] cubeIdx;   //Cube list
    private int direction;
    // 死亡行为
    // 死后使地下的方块塌陷
    public Breaker(Vector3 pos,World world):base(pos)
    {
        //Debug.Log("world: " + world);
        //随机方向种子
        thisWorld = world;
        Random.InitState(0);
        //初始化各个参数
        cubeIdx = thisWorld.GetCubes();
        moveSpeed = 0.2f;
        traceDepth = 5;
        health = 1;
        fallRange = 0;  //0代表只陷落1个方块
        monster = GameObject.Instantiate(PrefabManager.GetInstance().GetPrefab(PrefabType.Breaker).GetGameObject());
        monsterAni = monster.GetComponent<Animator>();
        monster.transform.position = pos;
        this.OnCreate();

    }

    
    public override void DeadAction()
    {
        int x = RangeInt(monster.transform.localPosition.x);
        int y = RangeInt(monster.transform.localPosition.y);
        int z = RangeInt(monster.transform.localPosition.z);
        Vector3Int tmp = new Vector3Int(x, y-1, z);
        //Debug.Log("tmp");
        //Debug.Log(tmp);
        this.SetFallCenter(tmp);
        //播放死亡动画
        this.GetAnimator().SetBool("isDead", true);
        this.SetDead(true);
    }
    // 获得位置
    // 正常行为
    // 随机向一个方向走一格
    public override void NormalAction()
    {
        //没有移动时 开始移动
        if(!isMoving&&!isRotating)
        {
            int index = 0;
            do{
                direction = Random.Range(0, 4);
                //寻找一个没有怪物的方块,防止碰撞
                if (thisWorld.GetMonsterManager().FindConflict(RandomDirection(direction) + this.GetDestination()))
                    continue;
                tar = RandomDirection(direction) + this.GetDestination();
                index++;
            }while (!canMove(tar)&&index < 5);
            if (index == 5)
                return;
            //根据随机设置方向index
            //转入旋转等待
            this.SetFace(direction);
            this.SetTime(0);
            this.SetRotating(true);
        }
        else if(isRotating)
        {
            this.RotatePause(15);
            this.SetDestination(tar);
        }
        else
        {
            //如果旋转完成
            //Debug.Log(Vector3.Distance(tar, this.GetPosition()));
            //Debug.Log(Vector3.Distance(tar, monster.transform.localPosition));
            if (Vector3.Distance(tar, monster.transform.localPosition) < 0.025) 
            {
                monster.transform.Translate(Vector3.Distance(tar, monster.transform.localPosition) * RandomDirection(direction));
                this.SetMoving(false);
                this.GetAnimator().SetBool("isMoving", false);
            }
            else
            {
                // 执行moving动画
                //移动
                this.GetAnimator().SetBool("isMoving", true);
                monster.transform.localPosition = Vector3.MoveTowards(monster.transform.localPosition, tar, Time.deltaTime * moveSpeed);
            }
        }
        
    }
    // 创建
    public override void OnCreate()
    {
        thisWorld.AddEnermy(this);
        //throw new System.NotImplementedException();
    }
    // 死亡
    public override void OnDie()
    {
        thisWorld.OnEnermyDie(this);       
    }
    // 追踪行为
    public override void TraceAction()
    {
        throw new System.NotImplementedException();
    }

    public bool canMove(Vector3 tar)
    {
        int x = RangeInt(tar.x);
        int y = RangeInt(tar.y);
        int z = RangeInt(tar.z);
        //Debug.Log(x+", "+y+", "+z);

        // 面前是方块时
        if (thisWorld.GetCubeManager().OutOfBound(x, y, z) || thisWorld.GetCubeManager().OutOfBound(x, y - 1, z))
            return false;

        if (cubeIdx[x, y, z] != null)
            return false;
        // 脚下没有方块时的时候
        if (cubeIdx[x, y - 1, z] == null )
            return false;



        return true;
    }
    
    /*
     * Replay 的时候要清空world中的Monster
     */
    public override void Delete()
    {
        Object.Destroy(monster);
    }
}
