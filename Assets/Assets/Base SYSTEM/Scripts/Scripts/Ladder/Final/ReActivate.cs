using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReActivate : MonoBehaviour
{

    public GameObject ReActivateObject;
    public GameObject ReActivateObject1;
    //  public GameObject deActivateObject;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" )
        {

            if (ReActivateObject.active ==false)
            {

                ReActivateObject.SetActive(true);
             

            }

            if (ReActivateObject1.active == false)
            {

                ReActivateObject1.SetActive(true);


            }
            //if (deActivateObject.active == true)
            //{

            //    deActivateObject.SetActive(false);

            //}

        }


        }

}
