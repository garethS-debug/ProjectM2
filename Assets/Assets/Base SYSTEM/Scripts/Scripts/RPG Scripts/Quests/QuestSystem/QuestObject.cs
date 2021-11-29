using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestObject : MonoBehaviour {

    public static QuestObject questObject;

    private bool inTrigger = false;

   // public KeyCode guiInput = KeyCode.V;


    public List<int> availableQuestIDs = new List<int>();   //Available Quest List
    public List<int> recievableQuestIDs = new List<int>();  //Recievable Quest List (if unlocked in quest chain)

    //Quest UI Marker for NPC
    public GameObject questMarker;//quest marker  game object (canvas)
    public Image questMarkerImage;//image on canvas
    public Sprite questAvailableSprite; //sprite state of quest (available)
    public Sprite questRecievableSprite; //sprite state of quest (Recievable)


    //BOOLS FOR QUEST DIALOG
    public static bool RecievableQuestDialog = false;
    public static bool AcceptedQuestDialog = false;
    public static bool CompletedQuestDialog = false;
    public static bool StartQuestFromDialog = false;
    public static bool CompleteQuestFromDialog = false;
    public bool initiateQuestScript;

    //public static bool isQuestfromDialog = false;

    //Fixing the (THIS) error
    public QuestObject questCode;

    //QuestObjectScript
    public QuestObject QuestObjScript;

    // Use this for initialization
    void Start () 
    {
       

        //questCode = questCode = GetComponent<QuestObject>();

        /// ------------ <summary>
        /// Adding Quest from Dialog
        ///  ----------- </summary>
        if (inTrigger && StartQuestFromDialog == true) //ADD FOV INTERACTION HERE / END OF TEXT 
        {
            if (!QuestUIManager.uiManager.questPanelActive)
            {
                Debug.Log("Check Quests 2 >");
                print(this);
                //QUEST UI MANAGER
                QuestUIManager.uiManager.CheckQuests(this);
                //QuestManager.questManager.QuestRequest(this);
                StartQuestFromDialog = false;
            }

        }

       

        SetQuestMarker();

        /// ------------ <summary>
        ///  ----------- </summary> 


       	
	}

    //SET QUEST MARKER HERE
    public void SetQuestMarker()
    {
        if(QuestManager.questManager.CheckCompletedQuests(this)) // if this is an completed quest
        {
            Debug.Log("CheckCompletedQuests");
            print(questCode);

            questMarker.SetActive(true);//set quest marker canvas to true. 
            questMarkerImage.sprite = questRecievableSprite;//image sprite the recievable sprite
            questMarkerImage.color = Color.yellow;//set the quest marker colour
            //DIALOG RECEIVABLE 
            RecievableQuestDialog = false;//recievable Quest dialog
            AcceptedQuestDialog = false;
            CompletedQuestDialog = true;

            //Check to see if there is a quest Item

        }

        //DEBIG --- > this one is the standard state (DOES NOT CHANGE)

        else if (QuestManager.questManager.CheckAvailableQuests(this)) //if this is an availablequest 
        {
           // Debug.Log("CheckAvailableQuests");
           // print(this);
           
             questMarker.SetActive(true);//set quest marker canvas to true. 
            questMarkerImage.sprite = questAvailableSprite;//image sprite the recievable sprite
            questMarkerImage.color = Color.red;//set the quest marker colour
           //DIALOG accepted
            AcceptedQuestDialog = false;//recievable Quest dialog
            CompletedQuestDialog = false;
            RecievableQuestDialog = true;

        }

        else if (QuestManager.questManager.CheckAcceptedQuests(this)) //if this is an accepted quest 
        {
           // Debug.Log("CheckAcceptedQuests");
           // print(this);
           
             questMarker.SetActive(true);//set quest marker canvas to true. 
            questMarkerImage.sprite = questRecievableSprite;//image sprite the recievable sprite
            questMarkerImage.color = Color.grey;//set the quest marker colour
            //DIALOG completed
            CompletedQuestDialog = false;//recievable Quest dialog
            RecievableQuestDialog = false;
            AcceptedQuestDialog = true;
        }

        else
        {
            questMarker.SetActive(false);//set quest marker canvas to false
            CompletedQuestDialog = false;//recievable Quest dialog
            RecievableQuestDialog = false;
            AcceptedQuestDialog = false;
        }

    }



    //--------> Calling the Quest Pannel from the Dialog. 

    // Update is called once per frame
    void Update () 
    {
        //        SetQuestObjectScriptActive();

        //if (initiateQuestScript == true)
        //{
        //    //SET CODE TO INACTIVE UNTIL PLAYER LOOKS AT IT. 
        //    QuestObjScript = this.gameObject.GetComponent<QuestObject>();
        //    QuestObjScript.enabled = !QuestObjScript.enabled;

        //}

      



        if (StartQuestFromDialog == true ) { 

        //if (!QuestUIManager.uiManager.questPanelActive)         //SET QUEST PANEL ACTIVE
          //  {
                // Debug.Log("Start Quest From Dialog Panel Showing");
                //QUEST UI MANAGER
                //ERROR --> 'THIS' IS NOT THE RIGHT QUEST BEING PASSED OVER. 
                Debug.Log("CheckQuests");
                print(this); //CORRECT
                print(StartQuestFromDialog); //CORRECT

                QuestUIManager.uiManager.CheckQuests(this);
             
                //ERROR --> 'THIS' IS NOT THE RIGHT QUEST BEING PASSED OVER. 
              //  QuestManager.questManager.QuestRequest(this);


                //QuestObject.isQuestfromDialog = false;
                //DialogSystemManager.passQuestfromDialog = false;
                StartQuestFromDialog = false;
               
            // }


        }
        if (CompleteQuestFromDialog == true)
        {
           // if (!QuestUIManager.uiManager.questPanelActive)
            //{
                Debug.Log("CompleteQuestFromDialog == true >>>>");
                print(questCode);
                print(this);
            //QUEST UI MANAGER
            QuestUIManager.uiManager.CheckQuests(this);
               // QuestManager.questManager.QuestRequest(this);
                //QuestObject.isQuestfromDialog = false;
                //DialogSystemManager.passQuestfromDialog = false;
                CompleteQuestFromDialog = false;

               // print(questCode);
           // }

        }



        SetQuestMarker();
    }

    //--------> Calling the Quest Pannel. 


    //public void SetQuestObjectScriptActive()
    //{

    //    if (FieldOfView.ActivateQuestObject == true)
    //    {
    //        print("ActivateQuestObject is true");
    //        //SET CODE TO INACTIVE UNTIL PLAYER LOOKS AT IT. 
    //        QuestObjScript.enabled = true;
    //    }

       


    //}



    //flag to make sure when we are in area of NPC & on button press then open up quest pannel.
    //and if the NPC has an available quest in the quest manager. 
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            print("enable script");
            inTrigger = false;

        }

    }

    private void OnTriggeExit(Collider other)
    {

        if (other.tag == "Player")
        {
            print("disable script");
            inTrigger = false;
          
        }

    }


}
