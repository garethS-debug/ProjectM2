using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Selevtor : MonoBehaviour
{
    [Header("Level Select Exit")]
    [Tooltip("Exit out of the level selction")]
    public KeyCode levelSelectCode = KeyCode.L;
    [Tooltip("Exit Back to Hub - enter hub level name")]
    public string BacktoHub;

    [Space(10)]

    [Header("Level Selector")]
    [Tooltip("Reference to level selector")]
    public MainMenu levelSelect;
    // public SceneFader fader;
    [Tooltip("Buttons in the level select")]
    public Button[] levelButtons;

    public GameObject levelSelectMainMenu;

#if UNITY_EDITOR
    public Object scene;
#endif


    public void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 0);   //highest level that has been reached. Get the Int from Player pref. Default level is 1, when none is available. 

        print("LevelReached = " + levelReached);

        for (int i = 0; i < levelButtons.Length; i++)               //go through array. on each instance
        {
            print(levelButtons[i].gameObject.GetComponent<LevelSelectInformation>().LevelID);

            if (levelButtons[i].gameObject.GetComponent<LevelSelectInformation>().LevelID  > levelReached)      //if the level of the button is less than what has been achieved previously.  
            {
                levelButtons[i].interactable = false;               // toggles off the interactability 
                                                                    // Button buttonImage = levelButtons[i].gameObject.GetComponent<Button>();
                

                Color cat = levelButtons[i].targetGraphic.color;
                cat.a = 0.8f;
                levelButtons[i].targetGraphic.color = cat;

            }
           
        }   
    }

    public void Update()
    {

        if ( Input.GetKeyDown(levelSelectCode))// CHANGE THE PICKUP BUTTON HERE

        {

            SceneManager.LoadScene(BacktoHub);
        }

    }

    public void SelectLevel (Object levelName)
    {
     
        levelSelect.levelToLoad = levelName.name;
        levelSelect.Play();
        
    }

    //public void SelectLevel2(Object level)
    //{

    //    SceneManager.LoadScene(level.name);
    //   // levelSelect.Play();

    //}

    public void LevelPreview(GameObject preview)
    {

        //preview.gameObject.SetActive(true);
        preview.gameObject.GetComponent<Canvas>().enabled = true;
        GameObject ChildGameObject1 = preview.gameObject.transform.GetChild(1).gameObject;
        ChildGameObject1.GetComponent<GraphicRaycaster>().enabled = true;
        // preview.gameObject.GetComponent<GraphicRaycaster>().enabled = true;
        preview.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
        levelSelectMainMenu.gameObject.SetActive(false);


    }

    public void BackToMissionSelect(GameObject preview)
    {

        preview.gameObject.GetComponent<Animator>().SetBool("isClosed", true);
        StartCoroutine(ExampleCoroutine(preview));
      

    }

    IEnumerator ExampleCoroutine(GameObject preview)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1f);
        preview.gameObject.GetComponent<Animator>().SetBool("isClosed", false);
        preview.gameObject.GetComponent<Animator>().SetBool("isOpen", false);
        levelSelectMainMenu.gameObject.SetActive(true);

         preview.gameObject.GetComponent<Canvas>().enabled = false;
        GameObject ChildGameObject1 = preview.gameObject.transform.GetChild(1).gameObject;
        ChildGameObject1.GetComponent<GraphicRaycaster>().enabled = false;

        //  preview.gameObject.SetActive(false);



        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

//#if UNITY_EDITOR
//    public void OnValidate()
//    {
//        sceneName = "";

//        if (scene != null)
//        {
//            if (scene.ToString().Contains("(UnityEngine.SceneAsset)"))
//            {
//                sceneName = scene.name;
//            }
//            else
//            {
//                scene = null;
//            }
//        }
//    }
//#endif

}
