using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public static QuestManager questManager;

    
    public List<Quests> questlist = new List<Quests>();           //Master Quest List
    public List<Quests> currentQuestList = new List<Quests>();    //Current Quest List





    //private vars for our QuestObject (NPC Character / Quest Provider)

    private void Awake()
    {
         if (questManager == null)                              //Make sure quest manager doesnt duplicate
        {
            questManager = this;
        }
         else if (questManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);  
    }

   

    //ERROR --- > The Loop settles on the wrong Quest

    public void QuestRequest(QuestObject NPCQuestObject) //ID passed from quest Object. We did that by sending 
    {
  //      Debug.Log("QuestRequest(QuestObject NPCQuestObject) ");//CORRECT
//        print(NPCQuestObject); //CORRECT

        //AVAILABLE QUEST CHECK
        if (NPCQuestObject.availableQuestIDs.Count > 0)//if there anything in the available quest count. 
        {
    //        Debug.Log("NPCQuestObject.availableQuestIDs.Count ");//CORRECT
           // print(NPCQuestObject.availableQuestIDs);//CORRECT


            for (int i = 0; i < questlist.Count; i++) //checking the quest list one at a time. 
            {
                //loop through the available quest list. 
                //nested for loop
                for (int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
                {
                    //when we have something that is matching. We check IDs while going through. 
                    if (questlist[i].id == NPCQuestObject.availableQuestIDs[j] && questlist[i].progress == Quests.QuestProgress.AVAILABLE)
                    {


                       // Debug.Log("questlist[i].id"); //CORRECT
                       // print(questlist[i].id);//CORRECT
                       // Debug.Log("NPCQuestObject.availableQuestIDs[j]"); //CORRECT
                       // print(NPCQuestObject.availableQuestIDs[j]); //CORRECT

                        //CORRECT
                        Debug.Log("Quest ID: " + NPCQuestObject.availableQuestIDs[j] + " " + questlist[i].progress); //Return with what quest is available. 

                        // pass information to quest UI Manager
                        //QuestUIManager.uiManager.questAvailable = true;
                        //QuestUIManager.uiManager.availableQuests.Add(questlist[i]);   //add quest to list

                        //when we have an available quest in here, in the progrress. 
                        //we send back some info to the QUI manager
                       
                        QuestUIManager.uiManager.questAvailable = true; //setting the quest available to true. 
                        QuestUIManager.uiManager.availableQuests.Add(questlist[i]);//Populate the active quest list

                    }
                 //   Debug.Log("STOP HERE 1");

                   
                }
              //  Debug.Log("STOP HERE 2");
            }
          //  Debug.Log("STOP HERE 3");
        }


        //SECOND QUEST IS LOOPING THROUGH THE SECOND FOR LOOP. 
        //The active quest loop should only be accessed when checking for a completed quest
        //Maybe add a if check when being passed the info. 

        //-----------> TESTING PUTTING THE SECOND FOR LOOP IN SEPERATE THING




        //------------>


    }

    //_______.... BUG IS FROM HERE 

    // if (QuestObject.CompleteQuestFromDialog == true)
    //    {
    //        Debug.Log("CompleteQuestFromDialog == true");  //correct
    //        print(questObject);  //Correcy
    //          QuestManager.questManager.ActiveQuestRequest(questObject);
    //}
    //_______.... BUG IS FROM HERE 

    public void ActiveQuestRequest(QuestObject CompletedQuestObject) //ID passed from quest Object. We did that by sending 
    {
        //print(CompletedQuestObject); //Correct
        //print(currentQuestList.Count); //2
        //print(currentQuestList); //?

        //ACTIVE QUESTS
        for (int i = 0; i < currentQuestList.Count; i++) //checking the quest list one at a time. 
        {
            //Debug.Log("currentQuestList.Count>>>>>>>>>>>>>>>>>");
            //print(questlist[i].title); //NotCorrect
            //print(currentQuestList[i].title); //Not Correct 

            //print(CompletedQuestObject); //Correct

            //print(currentQuestList[i].id);
            //print(currentQuestList[i].progress);


            for (int j = 0; j < CompletedQuestObject.availableQuestIDs.Count; j++) //checking the quest list one at a time. 
            {

                //Debug.Log("NPCQuestObject.recievableQuestIDs.Count>>>>>>>>>>>>>>>>>");
                //print(CompletedQuestObject.recievableQuestIDs[j]);


                //when we have something that is matching. We check IDs while going through. 
                if (currentQuestList[i].id == CompletedQuestObject.recievableQuestIDs[j] && currentQuestList[i].progress == Quests.QuestProgress.ACCEPTED || currentQuestList[i].progress == Quests.QuestProgress.COMPLETE)
                {
                    //currentQuestList[i].progress = Quests.QuestProgress.DONE;                                                                                                                //Accept Quest TESTING
                    //CompleteQuest(NPCQuestObject.recievableQuestIDs[j]);
                    Debug.Log("Quest ID: " + CompletedQuestObject.recievableQuestIDs[j] + " is " + currentQuestList[i].progress); //return true.

                    // pass information to quest UI Manager
                    //CompleteQuest(NPCQuestObject.recievableQuestIDs[j]);    
                    //QuestUIManager.uiManager.questActive = true;
                    // QuestUIManager.uiManager.activeQuests.Add(activeQuests[i]);   //add quest to list

                    //Debug.Log("currentQuestList[i].id>>>>>>>>>>>>>>>>>>>>>");
                    //print(currentQuestList[i].id); // 3 (this is the first in the list)
                    //Debug.Log("NPCQuestObject.availableQuestIDs[j]");
                    //print(CompletedQuestObject.recievableQuestIDs[j]); //2 Available 


                    QuestUIManager.uiManager.questActive = true; // there is something in progress
                    QuestUIManager.uiManager.activeQuests.Add(currentQuestList[i]);//populate active quest list

                    Debug.Log("Current Quest Added :" + currentQuestList[i].title);

                }

            }
        }
    }










        //ACCEPT QUEST

        public void AcceptQuest(int questID)
    {
        for (int i=0; i <questlist.Count; i++)                                               //loop through. 
        {
           if (questlist[i].id == questID && questlist[i].progress == Quests.QuestProgress.AVAILABLE) //look for the ID
            {

                currentQuestList.Add(questlist[i]);                                                 //add to current quest list
               

                if (currentQuestList != null)
                {
                  //  currentQuestList[i].progress = Quests.QuestProgress.ACCEPTED;                      //quest state is accepted

                }

                // Debug.Log("questlist[i].id");
                print(questlist[i].id);

                Debug.Log("QUEST ACCEPTED");
                //  print(currentQuestList[i].id);
                // print(currentQuestList[i].title);

              
                questlist[i].progress = Quests.QuestProgress.ACCEPTED;                              //quest state is accepted
                                                                                                    //BUG IS HERE
                //currentQuestList[i].progress = Quests.QuestProgress.ACCEPTED;
            }
        }
    }




    //CANCEL QUEST
    public void GiveUpQuest(int questID) //when give up quest is called. 
    {
      //  Debug.Log("Give Up Quest from Log");

        for(int i=0; i< currentQuestList.Count; i++)
        {
           if( currentQuestList[i].progress == Quests.QuestProgress.ACCEPTED)
                //if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quests.QuestProgress.ACCEPTED)
                {
              //  Debug.Log("Trying to Give Up Quest from Log");
                currentQuestList[i].progress = Quests.QuestProgress.AVAILABLE;  //set quest to available
                currentQuestList[i].questObjectiveCount = 0;                    // reset the quest object count
                currentQuestList.Remove(currentQuestList[i]);                   //remove quest item
            }
        }
    }


    //COMPLETE THE QUEST
    public void CompleteQuest(int questID)
    {
        for(int i = 0; i < currentQuestList.Count; i++) //go through the quest list
        {
            if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quests.QuestProgress.COMPLETE)
            {
                currentQuestList[i].progress = Quests.QuestProgress.DONE;
                currentQuestList.Remove(currentQuestList[i]);
                //REWARDS

               // Debug.Log("currentQuestList[i].id");
                //print(currentQuestList[i].id);

                //Debug.Log("questID");
               // print(questID);
            }
        }
        //CHECK FOR CHAIN QUEST
        CheckChainQuest(questID);
    }


    //CHECK FOR CHAIN
    void CheckChainQuest(int questID)
    {
        //check if specific ID has a chain ID 
        int tempId = 0;
        for (int i = 0; i < questlist.Count; i++)
        {
            if(questlist[i].id == questID && questlist[i].nextQuest > 0) // if there is a chain quest available
            {
                tempId = questlist[i].nextQuest; //set this temp ID to the next quest

            }
        }
        if(tempId > 0 ) //unlock next quest
        {
            for(int i = 0; i < questlist.Count; i++)
            {
                if (questlist[i].id == tempId && questlist[i].progress == Quests.QuestProgress.NOT_AVAILABLE)
                {
                    questlist[i].progress = Quests.QuestProgress.AVAILABLE; // next quest is available. 
                }
            }
        }
    }


    //for (int i = 0; i < currentQuestList.Count; i++) //checking the quest list one at a time. 
    //{

    //for (int j = 0; j < CompletedQuestObject.availableQuestIDs.Count; j++) //checking the quest list one at a time. 
    //{

    //CURRENT BUG --- > When accepting Quests. The first quest to be accepted, needs to be completed first  
    //e.g. if first quest is accepted first then the first quest needs to be completed first in order for the complete quest system to work. 



    public void AddtoQuestObjective(int QuestID, int AddXtoQuest)
    {
        print("New QUest COmplete System Found);");
        print(QuestID);
        print(AddXtoQuest);

        //INformation Is being recieved ok. 
        for (int i = 0; i < questlist.Count; i++)
        {
            if (questlist[i].id == QuestID)
            {

                print("Success Master!!!!");
                print(questlist[i].id);

                if (questlist[i].questObjectiveCount < questlist[i].questObjectiveRequired & questlist[i].progress == Quests.QuestProgress.ACCEPTED)
                {
                    questlist[i].questObjectiveCount += AddXtoQuest;
                }


                if (questlist[i].questObjectiveCount >= questlist[i].questObjectiveRequired & questlist[i].progress == Quests.QuestProgress.ACCEPTED)
                {
                    questlist[i].progress = Quests.QuestProgress.COMPLETE;
                }


                break;

            }
        
  



        //    foreach (Quests LiveQuests in questlist)
        //{
            //print("LiveQuests.id");
            //print(LiveQuests.id);
            //print("questList");
            //print(questlist.Count);

            //if (LiveQuests.id == QuestID)
            //{
            //    print("LiveQuests.id == QuestID");
            //    print(LiveQuests.id);
            //    print(LiveQuests.title);
            //    print(QuestID);


            //    LiveQuests.questObjectiveCount += AddXtoQuest;
            //}


            //if (LiveQuests.questObjectiveCount >= LiveQuests.questObjectiveRequired & LiveQuests.progress == Quests.QuestProgress.ACCEPTED)
            //{
            //    LiveQuests.progress = Quests.QuestProgress.COMPLETE;
            //}


            ////if (LiveQuests.questObjectiveCount >= LiveQuests.questObjectiveRequired & LiveQuests.progress == Quests.QuestProgress.COMPLETE)
            ////{
            ////    Debug.Log("Break the New Quest script");
            ////    break;
            ////}


        }

    }



    ////ADD Items to the list (what we are collecting)
    //public void AddQuestItem(string questObjective, int itemAmount)        // add quest items list from any point. 
    //{
    //    Debug.Log("questObjective");
    //    print(questObjective);      //correct
    //    Debug.Log("itemAmount");
    //    print(itemAmount);      //correct


    //    //Loop through the current quest list. 
    //    //this will land on the '1st' quest in the list now. 

    //    for (int i = 0; i < currentQuestList.Count; i++)  //loop through quest to see what quest has the requested item
    //    {

    //        //for (int j = 0; j < questObjective.Length; j++) //checking the quest list one at a time. 
    //        //{

          

    //        //------->>> Debugging
    //        Debug.Log("currentQuestList >>>>>>>>>");
    //        print(currentQuestList.Count);                          //3
    //       // print(currentQuestList[i].id);                        //3
    //       // print(currentQuestList[i].progress);
    //        print(currentQuestList[i].questObjective);      //Leave Town 1
    //        print(questObjective);                          //Leave Town 2

          
    //        if (currentQuestList[i].questObjective == questObjective && currentQuestList[i].progress == Quests.QuestProgress.ACCEPTED)
    //        {
    //            //------->>> Debugging
    //           // Debug.Log("currentquestList[i]");
    //           // print(currentQuestList[i].questObjective);
    //           // print(currentQuestList[i].id);
    //           //------->>> Debugging

    //            currentQuestList[i].questObjectiveCount += itemAmount;
    //        }

    //        if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequired && currentQuestList[i].progress == Quests.QuestProgress.ACCEPTED)
    //        {
    //            //------->>> Debugging
    //            // Debug.Log("Error is here");
    //            // print(currentQuestList[i].progress);
    //            // print(currentQuestList[i].id);
    //            // print(currentQuestList[i]);
    //            // Debug.Log("Obj Count & Req");
    //            // print(currentQuestList[i].questObjectiveCount);
    //            // print(currentQuestList[i].questObjectiveRequired);
    //            //------->>> Debugging
    //            currentQuestList[i].progress = Quests.QuestProgress.COMPLETE; //set quest progrosss to be completed. 
    //        }

    //        if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequired && currentQuestList[i].progress == Quests.QuestProgress.COMPLETE)
    //        {
    //            Debug.Log("Break the script");
    //           return;
    //        }



    //    }

    //}


    //REMOVE Items from the list

    ////ADD Items to the list (what we are collecting)
    //public void AddQuestItem(string questObjective, int itemAmount)        // add quest items list from any point. 
    //{
    //    for (int i = 0; i < questlist.Count; i++)  //loop through quest to see what quest has the requested item
    //    {
    //        if (currentQuestList[i].questObjective == questObjective && currentQuestList[i].progress == Quests.QuestProgress.ACCEPTED)
    //        {
    //            currentQuestList[i].questObjectiveCount += itemAmount;
    //        }

    //        if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequired && currentQuestList[i].progress == Quests.QuestProgress.ACCEPTED)
    //        {
    //            currentQuestList[i].progress = Quests.QuestProgress.COMPLETE; //set quest progrosss to be completed. 
    //        }
    //    }

    //}












    //Bool for testing what we have, what is available, what has been acceptd

    public bool RequestAvailableQuest(int questID) // check from any point if a specific quest is accepted/available
    {
        for(int i = 0; i < questlist.Count; i++)
        {
            if (questlist[i].id == questID && questlist[i].progress == Quests.QuestProgress.AVAILABLE) //is the quest is available in the list
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID) // check from any point if a specific quest is accepted/available
    {
        for (int i = 0; i < questlist.Count; i++)
        {
            if (questlist[i].id == questID && questlist[i].progress == Quests.QuestProgress.ACCEPTED) //is the quest Accepted in the list
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompleteQuest(int questID) // check from any point if a specific quest is accepted/available
    {
        // Debug.Log("A completed Quest has been foind");
       // Debug.Log("questID");
       // print(questID);

        for (int i = 0; i < questlist.Count; i++)
        {
            if (questlist[i].id == questID && questlist[i].progress == Quests.QuestProgress.COMPLETE) //is the quest Completed in the list
            {

         //       Debug.Log("questlist[i].id");
           //     print(questlist[i].id); 

               // print(questlist[i].id);
                return true;
            }
        }
        return false;
    }


    //BOOLS 2 
    public bool CheckAvailableQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questlist.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.availableQuestIDs.Count; j++)
            {
                if (questlist[i].id == NPCQuestObject.availableQuestIDs[j] && questlist[i].progress == Quests.QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckAcceptedQuests(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questlist.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.recievableQuestIDs.Count; j++)
            {
                if (questlist[i].id == NPCQuestObject.availableQuestIDs[j] && questlist[i].progress == Quests.QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckCompletedQuests(QuestObject NPCQuestObject)
    {
//        Debug.Log("questlist.Count>>");
  //      print(questlist.Count);
    //    Debug.Log("NPCQuestObject>>");
      //  print(NPCQuestObject);

        for (int i = 0; i < questlist.Count; i++)

        {
            // Debug.Log("NPCQuestObject.recievableQuestIDs.Count");
            // Debug.Log(NPCQuestObject.recievableQuestIDs.Count);

            //  print(NPCQuestObject.recievableQuestIDs.Count); 


            //ERROR ?????????

            for (int j = 0; j < NPCQuestObject.recievableQuestIDs.Count; j++)
            {
                if (questlist[i].id == NPCQuestObject.recievableQuestIDs[j] && questlist[i].progress == Quests.QuestProgress.COMPLETE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //SHOW QUEST LOG 
    public void ShowQuestLog(int questID)
    {
        //iterate through current quest list
        for (int i = 0; i< currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID)
            {
                QuestUIManager.uiManager.ShowQuestLogPanel(currentQuestList[i]);
               // Debug.Log("QuestUIManager.uiManager.ShowQuestLogPanel(currentQuestList[i]);");
               // print(currentQuestList[i].title);
            }
        }
    }

}
