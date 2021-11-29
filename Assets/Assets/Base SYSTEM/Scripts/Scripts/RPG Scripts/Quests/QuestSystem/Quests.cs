using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Quests 
 {
    public enum QuestProgress {NOT_AVAILABLE, AVAILABLE, ACCEPTED,COMPLETE, DONE} // states of quests

    public string title;            //TITLE OF THE QUEST
    public int id;                  //ID Number for the quest 
    public QuestProgress progress;  //state of the current quest (see enum)   
    public string description;      //string from our quest Giver/Reciever 
    public string hint;             //Hint for the quest. 
    public string congratulations;  //String to complete quest from quest giver 
    public string summary;          //string from the quest giver/reciever
    public int nextQuest;           // the next quest in the chain if there is any. 

    public string questObjective;   //name of quest objective (also for remove)
    public int questObjectiveCount; //current  number of quest objective 
    public int questObjectiveRequired; //required amount for quest

    public int xpReward;            // XP reward
    public int goldReward;          // gold reward
    public string itemReward;       //  item reward
}

