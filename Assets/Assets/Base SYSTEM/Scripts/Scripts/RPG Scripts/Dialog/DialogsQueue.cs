using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsQueue : MonoBehaviour {

    public static DialogsQueue dialogsQueue;

    //QUEST AVAILABLE DIALOG
    public DialogMenu dialogStandardDialog;

    //QUEST AVAILABLE DIALOG
    public DialogMenu dialogQuestAvailable;

    //QUEST RUNNING DIALOG
    public DialogMenu dialogQuestRunning;

    //QUEST FINISHED DIALOG
    public DialogMenu dialogQuestFinished;

    //CURRENT QUEST DIALOG
    public DialogMenu currentDialog;

    //QUEST ID TO REFERNCE
    // public int QuestID;

    //Quest Flag Reference


    // Use this for initialization

    private void Start()
    {
        dialogsQueue = this;
    }

    // Update is called once per frame
    void Update () 
    {
        //QUEST AVAILABLE STATUS = DIALOG AVAILABLE 

        if (FieldOfView.RecievableQuestDialog == true)
        {
            Debug.Log("Recieveable quest Dialog here");
            currentDialog = dialogQuestAvailable;

        }

        //QUEST ACCEPTED STATUS = DIALOG AVAILABLE 
        else if (FieldOfView.AcceptedQuestDialog == true)
        {
            //currentDialog = dialogQuestRunning;
            Debug.Log("Accepted quest Dialog here");
            currentDialog = dialogQuestRunning;

        }

        //QUEST FINISHED STATUS = DIALOG AVAILABLE
        else if (FieldOfView.CompletedQuestDialog == true)
        {
            Debug.Log("Completed quest Dialog here");
            currentDialog = dialogQuestFinished;
        }

    
        //IF THERE IS NO QUEST THEN CURRENT DIALOG = STANDARD DIALOG 
        else 
        {
            Debug.Log("Default Dialog here");
            currentDialog = dialogStandardDialog;
        }

      
    }
}
