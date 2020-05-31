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

    // Update is called once per frame
    void Dead()
    {
        monster.OnDie();
        Destroy(this.monster.GetMonster());
    }
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
                
                this.Invoke("Dead", 0.7f);
            }
        }
    }

}
