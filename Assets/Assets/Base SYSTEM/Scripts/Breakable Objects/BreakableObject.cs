﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    //[Header("Non-Broken object")]
    //[Tooltip("Non-BrokenGameObj")]
    //public GameObject nonBrokenObj;

    [Header("Broken object")]
    [Tooltip("BrokenGameObj")]
    public GameObject BrokenObj;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "PlayerWeapon")
        {
            Instantiate(BrokenObj, transform.position, transform.rotation);
            Destroy(gameObject);
        }


   

    }
}
