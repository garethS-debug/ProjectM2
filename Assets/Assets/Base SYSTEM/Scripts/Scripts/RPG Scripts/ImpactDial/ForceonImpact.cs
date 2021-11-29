using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ForceonImpact : MonoBehaviour {

    public Rigidbody rb;
    //public float speed = 0f;
    //public float KEX;
    //public float KE;
    public float VelocityForce;
    public static float KEForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //setting the speed of the ridgid body * a float speed
        //this will increase the velocity 
        //rb.AddForce(Vector3.down * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
        VelocityForce = collision.relativeVelocity.magnitude;
        KEForce = rb.mass * collision.relativeVelocity.sqrMagnitude;
        //TRANSFER KEFORCE TO KEIMPACT DETECTION SCRIPT
        //Debug.Log("velocity :" + collision.relativeVelocity.magnitude);
        //Debug.Log("Real KE :" + KEForce);
        // And now you can use it for your calculations!
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;
        //Debug.Log("KE :" + collisionForce.x / Time.fixedDeltaTime);
        //Debug.Log("KE :" + collisionForce.y / Time.fixedDeltaTime);
        //Debug.Log("KE :" + collisionForce.z / Time.fixedDeltaTime);

        VelocityForce = collision.relativeVelocity.magnitude;

        KEForce = rb.mass * collision.relativeVelocity.sqrMagnitude;

        //TRANSFER KEFORCE TO KEIMPACT DETECTION SCRIPT

        Debug.Log("velocity :" + collision.relativeVelocity.magnitude);
        Debug.Log("Real KE :" + KEForce);
        // And now you can use it for your calculations!

        //Kinectic Energy 
       // KEX = collisionForce.x;

    }
    */


    // float KE = rb.mass * rb.velocity.sqrMagnitude / 2; // kinetic energy

}

