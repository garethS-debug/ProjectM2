using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    public Animator transitionAnimation;
    public GameObject FadeTurnOff;

    public static SceneLoad instance;

    [Header("Transition")]
    public GameObject transitionUI;
    public Image CutoutUIImage;

    public bool GameIsOver;

    private GameObject playerToReset;
    private GameObject respawnPoint;
    private GameObject ragdollGO;


    private void Awake()
    {
        transitionAnimation = transitionUI.gameObject.GetComponent<Animator>();
        //transitionUIImage = transitionUI.gameObject.GetComponent<Image>();
        StartCoroutine("LoadScene");
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        transitionUI.SetActive(true);
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator LoadScene()
    {
        //transitionAnimation.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        // FadeTurnOff.SetActive(false);
        // transitionUI.SetActive(false);
        Color alarm = CutoutUIImage.color;
        alarm.a = 0;
        CutoutUIImage.color = alarm;

    }

    public void Respawn (GameObject player, GameObject respawn, GameObject ragdoll)
    {
        Color alarm = CutoutUIImage.color;
        alarm.a = 1;
        CutoutUIImage.color = alarm;
        transitionUI.SetActive(true);
        transitionAnimation.SetBool("IsFadeOut", true);
     //   transitionAnimation.SetBool("IsFadeIn", false);

        playerToReset = player;
        respawnPoint = respawn;
        ragdollGO = ragdoll;


    StartCoroutine("RespawnDelay");

    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Resetting player POS");
        playerToReset.transform.position = respawnPoint.transform.position;
        // PlayerManager.instance.ResartLevel();
     //   
        transitionAnimation.SetBool("isFadeIn", true);
        transitionAnimation.SetBool("IsFadeOut", false);


        playerToReset.SetActive(true);
        ragdollGO.gameObject.SetActive(false);


        ResettingSceneForPlayer();
    }


    public void ResettingSceneForPlayer()
    {

        Debug.Log("Resetting scene for player");


    }



    public void ExitScene()
    {
        GameIsOver = true;
        //  StartCoroutine("ExitTheScene");
        Color alarm = CutoutUIImage.color;
        alarm.a = 1;
        CutoutUIImage.color = alarm;
        transitionUI.SetActive(true);
        transitionAnimation.SetBool("IsFadeOut", true);
        StartCoroutine("FailedLevel");

    }

    IEnumerator FailedLevel()
    {
        yield return new WaitForSeconds(4.5f);
        LevelFailed.instance.FailedLevel();
       // PlayerManager.instance.ResartLevel();
    }


  
    public void WinScene()
    {
        GameIsOver = true;
        //  StartCoroutine("ExitTheScene");
        Color alarm = CutoutUIImage.color;
        alarm.a = 1;
        CutoutUIImage.color = alarm;
        transitionUI.SetActive(true);
        transitionAnimation.SetTrigger("IsFadeOut");
       
        StartCoroutine("WinLevel");

    }

    IEnumerator WinLevel()
    {
        yield return new WaitForSeconds(4.5f);
        LevelCompleted.instance.WinLevel();
        // PlayerManager.instance.ResartLevel();
    }


}

