using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : Cube
{
    public Sand(Vector3 pos, World world) : base(pos)
    {
        this.world = world;
        cube = GameObject.Instantiate(world.GetPrefab(PrefabType.Sand));
        cube.transform.position = pos;
    }
}
