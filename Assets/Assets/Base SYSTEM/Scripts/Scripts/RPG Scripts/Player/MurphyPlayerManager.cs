using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MurphyPlayerManager : MonoBehaviour {

    public static MurphyPlayerManager Instance { get; set; }

    #region Singleton

    public static MurphyPlayerManager instance;

    [Header("Scenesettings")]
    public SceneSettings sceneSettings;


    [Header("Ragdoll")]
  [HideInInspector]  public GameObject playerRagdoll;
    private bool Raddolling = false;
    private float thrust = 10000.0f;
    private Rigidbody rb;

    [Header("Main Camera")]
    public Camera camera;


    [Header("Respawn Point")]
    public GameObject playerRespawnPoint;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    [Header("Player")]
    [HideInInspector]   public GameObject player;
//    public Vector3 playerPOS;
    public PlayerStats PlayerStats;


    [Header("Bullettime")]
    public float slowdownFactor = 0.05f; // moving 20x slower
    public float slowdownLength = 2.0f;




    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {
            Debug.LogError("Mroe than one player manager");
            Destroy(this.gameObject);
        }

        if (SceneSettings.Instance != false)

        {
            if (SceneSettings.Instance.isSinglePlayer)
            {
                Debug.LogError("Adding Player from SceneSettings");

                if (SceneSettings.Instance.humanPlayer == null)
                {
                    Debug.Log("Human player not available");
                }

         

                if (SceneSettings.Instance.humanPlayer != null)
                {
                    player = sceneSettings.humanPlayer;
                }


                PlayerStats = player.gameObject.GetComponent<PlayerStats>();

                playerRagdoll.SetActive(false);

                rb = playerRagdoll.gameObject.GetComponent<Rigidbody>();

            }
            if (SceneSettings.Instance.isMultiPlayer)
            {

                PlayerStats = player.gameObject.GetComponent<PlayerStats>();

                playerRagdoll.SetActive(false);

                rb = playerRagdoll.gameObject.GetComponent<Rigidbody>();


            }
        }
 

       

    }

    //Return to normal time after bullettime 
    public void Update()
    {
        Time.timeScale += (1.0f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
//        playerPOS = player.transform.position;
    }

    public void KillPlayer()
    {
        
        print("Player Dead");


        if (Raddolling == false)
        {


           
            foreach (Transform child in SceneSettings.Instance.humanPlayer.transform)
            {
                if (child.tag == "Player")
                    player = child.gameObject;
                Debug.Log("Correct Player found ".Bold().Color("green") + child.gameObject.name) ;
            }

           


            playerRagdoll = player.gameObject.GetComponentInChildren<CharacterStats>().RagdollGO;


            rb = playerRagdoll.gameObject.GetComponent<Rigidbody>();


            SceneLoad.instance.Respawn(player, playerRespawnPoint, playerRagdoll);

            // CameraOnRails.instance.followoffset = new Vector3(0, 5.91f, -4.9f);
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;



            playerRagdoll.transform.position = player.transform.position;
            playerRagdoll.SetActive(true);


            player.SetActive(false);
            //Move player to Respawn points to avoid AI going wrong
           // player.transform.position = playerRespawnPoint.transform.position;


            rb.AddForce(transform.forward * thrust);
            Raddolling = true;


          
            
        }
       
    }

    public void ResartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//restart the scene
    }


}
