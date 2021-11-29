using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystemManager : MonoBehaviour {

    //------- reference to dialog box
    public Image Dialog_Panel;

    //----> Passing quest from dialog to quest system 
    public static bool passQuestfromDialog = false;
    //---> 

    // Use this for initialization
    void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {

        //---------> WHILE CHARACTER IS IN RANGE
        if (NPC_FOV.DialogReady == true)
        {
            //If the npc fov = true determines if the NPC is in field of view of the character
            //however, if there are more than 1 NPC's in the fov, this is both true and false
            //this should be changed to IF PLAYER is in range of an NPC, set dialog box on target NPC to true.

            Dialog_Panel.gameObject.SetActive(true);//set panel to open

            //if the player 


        }

        else
        {
            Dialog_Panel.gameObject.SetActive(false);//set panel to closed
        }

        //--------->

        //---------> IF Dialog passed 

       

        //----->

    }
}
