    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Alarm : MonoBehaviour
{


    public static Alarm Instance { get; set; }

    [Header("List of Guards & Staff & Civilians")]
    public List<GameObject> Guards = new List<GameObject>();
    public List<GameObject> Staff = new List<GameObject>();
    public List<GameObject> Civilian = new List<GameObject>();



    [Header("List of FOV /AI Script")]
    //GUARDS
    private List<GuardAI> guardAI = new List<GuardAI>();
    private List<GuardPatrol> guardPatrol = new List<GuardPatrol>();
    [HideInInspector]  public List<StateController> guardStateController = new List<StateController>();
    //Staff
    private List<StaffAIController> staffAIController = new List<StaffAIController>();
    private List<StaffPatrol> staffPatrol= new List<StaffPatrol>();
    //Civilian AI's 
    private List<CivilianAI> civilianAIs = new List<CivilianAI>();
    //Police AI's 
    //private List<PoliceAI> policeAIs = new List<PoliceAI>();
    //private List<PolicePatrol> policePatrol = new List<PolicePatrol>();
    


    [Header("Alarm")]
    public bool RaiseAlarm;
    public bool falseAlarm;
    public float AlarmRange;
    public bool PlayerinCCTVFOV;

    [Header("Alarm Timer")]
    public float Alarmtime = 15.00f;
    public TextMeshProUGUI timerLabel;
    private float defaultAlarmTime;

    [Header("Costume Check")]
    public bool checkTheCostume;
    public bool fooledByCostume;

    [Header("police")]
    public List<GameObject> PoliceVan = new List<GameObject>();
    public List<GameObject> PoliceOfficers = new List<GameObject>();
    ////public List<Animator> policeAnim = new List<Animator>();
    //private Animator policeAnim;
    //private IEnumerator coroutine;
    //public float PoliceResponsetime;
    //private float PoliceResponsetimeMAX;
    //private GameObject TheMainPoliceVan;
    public bool callThePolice;
    //public bool DaPoliceAreHere;
    public float CallPoliceDistance;

    [Header("UI")]
    public Image AlarmUI;

    [Header("FoodTime")]
    public List<Transform> FoodGatheringLocations = new List<Transform>();

    [Header("Characters distracted by Event")]
    public bool DistractGuard;
    public bool DistractStaff;
    public bool DistractCivilian;
    public bool DistractPolice;

    [Header("CrimeCube")]
    public GameObject crimeCube;
    private float TimerTime;
    public float startTimerTime = 3.0f;

    [Header("Call Police")]
    public GameObject CallPoliceCube;
    public bool CivilianHasCalledPolice;
    public GameObject CalledPoliceCubedLocation;
    private Vector3 PoliceResetPOS;
    //public bool policehavebeencalled;

    [Header("Score")]
    private bool TurnOnOffScore;


    [Header("Distraction Timer")]
    public float Timer;
    private float TimerMax;


    [Header("RemoveProtection")]
    public bool removeCostumeProtection;
    public bool tempGuardCostumeRemoved;
    public bool tempStaffCostumeRemoved;
    public bool tempCameraCostumeRemoved;
    public Image GuardIcon;
    public Image CCTVIcon;
    public Image StaffIcon;

    [Header("Call for help")]
    public bool endCallForHelp;

    [Header("Guard")]
    private GuardPatrol guardpatrolscript;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Timer = TimerMax;

     //   PoliceResponsetimeMAX = PoliceResponsetime;

        defaultAlarmTime = Alarmtime;

        Color alarm = AlarmUI.color;
        alarm.a = 0;
        AlarmUI.color = alarm;

        Color alarmTimer = timerLabel.color;
        alarm.a = 0;
        timerLabel.color = alarm;

       


        foreach (GameObject guard in GameObject.FindGameObjectsWithTag("Enemy"))
        {

            Guards.Add(guard);
            // guardFOV.Add(guard.gameObject.GetComponent<EnemyFOV>());
            //  guardAI.Add(guard.gameObject.GetComponent<GuardAI>());
            //  guardPatrol.Add(guard.gameObject.GetComponent<GuardPatrol>());
            guardStateController.Add(guard.gameObject.GetComponent<StateController>());



        }

        foreach (GameObject staff in GameObject.FindGameObjectsWithTag("Staff"))
        {

            Staff.Add(staff);
            staffAIController.Add(staff.gameObject.GetComponent<StaffAIController>());
            staffPatrol.Add(staff.gameObject.GetComponent<StaffPatrol>());
        }

        foreach (GameObject civilian in GameObject.FindGameObjectsWithTag("Civilian"))
        {
            Civilian.Add(civilian);
            civilianAIs.Add(civilian.gameObject.GetComponent<CivilianAI>());
        }

        foreach (GameObject policeOfficer in PoliceOfficers)
        {
            PoliceResetPOS = policeOfficer.transform.position;

        }




    }

    // Update is called once per frame
    void Update()
    {

        //DEBUGG
        if (RaiseAlarm == true)
        {
            RaisingAlarm();
        }

        if (falseAlarm == true)
        {
            FalseAlarm();

        }

        if (removeCostumeProtection == true)
        {
            RemoveCostumeProtection();
        }

        //if (RaiseAlarm == false)
        //{
        //    FalseAlarm();
        //}
    }


    //public void EventTime(GameObject itemLocation, float distance)
    //{
    //    Debug.Log("Its Food Time!");
    //    if (DistractGuard == true)
    //    {
    //        //foreach (GuardPatrol enemyPatrol in guardPatrol)
    //        //{
    //        //    enemyPatrol.FoodGatheringLocations = FoodGatheringLocations;
    //        //    enemyPatrol.isRunningToFood = true;
    //        //    enemyPatrol.isOnPatrol = false;

    //        //}
    //        foreach (GameObject guard in Guards)
    //        {


    //            if (Vector3.Distance(guard.transform.position, itemLocation.transform.position) < distance)
    //            {
    //                guard.gameObject.GetComponent<StateController>().distractionEventActive = true;
    //                guard.gameObject.GetComponent<StateController>().distractionEventLocation = itemLocation;
    //                Debug.Log("Set the table! for " + guard.gameObject.name);
    //            }


    //               //// print(Vector3.Distance(guard.transform.position, itemLocation.transform.position) + guard.transform.name + " distance " + distance);

    //                // foreach (StateController statecontroller in guardStateController)
    //                // {
    //                // if (Vector3.Distance(guard.transform.position, itemLocation.transform.position) > distance)
    //                // {
    //                //     statecontroller.distractionEventActive = true;
    //                //     statecontroller.distractionEventLocation = itemLocation;
    //                //     Debug.Log("Set the table! for " + statecontroller.gameObject.name);
    //                // }

    //                // else
    //                // {
    //                //     Debug.Log("None for me!");
    //                // }
    //                // }


    //        }
          
    //    }

    //    //if (DistractPolice== true)
    //    //{

    //    //}

    //    //if (DistractStaff == true)
    //    //{
    //    //    foreach (StaffPatrol patrollingStaff in staffPatrol)
    //    //    {
    //    //        patrollingStaff.FoodGatheringLocations = FoodGatheringLocations;
    //    //        patrollingStaff.isRunningToFood = true;
    //    //        patrollingStaff .isOnPatrol = false;
    //    //        patrollingStaff.isSitting = false;
    //    //        patrollingStaff.isFiling = false;

    //    //    }
    //    //}

    //    //if (DistractCivilian == true)
    //    //{
    //    //    foreach (CivilianAI civilianAI in civilianAIs)
    //    //    {
    //    //        civilianAI.FoodGatheringLocations = FoodGatheringLocations;
    //    //        civilianAI.isRunningToFood = true;
        
    //    //    }
    //    //}


    //}


    public void GenerateCrimeCube(GameObject SceneOfcrime)
    {
        //Spawn Crime Cube
         GameObject ThecrimeCube = Instantiate(crimeCube, SceneOfcrime.transform.position, SceneOfcrime.transform.rotation);


        if (TimerTime <= 0)
        {
            Destroy(ThecrimeCube,3.0f);
     
        }
        else
        {
            //ADD animation of looking around while 'shocked'
          TimerTime  -= Time.deltaTime;
          
        }




        // CrimeCube SceneofTheCrime = ThecrimeCube.gameObject.GetComponent<CrimeCube>();
        //  SceneofTheCrime.crimeLocation = crime.transform;

        //if (gameObject.transform.tag == "Civilian")
        //{
        //    CivilianAI civilianAI = SceneOfcrime.gameObject.GetComponent<CivilianAI>();
        //    civilianAI.CrimeCubes.SetActive(true);
        //}



    }



    public void RaisingAlarm()
    {
        //if (Objective.instance.isThisAPlanningLevel == true)
        //{
        //    LevelFailed.instance.FailedLevel();
        //}


        if (TurnOnOffScore == false)
        {
           // Objective.instance.UpdateScore(10.0f); //Penelty Points;
            TurnOnOffScore = true;
        }

        Objective.instance.isAlarmRaised = true;


     

        //if (Objective.instance.SpottedNo < 1)
        //{
        //    Objective.instance.SpottedNo += 1;
        //    Objective.instance.spottedText.text = Objective.instance.SpottedNo.ToString("f0");
        //    Objective.instance.levelInformation.SpottedNo = Objective.instance.SpottedNo;

        //    Objective.instance.TotalScore = Objective.instance.Score - Objective.instance.SpottedPenetlies;
        //    Objective.instance.levelInformation.TotalScore = Objective.instance.TotalScore;

        //}

        //if (Objective.instance.SpottedPenetlies < 10)
        //{
        //    Objective.instance.SpottedPenetlies += 10.0f;
        //    Objective.instance.spottedScore.text = Objective.instance.SpottedPenetlies.ToString("f0");
        //    Objective.instance.levelInformation.SpottedPenetlies = Objective.instance.SpottedPenetlies;
        //}



        //  RaiseAlarm = true;

        Color alarm = AlarmUI.color;
        alarm.a = 1;
        AlarmUI.color = alarm;

        Color alarmTimer = timerLabel.color;
        alarm.a = 1;
        timerLabel.color = alarm;




        ////GUARDS
        //if (Alarm.Instance.CivilianHasCalledPolice == false) //stop guards responding to police call
        //{
        //    foreach (GuardAI enemyAI in guardAI)
        //    {
        //       // enemyAI.ICanHearTheAlarm = true;
        //       //Set state to Alerted   
        //    }
        //}


        TimerClock();




        //foreach (EnemyFOV enemyFOV in guardFOV)
        //{
        //    defaultViewRange = enemyFOV.viewRadius;

        //    //Increase FOV
        //    enemyFOV.viewRadius = Mathf.Lerp(enemyFOV.viewRadius, AlarmViewRange, Time.time);
        //    //Set state to Alerted   
        //}



        //Spawn Police / police van

        // Start function WaitAndPrint as a coroutine.
     


        //foreach (GameObject policeVan in PoliceVan)
        //{
        //    policeVan.gameObject.SetActive(true);
        //    TheMainPoliceVan = policeVan;

        //}

        //if (PoliceResponsetime <= 0)

        //{
          //  DaPoliceAreHere = true;

            //foreach (GameObject policeOfficer in PoliceOfficers)
            //{
            //    //   yield return new WaitForSeconds(3.0f);
            //    policeOfficer.gameObject.SetActive(true);
            //    policeAIs.Add(policeOfficer.gameObject.GetComponent<PoliceAI>());
            //    policePatrol.Add(policeOfficer.gameObject.GetComponent<PolicePatrol>());
         

            //}

//            nffn

            if(Alarm.Instance.CivilianHasCalledPolice)
            {
                    //foreach (PoliceAI policeAI in policeAIs)
                    //{
                    //    policeAI.IKnowWhoShouted = CalledPoliceCubedLocation;
                    //    policeAI.ICanHearSomeoneShouting = true;
                    //}
              
            }

            
      //  }

        //else if (PoliceResponsetime > 0)
        //{
        //    PoliceResponsetime -= Time.deltaTime;
        //}
       

        





        if (Alarmtime <= 0)
        {
            Alarmtime = defaultAlarmTime;
            RaiseAlarm = false;
            falseAlarm = true;
         //   PoliceResponsetime = PoliceResponsetimeMAX;
        }


        //STAFF

        foreach (StaffAIController staffAI in staffAIController)
        {

            staffAI.IcanHearTheAlarm = true;
            //Set state to 'Cower' 
        }

        //UPDATE UI WITH 'ALARM STATUS

    }


    public void FalseAlarm()
    {

        TurnOnOffScore = false;
      
        Alarmtime = defaultAlarmTime;

        //REVERT BACK TO NETURAL STATE
        RaiseAlarm = false;
       

        Color alarm = AlarmUI.color;
        alarm.a = 0;
        AlarmUI.color = alarm;

        Color alarmTimer = timerLabel.color;
        alarm.a = 0;
        timerLabel.color = alarm;

        PlayerinCCTVFOV = false;

        //Spawn Security
        //foreach (GameObject police in PoliceOfficers)
        //{
        //    policeAnim = police.gameObject.GetComponent<Animator>();
        //    policeAnim.SetBool("PoliceExit  ", true);
        //    //  police.gameObject.SetActive(false);
        //}
        // policehavebeencalled = false;

        //Police Exit Scene

        //if (PoliceResponsetime <= 0)

        //{
        //    foreach (GameObject policeOfficer in PoliceOfficers)
        //    {
        //        DaPoliceAreHere = false;
        //        //   yield return new WaitForSeconds(3.0f);
        //        policeOfficer.gameObject.SetActive(false);

        //        policeOfficer.transform.position = PoliceResetPOS;


        //    }

        //    foreach (GameObject policeVan in PoliceVan)
        //    {
        //        policeVan.gameObject.SetActive(false);

        //    }

            falseAlarm = false;

        //}

        //else if (PoliceResponsetime > 0)
        //{
        //    //RUN TO POLICE VAN


        //    foreach (PolicePatrol policePatrols in policePatrol)
        //    {
        //        //   yield return new WaitForSeconds(3.0f);

        //        policePatrols.RunningBackToVan(TheMainPoliceVan);
        //    }

        //    foreach (PoliceAI policeAI in policeAIs)
        //    {

        //        policeAI.IKnowWhoShouted = TheMainPoliceVan;
        //        policeAI.ICanHearSomeoneShouting = false;
        //    }



        //    PoliceResponsetime -= Time.deltaTime;
        //}



        foreach (GuardAI enemyAI in guardAI)
        {
            //Set state to Alerted   
            enemyAI.FalseAlarm = true;

        }


        foreach (StaffAIController staffAI in staffAIController)
        {

            staffAI.FalseAlarm = true;
            //Set state to 'Cower' 
        }

       
    }

    public void TimerClock()
    {

        Alarmtime -= Time.deltaTime;
        var minutes = Alarmtime / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = Alarmtime % 60;//Use the euclidean division for the seconds.
        var fraction = (Alarmtime * 100) % 100;

        //update the label value
        timerLabel.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void CallForHelp (Vector3 WhoCalled)
    {
        //if (DaPoliceAreHere == true)
        //{
        //SET GUARD AI TO 'RUN TO SHOUTER'

        //  fsjs

        //if (callThePolice == true)
        //  {
        //      foreach (PoliceAI policeAI in policeAIs)
        //      {
        //          //Set state to Alerted   
        //          policeAI.IKnowWhoShouted = WhoCalled;
        //          policeAI.ICanHearSomeoneShouting = true;

        //      }
        //     // policehavebeencalled = true;
        //      callThePolice = false;
        //  }

        //}



        if (callThePolice == true)
        {
       
           




            foreach (GameObject policeOfficer in PoliceOfficers)
            {

                

                if (Vector3.Distance(policeOfficer.transform.position, WhoCalled) <= CallPoliceDistance)
                {
                    print("WE HAVE DISTANCE"); //Check for distance between 'who called' and the police officer 
                    print(Vector3.Distance(policeOfficer.transform.position, WhoCalled));

                    policeOfficer.gameObject.GetComponent<StateController>().someoneIsCalling = true;

                    policeOfficer.gameObject.GetComponent<StateController>().whoCalled = WhoCalled;

                    if (policeOfficer.gameObject.activeSelf == false)
                    {
                        policeOfficer.gameObject.SetActive(true);
                        policeOfficer.transform.position = PoliceResetPOS;
                    }
                }


        


                
            }

            foreach (GameObject policeVan in PoliceVan)
            {
                policeVan.gameObject.SetActive(true);

            }

        
    
        }



        foreach (GameObject guard in Guards)
        {
            if (Vector3.Distance(guard.transform.position, WhoCalled) < AlarmRange)
            {
                    print("Guards in range");
                //guardpatrolscript = guard.gameObject.GetComponent<GuardPatrol>();



                //foreach (GuardPatrol enemyPatrol in guardPatrol)
                //{
                //    enemyPatrol.IsRunningToTheShouting(WhoCalled);
                //    enemyPatrol.isRunningToShout = true;
                //    enemyPatrol.isRunningToFood = false;
                //    enemyPatrol.isOnPatrol = false;
                //}


                foreach(StateController statecontroller in guardStateController)
                {
                    statecontroller.someoneIsCalling = true;
                    statecontroller.whoCalled = WhoCalled;
                 //   statecontroller.someoneIsCalling = false;
                }



                //foreach (PolicePatrol policePatrols in policePatrol)
                //{
                //    //Set state to Alerted   
                //    policePatrols.IsRunningToTheShouting(WhoCalled);
                //    policePatrols.isRunningToShout = true;
                //    policePatrols.isRunningToFood = false;
                //    policePatrols.isOnPatrol = false;

                //}



             //   Debug.Log("Guards over here Im" + WhoCalled.transform.name); 
            }

            if (Vector3.Distance(guard.transform.position, WhoCalled) > AlarmRange)
            {
                print("Out of Range");
            }

        }




        //        if (endCallForHelp != true)
        //        {
        ////SET GUARD AI TO 'RUN TO SHOUTER' 
        //        foreach (GuardAI enemyAI in guardAI)
        //        {
        //            //Set state to Alerted   
        //            enemyAI.IKnowWhoShouted = WhoCalled;
        //            enemyAI.ICanHearSomeoneShouting = true;

        //        }
        //            endCallForHelp = true;
        //        }



    }





    public void GuardsAlertedByStaff(Vector3 WhereisMurphy)
    {

       // RemoveCostumeProtection();                          // Staff have informed guards what costumer murphy is wearing.  

        ////SET GUARD AI TO 'RUN TO SHOUTER' 
        //foreach (GuardAI theguardAI in guardAI)
        //{
        //    //Set state to Alerted   
        //    theguardAI.WhereIsMurphy = WhereisMurphy;
        //    theguardAI.StaffAlertingGuard = true;
        //    print("Alarm: Im Setting Bool Staff Alerting Guard");

        //}

    }





    public void CallForPolice(GameObject WhoCalledPolice)
    {


        RaiseAlarm = true;
        CivilianHasCalledPolice = true;

        CalledPoliceCubedLocation = WhoCalledPolice;

       

        //  RaiseAlarm = true;

    }


    public void RemoveCostumeProtection ()
    {
        removeCostumeProtection = true;

        print("Removing Costume Protection");

        //GO THROUGH CURRENT EQUIPMENT.
        //IF COSTUMER ATTACHED
        //TURN OFF BOOLS FOR WHAT THEY FOOL.
        //POPUP Message Bar and state that costume is compramised. 

        if (removeCostumeProtection == true)
        {
            foreach (Equipment equipment in EquipmentManager.instance.currentEquipment)
            {
                if (equipment != null)
                {

                    if (equipment.IFoolTheStaff)
                    {
                        //UPDATE UI-- Play 'animation' of symbol disapearing
                        Color staffIcon = StaffIcon.color;
                        staffIcon.a = 0;
                        StaffIcon.color = staffIcon;

                        Debug.Log("IM SEEN BY STAFF");

                        //Add Temp Block
                        //   equipment.IFoolTheStaff = false;
                        tempStaffCostumeRemoved = true;
                    }

                    if (equipment.IFoolTheGuards)
                    {
                        //UPDATE UI -- Play 'animation' of symbol disapearing
                        Color guardIcon = GuardIcon.color;
                        guardIcon.a = 0;
                        GuardIcon.color = guardIcon;

                        Debug.Log("IM SEEN BY GUARDS");

                        //Add Temp Block
                        tempGuardCostumeRemoved = true;
                      //  equipment.IFoolTheGuards = false;
                    }

                    if (equipment.IFoolTheCamera)
                    {
                        //UPDATE UI-- Play 'animation' of symbol disapearing
                        Color cameraIcon = CCTVIcon.color;
                        cameraIcon.a = 0;
                        CCTVIcon.color = cameraIcon;

                        Debug.Log("IM SEEN BY CAMERA");


                        //Add Temp Block
                        //   equipment.IFoolTheCamera = false;
                        tempCameraCostumeRemoved = true;
                    }

                 
                   
                    break;
                }

                if (equipment == null)
                {
                   
                    break;
                }


                break;
            }

        }
    }






    public void CameraCheckCostume()
    {
        checkTheCostume = true;

        if (checkTheCostume == true )
        {
            //Check the equipemnt manager for any equipment that may blind enemy to player
            if (EquipmentManager.instance.currentEquipment.Length > 1)
            {
                foreach (Equipment equipment in EquipmentManager.instance.currentEquipment)
                {
                    if (equipment != null)
                    {
                        // generate a random number between 1-10, take away 'stealth' bonus, if number above e.g. 5 then Not Fooled by costume.
                        float StealthCheck = Random.Range(0.0f, 10.0f);
                        StealthCheck -= equipment.stealth;
                     //   print("Stealth" + StealthCheck);


                        if (equipment.IFoolTheCamera && tempCameraCostumeRemoved != true )
                        {
                            fooledByCostume = true;
                            Debug.Log("IM A BLIND Camera");
                            checkTheCostume = false;
                        }

                        else
                        {
                            fooledByCostume = false;
                            Debug.Log("I can see you ");
                            checkTheCostume = false;
                            break;
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
    }

