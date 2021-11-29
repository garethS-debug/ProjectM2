using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ground_Detection : MonoBehaviour {

    public float raycastDistance = 1f;
    public LayerMask Groundmask;

    public float distanceToGround = 0.5f;


    private void Start()
    {
      

    }

 
	// Update is called once per frame
	void Update () 
    {
        Getalignment();
	}


    //Get Alignment of Character
    void Getalignment ()
    {
  

      

    }
}
