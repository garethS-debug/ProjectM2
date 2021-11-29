using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour {

    //Spawn player on boat
    public Transform BoatSpawnPoint;
    private Transform PlayerBoatTransform;
    private Vector3 spawnPlayerhere;
    //Spawn player on boat

    public GameObject boat;
public GameObject boatCamera;
public GameObject player; 
public GameObject playerboatPOS;
    public GameObject mainCamera;
 //Keycode for quests
    public KeyCode SwitchInput = KeyCode.L;

    //bool for boat in or out
    public bool inBoat = false;


	// Use this for initialization
	void Start () 
    {
        boatCamera.SetActive(false);
        inBoat = false;
        boat.GetComponent<BoatController>().enabled = false;
        boatCamera.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () 

    {
       
        Vector3 newplayerPOs = new Vector3(BoatSpawnPoint.position.x, BoatSpawnPoint.position.y, BoatSpawnPoint.position.z);
       
       
        //Set to boat mode


        if (Input.GetKeyDown(SwitchInput))
        {
            if (inBoat == false) 
            {
            inBoat = true;
            boat.GetComponent<BoatController>().enabled = true;
            boatCamera.SetActive(true);
            //switch off main camera
            mainCamera.SetActive(false);
            player.SetActive(false);
            player.transform.position = newplayerPOs;

            }

            else if (inBoat == true)
            {
                inBoat = false;
                boat.GetComponent<BoatController>().enabled = false;
                boatCamera.SetActive(false);
                player.transform.position = newplayerPOs;
                player.SetActive(true);
                mainCamera.SetActive(true);


            }


        }


 
    

    }

   

}
