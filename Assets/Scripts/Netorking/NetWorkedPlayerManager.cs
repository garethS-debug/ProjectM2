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


        if (SceneSettings.Instance.isMultiPlayer == true)
        {

            Debug.Log("Player Prefab = " + playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]]);
            Debug.Log("Player ID = " + ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]));

            //if ((int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"] > 0)
            //{

            //}
        }





        if (SceneSettings.Instance.isSinglePlayer == true)
        {
            if (spawnPoints.Count > 0)
            {
                s_CreateController();
            }
        }


        if (SceneSettings.Instance.isMultiPlayer == true)
        {
            if (PV.IsMine) // if owned by local players
            {
                if (spawnPoints.Count > 0)
                {
                    m_CreateController();
                }

            }
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

        Debug.Log("Im located on " + this.gameObject);
        //  PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);

            playerInScene = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
        
    }

    void s_CreateController()
    {
        
            int randomNumber = Random.Range(0, spawnPoints.Count);
            Transform spawnPoint = spawnPoints[randomNumber].transform;
            
        //Access Save File 
            GameObject playerToSpawn = playerPrefabs[playerSavedData.PlayerCharacterChoise];
           
        Debug.Log("Cretated Player Controller " + playerToSpawn.name);


            if (SceneSettings.Instance.isSinglePlayer == true)
     
                playerInScene = Instantiate(playerToSpawn, spawnPoint.position, spawnPoint.rotation);
 
    }
}
