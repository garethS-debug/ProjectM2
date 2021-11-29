using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KEImpactDetection : MonoBehaviour {

    #region ListOf Setup
    public static bool isDialing = false;
    public static bool isHealthy = false;

    int IntKE;

   public static int PotentialEnergy = 0; // stored potential energy in dial
    public int MaxPotentialEnergy;
    //LIST OF KE
    public List<float> KE = new List<float>();

    public Rigidbody rb;
    public float VelocityForce; //velocity of object
    public float KEForce ; //Kenetic Energy
    public float KEForceMultiplier;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //get the rigidbody component

    }

    // Update is called once per frame
    void Update()
    {
        //convert KE to Int Value
        IntKE = (int)KEForce;
       
    }



    void OnCollisionEnter(Collision collision) // on collision 
    {
        if (PotentialEnergy < MaxPotentialEnergy)
        {
            Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
            VelocityForce = collision.relativeVelocity.magnitude; //Calcing Velocity
            KEForce = rb.mass * collision.relativeVelocity.sqrMagnitude;
            KE.Add(IntKE); // this adds to the List
           


            isDialing = true;
            isHealthy = true;
            //THIS SHOULD BE UPDATED TO MAKE IT CLEANER
            Debug.Log("KE PASSED TO KE:" + KEForce);
            Debug.Log("Potential Energy:" + PotentialEnergy);
            PotentialEnergy = PotentialEnergy + IntKE;
        }

        /*

        void OnCollisionEnter(Collision collision) // on collision 
        {
            if (PotentialEnergy < MaxPotentialEnergy) { //if Potential Energy is < Max Potential Energy 
                //Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
                VelocityForce = collision.relativeVelocity.magnitude; //Calcing Velocity
                KEForce = ForceonImpact.KEForce; 
                //(rb.mass * collision.relativeVelocity.sqrMagnitude) * KEForceMultiplier; // calcing KE

                KE.Add(IntKE); // this adds to the List
                isDialing = true;
                isHealthy = true;

                //THIS SHOULD BE UPDATED TO MAKE IT CLEANER
                Debug.Log("KE PASSED TO KE:" + KEForce);

                PotentialEnergy = PotentialEnergy + IntKE;

                //sendmessage to health bar
                //.SendMessage((isHealthy) ? "HealDamage" : "TakeDamage", Time.deltaTime * PotentialEnergy);

                //Debug.Log("velocity :" + collision.relativeVelocity.magnitude); // debuging
                //Debug.Log("Real KE :" + KEForce); // debuging

            }
        */

    }

  

    private void OnCollisionExit(Collision collision)// on collision Exit 
    {
        isDialing = false;
        isHealthy = false;

    }




}

