using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSettings : MonoBehaviourPunCallbacks
{


    private static SceneSettings _instance;

    public static SceneSettings Instance { get { return _instance; } }

    [Header("PlayerData")]
    public PlayerSO playerSOData;

    [Header("Game Mode ")]
    public bool isSinglePlayer;
    public bool isMultiPlayer;
    public int connectionType;

    [Header(" players ")]
    public GameObject humanPlayer;
    public float playerdistance;
    public List<GameObject> humanPlayers = new List<GameObject>();


    [Header(" Steam ")]
    public GameObject steamSettings;
    public bool enableSteamSetttings;

    [Header(" Scenes ")]
    public SceneReference NextScene;


    [Header(" UI ")]
    public Image HealthImage;
    //  PhotonView PV;
    //  private GameObject[] players;
    // public GameObject myPlayer;

    public bool DebugMode;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Scene settings Instance is here ".Bold().Color("green"));
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;


            if (playerSOData.SingleOrMultiPlayer == 1)
            {
                // 1 = multiplayer
                //2 = single player
                isMultiPlayer = true;
                connectionType = 1;
                isSinglePlayer = false;

            }

            else if (playerSOData.SingleOrMultiPlayer == 2)
            {
                isSinglePlayer = true;
                connectionType = 2;
                isMultiPlayer = false;
            }



            if (isSinglePlayer == false && isMultiPlayer == false)
            {
                Debug.LogError("SceneSettings : Set bool to either single or multiplayer");
            }
        }

        if (enableSteamSetttings)
        {
            steamSettings.gameObject.SetActive(true);
        }

        else if (enableSteamSetttings == false) 
        {
            if (steamSettings != false)
            {
                steamSettings.gameObject.SetActive(false);
            }
       
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveMultiplayerScript(GameObject subject)
    {
        if (subject.gameObject.GetComponent<PhotonTransformView>() == true)
        {
            Destroy(subject.gameObject.GetComponent<PhotonTransformView>());
        }

        if (subject.gameObject.GetComponent<PhotonView>() == true)
        {
            Destroy(subject.gameObject.GetComponent<PhotonView>());
        }
    }



    public void RestartGame()
    {

        if (SceneSettings.Instance.isSinglePlayer)
        {
            SceneManager.LoadScene(NextScene);
        }

        if (SceneSettings.Instance.isMultiPlayer)
        {
            // PhotonNetwork.LeaveRoom();

            SwitchLevel(NextScene);

        }



    }
    public void SwitchLevel(SceneReference level)
    {
        StartCoroutine(DoSwitchLevel(level));
    }

    IEnumerator DoSwitchLevel(string level)
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        SceneManager.LoadScene(NextScene);
    }




}
