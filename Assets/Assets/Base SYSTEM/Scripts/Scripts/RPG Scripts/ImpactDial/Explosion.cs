using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public GameObject explosionEffect;
    public float Explosionradius = 0f;
    bool hasExploded = false;
    public float Explosionforce = 0f;
    public int ImpactDialEnergy = 0;

    // Use this for initialization
    void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        Explosionforce = KEImpactDetection.PotentialEnergy;

        if (Input.GetKeyDown(KeyCode.K))            //onKeypress K
        {
        
        Explode();                                  //Run Explosion
            hasExploded = true;
        }
        Explosionforce = 0f;                        //Reset Stored Explosion



        if (KEImpactDetection.isDialing == true)    //If Impact Dial is collecting Energy
        {
            ImpactDialEnergy = KEImpactDetection.PotentialEnergy;   //take the PE 
            StoredImpact(+ImpactDialEnergy);                        //Store It
            KEImpactDetection.isDialing = false;                   //reset the bool

        }

    }







    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);               //show effect

        Collider[] colliders = Physics.OverlapSphere(transform.position, Explosionradius); //get nearby objects

        foreach (Collider nearbyObject in colliders)                //loop through each of them 
        {
           Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();   //add force to rigidbody
            if (rb != null)                                         // check if there is a RB on the object
            {
                rb.AddExplosionForce(Explosionforce, transform.position, Explosionradius);
            }
            //add damage
        }

    }

    public void StoredImpact(int amount)                            //add power to explosion force
    {
        Explosionforce += amount;

    }

}
