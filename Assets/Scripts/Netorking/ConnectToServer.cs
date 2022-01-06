using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks            //Photon callback = called automatically when a certain event happens
{

    public TMP_InputField userNameInput;
    public TMP_Text buttonText;

    public SceneReference levelToLoad;

    [Header("SO")]
    public PlayerSO playerSOData;

    [Header("Autoconnect")]
    public GameObject AutoConnectUI;
    public GameObject manualLogin;
    public float loadingProgress;

    [Header("Loading Bar")]
    public Slider slider;
    public TMP_Text loadingVlaue;



    public void Awake()
    {
        if (playerSOData.AutoConnect == true)
        {
            AutoConnectUI.SetActive(true);
            userNameInput.text = playerSOData.PlayerName;
            PhotonNetwork.NickName = userNameInput.text;            //Setting players username to Pun's 'Nickname'
            buttonText.text = "Connecting....";
            PhotonNetwork.AutomaticallySyncScene = true;            //Client scene can be changed by master 
            PhotonNetwork.ConnectUsingSettings();                   //Connect to Photon server
            manualLogin.gameObject.SetActive(false);
        }

        else
        {
            AutoConnectUI.SetActive(false);
            manualLogin.gameObject.SetActive(true);
        }
    }
    public void OnClickConnect()
    {
        if (userNameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = userNameInput.text;            //Setting players username to Pun's 'Nickname'
            buttonText.text = "Connecting....";
            PhotonNetwork.AutomaticallySyncScene = true;            //Client scene can be changed by master 
            PhotonNetwork.ConnectUsingSettings();                   //Connect to Photon server
        }
    }

    public override void OnConnectedToMaster()
    {


        StartCoroutine(LoadAsync());


    }

    IEnumerator LoadAsync ()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);                            //Copy to lobby scene. 
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingProgress = operation.progress;
            slider.value = loadingProgress;
            loadingVlaue.text = progress * 100f + "%";
            yield return null; //wait for next frame
        }
    }

 

}
