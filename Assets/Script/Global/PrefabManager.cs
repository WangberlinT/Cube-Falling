using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PrefabType, 管理Prefab
 */
public enum PrefabType
{
    Stone, Sand,Ice,Mud,
    Breaker
}
public class PrefabManager : MonoBehaviour
{

    //记录world中使用的prefab，方便生成物体(方块)
    public Dictionary<PrefabType, PrefabObject> prefabs = new Dictionary<PrefabType, PrefabObject>();

    private static PrefabManager instance;

    public static PrefabManager GetInstance()
    {
        return instance;
    }
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        LoadPrefab();
    }

    /*
     * 获取方块prefab
     */
    public PrefabObject GetPrefab(PrefabType type)
    {
        if (prefabs[type] != null)
            return prefabs[type];
        else
            throw new System.Exception("no this prefab");
    }

    public PrefabObject GetPrefabByMonsterType(MonsterType type)
    {
        if (type == MonsterType.Breaker)
            return GetPrefab(PrefabType.Breaker);
        else
            throw new System.Exception("no this prefab");
    }

    private void LoadPrefab()
    {
        prefabs[PrefabType.Stone] = new StonePrefab((GameObject)Resources.Load("Prefabs/Cubes/Stone", typeof(GameObject)));
        prefabs[PrefabType.Sand] = new SandPrefab((GameObject)Resources.Load("Prefabs/Cubes/Sand", typeof(GameObject)));
        prefabs[PrefabType.Breaker] = new BreakerPrefab((GameObject)Resources.Load("Prefabs/Enermy/Breaker", typeof(GameObject)));
        prefabs[PrefabType.Ice] = new IcePrefab((GameObject)Resources.Load("Prefabs/Cubes/Ice", typeof(GameObject)));
        prefabs[PrefabType.Mud] = new MudPrefab((GameObject)Resources.Load("Prefabs/Cubes/Mud", typeof(GameObject)));
    }
}
