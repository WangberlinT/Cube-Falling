using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUpdate : MonoBehaviour
{
    public Monster monster=null;
    
    public void SetMonster(Monster mon)
    {
        monster = mon;
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("do fixupdate.");
        
        if (monster!=null)
        {
            if (monster.GetPaused())
                return;
            if (monster.GetHealth() > 0)
            {
                // 仇恨行为
                //Debug.Log("health existed!");
                //正常行为
                monster.NormalAction();
            }
            else
            {
                monster.OnDie();
            }
        }
    }

}
