using System.Collections;
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
        monsterAni = monster.GetComponent<Animator>();
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
        //没有移动时 开始移动
        if(!isMoving&&!isRotating)
        {
            int index = 0;
            do{
                direction = Random.Range(0, 4);
                tar = RandomDirection(direction) + this.GetPosition();
                index++;
            }while (cubeIdx[(int)(tar.x-0.5),(int)(tar.y-1),(int)(tar.z-0.5)]==null&&index < 5);
            //根据随机设置方向index
            //转入旋转等待
            this.SetFace(direction);
            this.SetTime(0);
            this.SetRotating(true);
        }
        else if(isRotating)
        {
            this.RotatePause(15);
        }
        else
        {
            //如果旋转完成
            //Debug.Log(Vector3.Distance(tar, this.GetPosition()));
            if (Vector3.Distance(tar, this.GetPosition()) < 0.02) 
            {
                this.SetMoving(false);
                this.GetAnimator().SetBool("isMoving", false);
            }
            else
            {
                // 执行moving动画
                //移动
                this.GetAnimator().SetBool("isMoving", true);
                monster.transform.localPosition = Vector3.MoveTowards(monster.transform.localPosition, tar, Time.deltaTime * moveSpeed);
                this.SetPosition(monster.transform.localPosition);
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
