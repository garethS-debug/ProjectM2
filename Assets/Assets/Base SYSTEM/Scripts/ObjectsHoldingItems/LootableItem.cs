using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LootableItem : MonoBehaviour
{
    [TextArea]
    public string Notes = "Alarm Box - Use this to disable Alarm, set tag to Alarm Box.Set to Universal loot"; // Do not place your note/comment here. 
                                                                                                                // Enter your note in the Unity Editor.


    //[Header("Reference to player")]
    //[Tooltip("Reference to player controller to judge distance")]
    //private PlayerManager playerManager;
    //public GameObject player;
    [Header("InteractionProperties (AutoSet)")]
    [Tooltip("references to player and interaction point")]
    [HideInInspector] public Transform player;
    [HideInInspector] public Transform interactionTransform;
    [HideInInspector] public GameObject PickUpTextBox;
    [Space(10)]

    [Header("TextBox")]
    [Tooltip("What message do you want to send to the text box")]
    public string BoxTextNeeded;// Text to be placed in text box when correct costumer needed
    public string BoxTextSuccess; // Text to be placed in text box when correct costume in inventory

    [Space(2)]

    [Header("Interaction")]
    [Tooltip("Whats the 'GoDownstairs' Button")]
    public KeyCode LootItem = KeyCode.L;
    //  public string FloorToLoad;
    public bool ItemLooted;
    public bool playerInProximity;


    [Header("Available Items")]
    [Tooltip("What Items are available to loot")]
    public bool SpawnableLoot;
    public GameObject lootableItems;
    public float SpawnDistance;
    public Transform spawnLoc;

    [Header("Loot Bar")]
    //Reference to character stats
    public int currentLooting;
    private float maxLoot = 100;
    public float lootPercent;
    public float Difficulty;

    [Header("Universal or Costume Specific")]
    public bool UniversalLoot;
    public bool CostumeSpecific;
    public string CostumeNeeded;
    private bool CorrectCostume;
    private bool checkTheCostume;

    [Header("Alarm")]
    public List<GameObject> ListOfCCTV = new List<GameObject>();
    [TextArea]
    public string AlarmNotes = "List of all effected alarms FOV"; // Do not place your note/comment here. 


    [Header("Event Sytem ")]
    [Tooltip("Food Stands call guards when activated")]
    public int eventID;
    public bool thisIsGatheringEventSite; // Event Site
    public bool thisIsAnEventSite;
    public bool startEvent;
     public bool startEventSequence;
    public List<Transform> FoodGatheringLocations = new List<Transform>();
    public float DistractionDistance;
    [TextArea]
    public string EventNotes = "Set even distance so only a range of NPC's are effected. Gathering Event site = Call AI's // Even site is a shout for a listening script"; // Do not place your note/comment here. 


    [Header("Characters distracted by Event")]
    public bool DistractGuard;
    public bool DistractStaff;
    public bool DistractCivilian;
    public bool DistractPolice;

    [Header("Highlight")]
    public Material MaterialSwap;
    public Material OriginalMaterial;

    [Header("Progress Bar")]
    public GameObject uiLootable;
    private Image LootImage;

    [Header("Prompt")]
    public GameObject Prompt;
    Image lootSlider;


    [Header("Effects")]
    public ParticleSystem PickupVFX;
    private bool hasPickupEffect;


    [Header("UI/Score")]
 
    public GameObject TaskMenuGO;
    private TextMeshProUGUI textmesh;
    public string textOnObjective;
    public Image PassMenuImage;
    public int MasteryScore;

    private void Start()
    {

        player = PlayerManager.instance.player.transform;
        interactionTransform = this.gameObject.transform;
        //  PickUpTextBox = GameObject.FindGameObjectWithTag("ItemPickUpText");
        PickUpTextBox = MasterPickupMessage.Instance.pickupmessageText;
        Prompt.SetActive(false);
        
        if (TaskMenuGO != false)
        {
            textmesh = TaskMenuGO.gameObject.GetComponent<TextMeshProUGUI>();
            textmesh.SetText(textOnObjective);
        }
      
    }

    private void Update()
    {

       


        //DEBUGGING EVENT
        if (startEvent == true)
        {
            StartEvent();
        }

        // player = PlayerManager.instance.player.transform;           //Reference to the player
        // PickUpTextBox = GameObject.FindGameObjectWithTag ( "ItemPickUpText" );

        while (ItemLooted == false && Input.GetKey(LootItem) && playerInProximity == true)// CHANGE THE PICKUP BUTTON HERE

        {
           // Difficulty = Difficulty - PlayerManager.instance.PlayerStats.lockPick.GetValue();
          //  print("I Am" + Difficulty + " Difficult to open");

            if (UniversalLoot == true)
            {   

                LootImage = uiLootable.GetComponentInChildren<Image>();
                lootSlider = LootImage;



                if (lootPercent < maxLoot)
                {
                    Prompt.SetActive(false);
                    float difficult = Difficulty - PlayerManager.instance.PlayerStats.lockPick.GetValue();
                    uiLootable.SetActive(true);
                    lootPercent += difficult * Time.deltaTime;
                    lootSlider.fillAmount = lootPercent / maxLoot;
                    print("I Am" + difficult + " Difficult to open");

              
                }

                if (lootPercent >= maxLoot)
                {
                    uiLootable.SetActive(false);

                    if (this.gameObject.tag != "AlarmBox")
                    {
                        if (TaskMenuGO != false)
                        {
                            textmesh.faceColor = new Color32(53, 53, 53, 100); // Set objective to 'complete' i.e greyed out 
                              
                            //CHECK LEVEL INFORMATION IF THIS EVENT HAS ALREADY TRIGGERED
                            //If there is an event ID already there, the even has already triggered.
                            for (int i = 0; i < Objective.instance.levelInformation.eventID.Length; i++)
                            {

                                if (Objective.instance.levelInformation.eventID[i] == eventID)   //Do Nothing as event already happened
                                {
                                 
                                    break;
                                }



                                if (Objective.instance.levelInformation.eventID[i] <= 0)
                                {
                                    Objective.instance.levelInformation.eventID[i] = eventID;
                                    PassMenuImage.color = new Color(255, 255, 255, 255);  //Update Complete Menu
                                    Objective.instance.Score += MasteryScore; //Update masery Score
                                    //Do Nothing
                                    break;
                                }

                               

                            }

                       
                        }


                        PickupEffect();
                        Prompt.SetActive(false);
                        if (SpawnableLoot == true)
                        {
                            Instantiate(lootableItems, spawnLoc.transform.position, transform.rotation);
                        }

                        if (thisIsAnEventSite == true)
                        {
                            startEvent = true;
                        }

                        if (thisIsGatheringEventSite)
                        {
                            startEvent = true;
                        }
         
                        ItemLooted = true;
                    }

                    if (this.gameObject.tag == "AlarmBox")
                    {
                        PickupEffect();
                        //Toggle Alarm on or OFF
                        Alarm.Instance.FalseAlarm();
                        foreach (GameObject CCTV in ListOfCCTV)
                        {
                            CCTV.gameObject.SetActive(false);
                        }

                        if (thisIsAnEventSite == true)
                        {
                            startEvent = true;
                        }

                        // Mess with the alarm. 
                    }

                }

            }

            if (CostumeSpecific == true)
            {
             


                checkCostume();
                checkTheCostume = true;
                print("Interactable: Check Costume");

                if (CorrectCostume == true)
                {
                    print("You have the correct costume sir");

                    LootImage = uiLootable.GetComponentInChildren<Image>();
                    lootSlider = LootImage;



                    if (lootPercent < maxLoot)
                    {
                        float difficult = Difficulty - PlayerManager.instance.PlayerStats.lockPick.GetValue();
                        Prompt.SetActive(false);
                        uiLootable.SetActive(true);
                        lootPercent += difficult * Time.deltaTime;
                        lootSlider.fillAmount = lootPercent / maxLoot;
                    }

                    if (lootPercent >= maxLoot)
                    {
                        if (TaskMenuGO != false)
                        {
                            textmesh.faceColor = new Color32(53, 53, 53, 100); // Set objective to 'complete' i.e greyed out

                            //CHECK LEVEL INFORMATION IF THIS EVENT HAS ALREADY TRIGGERED
                            //If there is an event ID already there, the even has already triggered.
                            for (int i = 0; i < Objective.instance.levelInformation.eventID.Length; i++)
                            {
                                if (Objective.instance.levelInformation.eventID[i] == eventID)   //Do Nothing as event already happened
                                {
                           
                                    break;
                                }



                                if (Objective.instance.levelInformation.eventID[i] <= 0)
                                {
                                    Objective.instance.levelInformation.eventID[i] = eventID;
                                    PassMenuImage.color = new Color(255, 255, 255, 255);  //Update Complete Menu
                                    Objective.instance.Score += MasteryScore; //Update masery Score
                                    break;
                                }



                            }


        

                        }
                        Prompt.SetActive(false);
                        PickupEffect();
                            
                        uiLootable.SetActive(false);

                        if (this.gameObject.tag != "AlarmBox")
                        {
                            //CHECK FOR EVENT SYSTEM I.E If this is a Hotdog Stand, Call all guards.
                           // StartEvent();
                            startEvent = true;

                          //  Instantiate(lootableItems, spawnLoc.transform.position, transform.rotation);
                            ItemLooted = true;
                        }

                        if (this.gameObject.tag == "AlarmBox")
                        {
                            //Toggle Alarm on or OFF
                            Alarm.Instance.FalseAlarm();


                            // Mess with the alarm. 
                        }

                    }

                }
            }

            // Instantiate(uiPrefab, Loottarget.transform);

            // lootPercent = currentLooting / maxLoot;

            break;
            //SceneManager.LoadScene(levelToLoad);


        }

        //if (Input.GetKeyUp(LootItem))
        //{
        //    if (lootPercent > 0)
        //    {
        //        lootPercent -= 5 * Time.deltaTime;
        //        lootSlider.fillAmount = lootPercent / maxLoot;
        //    }
        //}

        //if (EquipmentManager.ItemToReveal.name == item.transform.name)
        //{
        //    print("REVEAL MAG GLASS");
        //}

        Timer();
    }



    public void Timer()
    {
        //If the instance of Chase on this gameobject i.e if THIS character is attacking

        if (lootPercent <= 0)
        {

        }

        else
        {
            lootPercent -= 7 * Time.deltaTime;
            lootSlider.fillAmount = lootPercent / maxLoot;
        }


    }



    #region Colliders Detecting Player 
    //Collision Detection function
    //---------------------->
    private void OnTriggerEnter(Collider collider)
    {
        //collider = Colider of the game object
        if ((collider.gameObject.tag == "Player") && ItemLooted == false)
        {


            if (UniversalLoot== true && ItemLooted == true)
            {
            
              //  MasterPickupMessage.Instance.PickupMessageImage.GetComponent<Image>().sprite = MasterPickupMessage.Instance.MurphyCelebrating;
                PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = BoxTextSuccess;
                playerInProximity = true;
            }


            if (UniversalLoot == true && ItemLooted == false)
            {

                //  MasterPickupMessage.Instance.PickupMessageImage.GetComponent<Image>().sprite = MasterPickupMessage.Instance.MurphyCelebrating;
                PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = BoxTextNeeded;
                playerInProximity = true;
            }



            if (CostumeSpecific == true)
            {



                playerInProximity = true;
                //  PickupText.gameObject.SetActive(true);
                // PickUpTextBox.SetActive(true);

                MasterPickupMessage.Instance.turnOnnPickupMessage = true;

                if (PickUpTextBox != null)
                {

                    checkTheCostume = true;
                    checkCostume();

                    print("Interactable: Check Costume");

                    if (CorrectCostume == true)
                    {
                        print("Interactable: Check Costume is Correct");
                        MasterPickupMessage.Instance.PickupMessageImage.GetComponent<Image>().sprite = MasterPickupMessage.Instance.MurphyCelebrating;
                        PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = BoxTextSuccess;
                    }

                    if (CorrectCostume == false)
                    {
                        print("Interactable: Check Costume is False");
                        MasterPickupMessage.Instance.PickupMessageImage.GetComponent<Image>().sprite = MasterPickupMessage.Instance.MurphyThinking;
                        PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = BoxTextNeeded;
                    }

                }

              
            }
            this.gameObject.GetComponent<MeshRenderer>().material = MaterialSwap;
            Prompt.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if ((collider.gameObject.tag == "Player") && ItemLooted == false)
        {
            if (MasterPickupMessage.Instance.turnOnnPickupMessage == false)
            {
                MasterPickupMessage.Instance.turnOnnPickupMessage = true;
            }
            
        }

        if((collider.gameObject.tag == "Player") && ItemLooted == true)
        {

            if (UniversalLoot == true )
            {

                //  MasterPickupMessage.Instance.PickupMessageImage.GetComponent<Image>().sprite = MasterPickupMessage.Instance.MurphyCelebrating;
                PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = BoxTextSuccess;
                playerInProximity = true;
            }
        }
    }


        private void OnTriggerExit(Collider collider)
    {
        //collider = Colider of the game object
        if (collider.gameObject.tag == "Player")
        {
            playerInProximity = false;
            //  PickupText.gameObject.SetActive(false);
            if (PickUpTextBox != null)
            {
                PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = "";
            }

            // PickUpTextBox.SetActive(false);
            MasterPickupMessage.Instance.turnOffPickupMessage = true;


            CorrectCostume = false;
            this.gameObject.GetComponent<MeshRenderer>().material = OriginalMaterial;
            Prompt.SetActive(false);
        }
    }
    //Collision Detection function
    #endregion




    public void checkCostume()
    {
        checkTheCostume = true;

        if (checkTheCostume == true)
        {
            //Check the equipemnt manager for any equipment that may blind enemy to player
            if (EquipmentManager.instance.currentEquipment.Length > 0)
            {
                foreach (Equipment equipment in EquipmentManager.instance.currentEquipment)
                {
                    if (equipment != null)
                    {


                        // generate a random number between 1-10, take away 'stealth' bonus, if number above e.g. 5 then Not Fooled by costume.
                        float StealthCheck = Random.Range(0.0f, 10.0f);
                        StealthCheck -= equipment.stealth;
//                        print("Stealth" + StealthCheck);


                        foreach (string whoDoIFools in equipment.WhoDoIFools)
                        {
                            if (CostumeNeeded == whoDoIFools)
                            {
                                CorrectCostume = true;
                                print("WE HAVE A HOTDOG");
                            }

                            else
                            {
                                CorrectCostume = false;
                                Debug.Log("Lootable Object: I cant see you ");
                                checkTheCostume = false;
                                break;
                            }
                            break;
                        }

                    

                        
                        break;
                    }

                    if (equipment == null)
                    {
                        CorrectCostume = false;
                        break;
                    }


                    break;
                }

            }
        }
    }


    public void EventTime(GameObject itemLocation, float distance)
    {
        Debug.Log("Its Food Time!");
        if (DistractGuard == true)
        {
            //foreach (GuardPatrol enemyPatrol in guardPatrol)
            //{
            //    enemyPatrol.FoodGatheringLocations = FoodGatheringLocations;
            //    enemyPatrol.isRunningToFood = true;
            //    enemyPatrol.isOnPatrol = false;

            //}
            foreach (GameObject guard in Alarm.Instance.Guards)
            {


                if (Vector3.Distance(guard.transform.position, itemLocation.transform.position) < distance)
                {
                    guard.gameObject.GetComponent<StateController>().distractionEventActive = true;
                    guard.gameObject.GetComponent<StateController>().distractionEventLocation = itemLocation;
                    Debug.Log("Set the table! for " + guard.gameObject.name);
                }


                //// print(Vector3.Distance(guard.transform.position, itemLocation.transform.position) + guard.transform.name + " distance " + distance);

                // foreach (StateController statecontroller in guardStateController)
                // {
                // if (Vector3.Distance(guard.transform.position, itemLocation.transform.position) > distance)
                // {
                //     statecontroller.distractionEventActive = true;
                //     statecontroller.distractionEventLocation = itemLocation;
                //     Debug.Log("Set the table! for " + statecontroller.gameObject.name);
                // }

                // else
                // {
                //     Debug.Log("None for me!");
                // }
                // }


            }
        }

        if (DistractPolice == true)
        {

        }

        }

        public void StartEvent()
    {
        //FOOD STAND EVENT
        if (thisIsGatheringEventSite == true && startEvent == true)
        {
           // Alarm.Instance.FoodGatheringLocations = FoodGatheringLocations;
            // Alarm.Instance.EventTime(this.gameObject, DistractionDistance);
            EventTime(this.gameObject, DistractionDistance);

            if (DistractCivilian == true)
            {
                Alarm.Instance.DistractCivilian = true;
            }

            if (DistractGuard == true)
            {
                Alarm.Instance.DistractGuard = true;
            }

            if (DistractPolice == true)
            {
                Alarm.Instance.DistractPolice = true;
            }

            if (DistractStaff == true)
            {
                Alarm.Instance.DistractStaff = true;
            }

            // startEvent = false;
            Invoke("SetEventBoolBack", 3.0f);
        }

        if (thisIsAnEventSite == true && startEvent == true)
        {
            startEventSequence = true;
        }

            

       
    }


    private void SetEventBoolBack()
    {
      
            startEvent = false;
            startEventSequence = false;

        
    }

    public void PickupEffect()
    {
        if (hasPickupEffect != true)
        {
            PickupVFX.Play();
            hasPickupEffect = true;
        }
    }
}
       
