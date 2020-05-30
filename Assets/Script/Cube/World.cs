using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * World 类
 * 管理世界中的Cube,Player,Enermy
 * 处理Cube, Player, Enermy的交互
 * 唯一存在
 */
public class World : MonoBehaviour, WorldObserver
{
    
    //世界的大小(x=z=worldWidth, y=worldHeight)
    public int worldWidth = 5;
    public int worldHeight = 5;
    public Vector3 spawnPos;
    //玩家出生点(单人游戏中保证唯一性)
    public GameObject player;
    //记录管理Cube
    private CubeManager cubeManager;
    //DEBUG 标签
    private const string TAG = "World";
    //文件名
    private string name = "";
    //敌人记录
    private MonsterManager monsterManager;

    //单例
    private static World instance;

    //获取world单例
    public static World GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;
        }   
    }

    private void WorldGenerate()
    {
        if(player != null)
        {
            player.SetActive(false);
            player.transform.position = spawnPos;
            player.SetActive(true);
        }
            
        cubeManager.GenerateCubes();
        monsterManager.GenerateEnermys();
    }

    /*
     * 保存世界状态信息
     */
    public void SaveWorld()
    {
        Debug.Log("save: " + name);
        SaveSystem.SaveWorld(this, name);
    }
    /*
     * GameManager调用加载存档，唯一入口
     * @param name
     * 存档名称
     */
    public void LoadWorld(string name)
    {
        //设置world 名称
        this.name = name;
        //导入存档
        WorldData data = SaveSystem.LoadWorld(name);
        InitSetting(data);
        WorldGenerate();
    }
    private void InitSetting(WorldData data)
    {  
        worldWidth = data.worldWidth;
        worldHeight = data.worldHeight;
        spawnPos = data.GetSpawnPos();
        cubeManager = new CubeManager(this);
        monsterManager = new MonsterManager(this);
        cubeManager.LoadCubeDatas(data.cubeDatas);
        monsterManager.LoadMonsterDatas(data.monsters);
    }

    /*
     * 获取CubeManager
     */
    public CubeManager GetCubeManager()
    {
        return cubeManager;
    }

    /*
     * 获取MosterManager
     */
    public MonsterManager GetMonsterManager()
    {
        return monsterManager;
    }

    /*
     * 重启游戏
     */
    public void Replay()
    {
        //重置CubeManager状态
        //cubeCount = 0;
        monsterManager.DestroyMonsters();
        //DestroyCubes();
        cubeManager.ResetCubes();
        WorldGenerate();
    }

    /*
     * Finished
     * 获取cubes
     */
    public Cube[,,] GetCubes()
    {
        //重构
        //return cubes;
        return cubeManager.GetCubes();
    }
    /*
     * Finished
     * 对pos设置类型为type的方块
     */
    public void SetCube(Vector3 pos, CubeType type)
    {
        cubeManager.SetCube(pos, type);
    }

    /*
     * Finished
     * 破坏方块
     */
    public void BreakBlock(Vector3 pos)
    {
        cubeManager.BreakBlock(pos);
    }


    /*
     * FInished
     * 使世界中某个位置产生一个坠落影响，作用于上下左右的方块
     * isDie = true 是怪物死亡产生的坠落效果
     * isDie = false 是方块连锁产生的效果
     */
    public void FallAround(Vector3 diePos, bool isDie, float time = 0)
    {
        cubeManager.FallAround(diePos, isDie, time);
    }

    /*
     * 获取世界中怪物信息
     */
    public List<MonsterData> GetMonsterDatas()
    {
        return monsterManager.GetMonsterDatas();
    }

    //添加MonsterData
    public void AddMonsterData(MonsterData monster)
    {
        monsterManager.AddMonster(monster);
    }
    /*
     * 删除指定初始位置的怪物
     */
    public void DeleteMonster(Vector3 pos)
    {
        monsterManager.DeleteMonster(pos);
    }

    /*
     * 怪物死亡时触发死亡位置的陷落事件
     */
    public void OnEnermyDie(EnermySubject enermy)
    {
        Vector3 diePos = enermy.GetPosition();
        FallAround(diePos,true);
    }
    /*
     * 玩家死亡时，打开游戏结束提示框
     */
    public void OnPlayerDie()
    {
        //TODO: Player 死亡后的行为
    }
    /*
     * 玩家加入游戏，添加至观察列表
     */
    public void AddPlayer()
    {
        //将Player加入观察列表
    }
    /*
     * Enermy加入world，添加至观察列表
     */
    public void AddEnermy(EnermySubject e)
    {
        monsterManager.AddEnermySubject(e);
    }
}
