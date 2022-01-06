using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{

    public GameObject[] RespawnPOints;
    GameObject cuurentSpawnPoint;



    public void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Player")
        {
            if (RespawnPOints.Length > 0)
            {
                int index = Random.Range(0, RespawnPOints.Length);
                cuurentSpawnPoint = RespawnPOints[index];
                other.gameObject.transform.position = cuurentSpawnPoint.transform.position;
            }
           


           
        }

    }


    public void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            if (RespawnPOints.Length > 0)
            {
                int index = Random.Range(0, RespawnPOints.Length);
                cuurentSpawnPoint = RespawnPOints[index];

                other.gameObject.transform.position = cuurentSpawnPoint.transform.position;
            }
        }


    }



}
