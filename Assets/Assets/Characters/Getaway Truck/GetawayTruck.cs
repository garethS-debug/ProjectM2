using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetawayTruck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        

        //If called 'Money Bag' then assign Points 

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Money Bag")
        {
            print("Give Murphy a 10");
            Objective.instance.Score += 100;
            other.gameObject.name = ("Spent Bag");
            other.GetComponent<BoxCollider>().enabled = false;
        }

        else 
                {
            print("Give Murphy NOTHING");
        }

        }

    }
