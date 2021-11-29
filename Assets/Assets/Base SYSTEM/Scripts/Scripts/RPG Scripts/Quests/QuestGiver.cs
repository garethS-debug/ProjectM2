using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour {

    public Quest quest;
    public PlayerQuest player;
    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text goldText;

    public KeyCode guiInput = KeyCode.Q;
    public bool guiOpen = false;

    public void Start()
    {
        questWindow.SetActive(false);
       
    }

    public void Update()
    {
        if (Input.GetKeyDown(guiInput))
        {
            OpenQuestWindow();
        }

    }



    public void OpenQuestWindow()
    {
        guiOpen = true;
        //ADD CALL FUNTION VIA BUTTON OR OTHER CODE 
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        //give quest to player
        player.quest = quest;
    }


}
