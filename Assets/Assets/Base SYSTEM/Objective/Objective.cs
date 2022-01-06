using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objective : MonoBehaviour
{
    [Header("Images")]
    public GameObject compas;
    public GameObject pointer;
    //  public float compensate;
    //   public Transform Player;

    [Header("Icons")]
    public Sprite UIMainObjective_Door;
    public Sprite UIMainObjective_Key;
    public Image MainObj;

    

    [Header("Spotted Icons")]
    public TextMeshProUGUI spottedText;
    public TextMeshProUGUI spottedScore;

    [Header("References")]
   // public GameObject player;                                                         //Unable to directly reference in multiplayer
    public GameObject target;

    public GameObject ObjectiveGO;

    public static Objective instance;

    [Header("Score")]
    public float Score;
    public float SpottedPenetlies;
    public float TotalScore;
    public float SpottedNo;
    public bool updatingScore;
    public float Cash;
    public float RequiredMastery;
   // public List<GameObject> MoneyBags = new List<GameObject>();
  //  private int MaxScore;


    [Header("Level Information")]
    [Tooltip("Determines whether this is a Main Level or a Planning Level")]
    public LevelInformation levelInformation;
   // public bool isThisAPlanningLevel;
   // public bool isThisAMainHeist;

    [Header("Tutorial Info")]
    [Tooltip("Information for the tutorial Level")]
    public bool isTutorial;
    public bool isAlarmRaised;
    public PopupBox tutorialPopUpbox;
  

    [Header("Events to Activate / deactivate")]
    [Tooltip("Determines whether Gameobjecst should be activated / deactivated based based on the planning level")]
  //  public GameObject compassContainer;
 //   public GameObject FoodEventGO;
  //  public GameObject MechanicEventGO;
 //   private int AmountofGuards;
    //public int GuardAmountWithRota;
    //public int GuardAmountWithoutRota;
    public List<GameObject> Guards = new List<GameObject>();
    //public GameObject[] Guards;

    [Header("What Should we Activate")]
    [Tooltip("Determines What should be activated")]
    public bool ActivateFoodEvent;
    public bool ActivateGuardRota;
    public bool ActivateKeyLocation;
    public bool ActivateMechanicEvent;


    [Header("UI")]
    public GameObject TaskMenu;
    public KeyCode TaskMenuButton;
    public bool TaskMenuActive;
    private Animator TaskmenuAnim;



    //   public bool Test;

    // public Transform ObjectiveTransform;

    //Vector3 dir;


    //  private Vector3 targetPosition;
    //  public RectTransform pointerRectTransform;

    // Start is called before the first frame update

    void Awake()
    {
        instance = this.gameObject.GetComponent<Objective>();

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
        }
        instance = this; // creating a static variable.
        //Item2Reveal = new List<GameObject>();
      
    }


    void Start()
    {
        /*                                                          //-- Removed -- 06/01/2021

        if (player == null)                                      //Unable to directly reference in multiplayer
        {
            if (SceneSettings.Instance.humanPlayer != null)
            {
                player = SceneSettings.Instance.humanPlayer;
             //   break;
            }

            if (SceneSettings.Instance.humanPlayer == null)
            {
                Debug.Log("Human player not available");
            }
           // break;
        
         }
     
        */

        //-- Removed -- 06/01/2021
        /*
        if (Objective.instance != null)
        {
            //      targetPosition = ObjectiveTransform.transform.position;
            if (UIMainObjective_Key != null)
            {
                MainObj.sprite = UIMainObjective_Key;
            }

        }
        */
        isAlarmRaised = false;
        TaskMenuActive = false;

        if (TaskMenu != null)
        {
            TaskmenuAnim = TaskMenu.gameObject.GetComponent<Animator>();
        }
       

        InformationToSave.instance.SaveLevelInformation();

       
    }

    void Update()
    {

       if(Input.GetKeyDown(TaskMenuButton) && InventoryUI.closeOtherUI == false)
        {
            TaskMenuActive = !TaskMenuActive;

            if (TaskMenuActive == true)
            {
                TaskmenuAnim.SetBool("isOpenFile", true);
                TaskmenuAnim.SetBool("isCloseFile", false);
            }

            if (TaskMenuActive == false)
            {
                TaskmenuAnim.SetBool("isCloseFile", true);
                TaskmenuAnim.SetBool("isOpenFile", false);
            }
        }

        if (InventoryUI.closeOtherUI == true && TaskMenuActive == true)
        {
            TaskmenuAnim.SetBool("isCloseFile", true);
            TaskmenuAnim.SetBool("isOpenFile", false);
            TaskMenuActive = false;

        }



        if (isAlarmRaised == true)
        {
                                                      
            tutorialPopUpbox.tutorialPopUp();                                                                         //Show popup
        }


        /*      //-- Compas Removed -- 06/01/2021

        Vector3 dir = transform.InverseTransformDirection(player.transform.position - target.transform.position);
        float angle = Mathf.Atan2(-dir.x, dir.z) * Mathf.Rad2Deg;
        compas.transform.eulerAngles = new Vector3(0, 0, angle-180f);
        */


    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }


    public void UpdateObjectiveMarker(GameObject ObjectiveGO)
    {

                                                                                                                        //Update Main Icon
        MainObj.sprite = UIMainObjective_Door;
        target = ObjectiveGO;
                                                                                                                          //Update Desired Object
    }


    

    public void UpdateScorePenelty(float penetlies)                                                                         //This determine level score
    {
        updatingScore = true;

        SpottedNo += 1;
        SpottedPenetlies += penetlies;

        TotalScore = Score - SpottedPenetlies;

        levelInformation.TotalScore = TotalScore;
        levelInformation.SpottedPenetlies = SpottedPenetlies;
        levelInformation.SpottedNo = SpottedNo;

        spottedText.text = SpottedNo.ToString("f0");
        spottedScore.text = SpottedPenetlies.ToString("f0");

    }

    public void SaveScore()
    {
        updatingScore = true;

        TotalScore = Score - SpottedPenetlies;

        levelInformation.Score = Score;
        levelInformation.TotalScore = TotalScore;
        levelInformation.SpottedPenetlies = SpottedPenetlies;
        levelInformation.SpottedNo = SpottedNo;

        spottedText.text = SpottedNo.ToString("f0");
        spottedScore.text = SpottedPenetlies.ToString("f0");
    }



    public void CashPrize(float CashAmount)
    {
        levelInformation.Cash += CashAmount;
        Debug.Log("CashAmount" + CashAmount);
    }

    

 
   
}
