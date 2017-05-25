using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHorizontal : MonoBehaviour {

    public string player;

    public float horSpeed;
    public float verSpeed;

    public GameObject secondArm;
    public float angle1;
    public float angle2;

    void Update ()
    {
        float h = Input.GetAxis(player + "RHorizontal");
        float v = Input.GetAxis(player + "RVertical");

        transform.Rotate(Vector3.up * h * Time.deltaTime * horSpeed);

        secondArm.transform.Rotate(Vector3.right * v * Time.deltaTime * verSpeed);

        float xAngle = secondArm.transform.rotation.eulerAngles.x;
                
        if (xAngle > angle1 && xAngle < angle2)
        {
            if (xAngle < 180) xAngle = angle1;
            if (xAngle > 180) xAngle = angle2;
        }

        secondArm.transform.eulerAngles = new Vector3(xAngle, secondArm.transform.rotation.eulerAngles.y, secondArm.transform.rotation.eulerAngles.z);
    }
}
