/*  
    This component is for creating Level Information
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[CreateAssetMenu(fileName = "New Level", menuName = "Level/Level Info")]

public class LevelInformation : ScriptableObject
{

    //This stores the difficulty of the level by determining whether there will be markers, whether there will be distraction events and the amount of guards.
    // This will be determined by the planning level success.
    //The planning level will allow you to pick up staff rota, key location, Distraction Locations.
    //If you get spotted during a planning stage, if you get spotted too many times the level will end (just like the main level.
    //THIS IS SHARED BETWEEN PLANNING AND MAIN STAGE


        [Header("Level ID")]
        [Tooltip("A Unique Level ID to determine whether a level has been completed when refering to the Save File")]
        public int LevelID;


        [Header("Level Information")]
        [Tooltip("Information gathered from planning stage")]

        public bool hasKeyLocation;


        [Header("Score")]
        public float Score;
        public float SpottedPenetlies;
        public float TotalScore;
        public float SpottedNo;
        public float Cash;

    [Header("Completed")]
    public bool isCompleted;        //update from completed screen

    [Header("Bonuses")]
    public bool neverSeenOnCCTV;    //update from  CCTV FOV
    public bool neverSpotted;       //update from Guard/Police/Staff FOV
    public bool neverCrime;          //update from Crime cube

    //Mastery


    [Header("Mastery Events")]

    //ADD A EVENTID / BOOL TO CHECK IF EVENT HAS TRIGGERED
    //SAVE THIS BOOL
    public int[] eventID;

    [TextArea]
    public string Notes = "MAKE SURE THERE IS ALWAYS A LIST OF 0. This will be populated when completed"; // Do not place your note/comment here. 
                                                                                                               // Enter your note in the Unity Editor.

                                                                                                               //NEEDED ---> Script to 'reset' information when a 'new' game is started. 
                                                                                                               //NEEDED ---> Script to 'reset' information IF 'replaying' the level. 
                                                                                                               //NEEDED ---> Script to 'reset' information when a 'new' game is started. 
                                                                                                               //NEEDED ---> Script to 'reset' information IF 'replaying' the level.
                                                                                                               //NEEDED ---> Script to 'reset' information when a 'new' game is started. 
                                                                                                               //NEEDED ---> Script to 'reset' information IF 'replaying' the level.



    void Start()
    {
        if (hasKeyLocation == true)
        {
            //Objective is set at key / Vault
        }


    }

    // Update is called once per frame
    void Update()
    {

      
    }

    
}
