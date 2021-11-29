using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----->  This system is responsible for reseting the dialog / closing the panel / passing on quests quests 


public class DialogManager : MonoBehaviour {

    public static DialogManager dialogManager;
    //Bool for dialog box
    public static bool ResetDialogBool = false;
    public static bool passQuestfromDialog = false;
    public static bool DialogManagerSaysClose = false;






    void Start () 
    {

       // dialogManager = this;
		
	}

	void Update () 
    {
       // THIS UPDATES WHEN DIALOG PANEL IS OPEN AT SAME TIME AS QUEST MENU-------->>>
        while (Dialog.endoftheDialog ==true && QuestUIManager.uiManager.questPanelActive == true)
        {
            Debug.Log("Switch Dialog Off here please");
            ResetDialogBool = true;
            break;
          
        }
        if (QuestUIManager.uiManager.questPanelActive == false)
        {
            ResetDialogBool = false;

        }

        if (Dialog.endoftheDialog == true)
        {
            Debug.Log("Its the end of the dialog. Close box");
            DialogManagerSaysClose = true;
            Dialog.endoftheDialog = false;

        }
        //THIS UPDATES WHEN DIALOG PANEL IS OPEN AT SAME TIME AS QUEST MENU-------->>>


        // THIS UPDATES WHEN DIALOG PANEL PASSES OVER A QUEST-------->>>
        if (passQuestfromDialog == true)
        {
            Debug.Log("Pass quest here please");

        }
        // THIS UPDATES WHEN DIALOG PANEL PASSES OVER A QUEST-------->>>





        

    }
}
