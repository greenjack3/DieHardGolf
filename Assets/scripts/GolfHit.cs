using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHit : MonoBehaviour {

    public string player;
    public int teamNo;

    public float powerGround;
    public float powerAir;
    public float powerBuildUp;
    float power;
    public float buildUpSpeed;
   
    public Transform vectorG;
    public Transform vectorA;

	bool aiming;
	float cd;
	bool aimAir;

    Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
		aiming = false;
	}
	
	void Update ()
    {
        if (Input.GetAxis(player + "Fire1") >= 0.5 && !aiming)
        {
            aiming = true;
            aimAir = false;          
        }

        if (Input.GetAxis(player + "Fire2") >= 0.5 && !aiming)
        {
            aiming = true;
            aimAir = true;        
        }
                
        if (Input.GetAxis(player + "Fire1") < 0.5 && aiming && !aimAir)
        {
            anim.SetTrigger("Attack");
            power = powerBuildUp;
            powerBuildUp = 0;
            aiming = false;
		}
		if (Input.GetAxis(player + "Fire2") < 0.5 && aiming && aimAir)
		{
			anim.SetTrigger("Attack");
            power = powerBuildUp;
            powerBuildUp = 0;
            aiming = false;
        }

        if (aiming)
        {
            powerBuildUp += Time.deltaTime * buildUpSpeed;
        }
        if (powerBuildUp >= 1)
        {
            powerBuildUp = 1;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GolfBall>())
        {
            other.GetComponent<GolfBall>().team = teamNo;
            other.GetComponent<GolfBall>().airborne = aimAir;

            if (!aimAir)
				other.GetComponent<Rigidbody>().AddForce(vectorG.forward * powerGround * power);

			if (aimAir)
				other.GetComponent<Rigidbody>().AddForce(vectorA.forward * powerAir * power);

            power = 0;
        }
    }
}
