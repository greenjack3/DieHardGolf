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
    public Text player1WIN;
    public Text player2WIN;

    public float time;
    public int victoryPoints;
    int p1VP;
    int p2VP;
    private void Start()
    {
        time *= 60;
        player1WIN.enabled = false;
        player2WIN.enabled = false;
    }
    void Update ()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 0;
            CheckTimeWin();
        }

        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        timer.text = "" + minutes + ":" + seconds;
        UpdateVP();
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
            if(i < player1VP.Length)
                player1VP[i].enabled = true;
        }
        for (int i = p2VP - 1; i > -1; i--)
        {
            if (i < player2VP.Length)
                player2VP[i].enabled = true;
        }
        CheckWin();
    }

    void CheckWin()
    {
        if (p1VP >= victoryPoints)
        {
            AnnounceWinner(1);
        }
        if (p2VP >= victoryPoints)
        {
            AnnounceWinner(2);
        }
    }
    void CheckTimeWin()
    {
        if (player1.hp < player2.hp)
        {
            AnnounceWinner(1);
        }
        if (player2.hp < player1.hp)
        {
            AnnounceWinner(2);
        }
    }
    void AnnounceWinner(int player)
    {
        player1.Death();
        player2.Death();

        switch (player)
        {
            case 1:
                player1WIN.enabled = true;
                break;
            case 2:
                player2WIN.enabled = true;
                break;
        }
    }
}
