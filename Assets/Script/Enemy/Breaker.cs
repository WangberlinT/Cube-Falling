using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Breaker : MonoBehaviour, Enermy, EnermySubject
{
    public World thisWorld;
    public float moveSpeed = 10;    // 移动速度
    public float traceDepth = 5;    // 仇恨范围
    public int health = 1;          // 生命
    // 死亡行为
    // 死后使地下的方块塌陷
    public void DeadAction()
    {
        OnDie();
        //播放死亡动画
        Destroy(this);
        throw new System.NotImplementedException();
    }
    // 获得位置
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
    // 正常行为
    // 随机向一个方向走一格
    public void NormalAction()
    {
        // Cube[,,] tmp= thisWorld.GetCubes();
        GameObject[] cube;
        cube = GameObject.FindGameObjectsWithTag("Cube");
        Vector3 next;
        int index = 0;
        //do{
            next = transform.position + RandomDirection(Random.Range(0, 4));
            Debug.Log("finding Next...");
            Debug.Log(next);
            index++;
        //}while (next ==&&index<50);

        gameObject.GetComponent<Rigidbody>().MovePosition(next);
    }
    // 创建
    public void OnCreate()
    {
        thisWorld.AddEnermy(this);
        throw new System.NotImplementedException();
    }
    // 死亡
    public void OnDie()
    {
        thisWorld.OnEnermyDie(this);
        throw new System.NotImplementedException();
    }
    // 追踪行为
    public void TraceAction()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //设定一个种子
        Random.InitState(0);
        this.NormalAction();
        /*
        GameObject[] cube;
        cube = GameObject.FindGameObjectsWithTag("Cube");
        foreach(GameObject c in cube)
        {
            Debug.Log(c.transform.position);
        }
        */
        Debug.Log(this.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.Invoke("NormalAction", 1000);
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
}
