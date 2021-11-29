using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger2 : MonoBehaviour {

   //  public string questObj;                 //quest Object Must match name in Quest Manager
   // public int AddXtoQuest;                 //adds number to quest goal

 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {

        
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

             // QuestManager.questManager.AddQuestItem("LeaveTown1", 1);
          //  QuestManager.questManager.AddQuestItem("LeaveTown2", 1);
            //  QuestManager.questManager.AddQuestItem("LeaveTown2", 1);
            //when going through the box collider we auotmatically send the information to add a quest item. 
            //CHANGE MISSION NAME HERE E.G. .AddQuestItem("Leave Town 2", 1); TO .AddQuestItem("PICK UP LEAF", 1);
            //MAKE SURE 'QUEST OBJECTIVE' IS UPDATED TO THE SAME IN THE QUEST MANAGER
            //passing string from Game Manager Quest Obj, giving 1 item amount. 

        }
       
    }
}
