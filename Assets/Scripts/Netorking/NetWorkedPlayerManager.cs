using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(CapsuleCollider))]
//[RequireComponent(typeof(Animator))]



public class NetWorkedPlayerManager : MonoBehaviour
{

    public GameObject[] playerPrefabs;         //stored player prefabs
    //public Transform[] spawnPoints;             //

    public List<GameObject> spawnPoints = new List<GameObject>();

    public GameObject playerInScene;

    PhotonView PV;

    [Header("Single PLayer Instnace")]
    public PlayerSO playerSavedData;

    private void Awake()
    {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("spawnPoint"))
        {

            spawnPoints.Add(fooObj);
        }


   
    }


    // Start is called before the first frame update
    void Start()
    {
        if (SceneSettings.Instance == null)
        {
            Debug.Log("Scene settings instance null".Bold().Color("red"));
        }

        if (SceneSettings.Instance.isMultiPlayer == true)
        {

            Debug.Log("Player Prefab = " + playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]]);
            Debug.Log("Player ID = " + ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]));

            //if ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"] > 0)
            //{

            //}

        
                //Photon
                PV = GetComponent<PhotonView>();
          


            if (PV == null)
            {
                Debug.Log("PV not mine".Bold().Color("red"));
            }


            if (PV.IsMine == false)
            {
                Debug.Log("PV not also not".Bold().Color("red"));
            }


            if (PV.IsMine) // if owned by local players
            {
                Debug.Log("PV is mine ");
                if (spawnPoints.Count > 0)
                {
                    Debug.Log("Spawn points more than 0".Bold().Color("green"));
                    m_CreateController();
                }

                else if (spawnPoints.Count <= 0 )
                {
                    Debug.Log("Spawn points less than 0".Bold().Color("red"));
                }

            }
        }




        if (SceneSettings.Instance.isSinglePlayer == true)
        {
            SceneSettings.Instance.RemoveMultiplayerScript(this.gameObject);
            Debug.Log("Game Is Single Player : Removing Multiplayer Script ".Bold().Color("white"));
            if (spawnPoints.Count > 0)
            {
                s_CreateController();
                Debug.Log("Game Is Single Player : Spawn Points are move than 0 ".Bold().Color("white"));
            }

            if (spawnPoints.Count <= 0)
            {
                Debug.Log("not enough spawn points ".Bold().Color("red"));
            }
            }


        if (SceneSettings.Instance.isMultiPlayer == true)
        {
            PV = GetComponent<PhotonView>();
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void m_CreateController()
    {
        int randomNumber = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[randomNumber].transform;
        GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
        
        Debug.Log("Cretated Player Controller " + playerToSpawn.name);

        //  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
     
            playerInScene = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);

            playerInScene.gameObject.SetActive(true);

   
            SceneSettings.Instance.humanPlayer = playerInScene;
            SceneSettings.Instance.humanPlayers.Add(playerInScene);

       Debug.Log("Im  " + playerInScene.gameObject.name + " " + "Adding to  " + SceneSettings.Instance.humanPlayer);


    }

    void s_CreateController()
    {

        int randomNumber = Random.Range(0, spawnPoints.Count);
        Transform spawnPoint = spawnPoints[randomNumber].transform;

        Debug.Log("Cretating Player Controller:  " + playerPrefabs[playerSavedData.PlayerCharacterChoise]);
        Debug.Log("Spawn Points: " + spawnPoint);


        //Access Save File 
        GameObject playerToSpawn = playerPrefabs[playerSavedData.PlayerCharacterChoise];





        playerInScene = Instantiate(playerToSpawn, spawnPoint.position, spawnPoint.rotation);
        SceneSettings.Instance.humanPlayer = playerInScene;
        Debug.Log("Im  " + playerInScene.gameObject.name + " " + "Adding to  " + SceneSettings.Instance.humanPlayer);

    }
}
