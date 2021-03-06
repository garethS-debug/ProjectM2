using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScroll : MonoBehaviour
{

    public static ShowScroll Instance { get; private set; }

    [Header("Scroll Bar")]
    [SerializeField] private GameObject ScrollBarContainerObj;
    [SerializeField] private Animator ScrollBarAnimator;

    [Header("Camera")]
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Camera UICamera;

    [Header("Camera")]
    [SerializeField] private GameObject disableHealth;
    [SerializeField] private GameObject disableDialog;
    [SerializeField] private GameObject disablePickup;
    //[SerializeField] private GameObject disableNoise;

    [Header("Backpack")]
    public GameObject BackPack;
    public Transform BackPackspawnPoint;
    private GameObject tempBackpack;

    [Header("References")]
    public Transform InventoryCamPOS;
    private Transform playerTransform;
    [HideInInspector]
    public GameObject player;
    private Animator anim;
 

    public Vector3 originalPos;
    public ResetPOS resetPOS;

    private void Awake()
    {
        
        originalPos = new Vector3(48.24016f, -20f, -147.0188f);
        resetPOS = ScrollBarContainerObj.gameObject.GetComponent<ResetPOS>();

       // Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        UICamera.enabled = false;



        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {
            //   Destroy(gameObject);
            Debug.LogError("<color=red>Error: </color>PLEASE CHECK THIS");
        }
    }

    private void Start()
    {
        if (SceneSettings.Instance.humanPlayer != null)
        {
            player = SceneSettings.Instance.humanPlayer;
            playerTransform = player.transform;
            anim = player.gameObject.GetComponent<Animator>();
        }
    }

    public void ShowBars()
    {

        if (anim == null)
        {
            player = SceneSettings.Instance.humanPlayer;
            playerTransform = player.transform;
            anim = player.gameObject.GetComponentInChildren<Animator>();
        }


        print("Show Bars");
        ScrollBarContainerObj.SetActive(true);

        //Kneeling animation
        anim.SetBool("isKneeling", true);


        //Create Backpack
        if (BackPackspawnPoint == null)
        {
            player = SceneSettings.Instance.humanPlayer;
            BackPackspawnPoint = player.GetComponentInChildren<murphyPlayerController>().backPackSpawnPoint.transform;
        }
        Instantiate(BackPack, BackPackspawnPoint.position, BackPackspawnPoint.transform.rotation);
        tempBackpack = GameObject.FindGameObjectWithTag("BackPack");



        //OverShoulder Camera
        /*
        if (UICamera == null || InventoryCamPOS == null || playerTransform == null)
        {
            player = SceneSettings.Instance.humanPlayer;
            playerTransform = player.transform;
            InventoryCamPOS = player.GetComponentInChildren<murphyPlayerController>().inventoryCamPOS.transform;
        }
        UICamera.enabled = true;
        UICamera.transform.position = InventoryCamPOS.transform.position;
        UICamera.transform.LookAt(playerTransform);

        MainCamera.enabled = false;
          */
        disablePickup.SetActive(false);
        disableDialog.SetActive(false);
        disableHealth.SetActive(false);
      
        
        
        //  disableNoise.SetActive(false);
        // ScrollBarAnimator.SetTrigger("ShowScroll");
        //  StartCoroutine(Show());

        SceneSettings.Instance.humanPlayer.gameObject.GetComponentInChildren<murphyPlayerController>().enabled = false;
    }


    public void HideBars()
    {

        //Kneeling animation
        anim.SetBool("isKneeling", false);

        //Destroy Backpack
        Destroy(tempBackpack);

        ScrollBarContainerObj.transform.position = new Vector3(48.24016f, -20f, -147.0188f);
      //  print(ScrollBarContainerObj.name);
       // print("Hide");
      
        if (ShowScroll.Instance != null )
        {
            StartCoroutine(HideAndDisable());
        }

        if (ShowScroll.Instance == null)
        {
            Debug.LogError("<color=red>Error: </color>PLEASE CHECK THIS");
            print(ShowScroll.Instance.gameObject.name);
        }


        // ScrollBarAnimator.SetBool("OpenScroll", false);


    }


    private IEnumerator HideAndDisable()
    {
        ScrollBarAnimator.SetTrigger("HideScroll");
        // cinematicBarAnimator.SetBool(")

       

        yield return new WaitForSeconds(0.1f);

       
        /*

        UICamera.enabled = false;
        MainCamera.enabled = true;
        */
        disablePickup.SetActive(true);
        disableDialog.SetActive(false);
        disableHealth.SetActive(true);
        ScrollBarContainerObj.SetActive(false);

        SceneSettings.Instance.humanPlayer.gameObject.GetComponentInChildren<murphyPlayerController>().enabled = true;
    }

    private IEnumerator Show()
    {
        yield return new WaitForSeconds(0.5f);
        ScrollBarAnimator.SetBool("OpenScroll", true);
    }

    }
