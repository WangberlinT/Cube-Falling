using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : Cube
{
    private Rigidbody rigidbody;
    public Sand(Vector3 pos, World world) : base(pos)
    {
        chainAble = true;
        this.world = world;
        cube = GameObject.Instantiate(world.GetPrefab(PrefabType.Sand));
        cube.transform.position = pos;
        rigidbody = cube.GetComponent<Rigidbody>();
    }

    public override void Disappear()
    {
        //TODO: 播放动画
        Object.Destroy(cube);
    }

    public override void FallDown()
    {
        if (!isFalling)
        {
            isFalling = true;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            world.FallAround(initPos,false,delay);
        }
    }

}
