using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Cube
{
    CubeActor actor;
    float fallDownSpeed = 1f;
    float breakDelay = 5f;

    public Cloud(Vector3 pos, World world) : base(pos)
    {
        this.world = world;
        cube = GameObject.Instantiate(PrefabManager.GetInstance().GetPrefab(PrefabType.Cloud).GetGameObject());
        cube.transform.position = pos;
        cube.AddComponent<CubeActor>();
        actor = cube.GetComponent<CubeActor>();
        standTime = 2;
        chainAble = true;
    }

    /*
     * Delay to disappear
     */
    public override void DelayToFall()
    {
        if (!isFalling)
            actor.DelayToFall(delay, this);
    }

    //云的陷落是消失
    public override void FallDown()
    {
        actor.UniformDecline(fallDownSpeed, new Vector3(0, -1, 0));
        actor.DelayToDestory(breakDelay);
    }

    public override void Disappear()
    {
        if (cube != null)
        {
            //TODO: 播放动画
            actor.DestoryCube();
        }
    }

    public void OnTread()
    {
        actor.DelayToFall(standTime,this);
    }
}
