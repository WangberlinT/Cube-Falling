using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PrefabType, 管理Prefab
 */
public enum PrefabType
{
    Stone, Dirt
}
/*
 * World 类
 * 管理世界中的Cube,Player,Enermy
 */
public class World : MonoBehaviour
{
    
    //世界的大小(x=z=worldWidth, y=worldHeight)
    public int worldWidth = 5;
    public int worldHeight = 5;
    public Vector3 spawnPos;

    public GameObject player;

    private Vector2Int worldCenter = Vector2Int.zero;
    public Dictionary<PrefabType, GameObject> prefabs = new Dictionary<PrefabType, GameObject>();
    private Cube[,,] cubes;
    private string TAG = "World";


    //Debug 
    DebugScreen screen;
    void Start()
    {
        screen = DebugScreen.GetInstance();
        Debug.Log("World init");
        LoadPrefab();
        cubes = new Cube[worldWidth, worldHeight, worldWidth];
        transform.position = new Vector3(worldCenter.x,worldCenter.y,0);

        player.transform.position = spawnPos;
        GenerateCubes();
    }

    private void LoadPrefab()
    {
        prefabs.Add(PrefabType.Stone, (GameObject)Resources.Load("Prefabs/Cubes/Stone",typeof(GameObject)));
        screen.Log(TAG, "prefab size" + prefabs.Count);
    }

    private void GenerateCubes()
    {
        for(int x = 0;x < worldWidth;x ++)
        {
            for(int y = 0;y < worldHeight;y++)
            {
                for(int z = 0; z < worldWidth; z++)
                {
                    cubes[x,y,z] = new Stone(IndexToPosition(x,y,z), this);
                }
            }
        }
    }

    private Vector3 IndexToPosition(int x,int y, int z)
    {
        return new Vector3(x - worldWidth / 2 + worldCenter.x, y - worldHeight / 2, z - worldWidth / 2 + worldCenter.y);
    }

    public GameObject GetPrefab(PrefabType type)
    {
        if (prefabs[type] != null)
            return prefabs[type];
        else
            throw new System.Exception("no this prefab");
    }
}
