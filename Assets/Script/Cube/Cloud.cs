using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Cube
{
    CubeActor actor;
    public Cloud(Vector3 pos, World world) : base(pos)
    {
        this.world = world;
        cube = GameObject.Instantiate(PrefabManager.GetInstance().GetPrefab(PrefabType.Cloud).GetGameObject());
        cube.transform.position = pos;
        cube.AddComponent<CubeActor>();
        actor = cube.GetComponent<CubeActor>();
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
        Disappear();
    }

    public override void Disappear()
    {
        if (cube != null)
        {
            //TODO: 播放动画
            actor.DestoryCube();
        }
    }
}
