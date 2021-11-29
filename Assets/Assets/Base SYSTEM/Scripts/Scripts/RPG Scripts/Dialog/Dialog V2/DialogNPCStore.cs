using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogNPCStore : MonoBehaviour {


    public string[] NPCNames;                       //NPC NAME
    public int NPCID;                               //NPC ID for Quests
    public DialogMenu storedNPCDialog;              //TEST

    public DialogMenu dialogStandardDialog;      //QUEST STANDARD DIALOG
    public DialogMenu dialogQuestAvailable;     //QUEST AVAILABLE DIALOG
    public DialogMenu dialogQuestRunning;       //QUEST RUNNING DIALOG
    public DialogMenu dialogQuestFinished;      //QUEST FINISHED DIALOG
    public DialogMenu defaultDialog;            //Default DIALOG



    /*

    private QuestObject recievableQuestDialog;


    public static DialogNPCStore dialogSystem;
    //Standard DIALOG
    public DialogMenu dialogStandardDialog;

    //QUEST AVAILABLE DIALOG
    public DialogMenu dialogQuestAvailable;

    //QUEST RUNNING DIALOG
    public DialogMenu dialogQuestRunning;

    //QUEST FINISHED DIALOG
    public DialogMenu dialogQuestFinished;

    //Default DIALOG
    public DialogMenu defaultDialog;


    public string[] characterDialogtext;
    private bool isQuestDialog = false;
    public static bool isCompletedQuest = false;


    private void Start()
    {
        characterDialogtext = defaultDialog.DialogueCharacterStrings;
        isQuestDialog = defaultDialog.isCharacterQuestDialog;

    }

    public void Update()
    {
        DialogChange();
    }

    public void DialogChange()
    {

        if (QuestObject.RecievableQuestDialog == true)
        {
            Debug.Log("Recieveable quest Dialog here");
            characterDialogtext = dialogQuestAvailable.DialogueCharacterStrings;
            isQuestDialog = dialogQuestAvailable.isCharacterQuestDialog;
            isCompletedQuest = dialogQuestAvailable.isCharacterQuestCompleteDialog;
        }

        //QUEST ACCEPTED STATUS = DIALOG AVAILABLE 
        else if (QuestObject.AcceptedQuestDialog == true)
        {
            //currentDialog = dialogQuestRunning;
            Debug.Log("Accepted quest Dialog here");
            characterDialogtext = dialogQuestRunning.DialogueCharacterStrings;
            isQuestDialog = dialogQuestRunning.isCharacterQuestDialog;
            isCompletedQuest = dialogQuestRunning.isCharacterQuestCompleteDialog;

        }

        //QUEST FINISHED STATUS = DIALOG AVAILABLE
        else if (QuestObject.CompletedQuestDialog == true)
        {
            Debug.Log("Completed quest Dialog here");
            characterDialogtext = dialogQuestFinished.DialogueCharacterStrings;
            isQuestDialog = dialogQuestFinished.isCharacterQuestDialog;
            isCompletedQuest = dialogQuestFinished.isCharacterQuestCompleteDialog;
        }


        //IF THERE IS NO QUEST THEN CURRENT DIALOG = STANDARD DIALOG 
        else
        {
            Debug.Log("Default Dialog here");
            characterDialogtext = dialogStandardDialog.DialogueCharacterStrings;
            isQuestDialog = dialogStandardDialog.isCharacterQuestDialog;

        }

    }
    */
}
