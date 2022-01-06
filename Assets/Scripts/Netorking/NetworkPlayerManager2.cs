using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class NetworkPlayerManager2 : MonoBehaviourPunCallbacks
{

    public GameObject[] playerPrefabs;         //stored player prefabs
    //public Transform[] spawnPoints;             //

    public List<GameObject> spawnPoints = new List<GameObject>();

    public GameObject playerInScene;

    PhotonView PV;

    [Header("Single PLayer Instnace")]
    public PlayerSO playerSavedData;

    public SceneSettings sceneSettings;

    private void Awake()
    {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("spawnPoint"))
        {

            spawnPoints.Add(fooObj);
        }


        if (SceneSettings.Instance.isSinglePlayer == true)
        {
            SceneSettings.Instance.RemoveMultiplayerScript(this.gameObject);
        }


        if (SceneSettings.Instance.isMultiPlayer == true)
        {
            PV = GetComponent<PhotonView>();
        }


    }

    // Start is called before the first frame update
    void Start()
    {

        if (sceneSettings.isMultiPlayer == true)
        {

            Debug.Log("Player Prefab = " + playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]]);
            Debug.Log("Player ID = " + ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]));

            //if ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"] > 0)
            //{

            //}
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
