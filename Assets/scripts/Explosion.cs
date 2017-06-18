using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float scale;
    public float dmg;
    public int teamNo;
    public float increase;
    public float maxScale;
    public float decay;
    float decTimer;
    bool canSpawn;
    bool canDamage = true;
    public GameObject explosion;
    public Material[] materials;

	void Start ()
    {
        transform.localScale *= scale;
        decTimer = decay;
        canSpawn = true;
        ChangeColor();
        if (scale > maxScale)
            Destroy(gameObject);
	}
	
	
	void Update ()
    {
        decTimer -= Time.deltaTime;

        if (decTimer <= 0 && canSpawn)
        {
            GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            newExplosion.GetComponent<Explosion>().scale = scale*increase;
            newExplosion.GetComponent<Explosion>().dmg = dmg/increase;
            newExplosion.GetComponent<Explosion>().teamNo = teamNo;
            newExplosion.GetComponent<Explosion>().canDamage = canDamage;
                        
            canSpawn = false;
            Destroy(gameObject);            
        }
		
	}

    void ChangeColor()
    {
        switch (teamNo)
        {
            case 0:
                GetComponent<Renderer>().material = materials[0];
                break;
            case 1:
                GetComponent<Renderer>().material = materials[1];
                break;
            case 2:
                GetComponent<Renderer>().material = materials[2];
                break;
            case 3:
                GetComponent<Renderer>().material = materials[3];
                break;
            case 4:
                GetComponent<Renderer>().material = materials[4];
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDamage)
        {
            if (other.GetComponent<Hitable>())
            {
                other.GetComponent<Hitable>().HitMe(dmg, teamNo);
                canDamage = false;
            }
        }
    }
}
