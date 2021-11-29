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



    public void ExitScene()
    {
        GameIsOver = true;
        //  StartCoroutine("ExitTheScene");
        Color alarm = CutoutUIImage.color;
        alarm.a = 1;
        CutoutUIImage.color = alarm;
        transitionUI.SetActive(true);
        transitionAnimation.SetTrigger("IsFadeOut");
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

