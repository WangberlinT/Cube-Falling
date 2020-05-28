using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : Cube
{
    private Rigidbody rigidbody;
    private CubeActor actor;
    public Sand(Vector3 pos, World world) : base(pos)
    {
        chainAble = true;
        this.world = world;
        cube = GameObject.Instantiate(PrefabManager.GetInstance().GetPrefab(PrefabType.Sand));
        cube.transform.position = pos;
        rigidbody = cube.GetComponent<Rigidbody>();
        cube.AddComponent<CubeActor>();
        actor = cube.GetComponent<CubeActor>();
    }

    public override void Disappear()
    {
        //TODO: 播放动画
        actor.DestoryCube();
    }

    public override void FallDown()
    {
        if (!isFalling)
        {
            isFalling = true;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            world.FallAround(initPos,false,delay);
            actor.CheckToDestory();
        }
    }

}
