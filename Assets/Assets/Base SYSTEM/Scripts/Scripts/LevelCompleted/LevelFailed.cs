using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFailed : MonoBehaviour
{
    [Header("Direct Player to Failed Level")]
    [Tooltip("Where to send the player upon failure")]
    public string NavigateTo;

    public GameObject GameOvereUI;
    public Text Score;
    public Text Spotted;
    public Text TotalScore;

    public static LevelFailed instance { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            //   Destroy(gameObject);
            Debug.LogError("<color=red>Error: </color>PLEASE CHECK THIS: Level Failed");
        }
    }

    public void Start()
    {
        if (GameOvereUI != null)
        {
            GameOvereUI.SetActive(false);
        }
      if (Score != null )
        {
            Score = Score.gameObject.GetComponent<Text>();
        }

      if (Spotted != null)
        {
            Spotted = Spotted.gameObject.GetComponent<Text>();
        }

      if (TotalScore != null)
        {
            TotalScore = TotalScore.gameObject.GetComponent<Text>();
        }
        
       
        
    }


    public void FailedLevel()
    {
        GameOvereUI.SetActive(true);



        TotalScore.text = Objective.instance.TotalScore.ToString("f0"); //Penelty Points;
        Score.text = Objective.instance.Score.ToString("f0"); //Penelty Points;
        Spotted.text = Objective.instance.SpottedPenetlies.ToString("f0"); //Penelty Points;

        SceneLoad.instance.GameIsOver = true;

        // SceneManager.LoadScene(NavigateTo);

    }

    public void Retry()
    {
       
        MurphyPlayerManager.instance.ResartLevel();
    }

    public void Menu()
    {
        InformationToSave.instance.SaveLevelInformation();

        SceneManager.LoadScene(NavigateTo);
     
    }
}
