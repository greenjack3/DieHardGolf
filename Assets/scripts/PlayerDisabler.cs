using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisabler : MonoBehaviour
{

    public RotateHorizontal camControls;
    public Camera cam1;
    public Camera cam2;
    public GolfHit clubControls;
    public Controls playerControls;

    public void DisableControls()
    {
        cam1.enabled = false;
        cam2.enabled = false;
        camControls.enabled = false;
        clubControls.enabled = false;
        playerControls.enabled = false;
    }
}
