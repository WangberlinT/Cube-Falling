using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PrefabType, 管理Prefab
 */
public enum PrefabType
{
    Stone, Sand
}
/*
 * World 类
 * 管理世界中的Cube,Player,Enermy
 * 
 */
public class World : MonoBehaviour, WorldObserver
{
    
    //世界的大小(x=z=worldWidth, y=worldHeight)
    public int worldWidth = 5;
    public int worldHeight = 5;
    public Vector3 spawnPos;
    //玩家出生点(单人游戏中保证唯一性)
    public GameObject player;

    private Vector3 worldCenter;
    //记录world中使用的prefab，方便生成物体(方块)
    public Dictionary<PrefabType, GameObject> prefabs = new Dictionary<PrefabType, GameObject>();
    //记录世界中每个点的cube对象
    private Cube[,,] cubes;
    //从存档导入的cube data，如果为空则未导入任何地图
    private CubeData[,,] loadData;
    //DEBUG 标签
    private const string TAG = "World";

    //敌人记录
    private List<EnermySubject> enermyList = new List<EnermySubject>();


    //Debug 
    DebugScreen screen;
    void Start()
    {
        WorldGenerate();   
    }

    private void Update()
    {
        if(player != null)
            screen.Log("Player Position", "at " + player.transform.position);
        screen.Log("Player", "spawnPos = " + spawnPos);
    }

    private void WorldGenerate()
    {
        LoadSetting();
        cubes = new Cube[worldWidth, worldHeight, worldWidth];
        if(player != null)
        {
            player.SetActive(false);
            player.transform.position = spawnPos;
            player.SetActive(true);
        }
            
        GenerateCubes();
    }

    public Cube[,,] GetCubes()
    {
        return cubes;
    }

    public void SetCube(Vector3 pos, CubeType type)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;
        int z = (int)pos.z;
        if (x < 0 || x >= worldWidth || y < 0 || y >= worldHeight || z < 0 || z >= worldWidth)
        {
            screen.Log("TAG", "out of world size");
            return;
        }
            

        if (type == CubeType.Stone)
            cubes[x,y,z] = new Stone(pos, this);
        else if (type == CubeType.Sand)
            cubes[x,y,z] = new Sand(pos, this);
        else
            screen.Log(TAG, "SetCube inexsist!");
    }

    private bool OutOfBound(int x, int y, int z)
    {
        return x < 0 || x >= worldWidth || y < 0 || y >= worldHeight || z < 0 || z >= worldWidth;
    }

    public void BreakBlock(Vector3 pos)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;
        int z = (int)pos.z;
        if (OutOfBound(x,y,z))
        {
            screen.Log("TAG", "out of world size");
            return;
        }
        if (cubes[x,y,z] != null)
        {
            cubes[x, y, z].Disappear();
            cubes[x, y, z] = null;
        }
    }

    public void SaveWorld()
    {
        SaveSystem.SaveWorld(this);
    }
    /*
     * 加载存档
     * @param name
     * 存档名称
     */
    public void LoadWorld(string name)
    {
        //清空现有属性: Cubes, TODO: Player, Monster...
        DestroyCubes();
        //导入存档
        WorldData data = SaveSystem.LoadWorld(name);
        worldWidth = data.worldWidth;
        worldHeight = data.worldHeight;
        spawnPos = data.GetSpawnPos();
        loadData = data.cubeDatas;
        //重置world
        WorldGenerate();
    }

    private void DestroyCubes()
    {
        for (int x = 0; x < worldWidth; x++)
        {
            for (int y = 0; y < worldHeight; y++)
            {
                for (int z = 0; z < worldWidth; z++)
                {
                    if(cubes[x,y,z] != null)
                        Destroy(cubes[x, y, z].GetCubeObject());
                }
            }
        }
    }

    private void LoadSetting()
    {
        screen = DebugScreen.GetInstance();
        worldCenter = new Vector3(worldWidth / 2, 0 , worldWidth / 2);
        if(loadData == null)
            spawnPos = worldCenter;
        LoadPrefab();
    }

    private void LoadPrefab()
    {
        prefabs[PrefabType.Stone] = (GameObject)Resources.Load("Prefabs/Cubes/Stone",typeof(GameObject));
        prefabs[PrefabType.Sand] = (GameObject)Resources.Load("Prefabs/Cubes/Sand", typeof(GameObject));
        screen.Log(TAG, "prefab size " + prefabs.Count);
    }

    private void GenerateCubes()
    {
        if (loadData == null)
        {
            cubes[(int)worldCenter.x, 0, (int)worldCenter.z] = new Stone(worldCenter, this);
            return;
        }
            
        for (int x = 0;x < worldWidth;x ++)
        {
            for(int y = 0;y < worldHeight;y++)
            {
                for(int z = 0; z < worldWidth; z++)
                {
                    if(loadData[x,y,z] != null)
                    {
                        if (loadData[x, y, z].cubeType == CubeType.Stone)
                            cubes[x, y, z] = new Stone(new Vector3(x, y, z), this);
                        else if (loadData[x, y, z].cubeType == CubeType.Sand)
                            cubes[x, y, z] = new Sand(new Vector3(x, y, z), this);
                    }
                }
            }
        }
    }
    /*
     * 获取方块prefab
     */
    public GameObject GetPrefab(PrefabType type)
    {
        if (prefabs[type] != null)
            return prefabs[type];
        else
            throw new System.Exception("no this prefab");
    }

    private IEnumerator FallTimer(float time,Cube temp)
    {
        screen.Log("[Timer]", "delay: " + time);
        yield return new WaitForSeconds(time);
        temp.FallDown();
    }

    /*
     * 使世界中某个位置产生一个坠落影响，作用于上下左右的方块
     * isDie = true 是怪物死亡产生的坠落效果
     * isDie = false 是方块连锁产生的效果
     */
    public void FallAround(Vector3 diePos,bool isDie,float time = 0)
    {
        Vector3Int[] offset =  { new Vector3Int( 0, 1, 0 ), new Vector3Int( 0, -1, 0 ),
                                 new Vector3Int( -1, 0, 0 ), new Vector3Int( 1, 0, 0 ),
                                 new Vector3Int( 0, 0, 1 ), new Vector3Int( 0, 0, -1 )};

        int x = Mathf.FloorToInt(diePos.x);
        int y = Mathf.FloorToInt(diePos.y);
        int z = Mathf.FloorToInt(diePos.z);

        for (int i = 0; i < offset.Length; i++)
        {
            Vector3Int pos = new Vector3Int(x, y, z);
            pos += offset[i];
            if (!OutOfBound(pos.x, pos.y, pos.z))
            {
                Cube temp = cubes[pos.x, pos.y, pos.z];
                if (temp != null)
                {
                    if (isDie)
                    {
                        temp.FallDown();
                    }
                    else
                    {
                        if (temp.GetChainable())
                        {
                            if(time != 0)
                                StartCoroutine(FallTimer(time,temp));
                        }
                    }
                }
                
            }
        }
    }

    public void OnEnermyDie(EnermySubject enermy)
    {
        Vector3 diePos = enermy.GetPosition();
        FallAround(diePos,true);
    }

    public void OnPlayerDie()
    {
        //TODO: Player 死亡后的行为
    }

    public void AddPlayer()
    {
        //将Player加入观察列表
    }

    public void AddEnermy(EnermySubject e)
    {
        enermyList.Add(e);
    }
}
