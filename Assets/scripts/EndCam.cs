using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCam : MonoBehaviour
{

    public Transform handle;

    void Update()
    {
        transform.LookAt(handle);
    }
}
