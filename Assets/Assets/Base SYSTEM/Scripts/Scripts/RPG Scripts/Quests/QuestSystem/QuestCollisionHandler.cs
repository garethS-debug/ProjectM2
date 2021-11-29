using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCollisionHandler : MonoBehaviour {

   // public string questObj;                 //quest Object Must match name in Quest Manager
    //public int AddXtoQuest;                 //adds number to quest goal
                                            //if player enters collider 



    public void Start()
    {
        //COMPLETING QUEST OBJECTIVES
//        QuestManager.questManager.AddQuestItem("LeaveTown2", 1); //passing string from Game Manager Quest Obj, giving 1 item amount. 
        //when going through the box collider we auotmatically send the information to add a quest item. 




        //CHANGE MISSION NAME HERE E.G. .AddQuestItem("Leave Town 2", 1); TO .AddQuestItem("PICK UP LEAF", 1);
        //MAKE SURE 'QUEST OBJECTIVE' IS UPDATED TO THE SAME IN THE QUEST MANAGER
        Debug.Log("Leaving Town Detected");
    }

   
}
