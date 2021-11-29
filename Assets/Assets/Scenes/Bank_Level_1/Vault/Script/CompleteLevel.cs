using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{

    [Header("Level Completed")]
    [Tooltip("Assigning the levelComplete variable in order to modify it")]
 //   private LevelCompleted levelcompleted;
    public GameObject GameManager;
    public string levelToLoad = "Level Select";

    [Header("Tutorial")]
    public bool isFinalTutorial;

    [Header("Cash Prize")]
    public float CashAward;

    // Start is called before the first frame update
    void Start()
    {

    //    levelcompleted = GameManager.gameObject.GetComponent<LevelCompleted>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            //  levelcompleted.LevelComplete = true;            //Completing the level
            // SceneManager.LoadScene(levelToLoad);

            // SceneLoad.instance.ExitScene();
            Objective.instance.CashPrize(CashAward);
            
            EquipmentManager.instance.UnequipAll();

            SceneLoad.instance.WinScene();

            if (isFinalTutorial == true)
            {
                InformationToSave.instance.savedInformation.tutorialCompleted = true;
            }


          
            
       
       
        }
    }

  
}
