using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{

    [Header("Level Completed")]
    [Tooltip("Level Completed Bool")]
    public bool LevelComplete;

    [Header("Next Level")]
    [Tooltip("Next Level")] 
    public string nextLevel = "level02";
    public int levelToUnlock = 2;

    [Header("UI")]
    public GameObject CompleteUI;
    public Text Score;
    public Text Spotted;
    public Text TotalScore;
    public Image MasteryIcon;


    [Header("Events Completed")]
    public Image event1;
    public Image event2;
    public Image event3;
    public Image event4;


    [Header("Bonus")]
    public Image neverspottedTick;
    public Image neverCCTVTick;
    public Image neverCrimeTick;
    public Image completedLevel;

    public static LevelCompleted instance { get; set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()

        
    {

        if (neverspottedTick != null)
        {
            neverspottedTick.enabled = false;
        }
        
        neverCCTVTick.enabled = false;
        neverCrimeTick.enabled = false;
        completedLevel.enabled = false;

        LevelComplete = false;
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);   //highest level that has been reached. Get the Int from Player pref. Default level is 1, when none is available. 

       
    }

    // Update is called once per frame
    void Update()
    {

        //if (LevelComplete == true)
        //{
        //    WinLevel();
        //    LevelComplete = false;
        //}
        
    }

    public void WinLevel()
    {
        InformationToSave.instance.SaveLevelInformation();

        if (LevelComplete == false)
        {
          //  Objective.instance.Score += 100;
            LevelComplete = true;
        }

        //add score

        //  Objective.instance.UpdateScore(0);

        // Objective.instance.Score += 100;
        Objective.instance.SaveScore();

    
        //check to see if the bonus objectives have been completed
        for (int i = 0; i < Objective.instance.levelInformation.eventID.Length; i++)
        {
            if (Objective.instance.levelInformation.eventID[i] == 1)   //update the event icon
            {
                event1.color = new Color(255, 255, 255, 255);  //Update Complete Menu
                //break;
            }



            if (Objective.instance.levelInformation.eventID[i] == 2)
            {
                event2.color = new Color(255, 255, 255, 255);  //Update Complete Menu
                //break;
            }

            if (Objective.instance.levelInformation.eventID[i] == 3)
            {
                event3.color = new Color(255, 255, 255, 255);  //Update Complete Menu
                //break;
            }


            if (Objective.instance.levelInformation.eventID[i] == 4)
            {
                event4.color = new Color(255, 255, 255, 255);  //Update Complete Menu
                //break;
            }

            //if ( i == Objective.instance.levelInformation.eventID.Length)
            //{
            //    Objective.instance.Score += 100;
            //    break;
            //}
        }

        if (levelToUnlock    > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }

        CompleteUI.SetActive(true);

        Objective.instance.TotalScore = Objective.instance.Score - Objective.instance.SpottedPenetlies;
        TotalScore.text = Objective.instance.TotalScore.ToString("f0"); //total Points;
        Score.text = Objective.instance.Score.ToString("f0"); //Score Points;
        Spotted.text = Objective.instance.SpottedPenetlies.ToString("f0"); //Spotted Points;



        if (Objective.instance.levelInformation.isCompleted == false)
        {
            Objective.instance.levelInformation.isCompleted = true;
        }

        //Mastery
       // MasteryIcon.fillAmount = (Objective.instance.TotalScore / Objective.instance.RequiredMastery); // CHANGE THIS TO 'BONUS OBJECTIVES COMPLETED
        // Mastery = Total Score + Bonus Objectives (e.g. 100 + 3 / Mastery of 100% 103)
        // At beginning of level scan for 'Money Bags' count  = Score max


        if (Objective.instance.levelInformation.neverSpotted == false)
        {

            neverspottedTick.enabled = true;
      
        }

        if (Objective.instance.levelInformation.neverSeenOnCCTV == false)
        {

          
            neverCCTVTick.enabled = true;
            
       
        }

        if (Objective.instance.levelInformation.neverCrime == false)
        {


            neverCrimeTick.enabled = true;
        }

        if (Objective.instance.levelInformation.isCompleted == true)
        {
            completedLevel.enabled = true;
        }


    }
}
