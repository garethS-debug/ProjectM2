using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour 
{
    public int experience;
    public int gold;


    //public List<Quest> quests = new List<Quest>();
    public Quest quest; //must turn into list to have more than 1 quest at a time.

    public void Quest()
    {
        if (quest.isActive)
        {
            quest.goal.EnemyKilled();//enemy killed quest
            if (quest.goal.isReached())
            {
                experience += quest.experienceReward;
                gold += quest.goldReward;
                quest.Complete();
            }
        }
    }
}
