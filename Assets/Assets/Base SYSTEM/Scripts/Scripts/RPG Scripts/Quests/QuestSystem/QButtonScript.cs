using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QButtonScript : MonoBehaviour {

    public int questID;
    public Text questTitle;

    public static QButtonScript qButtonScript;
    //private GameObject acceptButton;
    //private GameObject giveUpButton;
    //private GameObject completeButton;

    //private QButtonScript acceptButtonScript;
    //private QButtonScript giveUpButtonScript;
    //private QButtonScript completeButtonScript;


    //private void Start()
    //{
    //    acceptButton = GameObject.Find("QuestCanvas").transform.FindChild("QuestPanel").transform.FindChild("QuestDescription").transform.FindChild("GameObject").transform.FindChild("AcceptButton").gameObject;
    //    acceptButtonScript = acceptButton.GetComponent<QButtonScript>();


    //    giveUpButton = GameObject.Find("QuestCanvas").transform.FindChild("QuestPanel").transform.FindChild("QuestDescription").transform.FindChild("GameObject").transform.FindChild("GiveUpButton").gameObject;
    //    giveUpButtonScript = giveUpButton.GetComponent<QButtonScript>();

    //    completeButton = GameObject.Find("QuestCanvas").transform.FindChild("QuestPanel").transform.FindChild("QuestDescription").transform.FindChild("GameObject").transform.FindChild("CompleteButton").gameObject;
    //    completeButtonScript = completeButton.GetComponent<QButtonScript>();


    //    acceptButton.SetActive(false);     //    giveUpButton.SetActive(false);     //    completeButton.SetActive(false);

    //}


    //SHOW ALL INFO
    public void ShowAllInfos()
    {
    
       // Debug.Log(QuestUIManager.uiManager.giveUpButtonScript.questID);
        QuestUIManager.uiManager.ShowSelectedQuest(questID);

        //SHOW WHAT BUTTON IS NEEDED 

        //Accept Button
        if (QuestManager.questManager.RequestAvailableQuest(questID))         {             Debug.Log("Accept Button Show");             QuestUIManager.uiManager.acceptButton.SetActive(true);             QuestUIManager.uiManager.acceptButtonScript.questID = questID;         }         else         {             QuestUIManager.uiManager.acceptButton.SetActive(false);
                    }

        //GIVE UP BUTTON
      
        if (QuestManager.questManager.RequestAcceptedQuest(questID))
        {
            Debug.Log("GiveUp Button Show");
            QuestUIManager.uiManager.giveUpButton.SetActive(true);
            QuestUIManager.uiManager.giveUpButtonScript.questID = questID;
        }
        else
        {
            QuestUIManager.uiManager.giveUpButton.SetActive(false);
        }

        //Complete BUTTON

        if (QuestManager.questManager.RequestCompleteQuest(questID))
        {
            //IF THE PLAYER IS LOOKING AT THE ORIGINAL QUEST GIVER 
            if (FieldOfView.questID == questID)
            {
                Debug.Log("Complete Button Show");
                QuestUIManager.uiManager.completeButton.SetActive(true);
                QuestUIManager.uiManager.completeButtonScript.questID = questID;
            }

       

        }
        else
        {
            QuestUIManager.uiManager.completeButton.SetActive(false);
        }

    }

    public void AcceptQuest()     {
        Inventory.CheckQuestObj = true;//Check if there is a quest item. 

        Debug.Log("Accept Quest");         QuestManager.questManager.AcceptQuest(questID);
        //Hide Panel
        QuestUIManager.uiManager.HideQuestPanel();
        //acceptButton.SetActive(false);

        //UPDATE ALL NPC'S / QUEST GIVERS
        QuestObject[] currentQuestGivers = FindObjectsOfType(typeof(QuestObject)) as QuestObject[]; //reference to all quest objects in scene
        foreach(QuestObject questObj in currentQuestGivers) //find quest states 
        {
            //setquestmarker
            questObj.SetQuestMarker();
        }
    }

    public void GiveUpQuest()
    {
        Debug.Log("Give Up Quest");
        QuestManager.questManager.GiveUpQuest(questID);
        //Hide Panel
        QuestUIManager.uiManager.HideQuestPanel();
        //acceptButton.SetActive(false);

        //Close the GiveUp button
        QuestUIManager.uiManager.giveUpButton.SetActive(false);

        QuestUIManager.uiManager.questLogDescription.text = "";
        QuestUIManager.uiManager.questLogSummary.text = "";

        QuestUIManager.uiManager.acceptButton.SetActive(false);


        //UPDATE ALL NPC'S / QUEST GIVERS
        QuestObject[] currentQuestGivers = FindObjectsOfType(typeof(QuestObject)) as QuestObject[]; //reference to all quest objects in scene
        foreach (QuestObject questObj in currentQuestGivers) //find quest states 
        {
            //setquestmarker
            questObj.SetQuestMarker();
        }
    }
    public void CompleteQuest()
    {
        Debug.Log("Complete Quest");
        QuestManager.questManager.CompleteQuest(questID);
        //Hide Panel
        QuestUIManager.uiManager.HideQuestPanel();
        //acceptButton.SetActive(false);

        QuestUIManager.uiManager.acceptButton.SetActive(false);

        Inventory.CheckQuestObj = true; // Check if item in ivnetory

        Debug.Log("Delete Quest Obj");

        //UPDATE ALL NPC'S / QUEST GIVERS
        QuestObject[] currentQuestGivers = FindObjectsOfType(typeof(QuestObject)) as QuestObject[]; //reference to all quest objects in scene
        foreach (QuestObject questObj in currentQuestGivers) //find quest states 
        {
            //setquestmarker
            questObj.SetQuestMarker();
        }
    }

    public void ClosePanel ()
    {
//        Debug.Log("Close Panel Selected");
        QuestUIManager.uiManager.HideQuestPanel(); // hide quest panel
        QuestUIManager.uiManager.acceptButton.SetActive(false);
        QuestUIManager.uiManager.giveUpButton.SetActive(false);
        QuestUIManager.uiManager.completeButton.SetActive(false);

    }

}
