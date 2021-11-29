using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager Instance { get; set; }

    #region Singleton

    public static PlayerManager instance;


    [Header("Ragdoll")]
    public GameObject playerRagdoll;
    private bool Raddolling = false;
    private float thrust = 10000.0f;
    private Rigidbody rb;

    [Header("Main Camera")]
    public Camera camera;


    private void Awake()
    {
        instance = this;
    }
    #endregion
    [Header("Player")]
    public GameObject player;
    public Vector3 playerPOS;
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
        }

        PlayerStats = player.gameObject.GetComponent<PlayerStats>();

        playerRagdoll.SetActive(false);
       
        rb = playerRagdoll.gameObject.GetComponent<Rigidbody>();
    }

    //Return to normal time after bullettime 
    public void Update()
    {
        Time.timeScale += (1.0f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        playerPOS = player.transform.position;
    }

    public void KillPlayer()
    {
        
        print("Player Dead");


        if (Raddolling == false)
        {
           // CameraOnRails.instance.followoffset = new Vector3(0, 5.91f, -4.9f);
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            player.SetActive(false);
            playerRagdoll.transform.position = player.transform.position;
            playerRagdoll.SetActive(true);
            rb.AddForce(transform.forward * thrust);
            Raddolling = true;
            SceneLoad.instance.ExitScene();
            
        }
       
    }

    public void ResartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//restart the scene
    }


}
