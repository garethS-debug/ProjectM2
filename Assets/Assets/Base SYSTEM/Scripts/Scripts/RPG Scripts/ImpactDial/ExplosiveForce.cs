using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveForce : MonoBehaviour {

    // Applies an explosion force to all nearby rigidbodies

    public float radius = 5.0F;
    public  int power;
    public  int ImpactDialEnergy;
    public int PEmultiplier = 10;

    public GameObject explosionSpawn;
    public GameObject explosionEffect;
    public float Explosionradius = 0f;
    public float Explosionforce = 0f;

    void Start()
    {
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            //Instantiate(explosionEffect, explosionSpawn.transform.position, explosionSpawn.transform.rotation);               //show effect
            Collider[] colliders = Physics.OverlapSphere(transform.position, Explosionradius);  //get nearby objects
            foreach (Collider nearbyObject in colliders)                                        //loop through each of them 
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();  //add force to rigidbody
                if (rb != null)                                         // check if there is a RB on the object
                {
                    rb.AddExplosionForce(power, transform.position, Explosionradius);
                }
                //ADD DAMAGE TO ENEMY

               // Destroy(explosionEffect);
            }


            ImpactDialEnergy = 0;
            power = 0;

            //EnergyBar.healthBar.fillAmount = 0;
            //EnergyBar.healthBar.fillAmount = 0;
        }

        if (KEImpactDetection.isDialing == true)
        {
            print("IsDialing Found");
            ImpactDialEnergy = KEImpactDetection.PotentialEnergy;
            print("Impact Dial Recieved");
            StoredImpact(+ImpactDialEnergy);
            print("Storing Impact");
            KEImpactDetection.isDialing = false;
           
        }

    }


 
public void StoredImpact (int amount)
    {
        print("Impact Stored");
        power += amount;

    }

}
