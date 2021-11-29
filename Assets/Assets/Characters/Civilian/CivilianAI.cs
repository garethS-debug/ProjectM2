    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class CivilianAI : MonoBehaviour
{
    [Header("Ragdoll")]
    public GameObject RagdollGO;
   // public GameObject CrimeCubes;

    [Header("Costume")]
    public GameObject Costume;
    // Start is called before the first frame update

    [Header("Food Time")]
    public bool isRunningToFood;
    public List<Transform> FoodGatheringLocations = new List<Transform>();
    private int randomFoodLOC;

    [Header("Walking")]
    public float speedDampTime = 0.09f;
    private Animator anim;
    private NavMeshAgent _navMeshAgent;


    [Header("State Marker")]
    [Tooltip("Status Marker")]
    //Quest UI Marker for NPC
    public GameObject StateMarker;//quest marker  game object (canvas)
    public Image stateMarkerImage;//image on canvas
    public Sprite state0Sprite; //sprite state 0 - Neutral
    public Sprite state1Sprite; //sprite state 1 - Alterted


    [Header("Bools")]
    public bool IveSeenThePlayer;
    public bool IveSeenTheDistraction;
    public bool dissolveDistraction;
    public bool CantSeeDistraction;

    public bool State0;
    public bool State1;


    [Header("WaitTime")]
    private float waitTime;                 //Global wait time for when the enemy is 'Alerted' or 'shocked'
    private float StartleWaitTime;             //Global wait time for when the enemy is 'Alerted' or 'shocked'    


    [Header("Startled")]
    public bool Startled;



    [Header("Climbdown")]
    public bool StartClimbDown;
    public float stepDownWaitTime;
    public float startStepDownWaitTime;

    [Header("Reference to other scripts")]
    private CivilianPatrol patrol;
    private CivilianFOV civilianFOV;



    private enum State
    {
        State0Neutral,
        State1Alerted
    }

    private State state;



    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>(); // get the animator component attached to enemy
        _navMeshAgent = GetComponent<NavMeshAgent>(); //REference to the Navmesh Agent


        state = State.State0Neutral;

       

        civilianFOV = gameObject.GetComponent<CivilianFOV>();
        patrol = gameObject.GetComponent<CivilianPatrol>();


        stepDownWaitTime = startStepDownWaitTime;
        anim = this.gameObject.GetComponent<Animator>(); // get the animator component attached to enemy
     //   waitTime = Alarm.Instance.PoliceResponsetime + 1.0f;

        if (state0Sprite != null)
        {
            stateMarkerImage.sprite = state0Sprite;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (StartClimbDown == true)
        {
            StepDownTimer();
        }



        while (isRunningToFood == true)
        {
            //  _navMeshAgent.SetDestination(AlarmRaisedLocations[randomAlarmLOC].position);

            //  randomFoodLOC = Random.Range(0, FoodGatheringLocations.Count);

            if (randomFoodLOC == 0)
            {
                randomFoodLOC = Random.Range(0, Alarm.Instance.FoodGatheringLocations.Count);

            }

            // waiting once we have reached a location close to the target position
            while (Vector3.Distance(transform.position, FoodGatheringLocations[randomFoodLOC].position) < 1.9f)
            {


                gameObject.GetComponent<NavMeshAgent>().isStopped = true;

                anim.SetFloat("Speed", 0.0f, speedDampTime, Time.deltaTime);
                //anim.SetBool("isWalking", false);
                anim.SetBool("isEating", true);

                break;

            }

            while (Vector3.Distance(transform.position, FoodGatheringLocations[randomFoodLOC].position) > 1.9f)
            {
                _navMeshAgent.SetDestination(FoodGatheringLocations[randomFoodLOC].position);
                //  Walking = true;
                //Jogging = true;
                //anim.SetBool("isWalking", true);
                anim.SetFloat("Speed", 0.02f, speedDampTime, Time.deltaTime);
                break;
            }


            break;
        }


        switch (state)
        {
            default:
            case State.State0Neutral:
               if (state0Sprite != null)
                {
                    stateMarkerImage.sprite = state0Sprite;
                }


                IveSeenThePlayer = false; //Setting back to 0;

                IveSeenTheDistraction = false;
                anim.SetBool("isScared", false);
                IveSeenTheDistraction = false;


                //States
                State0 = true;
                State1 = false;

                if (civilianFOV != null)
                {
                    //While we are patrolling & if the player enters the FOV
                    if (civilianFOV.Distraction == true && CantSeeDistraction == false || civilianFOV.SeenCrime == true)
                    {
                        state = State.State1Alerted;
                    }

                    //if (civilianFOV.PlayerinFOV == true || civilianFOV.Distraction == true && CantSeeDistraction == false || civilianFOV.SeenCrime == true)
                    //{
                    //    state = State.State1Alerted;
                    //}
                }
            


                break;



            case State.State1Alerted:
                // end patrol

                //States
                State0 = false;
                State1 = true;


                stateMarkerImage.sprite = state1Sprite;

                //When we enter the alerted state the enenmy will pause. 
                if (IveSeenThePlayer == false || civilianFOV.Distraction == true && IveSeenTheDistraction == false)
                {
                    if (waitTime <= 0)
                    {
                        waitTime = StartleWaitTime;


                        GameObject PoliceCube = Instantiate(Alarm.Instance.CallPoliceCube, this.gameObject.transform.position, this.gameObject.transform.rotation);
                        Alarm.Instance.CallForPolice(PoliceCube);
                        
                            

                        if (civilianFOV.PlayerinFOV == true)
                        {
                            IveSeenThePlayer = true;
                            patrol.Startled = false;
                        }

                        if (civilianFOV.PlayerinFOV == false)
                        {
                            IveSeenThePlayer = true;
                            patrol.Startled = false;
                        }

                        //patrol.isAlerted = true;
                        //  }

                        if (civilianFOV.Distraction == true && CantSeeDistraction == false)
                        {
                            IveSeenTheDistraction = true;
                            //patrol.isAlerted = true;                                            //CHECK THIS . ITS SAPPOSED TO TURN OFF IF PLAYER LEAVES BUT IS USED TO UPDATE THE LKL
                        }

                        //The timerwont run again until we 'forget' about the player / distraction
                    }
                    else
                    {
                        anim.SetBool("isAlerted", true);
                        //ADD animation of looking around while 'shocked'
                        waitTime -= Time.deltaTime;

                    }
                }


                //After the civilian becomes startled and calls the police, this will determine what they do afterwards, depending on whether Murphys is in the FOV or not. 

                //if the player is STILL IN FOV. wait a few seconds.-
                while (IveSeenThePlayer == true && civilianFOV.PlayerinFOV == true)
                {
                    // if player IS in FOV
                    //JOG TO THE PLAYER

                    //  patrol.isWalkingToLastKnownLOC = true;
                    anim.SetBool("isScared", true);
                    anim.SetBool("isAlerted", false);
                    //patrol.isAlerted = true;
                    // patrol.isWalkingToLastKnownLOC = false;
                    // patrol.Startled = true;
                    patrol.Startled = false;

                    break;
                }


                while (IveSeenThePlayer == true && civilianFOV.PlayerinFOV == false)
                {
                    //  patrol.isWalkingToLastKnownLOC = true;

                    // if player is not in FOV
                    //JOG TO LAST KNOWN LOCATION
                    // patrol.isWalkingToLastKnownLOC = true;
                    // patrol.Startled = true;
                    patrol.Startled = false;
                    anim.SetBool("isScared", true);
                    anim.SetBool("isAlerted", false);

                    break;
                }




                while (civilianFOV.Distraction == true && IveSeenTheDistraction == true && CantSeeDistraction == false)
                {
                    patrol.isWalkingToDistraction = true;
                    patrol.isAlerted = false;
                    break;
                }




                break;

        }

        


    }

    public void StepDownTimer()
    {
        if (stepDownWaitTime <= 0)
        {

            if (state == State.State1Alerted)
            {
                IveSeenThePlayer = false;


                if (IveSeenTheDistraction == true)               //Resetting the Tag via FOV
                {
                    dissolveDistraction = true;
                    IveSeenTheDistraction = false;
                    CantSeeDistraction = true;
                }

                state = State.State0Neutral;
                stepDownWaitTime = startStepDownWaitTime;
            }

            //if (state == State.State2Alarmed)
            //{
            //    state = State.State1Alerted;
            //    stepDownWaitTime = startStepDownWaitTime;
            //}

        }
        else
        {
            //ADD animation of looking around while 'shocked'
            stepDownWaitTime -= Time.deltaTime;
            patrol.Startled = false;
            patrol.isAlert = false;
        }




    }
}
