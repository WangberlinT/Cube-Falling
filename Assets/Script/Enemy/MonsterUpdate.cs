using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUpdate : MonoBehaviour
{
    public Monster monster=null;
    public MonsterUpdate(Monster mon)
    {
        monster = mon;
    }
    public void SetMonster(Monster mon)
    {
        monster = mon;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("do fixupdate.");
        if(monster!=null)
        {
            if (monster.GetHealth() > 0)
            {
                // 仇恨行为
                Debug.Log("health existed!");
                //正常行为
                monster.NormalAction();
            }
            else
            {
                // 死亡行为
                monster.OnDie();
            }
        }
    }
}
