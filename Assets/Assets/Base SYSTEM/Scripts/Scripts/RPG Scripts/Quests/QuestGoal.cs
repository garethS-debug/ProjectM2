using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal  
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;
    

    public bool isReached() //checking if quest completed
    {
        return (currentAmount >= requiredAmount);//if this is true, return true for the function. 
    }


    //--------------------> List of quests 



    //---->     1. ENEMY KILL Quest
    public void EnemyKilled() //ENEMY KILL QUEST
    {
        if (goalType == GoalType.Kill) {
            currentAmount++; //increase by one. 
        }
    }
    //---->     ENEMY KILL Quest




    //---->     1. Item Collect Quest
    public void itemCollected() //COLLECT QUEST 
    {
        if (goalType == GoalType.Gathering)
        {
            currentAmount++; //increase by one. 
        }
    }
    //---->     1. Item Collect Quest

   



        //----->Set Quest Titles here
    public enum GoalType
    {
        Kill,
        Gathering,
        //add other goals as needed
    }
}