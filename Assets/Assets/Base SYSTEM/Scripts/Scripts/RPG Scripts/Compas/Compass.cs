using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {

    public Transform player;
    Vector3 vector;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    
    {
        vector.z = player.eulerAngles.y;
        transform.localEulerAngles = vector;
	}
}
