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
    public int team;
    public Material[] materials;
    private TrailRenderer trail;
    public Material[] trails;
    public float idleTimer;
    float timer;
    bool idle = false;
    #endregion

    public void calcDmg() // obliczanie obrażeń 
    {
        if (speed < minSpeedtoDmg)
        {
            dmg = 0;
            idle = true;
            //trail.enabled = false;
        }

        else if (speed >= minSpeedtoDmg)
        {
            dmg = speed * dmgMod;
            idle = false;
            //trail.enabled = true;
        }

        if (dmg >= maxDmg)
        {
            dmg = maxDmg;
        }
    }

    public void FixedUpdate()
    {
        ChangeColor();
        curSpeed = rb.velocity.magnitude;

        if (speed != curSpeed)

        {
            speed = curSpeed;
            calcDmg();
        }
        if (idle)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                idle = false;
                team = 0;
                timer = idleTimer;
            }
        }
        else
        {
            timer = idleTimer;
        }
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Hitable>())
        {
            other.gameObject.GetComponent<Hitable>().HitMe(dmg, team);
        }
    }
    void ChangeColor()
    {
        switch (team)
        {
            case 0:
                GetComponent<Renderer>().material = materials[0];
                trail.material = trails[0];
                break;
            case 1:
                GetComponent<Renderer>().material = materials[1];
                trail.material = trails[1];
                break;
            case 2:
                GetComponent<Renderer>().material = materials[2];
                trail.material = trails[2];
                break;
            case 3:
                GetComponent<Renderer>().material = materials[3];
                trail.material = trails[3];
                break;
            case 4:
                GetComponent<Renderer>().material = materials[4];
                trail.material = trails[4];
                break;

        }
    }

    public void Start()
    {
        speed = curSpeed;
        rb = GetComponent<Rigidbody>();
        timer = idleTimer;
        trail = GetComponent<TrailRenderer>();
    } 
}



