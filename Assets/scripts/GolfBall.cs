using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GolfBall : MonoBehaviour
{

    #region variables

    public float dmg;    // Wartośc obrażeń 
  //  [Range(0.5f, 5f)]
    public float dmgMod; // modyfikator obrażeń 
    public float speed; // Wartość prędkości piłki 
    public float curSpeed; // Aktualna prędkość piłki 
 //   [Range(0.2f, 2f)]
    public float minSpeedtoDmg; // minimalna prędkość do zadawania obrażeń 
    Rigidbody rb;
    public float maxDmg;
    #endregion

    public void calcDmg() // obliczanie obrażeń 
    {
        if (speed < minSpeedtoDmg)
        {
            dmg = 0;
        }

        else if (speed >= minSpeedtoDmg)
        {
            dmg = speed * dmgMod;
        }

        if (dmg >= maxDmg)
        {
            dmg = maxDmg;
        }
    }

    public void FixedUpdate()
    {
        curSpeed = rb.velocity.magnitude;

        if (speed != curSpeed)

        {
            speed = curSpeed;
            calcDmg();
        }
        
    }


    public void Start()
    {
        speed = curSpeed;
        rb = GetComponent<Rigidbody>();
    } 
}



