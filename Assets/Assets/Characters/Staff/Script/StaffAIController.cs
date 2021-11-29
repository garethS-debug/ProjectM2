using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaffAIController : MonoBehaviour
{

    [Header("Reference to other scripts")]
    private StaffPatrol patrol;
    private StaffFOV staffFOV;


    private enum State
    {
        State0Neutral,
        State1Alerted,
        State2Alarmed,
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
    public bool State0;
    public bool State1;
    public bool State2;
    public bool State3;
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

    [Header("Crime")]
    public bool IveSeenACrime;


    [Header("Alarm")]
    public bool IcanHearTheAlarm;
    public bool FalseAlarm;

    [Header("Distraction traps")]
    public bool Foodtime;
    public float DistractionTimer = 3.0f;
    private float DistractionTimerMax;


    // Start is called before the first frame update
    void Start()
    {
        patrol = this.gameObject.GetComponent<StaffPatrol>();
        state = State.State0Neutral;

        waitTime = StartleWaitTime;

        staffFOV = gameObject.GetComponent<StaffFOV>();

        stepDownWaitTime = startStepDownWaitTime;

        anim = this.gameObject.GetComponent<Animator>(); // get the animator component attached to enemy

        AtkCoolDownTimerMax = AtkCoolDownTimer;

        stateMarkerImage.sprite = state0Sprite;

        DistractionTimerMax = DistractionTimer;

    }

    // Update is called once per frame
    void Update()
    {

        if (IcanHearTheAlarm == true)
        {
            state = State.State3AlarmRaised;
            IcanHearTheAlarm = false;
        }

        if (FalseAlarm == true)
        {
            state = State.State0Neutral;
            FalseAlarm = false;
        }


        switch (state)
        {
            default:
            case State.State0Neutral:
                //Patrol code
                stateMarkerImage.sprite = state0Sprite;

                patrol.isOnPatrol = true;

                if (patrol.isRunningToFood == false)
                {
                    patrol.isOnPatrol = true;
                }

                patrol.Jogging = false;
                patrol.isAlerted = false;
                IveSeenThePlayer = false; //Setting back to 0;
                patrol.isWalkingToLastKnownLOC = false;
                StartClimbDown = false;
                dissolveDistraction = false;
                IveSeenTheDistraction = false;
                anim.SetBool("isScared", false);
                StartClimbDown = false;
                IveSeenTheDistraction = false;
                patrol.isWalkingToDistraction = false;
                // patrol.distraction = null;
                IveSeenACrime = false;

    
         
              

                if (patrol.LastKnownGuardLOC.Count > 1)
                {
                    // patrol.LastKnownGuardLocation = new Vector3(0, 0, 0);
                    int ListNumber = patrol.LastKnownGuardLOC.Count;
                    patrol.LastKnownGuardLocation = patrol.LastKnownGuardLOC[ListNumber - 1];
                    patrol.LastKnownGuardLOC.Clear();
                }


                //States
                State0 = true;
                State1 = false;
                State2 = false;

                //While we are patrolling & if the player enters the FOV
                if (staffFOV.PlayerinFOV == true || staffFOV.Distraction == true && CantSeeDistraction == false || staffFOV.SeenCrime == true)
                {
                    state = State.State1Alerted;
                }


                break;



            case State.State1Alerted:
                
                patrol.isOnPatrol = false; // end patrol

                //States
                State0 = false;
                State1 = true;
                State2 = false;

                stateMarkerImage.sprite = state1Sprite;

                //When we enter the alerted state the enenmy will pause. 
                if (IveSeenThePlayer == false || staffFOV.Distraction == true && IveSeenTheDistraction == false)
                {
                    if (waitTime <= 0)
                    {
                        waitTime = StartleWaitTime;


                        patrol.Startled = false;
                        patrol.Walking = false;                                                     //CHANGED FROM TRUE TO FALSE


                        //   Alarm.Instance.CallForHelp(this.gameObject);


                        if (patrol.isSitting == true)
                        {
                            patrol.isSitting = false;
                        }



                        //  patrol.PauseInWalk = true;
                        //  patrol.Walking = false;

                        //  if (enemyFOV.PlayerinFOV == true)
                        //  {
                        if (staffFOV.PlayerinFOV == true)
                        {
                            IveSeenThePlayer = true;
                            patrol.Startled = false;
                        }

                        if (staffFOV.PlayerinFOV == false && staffFOV.Distraction == false && staffFOV.SeenCrime == false)
                        {
                            IveSeenThePlayer = true;
                            patrol.Startled = false;
                        }

                        if (staffFOV.Distraction == true && CantSeeDistraction == false)
                        {
                            IveSeenTheDistraction = true;
                            //patrol.isAlerted = true;                                            //CHECK THIS . ITS SAPPOSED TO TURN OFF IF PLAYER LEAVES BUT IS USED TO UPDATE THE LKL
                        }

                        if (staffFOV.PlayerinFOV == false && staffFOV.Distraction == false &&  staffFOV.SeenCrime == true)
                        {
                            patrol.Startled = false;
                            IveSeenACrime = true;
                        }

                        //The timerwont run again until we 'forget' about the player / distraction
                    }
                    else
                    {
                        //ADD animation of looking around while 'shocked'
                        waitTime -= Time.deltaTime;
                        patrol.Walking = false;
                        patrol.PauseInWalk = false;
                        patrol.Startled = true;                                                         //STAFF SHOULD STAY IN ALERTED MODE
                    }
                }


               
                //if the player is STILL IN FOV. wait a few seconds.-
                while (IveSeenThePlayer == true && staffFOV.PlayerinFOV == true)
                {
                    // if player IS in FOV
                    //JOG TO THE PLAYER

                    patrol.isWalkingToLastKnownLOC = true;
                    patrol.isAlerted = true;
                    //  patrol.isWalkingToLastKnownLOC = false;
                    break;
                }

                while (IveSeenACrime == true && staffFOV.PlayerinFOV == false)
                {
                    patrol.isWalkingToLastKnownLOC = true;
                    break;
                }


                while (IveSeenThePlayer == true && staffFOV.PlayerinFOV == false)
                {
                    patrol.isWalkingToLastKnownLOC = true;

                    // if player is not in FOV
                    //JOG TO LAST KNOWN LOCATION
                    // patrol.isWalkingToLastKnownLOC = true;

                    break;
                }

                while (staffFOV.Distraction == true && IveSeenTheDistraction == true && CantSeeDistraction == false)
                {
                    patrol.isWalkingToDistraction = true;
                    patrol.isAlerted = false;
                    break;
                }




                break;


            //Attacking State --- CHANGE THIS TO 
            case State.State2Alarmed:

                stateMarkerImage.sprite = state3Sprite;

                //code here
                patrol.isOnPatrol = false;
                patrol.isAlerted = false;

                patrol.isWalkingToLastKnownLOC = true;

                //States
                State0 = false;
                State1 = false;
                State2 = true;

                if (patrol.inAlertRange == true)
                {
                    //if Within Range
                    AlertGuard();
                }



                if (staffFOV.PlayerinFOV == true)
                {
                    // Attack player
                    StartClimbDown = false;
                    patrol.Jogging = false;
                    patrol.Walking = false;
                    patrol.Startled = false;

                }


                if (staffFOV.PlayerinFOV == false)
                {
                    // if player is not in FOV
                    //JOG TO LAST KNOWN LOCATION

                    //Start timer to move down state
                    StartClimbDown = true;
                }


                break;



            case State.State3AlarmRaised:

                //When the alarm is raised 'Cower'
                patrol.isOnPatrol = false;
              //  patrol.isAlerted = false;
               // patrol.isWalkingToLastKnownLOC = false;
                patrol.Jogging = false;
                patrol.Walking = false;
                patrol.Startled = false;

                State0 = false;
                State1 = false;
                State2 = false;
                State3 = true;

                anim.SetBool("isFiling", false);
                anim.SetBool("isSitting", false);

                if (patrol.isWalkingToLastKnownLOC == false )
                {
                    anim.SetBool("isScared", true);
                }

                if (FalseAlarm == true)
                {
                    state = State.State0Neutral;
                   
                }



                break;

        }

        if (patrol.isAlert == true)
        {
            state = State.State2Alarmed;
        }

        if (patrol.alertTheGuard == true)
        {
            AlertGuard();
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

    //public void Attack()
    //{

    //    transform.LookAt(player);

    //    print("error code 7: am attacking");
    //    anim.SetBool("isAttacking", true);

    //    //  StartCoroutine(PauseAfterAttack());


    //    if (pauseAfterAttack == false)
    //    {
    //        //Slider for the attack animations
    //        AttackCounter = Mathf.Lerp(AttackCounter, 1, Time.deltaTime / 3);
    //        //Set the slider on Mechanim
    //        anim.SetFloat("AttackCounter", AttackCounter);
    //        WaitAfterAttack();
    //        // StartCoroutine("WaitAfterAttack");

    //    }

    //    if (pauseAfterAttack == true)
    //    {
    //        WaitAfterAttack();
    //        anim.SetFloat("AttackCounter", 0);
    //    }

    //}

        public void AlertGuard()
    {

                                                                                                        //ADD FUNCTION TO RUN TO GUARDS

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
        if (stepDownWaitTime <= 0)
        {

            if (state == State.State1Alerted)
            {
                IveSeenThePlayer = false;


                //if (IveSeenTheDistraction == true)               //Resetting the Tag via FOV
                //{
                //    dissolveDistraction = true;
                //    IveSeenTheDistraction = false;
                //    CantSeeDistraction = true;
                //}

                state = State.State0Neutral;
                stepDownWaitTime = startStepDownWaitTime;
            }

            if (state == State.State2Alarmed)
            {
                state = State.State1Alerted;
                stepDownWaitTime = startStepDownWaitTime;
            }

        }
        else
        {
            //ADD animation of looking around while 'shocked'
            stepDownWaitTime -= Time.deltaTime;
            patrol.Startled = false;
            patrol.isAlert = false;
        }




    }

    public void AddTemporaryWaypoint()
    {
        if (WaypointWaitTime <= 0)
        {
            //Add Waypoint
            InstantiateWaypoint = true;
            Instantiate(TempWaypointGO, new Vector3(patrol.LastKnownGuardLocation.x, patrol.LastKnownGuardLocation.y, patrol.LastKnownGuardLocation.z), Quaternion.identity);
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

