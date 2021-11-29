using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour {

    public float TurnSpeed = 100f;
    public float AclSpeed = 100f;

    private Rigidbody boatBody;

	// Use this for initialization
	void Start () 
    {
        boatBody = GetComponent<Rigidbody>(); // gets the rigidbody of the boat. 
	}
	
	// Update is called once per frame
	void Update () 
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        boatBody.AddTorque(0f, h * TurnSpeed * Time.deltaTime, 0f);
        boatBody.AddForce(transform.forward * v * AclSpeed * Time.deltaTime);



    }
}
