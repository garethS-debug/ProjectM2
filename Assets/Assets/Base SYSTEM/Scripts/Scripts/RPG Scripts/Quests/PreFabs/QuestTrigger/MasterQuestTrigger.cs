using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterQuestTrigger : MonoBehaviour {

     public int questID;                 //quest Object Must match name in Quest Manager
     public int AddXtoQuest;                 //adds number to quest goal


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered trigger");
            QuestManager.questManager.AddtoQuestObjective(questID, AddXtoQuest);

        }

    }
}
