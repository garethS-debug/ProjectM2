using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestUIManager : MonoBehaviour {

    public static QuestUIManager uiManager; //access to all public variables from other script

    //BOOL to check if a quest is available & panels active
    public bool questAvailable = false;
    public bool questActive = false;
    public bool questPanelActive = false;
    private bool questLogPanelActive = false;

    //Panels
    public GameObject questPanel;
    public GameObject questLogPanel;

    //Quest Object
    private QuestObject currentQuestObject;

    //Quest Lists
    public List<Quests> availableQuests = new List<Quests>();
    public List<Quests> activeQuests = new List<Quests>();

    //Buttons
    public GameObject qButton;
    public GameObject qLogButton;
    private List<GameObject> qButtons = new List<GameObject>();

    public GameObject acceptButton;
    public GameObject giveUpButton;
    public GameObject completeButton;

    //SPACERS
    public Transform qButtonSpacer1;//spacer for Qbutton
    public Transform qButtonSpacer2;// running qButtons
    public Transform qLogButtonSpacer;// running qLog

    //Quest INFOS
    public Text questTitle;
    public Text questDescription;
    public Text questSummary;

    //Quest Log INFOS
    public Text questLogTitle;
    public Text questLogDescription;
    public Text questLogSummary;

    //Keycode for quests
    public KeyCode QuestInput = KeyCode.Q;

    public QButtonScript acceptButtonScript;
    public QButtonScript giveUpButtonScript;
    public QButtonScript completeButtonScript;

    public GameObject givingUpButton;

    private void Start()
    {

        acceptButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("GameObject").transform.Find("AcceptButton").gameObject;
        acceptButtonScript = acceptButton.GetComponent<QButtonScript>();


        // giveUpButton = GameObject.Find("QuestCanvas").transform.Find("QuestLogPanel").transform.Find("QuestDescription").transform.Find("GameObject").transform.Find("GiveUpButton").gameObject;
        giveUpButton = givingUpButton;
        giveUpButtonScript = giveUpButton.GetComponent<QButtonScript>();

        completeButton = GameObject.Find("QuestCanvas").transform.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("GameObject").transform.Find("CompleteButton").gameObject;
        completeButtonScript = completeButton.GetComponent<QButtonScript>();


        acceptButton.SetActive(false);
        giveUpButton.SetActive(false);
        completeButton.SetActive(false);

    }

    private void Awake()
    {
        if (uiManager == null)
        {
            uiManager = this;
        }
        else if (uiManager != this)
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(gameObject);

        HideQuestPanel();
    }

  

    // Update is called once per frame
    void Update () 
    {
        if (Input.GetKeyDown(QuestInput))
        {
            Debug.Log("Questing Menu Slected");
            questLogPanelActive = !questLogPanelActive;//set to reverse state
            // Show Quest Log Panel
            ShowQuestLogPanel();

        //hide quest panel
           
        }

      
    }

    //---------- LOADING WHEN SCENE LOADS

    //----------> CHECKING WHATS AVAILABLE/ACTIVE/COMPLETED 


    //1st Bug is here
    //Dialog system  QuestObject.StartQuestFromDialog = true;
    // Then  QuestUIManager.uiManager.CheckQuests(this);
    // Then  below --- >  public void CheckQuests(QuestObject questObject)
    //THe Script is looking for Quest #2 even though there are 3 in the scene.



    //CALLED FROM QUEST OBJECT
    public void CheckQuests(QuestObject questObject) //we send this quest object to quest quest manager itself
    {
        Debug.Log("CheckQuests(QuestObject questObject)"); //CORRECT
        print(questObject); //CORRECT
        print(currentQuestObject); //


        currentQuestObject = questObject; //setting the quest object to the one that has been passed over. 

        if (QuestObject.StartQuestFromDialog == true)
        {
          //  Debug.Log("StartQuestFromDialog == true");
           // print(questObject);
            QuestManager.questManager.QuestRequest(questObject);
        }


        if (QuestObject.CompleteQuestFromDialog == true)
        {
           // Debug.Log("CompleteQuestFromDialog == true");  //correct
           // print(questObject);  //Correcy
            QuestManager.questManager.ActiveQuestRequest(questObject);
        }

        if ((questActive || questAvailable) && !questPanelActive)
        {
            //show QuestLog Panel
            ShowQuestPanel();
          //  Debug.Log("Check Quests Quest Panel Show");

        }

        else
        {
            Debug.Log("No Quests Available"); //if no quests available.
        }
    }

    //----------> CHECKING WHATS AVAILABLE/ACTIVE/COMPLETED 




    //SHOW PANEL
    public void ShowQuestPanel()
    {
       // Debug.Log("Quest Panel is being Set as true");
        questPanelActive = true;
        //----> errors 
        questPanel.SetActive(questPanelActive);
        //fill in data
   
        FillQuestButtons();
     
    }


    //show questlog panel
    public void ShowQuestLogPanel()
    {
      //  Debug.Log("Quest Log Panel Showing");
        questLogPanel.SetActive(questLogPanelActive);
        if (questLogPanelActive && !questPanelActive)
        {
            foreach (Quests curQuest in QuestManager.questManager.currentQuestList)
            {
                GameObject questButton = Instantiate(qLogButton);
                QLogButtonScript qButton = questButton.GetComponent<QLogButtonScript>();
                qButton.questID = curQuest.id;
                qButton.questTitle.text = curQuest.title;
                questButton.transform.SetParent(qLogButtonSpacer, false);
                qButtons.Add(questButton);
            }
        }
        else if (!questLogPanelActive && !questPanelActive)
        {
            HideQuestLogPanel();
        }
    }

    //Showing Quest Log 

    public void ShowQuestLogPanel(Quests activeQuest)
    {
        questLogTitle.text = activeQuest.title;
        if (activeQuest.progress == Quests.QuestProgress.ACCEPTED)
        {
            questLogDescription.text = activeQuest.hint;
            questLogSummary.text = activeQuest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequired;
           // Debug.Log("Show Give Up Button Here");
            giveUpButton.SetActive(true);
            //QButtonScript.qButtonScript.ShowAllInfos();
        }
        else if (activeQuest.progress == Quests.QuestProgress.COMPLETE)
        {
            questLogDescription.text = activeQuest.congratulations;
            questLogSummary.text = activeQuest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequired;
           
        }

    }

    //quest log

    //HIDE QUEST PANEL
    public void HideQuestPanel()
    {
        questPanelActive = false;
        questAvailable = false;
        questActive = false;

        //clear text fields
        questTitle.text = "";
        questDescription.text = "";
        questSummary.text = "";

        //Clear Lists
        availableQuests.Clear();
        activeQuests.Clear();
        //clear button lists
        for (int i = 0; i< qButtons.Count;i++)
        {
            Destroy(qButtons[i]);
        }

        qButtons.Clear();

        //Hide Panel
        questPanel.SetActive(questPanelActive);
    }

    //Hide Quest Log Panel 
    public void HideQuestLogPanel()
    {
        questLogPanelActive = false;
        questLogTitle.text = "";
        questLogDescription.text = "";
        questLogSummary.text = "";

        //clear button list
        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }
        qButtons.Clear();
        questLogPanel.SetActive(questLogPanelActive);
    }

    //Fill buttons for quest panel
    void FillQuestButtons()
    {
        completeButton.SetActive(false);
        acceptButton.SetActive(false);

        foreach (Quests availableQuest in availableQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();//access Qbutton script

            qBScript.questID = availableQuest.id; //quest ID in the button
            qBScript.questTitle.text = availableQuest.title;

            questButton.transform.SetParent(qButtonSpacer1, false);
            qButtons.Add(questButton);
          
           // Debug.Log("Available Quest Showing");
            print(availableQuests.Count);
        }

        foreach (Quests activeQuest in QuestManager.questManager.currentQuestList)
        //foreach (Quests activeQuest in activeQuests)
        {
            if (FieldOfView.questID == activeQuest.id)
            {
                Debug.Log("FOV ID");
                print(FieldOfView.questID);

                GameObject questButton = Instantiate(qButton);
                QButtonScript qBScript = questButton.GetComponent<QButtonScript>();//access Qbutton script

                qBScript.questID = activeQuest.id;
                qBScript.questTitle.text = activeQuest.title;

                questButton.transform.SetParent(qButtonSpacer2, false);
                qButtons.Add(questButton);

                completeButton.SetActive(false);
               // Debug.Log("Shhowing ActiveQuests");
            }

           

            ////TEST
            //if (DialogSystem.isCompletedQuest == true  )
            //{
            //    Debug.Log("Is Completed Quest . Stop Button");
            //    acceptButton.SetActive(false);

            //}
          //  Debug.Log(activeQuest.title);
           

        }
    
    }

    //SHOW QUEST ON BUTTON PRESS IN PANEL
    public void ShowSelectedQuest(int questID)
    {
        //available quests
        for (int i = 0; i < availableQuests.Count; i++)
        {
            if (availableQuests[i].id == questID)
            {
                questTitle.text = availableQuests[i].title; 
                if (availableQuests[i].progress == Quests.QuestProgress.AVAILABLE)
                {
                    questDescription.text = availableQuests[i].description; // show descriptin.
                    //amount needed for quest
                    questSummary.text = availableQuests[i].questObjective + " : " + availableQuests[i].questObjectiveCount + " / " + availableQuests[i].questObjectiveRequired;
                }
            }
        }

        //ACTIVE QUESTS
        for (int i = 0; i < activeQuests.Count; i++)
        {
            Debug.Log("DEBUGGING NOW>>>>>>>");
            print(questID);             //2  (Quest 2)
            print(activeQuests[i].id);  //3  (Quest 1)
            print(FieldOfView.questID); //2  (Quest 2)



            if (activeQuests[i].id == questID)
            {
                questTitle.text = activeQuests[i].title;
                Debug.Log("Lets Look for a bug >>>>>");
                print(questTitle.text = activeQuests[i].title); //3


                //BUG IDENTIFIED ---- > activeQuests[i] references the wrong ID


                if (activeQuests[i].progress == Quests.QuestProgress.ACCEPTED)
                {
                    questDescription.text = activeQuests[i].hint; // show description.
                    //amount needed for quest
                    questSummary.text = activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequired;
                    Debug.Log("Bug is in Accepted Quests >>>>>");
                    print(questDescription.text = activeQuests[i].hint);
                }

                else if (activeQuests[i].progress == Quests.QuestProgress.COMPLETE)
                {
                    Debug.Log("Bug is in Completed Quests >>>>>");
                    print(questDescription.text = activeQuests[i].congratulations);

                    questDescription.text = activeQuests[i].congratulations; // show description.
                    questSummary.text = activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequired;
                }
            }
        }
    }
}

