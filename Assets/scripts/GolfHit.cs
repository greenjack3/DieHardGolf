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
        if (Input.GetButton(player + "Fire1") && !aiming)
        {
            aiming = true;
            aimAir = false;

            powerBuildUp += Time.deltaTime * buildUpSpeed;
        }

        if (Input.GetButton(player + "Fire2") && !aiming)
        {
            aiming = true;
            aimAir = true;

            powerBuildUp += Time.deltaTime * buildUpSpeed;
        }

        if (Input.GetButtonUp(player + "Fire1"))
        {
            anim.SetTrigger("Attack");
            power = powerBuildUp;
            powerBuildUp = 0;
		}
		if (Input.GetButtonUp(player + "Fire2"))
		{
			anim.SetTrigger("Attack");
            power = powerBuildUp;
            powerBuildUp = 0;
        }

		if (aiming) 
		{
			cd += Time.deltaTime;
			if (cd >= 2)
				aiming = false;
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GolfBall>())
        {
            other.GetComponent<GolfBall>().team = teamNo;

            if (!aimAir)
				other.GetComponent<Rigidbody>().AddForce(vectorG.forward * powerGround * power);

			if (aimAir)
				other.GetComponent<Rigidbody>().AddForce(vectorA.forward * powerAir * power);

            power = 0;
        }
    }
}
