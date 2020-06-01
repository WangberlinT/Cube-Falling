using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUpdate : MonoBehaviour
{
    public Monster monster=null;
    int index = 0;
    public void SetMonster(Monster mon)
    {
        monster = mon;
    }

    // Update is called once per frame
    void Dead()
    {
        monster.OnDie();
        Destroy(this.monster.GetMonster());
        this.monster = null;
        Destroy(this);
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
                if((index++) == 0)
                    this.monster.DeadAction();                
                this.Invoke("Dead", 0.7f);
            }
        }
    }

}
