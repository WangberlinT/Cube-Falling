using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Cube
{
    private World world;
    public Stone(Vector3 pos, World world):base(pos)
    {
        this.world = world;
        cube = GameObject.Instantiate(world.GetPrefab(PrefabType.Stone));
        cube.transform.position = pos;
    }
}
