using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour {

    public HitablePlayer player1;
    public HitablePlayer player2;
    public Text timer;
    public Image[] player1VP;
    public Image[] player2VP;

    public float time;
    public int victoryPoints;
    int p1VP;
    int p2VP;

    void Update ()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            time = 0;
        timer.text = "" + time;
	}

    public void GiveVP(int player)
    {
        switch (player)
        {
            case 1:
                p1VP++;
                p2VP = 0;
                break;
            case 2:
                p2VP++;
                p1VP = 0;
                break;
        }

    }

    void UpdateVP()
    {
        for (int i = 0; i < player1VP.Length; i++)
        {
            player1VP[i].enabled = false;
        }
        for (int i = 0; i < player2VP.Length; i++)
        {
            player2VP[i].enabled = false;
        }

        for (int i = p1VP-1; i > -1; i--)
        {
            player1VP[i].enabled = true;
        }
        for (int i = p2VP - 1; i > -1; i--)
        {
            player2VP[i].enabled = true;
        }
    }
}
