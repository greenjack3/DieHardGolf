using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImachine : MonoBehaviour {

    public HitablePlayer playerhealth;
    public GolfHit player;
    public Text hp;
    public Text power;
	
	// Update is called once per frame
	void Update ()
    {
        hp.text = "Health: " + playerhealth.hp;
        power.text = "Power: " + (int)player.powerBuildUp + "%";
	}
}
