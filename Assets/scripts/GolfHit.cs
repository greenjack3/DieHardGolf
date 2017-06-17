using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHit : MonoBehaviour
{

    public string player;
    public int teamNo;

    public float powerGround;
    public float powerAir;
    public float powerBuildUp;
    float power;
    public float buildUpSpeed;

    public Transform vectorG;
    public Transform vectorA;

    public Transform neutral;
    public Transform maxPower;
    public Transform attack1;
    public Transform attack2;
    public GameObject golfClub;

    public bool aiming;
    float cd;
    bool aimAir;

    bool attack;
    public float attackAnimSpeed;
    float timePassed;
    int key = 0;
    BoxCollider col;

    void Start()
    {
        aiming = false;
        col = GetComponent<BoxCollider>();
        col.enabled = false;
    }

    void Update()
    {
        if (!attack)
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
                attack = true;
                power = powerBuildUp;
                aiming = false;
                key = 1;
            }
            if (Input.GetAxis(player + "Fire2") < 0.5 && aiming && aimAir)
            {
                attack = true;
                power = powerBuildUp;
                aiming = false;
                key = 1;
            }

            if (aiming)
            {
                powerBuildUp += Time.deltaTime * buildUpSpeed;
                golfClub.transform.position = Vector3.Lerp(neutral.position, maxPower.position, powerBuildUp);
                golfClub.transform.rotation = Quaternion.Slerp(neutral.rotation, maxPower.rotation, powerBuildUp);
            }
            if (powerBuildUp >= 1)
            {
                powerBuildUp = 1;
            }
        }
        if (attack)
        {
            timePassed += Time.deltaTime * attackAnimSpeed;

            if (key == 1)
            {
                golfClub.transform.position = Vector3.Lerp(neutral.position, attack1.position, timePassed);
                golfClub.transform.rotation = Quaternion.Slerp(neutral.rotation, attack1.rotation, timePassed);
                timePassed -= Time.deltaTime;

                if (timePassed >= 1)
                {
                    timePassed = 0;
                    key = 2;
                    golfClub.transform.position = attack1.position;
                    golfClub.transform.rotation = attack1.rotation;
                    col.enabled = true;
                }
            }
            if (key == 2)
            {
                golfClub.transform.position = Vector3.Lerp(attack1.position, attack2.position, timePassed);
                golfClub.transform.rotation = Quaternion.Slerp(attack1.rotation, attack2.rotation, timePassed);
                timePassed -= Time.deltaTime;

                if (timePassed >= 1)
                {
                    timePassed = 0;
                    key = 3;
                    golfClub.transform.position = attack2.position;
                    golfClub.transform.rotation = attack2.rotation;
                    col.enabled = false;
                }
            }
            if (key == 3)
            {
                golfClub.transform.position = Vector3.Lerp(attack2.position, neutral.position, timePassed);
                golfClub.transform.rotation = Quaternion.Slerp(attack2.rotation, neutral.rotation, timePassed);
                timePassed -= Time.deltaTime;

                if (timePassed >= 1)
                {
                    timePassed = 0;
                    key = 0;
                    attack = false;
                    golfClub.transform.position = neutral.position;
                    golfClub.transform.rotation = neutral.rotation;
                    powerBuildUp = 0;
                }
            }
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
            col.enabled = false;
        }
    }
}
