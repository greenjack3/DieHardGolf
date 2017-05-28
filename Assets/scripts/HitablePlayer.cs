using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitablePlayer : Hitable {

    public float hp;
    public int teamNo;

    public override void HitMe(float dmg, int team)
    {
        if(team != teamNo)
        {
            hp -= dmg;
        }

        if(hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("IM DEAD");
    }
}
