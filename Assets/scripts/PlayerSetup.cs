using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour {

    public Controls controls;
    public GolfHit gh;
    public HitablePlayer hp;

    public string player;
    public int teamNo;

    private void Start()
    {
        controls.player = player;
        gh.player = player;
        gh.teamNo = teamNo;
        hp.teamNo = teamNo;
    }
}
