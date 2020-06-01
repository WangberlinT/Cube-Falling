using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUpdate : MonoBehaviour
{
    private Monster monster=null;
    public void SetMonster(Monster mon)
    {
        monster = mon;
    }
    public Monster GetMonster()
    {
        return monster;
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
                //延时

                    this.monster.DeadAction();
                    monster.OnDie();
                    this.monster = null;
                    Destroy(gameObject,0.5f);

                //this.Invoke("Dead", 0.7f);
            }
        }
    }

}
