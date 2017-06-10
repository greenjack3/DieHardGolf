using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitablePlayer : Hitable {

    public GameObject playerBody;
    public float hp;
    public int teamNo;

    GameControler gcont;

    private void Start()
    {
        gcont = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControler>();
    }

    public override void HitMe(float dmg, int team)
    {
        if(team != teamNo)
        {
            hp += dmg;

            if (team != 0)
                gcont.GiveVP(team);
        }

        
    }

    public void Death()
    {
        Destroy(playerBody);
    }
}
