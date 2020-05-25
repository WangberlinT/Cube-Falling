using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Cube
{
    private Rigidbody rigidbody;
    public Stone(Vector3 pos, World world):base(pos)
    {
        this.world = world;
        cube = GameObject.Instantiate(world.GetPrefab(PrefabType.Stone));
        cube.transform.position = pos;
        rigidbody = cube.GetComponent<Rigidbody>();
    }

    public override void FallDown()
    {
        if(!isFalling)
        {
            isFalling = true;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            world.FallAround(initPos,false,delay);
        }
    }
}
