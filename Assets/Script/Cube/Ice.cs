using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Cube
{
    private Rigidbody rigidbody;
    private CubeActor actor;
    public Ice(Vector3 pos, World world):base(pos)
    {
        this.world = world;
        cube = GameObject.Instantiate(PrefabManager.GetInstance().GetPrefab(PrefabType.Ice));
        cube.transform.position = pos;
        rigidbody = cube.GetComponent<Rigidbody>();
        cube.AddComponent<CubeActor>();
        actor = cube.GetComponent<CubeActor>();
    }

    public override void DelayToFall()
    {
        actor.DelayToFall(delay, this);
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
            world.FallAround(initPos, false, delay);
            actor.CheckToDestory();
            world.DecreaseCubeCount();
            world.CheckWin();
        }

    }
}
