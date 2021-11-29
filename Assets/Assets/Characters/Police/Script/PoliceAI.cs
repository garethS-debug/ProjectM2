using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceAI : MonoBehaviour
{
    [Header("Reference to other scripts")]
    private PolicePatrol patrol;
    private PoliceFOV enemyFOV;


    private enum State
    {
        State0Neutral,
        State1Alerted,
        State2Attacking,
        State3AlarmRaised

    }

    private State state;

    public float stepDownWaitTime;
    public float startStepDownWaitTime;

    [Header("WaitTime")]
    private float waitTime;                 //Global wait time for when the enemy is 'Alerted' or 'shocked'
    public float StartleWaitTime;             //Global wait time for when the enemy is 'Alerted' or 'shocked'    

    [Header("Temp Waypoints")]
    private float WaypointWaitTime;                     //G
    public float StartWaypointWaitTime;                 //G   
    public bool AddTempWaypoint;
    public bool InstantiateWaypoint;
    public GameObject TempWaypointGO;

    [Header("Bools")]
    public bool IveSeenThePlayer;
    public bool IveSeenTheDistraction;
    public bool State0;                                         //Neutral State - Normal Patrol
    public bool State1;                                         //
    public bool State2;
    public bool State3AlarmRaised;
    public bool StartClimbDown;
    public bool dissolveDistraction;
    public bool CantSeeDistraction;


    [Header("Weapons")]
    [Tooltip("Reference to weapoons to turn off by Murphy")]
    public GameObject[] Weapons;

    [Header("RagDoll GO")]
    public GameObject RagdollGO;

    [Header("Player")]
    public Transform player;

    [Header(" Anim")]
    private Animator anim;

    [Header("Attacking")]
    [Tooltip("Attacking")]
    public float PauseBetweenAttacks;
    public float AttackCounter;
    //public float AttackBufferTimeMax;
    //public float attackBufferTime;
    private float CoolDwnTimerMax;
    [Tooltip("This is the timer between attacks")]
    public float AtkCoolDownTimer;
    private float AtkCoolDownTimerMax;
    //public float AttackCoolDown;
    public bool pauseAfterAttack;
    public bool lookAtPlayer;

    [Header("State Marker")]
    [Tooltip("Status Marker")]
    //Quest UI Marker for NPC
    public GameObject StateMarker;//quest marker  game object (canvas)
    public Image stateMarkerImage;//image on canvas
    public Sprite state0Sprite; //sprite state 0 - Neutral
    public Sprite state1Sprite; //sprite state 1 - Alterted
    public Sprite state2Sprite; //sprite state 2 - Looking
    public Sprite state3Sprite; //sprite state 3 - Attacking

    [Header("AlertedByStaff")]
    public bool AlertedbyStaff;
    private StaffPatrol staffPatrol;
    private StaffFOV staffFOV;
    public GameObject staffMember;
    public bool StaffAlertingGuard;
    public Vector3 WhereIsMurphy;

    [Header("AlertedByAlarm")]
    private float AlarmViewRange;
    private float defaultViewRange;
    private bool IveHeardTheAlarm;
    public bool ICanHearTheAlarm;
    public bool ICanHearSomeoneShouting;
    public GameObject IKnowWhoShouted;
    public bool FalseAlarm;
    public bool RunningToAlarmPoint;

    [Header("AlertedByMurohy")]
    public bool IveJustSeenThePlayer;


    [Header("PoliceAlerted")]
    public float PoliceCalledAlarmTime = 3.0f;
    public float PoliceCalledAlarmTimeMax;

    [Header("Distraction traps")]
    public bool Foodtime;
    public float DistractionTimer = 3.0f;
    private float DistractionTimerMax;


    [Header("Crime")]
    public GameObject CrimeScene;

    //[Header("Police Reset")]
    //public bool resetTo0;

    // Start is called before the first frame update
    void Start()
    {
        patrol = this.gameObject.GetComponent<PolicePatrol>();
        state = State.State0Neutral;

        waitTime = StartleWaitTime;

        enemyFOV = gameObject.GetComponent<PoliceFOV>();

        stepDownWaitTime = startStepDownWaitTime;

        anim = this.gameObject.GetComponent<Animator>(); // get the animator component attached to enemy

        AtkCoolDownTimerMax = AtkCoolDownTimer;

        stateMarkerImage.sprite = state0Sprite;

        defaultViewRange = enemyFOV.viewRadius;

        AlarmViewRange = enemyFOV.viewRadius + 3.0f;

        DistractionTimerMax = DistractionTimer;

        PoliceCalledAlarmTimeMax = PoliceCalledAlarmTime;
    }

    // Update is called once per frame
    void Update()
    {

        //if (resetTo0 == true || Alarm.Instance.falseAlarm == true && enemyFOV.PlayerinFOV == false)
        //{
        //    state = State.State0Neutral;
        //    resetTo0 = false;
        //}



        //08-04-2020 - removed
        //if (AlertedbyStaff == true)
        //{
        //    state = State.State1Alerted;

        //    IveSeenThePlayer = true;


        //    if (staffMember != null)
        //    {
        //        print("Staff Member: Not Nukk");
        //        staffFOV = staffMember.gameObject.GetComponent<StaffFOV>();
        //        patrol.LastKnownLocation = staffFOV.LastKnownMurphyLOC; //Transfer Last Known Location from Staff
        //        patrol.isAlerted = true;
        //                                                            //RECORD MURPHY'S LOCATION

        //    }

        //}




        if (ICanHearSomeoneShouting == true && state != State.State3AlarmRaised)
        {
            //CHECK IF THE GUARD IS CURRENTLY CHASING THE PLAYER
            if (enemyFOV.PlayerinFOV == false)
            {
                StaffAlertingGuard = false;
                state = State.State3AlarmRaised;
            }

            if (enemyFOV.PlayerinFOV == true)
            {
                patrol.isWalkingToLastKnownLOC = true;
            }
            //ICanHearSomeoneShouting = false;
        }

        if (patrol.isRunningToShout == true)
        {
           
              
                patrol.isWalkingToLastKnownLOC = true;
                patrol.isRunningToShout = false;
                ICanHearSomeoneShouting = false;
            


        }


        if (StaffAlertingGuard == true)
        {
            //CHECK IF THE GUARD IS CURRENTLY CHASING THE PLAYER
            if (enemyFOV.PlayerinFOV == false)
            {
                state = State.State3AlarmRaised;
            }

            //ICanHearSomeoneShouting = false;
        }


        if (this.gameObject.tag == "Police")
        {
            if (ICanHearSomeoneShouting == true || Alarm.Instance.CivilianHasCalledPolice == true)
            {
                //CHECK IF THE GUARD IS CURRENTLY CHASING THE PLAYER
                if (enemyFOV.PlayerinFOV == false)
                {
                    state = State.State3AlarmRaised;

                }


                //ICanHearSomeoneShouting = false;
            }

        }






        if (ICanHearTheAlarm == true)
        {
            //CHECK IF THE GUARD IS CURRENTLY CHASING THE PLAYER
            if (enemyFOV.PlayerinFOV == false && state != State.State3AlarmRaised && state != State.State2Attacking)
            {
                state = State.State3AlarmRaised;
            }

            //if (enemyFOV.PlayerinFOV == true)
            //{
            //    patrol.isWalkingToLastKnownLOC = true;
            //}

            // ICanHearTheAlarm = false;
        }

        if (FalseAlarm == true)
        {
            if (ICanHearSomeoneShouting == true)
            {
                ICanHearSomeoneShouting = false;
            }

            if (patrol.isRunningToShout == true)
            {
                patrol.isRunningToShout = false;
            }
            state = State.State0Neutral;
            FalseAlarm = false;
        }


        switch (state)
        {
            default:
            case State.State0Neutral:
                //Patrol code
                stateMarkerImage.sprite = state0Sprite;


                //removing references to state 3 
                enemyFOV.viewRadius = defaultViewRange;

                bool resetreset = true;
                if (resetreset == true)
                {
                    patrol.resetPath = true;
                    resetreset = false;
                }


                if (patrol.isRunningToFood == false)
                {
                    patrol.isOnPatrol = true;
                }



                IveSeenThePlayer = false; //Setting back to 0;
                patrol.isWalkingToLastKnownLOC = false;
                StartClimbDown = false;
                dissolveDistraction = false;
                IveSeenTheDistraction = false;
                patrol.Startled = false;
                StaffAlertingGuard = false;
                patrol.HasBeenAlertedByStaff = false;
                patrol.isWalkingToDistraction = false;
                patrol.PauseInWalk = false;

                // enemyFOV.Distraction = false;

                if (patrol.LastKnownLOC.Count > 1)
                {
                    patrol.LastKnownLOC.Clear();
                }
                patrol.LastKnownLocation = new Vector3(0, 0, 0);

                //States
                State0 = true;
                State1 = false;
                State2 = false;
                State3AlarmRaised = false;

                //While we are patrolling & if the player enters the FOV
                if (enemyFOV.PlayerinFOV == true || enemyFOV.Distraction == true && CantSeeDistraction == false)
                {
                    if (enemyFOV.PlayerinFOV == true)
                    {
                        IveJustSeenThePlayer = true;
                    }
                    state = State.State1Alerted;

                }





                if (Alarm.Instance.RaiseAlarm == true)
                {
                    state = State.State3AlarmRaised;
                }




                break;



            case State.State1Alerted:
                // end patrol
                patrol.isOnPatrol = false;

                //removing references to state 3 
                enemyFOV.viewRadius = defaultViewRange;
                // patrol.isRunningToAlarmRaisedPatrolPoints = false;

                //States
                State0 = false;
                State1 = true;
                State2 = false;
                State3AlarmRaised = false;

                stateMarkerImage.sprite = state1Sprite;



                ////IF ALERTED BY ALARM
                if (AlertedbyStaff == false && Alarm.Instance.RaiseAlarm == true)
                {

                    state = State.State3AlarmRaised;
                }



                //IF ALERTED NOT BY ALARM

                if (AlertedbyStaff == false && Alarm.Instance.RaiseAlarm == false)
                {

                    //When we enter the alerted state the enenmy will pause. 
                    if (IveSeenThePlayer == false || enemyFOV.Distraction == true && IveSeenTheDistraction == false)
                    {
                        if (waitTime <= 0)
                        {
                            waitTime = StartleWaitTime;


                            patrol.Startled = false;
                            patrol.Walking = true;
                            //  patrol.PauseInWalk = true;
                            //  patrol.Walking = false;

                            //  if (enemyFOV.PlayerinFOV == true)
                            //  {
                            if (IveSeenTheDistraction == false && enemyFOV.PlayerinFOV == false)
                            {
                                if (IveJustSeenThePlayer == true)
                                {
                                    IveSeenThePlayer = true;
                                    IveJustSeenThePlayer = false;
                                }

                            }


                            //DISTRACTION
                            if (enemyFOV.Distraction == true && CantSeeDistraction == false)
                            {
                                IveSeenTheDistraction = true;
                                //  patrol.isAlerted = true; //CHECK THIS . ITS SAPPOSED TO TURN OFF IF PLAYER LEAVES BUT IS USED TO UPDATE THE LKL
                            }


                        }
                        else
                        {
                            //ADD animation of looking around while 'shocked'
                            waitTime -= Time.deltaTime;
                            patrol.Walking = false;
                            patrol.PauseInWalk = false;
                            patrol.Startled = true;
                        }
                    }

                    //if the player is STILL IN FOV. wait a few seconds.-
                    while (IveSeenThePlayer == true && enemyFOV.PlayerinFOV == true)
                    {
                        // if player IS in FOV
                        //JOG TO THE PLAYER


                        patrol.isAlerted = true;
                        patrol.isWalkingToLastKnownLOC = false;
                        break;
                    }


                    while (IveSeenThePlayer == true && enemyFOV.PlayerinFOV == false)
                    {


                        // if player is not in FOV
                        //JOG TO LAST KNOWN LOCATION
                        patrol.isWalkingToLastKnownLOC = true;
                        patrol.isAlerted = false;
                        break;
                    }

                    while (IveSeenThePlayer == false && enemyFOV.PlayerinFOV == false && enemyFOV.Distraction == true && IveSeenTheDistraction == true && CantSeeDistraction == false)
                    {

                        patrol.isWalkingToDistraction = true;
                        patrol.isWalkingToTheDistraction(enemyFOV.distrctionOBJ);
                        //Patrol . IS WALKING TO DISTRACTION
                        break;
                    }


                }





                break;


            //Attacking State
            case State.State2Attacking:

                stateMarkerImage.sprite = state3Sprite;

                //code here
                patrol.isOnPatrol = false;
                patrol.isAlerted = false;
                ICanHearSomeoneShouting = false;


                //States
                State0 = false;
                State1 = false;
                State2 = true;
                State3AlarmRaised = false;

                if (patrol.inAttackRange == true)
                {
                    //if Within Range
                    Attack();
                }





                if (enemyFOV.PlayerinFOV == true)
                {
                    // Attack player
                    StartClimbDown = false;
                    patrol.Jogging = false;
                    patrol.Walking = false;
                    patrol.Startled = false;

                }


                if (enemyFOV.PlayerinFOV == false)
                {
                    // if player is not in FOV
                    //JOG TO LAST KNOWN LOCATION
                    patrol.isWalkingToLastKnownLOC = true;
                    //Start timer to move down state
                    StartClimbDown = true;
                }


                break;



            //The alarm has been raised -- // Through Staff // Through Alarm
            case State.State3AlarmRaised:


                //States
                State0 = false;
                State1 = false;
                State2 = false;
                State3AlarmRaised = true;

                patrol.isOnPatrol = false;                  //Stop The Patrol
                stateMarkerImage.sprite = state1Sprite;     //Change the sprite





                //THIS COULD BE THE PROBLEM
                //ALARM HAS BEEN RAISED. But cant see playuer
                if (enemyFOV.PlayerinFOV == false)
                {
                    //  patrol.isRunningToAlarmRaisedPatrolPoints = true;
                    patrol.isWalkingToLastKnownLOC = true;
                }





                if (enemyFOV.PlayerinFOV == true)
                {
                    //patrol.isRunningToAlarmRaisedPatrolPoints = false;
                    patrol.isWalkingToLastKnownLOC = true;

                }

                //if (enemyFOV.PlayerinFOV == true)
                //{
                //    patrol.PauseInWalk = false;

                //    patrol.isWalkingToLastKnownLOC = true;
                //    IveJustSeenThePlayer = true;

                //    patrol.isRunningToShout = false;
                //    StartClimbDown = false;
                //    patrol.Startled = false;
                //    patrol.isAlerted = false;

                // //   Alarm.Instance.endCallForHelp = false;
                ////    Alarm.Instance.CallForHelp(player.gameObject);


                //    ICanHearSomeoneShouting = false;
                //    patrol.SearchArea = false;
                //    ICanHearTheAlarm = false;
                //}

                //if (enemyFOV.PlayerinFOV == false && IveJustSeenThePlayer == true)
                //{
                //    patrol.isWalkingToLastKnownLOC = true;
                //    StartClimbDown = true;

                //    patrol.isRunningToShout = false;
                //    patrol.Startled = false;
                //    patrol.isAlerted = false;

                //    patrol.PauseInWalk = false;


                //    //     Alarm.Instance.endCallForHelp = false;
                //    //     Alarm.Instance.CallForHelp(player.gameObject);



                //    ICanHearSomeoneShouting = false;
                //    patrol.SearchArea = false;
                //    ICanHearTheAlarm = false;
                //    StaffAlertingGuard = false;
                //    patrol.HasBeenAlertedByStaff = false;
                //}







                //Staff alerting the Guards function
                if (StaffAlertingGuard == true && enemyFOV.PlayerinFOV == true)
                {
                    StaffAlertingGuard = false;
                    patrol.HasBeenAlertedByStaff = false;

                    patrol.isWalkingToLastKnownLOC = true;
                }


                if (StaffAlertingGuard == true && enemyFOV.PlayerinFOV == false && ICanHearTheAlarm == false)
                {
                    //  StaffAlertingGuard = false;
                    patrol.isWalkingToLastKnownLOC = false;
                    patrol.HasBeenAlertedByStaff = true;
                    patrol.IsRunningToMurphysLOC(WhereIsMurphy);

                    // patrol.isAlerted = false;
                    // StartClimbDown = true;

                }






                while (ICanHearSomeoneShouting == true && enemyFOV.PlayerinFOV == false)
                {
                    //if (this.gameObject.tag == "Police")
                    //{
                    //    if (PoliceCalledAlarmTime > 0.01f)
                    //    {
                    //        //RUN TO EVENT
                    //        patrol.isRunningToShout = true;
                    //        patrol.IsRunningToTheShouting(IKnowWhoShouted);
                    //        patrol.isAlerted = false;
                    //        PoliceCalledAlarmTime -= Time.deltaTime;

                    //    }

                    //    if (PoliceCalledAlarmTime <= 0.01f)
                    //    {
                    //        patrol.isRunningToShout = false;
                    //        patrol.isAlerted = false;
                    //        patrol.isOnPatrol = false;
                    //        //PATROL . 'SEARCH AREA' FUNCTION
                    //        if (enemyFOV.PlayerinFOV == false)
                    //        {
                    //            patrol.SearchingTheArea(this.gameObject);
                    //        }

                    //}
                    //    }

                    patrol.isRunningToShout = true;
                    patrol.IsRunningToTheShouting(IKnowWhoShouted);
                    patrol.isAlerted = false;

                



                    if (this.gameObject.tag == "Enemy")
                    {


                        //Jog to Alarm Patrol Points 
                        // patrol.isWalkingToLastKnownLOC = true;
                        patrol.isRunningToShout = true;
                        patrol.IsRunningToTheShouting(IKnowWhoShouted);
                        patrol.isAlerted = false;

                    }
                    // StartClimbDown = true;


                    break;
                }




                //                                    //Increase the view range of the guards 
                //defaultViewRange = enemyFOV.viewRadius;
                //enemyFOV.viewRadius = Mathf.Lerp(enemyFOV.viewRadius, AlarmViewRange, Time.deltaTime);
                //                                   // Change the marker 





                //                                    //if the player is IN FOV and Someone is calling.  
                //if (ICanHearSomeoneShouting == true && enemyFOV.PlayerinFOV == true)
                //{
                //    patrol.isRunningToShout = false;
                //    patrol.isWalkingToLastKnownLOC = true;

                //}


                //while (ICanHearSomeoneShouting == true && enemyFOV.PlayerinFOV == false)
                //{
                //    //Jog to Alarm Patrol Points 
                //    // patrol.isWalkingToLastKnownLOC = true;
                //    patrol.isRunningToShout = true;
                //    patrol.IsRunningToTheShouting(IKnowWhoShouted);
                //    patrol.isAlerted = false;

                //    // StartClimbDown = true;


                //    break;
                //}



                //if (StaffAlertingGuard== true && enemyFOV.PlayerinFOV == true)
                //{
                //    patrol.HasBeenAlertedByStaff = false;
                //    patrol.isWalkingToLastKnownLOC = true;

                //}


                //while (StaffAlertingGuard == true && enemyFOV.PlayerinFOV == false && ICanHearTheAlarm == false)
                //{
                //    //Jog to Alarm Patrol Points 
                //    patrol.isWalkingToLastKnownLOC = false;
                //    patrol.HasBeenAlertedByStaff = true;
                //    patrol.IsRunningToMurphysLOC(WhereIsMurphy);
                //   // patrol.isAlerted = false;

                //    // StartClimbDown = true;


                //    break;
                //}

                //if (StaffAlertingGuard == true && enemyFOV.PlayerinFOV == false && ICanHearTheAlarm == true)
                //{

                //    patrol.isRunningToAlarmRaisedPatrolPoints = true;
                //    StaffAlertingGuard = false;
                //    patrol.isAlerted = false;

                //}



                ////IF Alarm is raised
                ////Startle the guard then walk to patrol point
                //if (IveHeardTheAlarm == false && ICanHearSomeoneShouting == false && Alarm.Instance.falseAlarm == false && AlertedbyStaff == false)
                //{
                //    if (waitTime <= 0)
                //    {
                //        waitTime = StartleWaitTime;

                //        IveHeardTheAlarm = true;
                //        patrol.Startled = false;
                //       // patrol.Walking = true;


                //    }
                //    else
                //    {
                //        //ADD animation of looking around while 'shocked'
                //        waitTime -= Time.deltaTime;
                //        patrol.Walking = false;
                //        patrol.PauseInWalk = false;
                //        patrol.Startled = true;
                //    }
                //}

                ////if the player is IN FOV. 
                //if (IveHeardTheAlarm == true && enemyFOV.PlayerinFOV == true && Alarm.Instance.falseAlarm == false && StaffAlertingGuard == false)
                //{
                //    patrol.isRunningToAlarmRaisedPatrolPoints = false;
                //    //Switch to state 1 - Alerted
                //    // state = State.State1Alerted;
                //   // patrol.isAlerted = true;
                //    patrol.isWalkingToLastKnownLOC = true;

                //}


                //while (IveHeardTheAlarm == true && enemyFOV.PlayerinFOV == false && StaffAlertingGuard == false)
                //{


                //    //Jog to Alarm Patrol Points 
                //    // patrol.isWalkingToLastKnownLOC = true;
                //    patrol.isRunningToAlarmRaisedPatrolPoints = true;
                //    patrol.isAlerted = false;

                //   // StartClimbDown = true;


                //    break;
                //}




                break;
        }




        if (patrol.isAttack == true)
        {
            state = State.State2Attacking;
        }

        if (StartClimbDown == true)
        {
            StepDownTimer();
        }

        if (AddTempWaypoint == true)
        {
            AddTemporaryWaypoint();
        }


        if (lookAtPlayer == true)
        {
            transform.LookAt(player.transform.position);
        }
    }

    public void Attack()
    {

        transform.LookAt(player);

        //Call for help and send to this location. 
      //  Alarm.Instance.CallForHelp(this.gameObject);

        print("error code 7: am attacking");
        anim.SetBool("isAttacking", true);

        //  StartCoroutine(PauseAfterAttack());


        if (pauseAfterAttack == false)
        {
            //Slider for the attack animations
            AttackCounter = Mathf.Lerp(AttackCounter, 1, Time.deltaTime / 3);
            //Set the slider on Mechanim
            anim.SetFloat("AttackCounter", AttackCounter);
            WaitAfterAttack();
            // StartCoroutine("WaitAfterAttack");

        }

        if (pauseAfterAttack == true)
        {
            WaitAfterAttack();
            anim.SetFloat("AttackCounter", 0);
        }

    }




    public void WaitAfterAttack()
    {
        if (AtkCoolDownTimer > 0)
        {
            AtkCoolDownTimer -= Time.deltaTime;
        }

        if (AtkCoolDownTimer <= 0)
        {
            AtkCoolDownTimer = AtkCoolDownTimerMax;
            pauseAfterAttack = !pauseAfterAttack;
        }

    }

    public void DistractionTimers()
    {

        if (DistractionTimer <= 0.1)
        {
            if (IveSeenTheDistraction == true)               //Resetting the Tag via FOV
            {
                dissolveDistraction = true;

                CantSeeDistraction = true;
                //enemyFOV.Distraction = false;
                //patrol.waitTime = patrol.StartWaitTime;
                patrol.isOnPatrol = true;
                DistractionTimer = DistractionTimerMax;

                if (state == State.State1Alerted)
                {
                    state = State.State0Neutral;
                }
                IveSeenTheDistraction = false;
            }
        }

        else
        {
            DistractionTimer -= Time.deltaTime;
        }

    }



    //step down states timer
    public void StepDownTimer()
    {
        if (stepDownWaitTime <= 0.5f && state != State.State0Neutral)
        {

            if (state == State.State1Alerted)
            {
                IveSeenThePlayer = false;
                patrol.resetPath = true;



                state = State.State0Neutral;
                patrol.isOnPatrol = true;
                stepDownWaitTime = startStepDownWaitTime;
            }




            if (state == State.State2Attacking)
            {
               // patrol.resetPath = true;
                state = State.State1Alerted;
                stepDownWaitTime = startStepDownWaitTime;
            }




            if (state == State.State3AlarmRaised && Alarm.Instance.RaiseAlarm == false)
            {
                if (patrol.HasBeenAlertedByStaff == true)
                {
                    patrol.HasBeenAlertedByStaff = false;
                }

                if (patrol.isRunningToShout == true)
                {
                    patrol.isRunningToShout = false;
                }

                patrol.resetPath = true;

                AlertedbyStaff = false;
                ICanHearSomeoneShouting = false;
                IveHeardTheAlarm = false;
                StaffAlertingGuard = false;
                AlertedbyStaff = false;
                FalseAlarm = true;
                patrol.HasBeenAlertedByStaff = false;


                print("State is 3 : Move to State: 1");
                state = State.State0Neutral;
                stepDownWaitTime = startStepDownWaitTime;
            }
            //Reset the step down timer if the alarm is still raised.
            if (state == State.State3AlarmRaised && Alarm.Instance.RaiseAlarm == true)
            {
                stepDownWaitTime = startStepDownWaitTime;
                patrol.resetPath = true;

            }




        }
        else
        {
            //ADD animation of looking around while 'shocked'
            stepDownWaitTime -= Time.deltaTime;
            patrol.Startled = false;
            patrol.isAttack = false;
        }




    }

    public void AddTemporaryWaypoint()
    {
        if (WaypointWaitTime <= 0)
        {
            //Add Waypoint
            InstantiateWaypoint = true;
            Instantiate(TempWaypointGO, new Vector3(patrol.LastKnownLocation.x, patrol.LastKnownLocation.y, patrol.LastKnownLocation.z), Quaternion.identity);
            WaypointWaitTime = StartWaypointWaitTime;
        }
        else
        {
            //Dont add waypoint
            InstantiateWaypoint = false;
            WaypointWaitTime -= Time.deltaTime;
        }

    }
}
