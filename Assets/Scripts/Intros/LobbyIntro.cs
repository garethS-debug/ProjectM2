using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LobbyIntro : MonoBehaviour
{
    [Header("Camera")]
    public Camera cutsceneCamera;


    [Header("Delays")]

    public float cutSceneDelayAtStart = 2;
  //  public float cutSceneDelay = 5;

    [Header("References")]
    public Animator trainAnimator;
    public LobbyManager lobbyManager;

    public GameObject trainFollowCube;

    [Header("UI")]
    public GameObject LobbyUI;

    [Header("Debug")]
    public bool DebugSkipCutScene;

    

    [Header("Co-Courtines")]
    Coroutine theCutSceneCoRoutine;


    /*
     * 
     *   
  \n    New line
  \t    Tab
  \v    Vertical Tab
  \b    Backspace
  \r    Carriage return
  \f    Formfeed
  \\    Backslash
  \'    Single quotation mark
  \"    Double quotation mark
  \d    Octal
  \xd    Hexadecimal
  \ud    Unicode character
     */

    void Awake()
    {




    }

    // Start is called before the first frame update
    void Start()
    {

        LobbyUI.gameObject.SetActive(false);

        //   trainAnimator.SetBool("StartEntry", false);
        //Start the coroutine we define below named ExampleCoroutine.
        if (DebugSkipCutScene == false)
        {
            theCutSceneCoRoutine = StartCoroutine(CutSceneCoRoutine());
        }

        if (DebugSkipCutScene == true)
        {
     
        }

 


    }

    // Update is called once per frame
    void Update()
    {
        //cutsceneCamera.transform.LookAt(trainFollowCube.transform);
    }

    IEnumerator CutSceneCoRoutine()
    {

        yield return new WaitForSeconds(cutSceneDelayAtStart);


        LobbyUI.gameObject.SetActive(true);

   


    }







}
