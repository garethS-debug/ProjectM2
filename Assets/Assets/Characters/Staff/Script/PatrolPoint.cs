using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Staff")
        {
            print("Waypoint:" + other.gameObject.name);
            //Disable Bool
            if (other.gameObject.name == "Sit")
            {
                print("Waypoint:" + other.gameObject.name);
             //   anim.SetBool("isSitting", true);
            }
        }
    }

}
