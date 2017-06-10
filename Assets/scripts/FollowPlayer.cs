using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform playerFollowed;
    public float yOffset;
    public float lerpSpeed;

    private Transform previousTransform;
    	
	void FixedUpdate ()
    {
        previousTransform = transform;
        Vector3 playerPosition = playerFollowed.position + new Vector3(0, yOffset, 0);

        Vector3 interpolatedPosition = Vector3.Lerp(previousTransform.position, playerPosition, Time.deltaTime*lerpSpeed);

       transform.position = interpolatedPosition;
	}
}
