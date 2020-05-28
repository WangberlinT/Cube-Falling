using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breaker : Monster, EnermySubject
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
        //随机方向种子
        thisWorld = world;
        Random.InitState(0);
        //初始化各个参数
        cubeIdx = thisWorld.GetCubes();
        thisWorld = GameObject.FindObjectOfType<World>();
        moveSpeed = 0.2f;
        traceDepth = 5;
        health = 1;
        monster = GameObject.Instantiate(PrefabManager.GetInstance().GetPrefab(PrefabType.Breaker));
        monster.transform.position = pos;
        this.OnCreate();
    }

    
    public override void DeadAction()
    {
        OnDie();
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
    public void OnCreate()
    {
        thisWorld.AddEnermy(this);
        //throw new System.NotImplementedException();
    }
    // 死亡
    public void OnDie()
    {
        thisWorld.OnEnermyDie(this);
        throw new System.NotImplementedException();
    }
    // 追踪行为
    public override void TraceAction()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //设定一个种子
        
        /*
        GameObject[] cube;
        cube = GameObject.FindGameObjectsWithTag("Cube");
        foreach(GameObject c in cube)
        {
            Debug.Log(c.transform.position);
        }
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // this.Invoke("NormalAction", 1000);
        this.NormalAction();
    }

    private Vector3 RandomDirection(int i)
    {
        switch(i)
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
    /*
     * Replay 的时候要清空world中的Monster
     */
    public void Delete()
    {
        Object.Destroy(monster);
    }
}
