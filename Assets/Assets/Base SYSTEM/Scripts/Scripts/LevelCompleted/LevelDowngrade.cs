using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDowngrade : MonoBehaviour
{

    public string NavigateTo = "Level Select";

   public void LevelReset()
    {

        PlayerPrefs.SetInt("levelReached", 1);
        SceneManager.LoadScene(NavigateTo);

    }
}
