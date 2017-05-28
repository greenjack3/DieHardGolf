using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHit : MonoBehaviour {

    public string player;

    public float powerGround;
    public float powerAir;

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
		if (Input.GetButtonDown(player + "Fire2") && !aiming)
        {
            anim.SetTrigger("Attack");
			aiming = true;
			aimAir = false;
        }
		if (Input.GetButtonDown(player + "Fire3") && !aiming)
		{
			anim.SetTrigger("Attack");
			aiming = true;
			aimAir = true;
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
			if (!aimAir)
				other.GetComponent<Rigidbody>().AddForce(vectorG.forward * powerGround);

			if (aimAir)
				other.GetComponent<Rigidbody>().AddForce(vectorA.forward * powerAir);
        }
    }
}
