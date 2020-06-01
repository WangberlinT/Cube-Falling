using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerBehaviour : MonoBehaviour, MonsterSubject
{
    WorldObserver world;
    private bool die = false;
    private void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
        world.AddEnermy(this);
    }
    private void Update()
    {
        GetUserInput();
        if (die)
            OnDie();
    }

    private void GetUserInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
            die = true;
        if (Input.GetKeyDown(KeyCode.R))
            die = false;
    }
    public void OnCreate()
    {
        
    }

    public void OnDie()
    {
        world.OnEnermyDie(this);
    }

    public Vector3 GetFallPosition()
    {
        return transform.position;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public int GetFallRange()
    {
        return 0;
    }
    public void Delete()
    {
        //Donothing
    }
}
