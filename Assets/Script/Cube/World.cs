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
public class World : MonoBehaviour
{
    
    //世界的大小(x=z=worldWidth, y=worldHeight)
    public int worldWidth = 5;
    public int worldHeight = 5;
    public Vector3 spawnPos;

    public GameObject player;

    private Vector2Int worldCenter;
    public Dictionary<PrefabType, GameObject> prefabs = new Dictionary<PrefabType, GameObject>();
    //记录世界中每个点的cube
    private Cube[,,] cubes;
    private CubeData[,,] loadData;
    private const string TAG = "World";


    //Debug 
    DebugScreen screen;
    void Start()
    {
        WorldGenerate();   
    }

    private void Update()
    {
        screen.Log("Player Position", "at " + player.transform.position);
    }

    private void WorldGenerate()
    {
        LoadSetting();
        cubes = new Cube[worldWidth, worldHeight, worldWidth];
        screen.Log("Player", "spawnPos = " + spawnPos);
        player.transform.position = spawnPos;//在读档的时候player 位置没有改变
        GenerateCubes();
    }

    public Cube[,,] GetCubes()
    {
        return cubes;
    }

    public void SaveWorld()
    {
        SaveSystem.SaveWorld(this);
    }

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
                    Destroy(cubes[x, y, z].GetCubeObject());
                }
            }
        }
    }

    private void LoadSetting()
    {
        screen = DebugScreen.GetInstance();
        worldCenter = new Vector2Int(worldWidth / 2, worldWidth / 2);
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
        for(int x = 0;x < worldWidth;x ++)
        {
            for(int y = 0;y < worldHeight;y++)
            {
                for(int z = 0; z < worldWidth; z++)
                {
                    if (loadData == null)
                        cubes[x, y, z] = new Stone(new Vector3(x, y, z), this);
                    else if (loadData[x, y, z].cubeType == CubeType.Stone)
                        cubes[x, y, z] = new Stone(new Vector3(x, y, z), this);
                    else if (loadData[x, y, z].cubeType == CubeType.Sand)
                        cubes[x, y, z] = new Sand(new Vector3(x, y, z), this);
                }
            }
        }
    }

    public GameObject GetPrefab(PrefabType type)
    {
        if (prefabs[type] != null)
            return prefabs[type];
        else
            throw new System.Exception("no this prefab");
    }
}
