using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breaker : Monster
{

    public World thisWorld;     //World 对象
    // for Debug is public
    private bool isMoving =false;   // 正在移动
    private Vector3 tar;        // 目标方向
    private Cube[,,] cubeIdx;   //Cube list
    // 死亡行为
    // 死后使地下的方块塌陷
    public Breaker(Vector3 pos,World world):base(pos)
    {
        Debug.Log("world: " + world);
        //随机方向种子
        thisWorld = world;
        Random.InitState(0);
        //初始化各个参数
        cubeIdx = thisWorld.GetCubes();
        moveSpeed = 0.2f;
        traceDepth = 5;
        health = 1;
        monster = GameObject.Instantiate(PrefabManager.GetInstance().GetPrefab(PrefabType.Breaker));
        monster.transform.position = pos;
        this.OnCreate();
    }

    
    public override void DeadAction()
    {
        //播放死亡动画
        throw new System.NotImplementedException();
    }
    // 获得位置
    // 正常行为
    // 随机向一个方向走一格
    public override void NormalAction()
    {
        
        if(!isMoving)
        {
            isMoving = true;
            do{
            tar = RandomDirection(Random.Range(0, 4)) + this.GetPosition();
            }while (cubeIdx[(int)tar.x,(int)tar.y,(int)tar.z]==null);
            Debug.Log(this.GetPosition());
            //this.SetPosition( Vector3.MoveTowards(this.GetPosition(), tar, Time.deltaTime * moveSpeed));
            monster.transform.localPosition = Vector3.MoveTowards(monster.transform.localPosition, tar, Time.deltaTime * moveSpeed);
        }
        else
        {
            
            if (tar == this.GetPosition())
            {
                isMoving = false;
            }else
            {
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
        this.DeadAction();
        thisWorld.OnEnermyDie(this);
        throw new System.NotImplementedException();
    }
    // 追踪行为
    public override void TraceAction()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // this.Invoke("NormalAction", 1000);
        if(health>0)
        {
            // 仇恨行为
            Debug.Log("health existed!");
            //正常行为
            this.NormalAction();
        }else
        {
            // 死亡行为
            OnDie();
        }

    }

    
    /*
     * Replay 的时候要清空world中的Monster
     */
    public override void Delete()
    {
        Object.Destroy(monster);
    }
}
