using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectManager : MonoBehaviour {

    public Text[] plReadyTexts = new Text[4];
    
    public Text golf;
    public string controler1;
    public string controler2;
    public string controler3;
    public string controler4;
    public Color[] teamcolors = new Color[4];

    int numberOfPlayers = 0;
    bool[] plEnabled = new bool[4];
    string[] plControls = new string[4];

    bool p1selected = false;
    bool p2selected = false;
    bool p3selected = false;
    bool p4selected = false;

    void Start ()
    {
        for (int i = 0; i < plReadyTexts.Length; i++)
        {
            plReadyTexts[i].color = Color.black;
        }

        golf.enabled = false;

        for (int i = 0; i < plEnabled.Length; i++)
        {
            plEnabled[i] = false;
        }
    }
	
	
	void Update ()
    {
        if (Input.GetButtonDown(controler1 + "Jump") && !p1selected)
        {
            p1selected = true;
            for (int i = 0; i < plEnabled.Length; i++)
            {
                if (!plEnabled[i])
                {
                    plEnabled[i] = true;
                    plControls[i] = controler1;
                    plReadyTexts[i].color = teamcolors[i];
                    numberOfPlayers++;
                    break;
                }
            }
        }
        if (Input.GetButtonDown(controler2 + "Jump") && !p2selected)
        {
            p2selected = true;
            for (int i = 0; i < plEnabled.Length; i++)
            {
                if (!plEnabled[i])
                {
                    plEnabled[i] = true;
                    plControls[i] = controler2;
                    plReadyTexts[i].color = teamcolors[i];
                    numberOfPlayers++;
                    break;
                }
            }
        }
        if (Input.GetButtonDown(controler3 + "Jump") && !p3selected)
        {
            p3selected = true;
            for (int i = 0; i < plEnabled.Length; i++)
            {
                if (!plEnabled[i])
                {
                    plEnabled[i] = true;
                    plControls[i] = controler3;
                    plReadyTexts[i].color = teamcolors[i];
                    numberOfPlayers++;
                    break;
                }
            }
        }
        if (Input.GetButtonDown(controler4 + "Jump") && !p4selected)
        {
            p4selected = true;
            for (int i = 0; i < plEnabled.Length; i++)
            {
                if (!plEnabled[i])
                {
                    plEnabled[i] = true;
                    plControls[i] = controler4;
                    plReadyTexts[i].color = teamcolors[i];
                    numberOfPlayers++;
                    break;
                }
            }
        }

        if (numberOfPlayers >= 2)
        {
            golf.enabled = true;
        }

    }
}
