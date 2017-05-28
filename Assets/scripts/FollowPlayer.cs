using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform playerFollowed;
    public float yOffset;
    public float lerpSpeed;

    private Transform previousTransform;
    Vector3 velocity = Vector3.zero;
	
	void FixedUpdate ()
    {
        previousTransform = transform;

        Vector3 interpolatedPosition = Vector3.SmoothDamp(previousTransform.position, playerFollowed.position, ref velocity, Time.deltaTime*lerpSpeed);

        interpolatedPosition.y += yOffset;
        transform.position = interpolatedPosition;
	}
}
