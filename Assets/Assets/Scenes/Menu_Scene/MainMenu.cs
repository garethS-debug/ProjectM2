using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transitionAnimation;
    public GameObject FadeTurnOff;
    public GameObject FadeGameObj;


    public string levelToLoad;


    public void Start()
    {
        transitionAnimation = FadeGameObj.gameObject.GetComponent<Animator>();
        StartCoroutine("turnOffFade");
    }

    public void Play ()
    {
        FadeTurnOff.SetActive(true);
        StartCoroutine("LoadScene");
        
    }

    public void Quit()
    {
        print("Quitting");
        Application.Quit();
    }

    IEnumerator LoadScene()
    {
        print("Fade Out");
        transitionAnimation.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelToLoad);
        
    }

    IEnumerator turnOffFade ()
    {
        yield return new WaitForSeconds(1.5f);
        FadeTurnOff.SetActive(false);
    }
}
