using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header("Multi PLayer Instnace")]
    public static RoomManager Instance;
    public GameObject networkedPlayerManager;
    public SceneSettings sceneSettings;


    [Header("Single PLayer Instnace")]
  [HideInInspector]  public GameObject S_PlayerInstance;
    public GameObject s_PlayerManager;



  //  [Header("Grief Bar")]
 //   public GriefBarDisplay griefBar;

    //[Header("Intro")]

  //  public GameObject LevelIntro;



    private void Awake()
    {
        if (Instance)                                   //If room manager already in scene
        {
            Destroy(gameObject);                        //If yes destroy
            return;
        }

        DontDestroyOnLoad(gameObject);                      //I am the only 1


        Instance = this;
    }


    public void Start()
    {
        spawnPlayers();




        //   CreatePlayer();                                   //Works but not on Claire's version


        if (sceneSettings.isSinglePlayer == true)
        {
            SceneSettings.Instance.RemoveMultiplayerScript(this.gameObject);
        }

   



    }


    private void OnEnable()
    {
        Debug.Log("OnEnabled Called");
       // base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;      //Works on Claire's but not on 

    }



    private void OnDisable()
    {
        Debug.Log("OnDisabledd Called");
        //  base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode LoadSceneMode)
    {
  
        //start evtry scene 

        //then spawn in players

      //  LevelIntro.gameObject.GetComponent<Level01Cutscene>().StartCoroutine("CutSceneCoRoutine");

    }

    public void spawnPlayers()
    {


        if (sceneSettings.isMultiPlayer == true)
        {
            networkedPlayerManager = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
            Debug.Log("Game Is Multiplayer : ".Bold().Color("green"));
        }

        else
        {
            Debug.Log("Game Is Single Player : ".Bold().Color("white"));
            s_CreatePlayer();
        }


    //    LevelIntro.gameObject.SetActive(false);


    }



    public void s_CreatePlayer()
    {
   
        print("Creating Player : ".Color("Green"));
        S_PlayerInstance = Instantiate(s_PlayerManager, Vector3.zero, Quaternion.identity);
        // networkedPlayer.gameObject.GetComponent<NetworkedPlayerController>().isInLobby = false

        //griefBar.CreateDisplay();

    }


}


