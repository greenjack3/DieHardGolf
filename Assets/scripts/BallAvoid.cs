using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAvoid : MonoBehaviour {

    public float additionalForce = 1000f;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * additionalForce, ForceMode.Force);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
