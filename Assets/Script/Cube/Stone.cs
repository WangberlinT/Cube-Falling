using System.Collections;
using UnityEngine;

/*
 * Stone 岩石
 * 不发生连锁陷落
 */
public class Stone : Cube
{
    private Rigidbody rigidbody;
    private CubeActor actor;
    public Stone(Vector3 pos, World world):base(pos)
    {
        this.world = world;
        cube = GameObject.Instantiate(PrefabManager.GetInstance().GetPrefab(PrefabType.Stone));
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
        if(!isFalling)
        {
            isFalling = true;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            world.FallAround(initPos,false,delay);
            actor.CheckToDestory();
        }

    }
}
