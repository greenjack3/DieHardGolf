using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHit : MonoBehaviour {

    public string player;

    public float powerGround;
    public float powerAir;

    public Transform vectorG;
    public Transform vectorA;

    Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (Input.GetButtonDown(player + "Fire1"))
        {
            anim.SetTrigger("Attack");
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GolfBall>())
        {
            Debug.Log("GolfBall Hit!");
        }
    }
}
