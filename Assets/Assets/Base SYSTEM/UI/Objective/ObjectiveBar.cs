using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveBar : MonoBehaviour
{

    public static ObjectiveBar Instance { get;  set; }

    [Header("Timer")]
   // public GameObject timerTextGO;
    public TextMeshProUGUI timerLabel;
    private float time;

    [Header("Who Cant See You")]
    public Image Guards;
    [Tooltip("WHO IS SHE. Used to determine what costumes fool this character")]
    public string WhoAmIGuard;
    public Image Staff;
    [Tooltip("WHO IS SHE. Used to determine what costumes fool this character")]
    public string WhoAmIStaff;
    public Image Camera;
    [Tooltip("WHO IS SHE. Used to determine what costumes fool this character")]
    public string WhoAmICamera;

    [Header("Eye Open/Closed")]
    public Sprite EyeOpen;
    public Sprite EyeClosed;

    [Header("What Costume am I wearing")]
    public Image CostumeUIContainer;


    [Header("Checks")]
    private bool checkTheCostume;
    private EquipmentManager equipmentManager;
    private bool fooledByCostume;


    [Header("Item Info")]
    public Equipment currentEquipedItem;
    public int SlotIndexNumber;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {
            //   Destroy(gameObject);

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EquipmentManager>();
      //  timerLabel = timerTextGO.gameObject.GetComponent<TextMeshPro>();

        if (Guards != null)
        {
            //Setting the icon opacity to 0
            Color g = Guards.color;
            g.a = 0;
            Guards.color = g;
        }

        if (Staff != null)
        {
            //Setting the icon opacity to 0
            Color s = Staff.color;
            s.a = 0;
            Staff.color = s;
        }

        if (Staff != null)
        {
            //Setting the icon opacity to 0
            Color c = Camera.color;
            c.a = 0;
            Camera.color = c;
        }

        if (Staff != null)
        {
            //Setting the icon opacity to 0
            Color d = CostumeUIContainer.color;
            d.a = 0;
            CostumeUIContainer.color = d;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimerClock();
    }



    public void UnequipAllUI()
    {
        if (Guards != null)
        {
            //Setting the icon opacity to 0
            Color g = Guards.color;
            g.a = 0;
            Guards.color = g;
        }

        if (Staff != null)
        {
            //Setting the icon opacity to 0
            Color s = Staff.color;
            s.a = 0;
            Staff.color = s;
        }

        if (Camera != null)
        {
            //Setting the icon opacity to 0
            Color c = Camera.color;
            c.a = 0;
            Camera.color = c;
        }

        if (CostumeUIContainer != null)
        {
            //Setting the icon opacity to 0
            Color d = CostumeUIContainer.color;
            d.a = 0;
            CostumeUIContainer.color = d;

        }
    }



    public void UnequipItemFromUI()
    {
        for (int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
        {
            EquipmentManager.instance.Unequip(SlotIndexNumber);
        }



        //Setting the icon opacity to 0
        Color g = Guards.color;
        g.a = 0;
        Guards.color = g;

        //Setting the icon opacity to 0
        Color s = Staff.color;
        s.a = 0;
        Staff.color = s;

        //Setting the icon opacity to 0
        Color c = Camera.color;
        c.a = 0;
        Camera.color = c;

        //Setting the icon opacity to 0
        Color d = CostumeUIContainer.color;
        d.a = 0;
        CostumeUIContainer.color = d;


    }




    public void checkCostume()
    {
        checkTheCostume = true;

        if (checkTheCostume == true)
        {
            //Check the equipemnt manager for any equipment that may blind enemy to player
            if (equipmentManager.currentEquipment.Length > 1)
            {
                foreach (Equipment equipment in equipmentManager.currentEquipment)
                {
                    if (equipment != null)
                    {


                        // generate a random number between 1-10, take away 'stealth' bonus, if number above e.g. 5 then Not Fooled by costume.
                        //float StealthCheck = Random.Range(0.0f, 10.0f);
                        //StealthCheck -= equipment.stealth;
                        //print("Stealth" + StealthCheck);

                        CostumeUIContainer.sprite = equipment.icon;
                        //Setting the icon opacity to 0
                        Color d = CostumeUIContainer.color;
                        d.a = 1;
                        CostumeUIContainer.color = d;


                        currentEquipedItem = equipment;

                        if (equipment.IFoolTheCamera == true)
                        {
                            //fooledByCostume = true;
                            //Debug.Log("IM A BLIND SECURITY GUARD");
                            //checkTheCostume = false;
                            print("Update Camera UI");

                            //Setting the icon opacity to 0
                            Color c = Camera.color;
                            c.a = 1.0f      ;
                            Camera.color = c;
                        }


                         if (equipment.IFoolTheGuards == true)
                        {
                            print("Update Guard UI");

                            //Setting the icon opacity to 0
                            Color g = Guards.color;
                            g.a = 1.0f;
                            Guards.color = g;

            
                        }



                         if (equipment.IFoolTheStaff == true)
                        {
                            print("Update Staff UI");

      

                            //Setting the icon opacity to 0
                            Color s = Staff.color;
                            s.a = 1.0f;
                            Staff.color = s;

                        }



                        else
                        {
                            print("I Dont fool anyone");
                        }
                        break;
                    }

                    if (equipment == null)
                    {
                        fooledByCostume = false;
                        break;
                    }


                    break;
                }

            }
        }


    }

    public void TimerClock()
    {

        time += Time.deltaTime;
        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.
        var fraction = (time * 100) % 100;

        if (timerLabel != null) 
        {
            //update the label value
            timerLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }


    }
}
