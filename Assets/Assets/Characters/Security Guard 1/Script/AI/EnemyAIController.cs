using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAIController : MonoBehaviour
{
    private enum State
    {
        State0Neutral,
        State1Alerted,
        State2AttackReady,
        State3HightnedAlert,
    }

    private State state;

    [Header("State Marker")]
    [Tooltip("Status Marker")]
    //Quest UI Marker for NPC
    public GameObject StateMarker;//quest marker  game object (canvas)
    public Image stateMarkerImage;//image on canvas
    public Sprite state0Sprite; //sprite state 0 - Neutral
    public Sprite state1Sprite; //sprite state 1 - Alterted
    public Sprite state2Sprite; //sprite state 2 - Looking
    public Sprite state3Sprite; //sprite state 3 - Attacking

    [Tooltip("Distance of AutoAttacking")]
    public float attackRange;
    private float distance; //Distance of player to Obecect
    public float AlertStateCountdown = 200.00f;
    private float AlertStateCountdownMax;
    public float DodgeCounter;
    public float DodgePercentage;
    public float Dodgespeed;



    [Header("Combat References")]
    public Transform player;
    public GameObject EnemyWeapon; //this tells what weapon the enemy is holding
                                   //public String EnemyWeaponName;
    public static bool enemyhitDetected = false;
    public static bool playerhitDetected = false;
    public static bool dodging;

    [Header("AlertStates")]
    public bool StartDodging;
    public bool State0Neutral;       //Neutral state
    public bool State1Alerted;       //alerted state state
    public bool State2AttackReady;   //Attack State
    public bool State3HightnedAlert; // Hightned alert
    public bool checkState;
    public bool startAlertCountdown;
    public int intelligence = 5;
    public bool DowngradeAlertState;
    public bool UpgradeAlertState;
    public bool enemyAlerted;
    

    [Header("Alert Delay")]
    [Tooltip("This is the delay between becoming alert and going into state 1. If enemy is still in the area the enemy will attack")]
    public float EnemyAlertDelay = 1.0f;
    public float EnemyChaseDelay = 0.5f;
    public float EnemyAttackDelay = 0.1f;
    [Tooltip("Distance when enemy alert disabled and attack starting")]
    public float attackDistance; //within attacking distance
                                 // public float atkDistOveride = 4;
    [Header("View")]
    public float viewRadius;    //viewing radius of player
    public bool PlayerinFOV;

    [Header("Reference to other script")]
    private CharacterCombat combat; // reference to Enemy Attacking Player
    public CharacterStats PlayerStats;
    private Patrol Patrol;
    private Chase chase;
    public EquipmentManager equipmentManager;
    static Animator anim;
    public static EnemyFOV enemyFOV;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();            // get the animator component attached to enemy
        chase = GetComponent<Chase>();              //refernece to  chase script
        combat = GetComponent<CharacterCombat>();   // reference to Enemy Attacking Player
        Patrol = GetComponent<Patrol>();            // reference to the patrol script

        //Set everything as 0 
        state = State.State0Neutral;                       //The starting state is 0.
        AlertStateCountdownMax = AlertStateCountdown;//Reset the alert countdown

        stateMarkerImage.sprite = state0Sprite;
        playerhitDetected = false;
        enemyhitDetected = false;

       // viewRadius = enemyFOV.viewRadius;
//        PlayerinFOV = enemyFOV.PlayerinFOV;

//        attackRange = enemyFOV.attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.State0Neutral:
                Patrol.isOnPatrol = true;

                State0Neutral = true;
                State1Alerted = false;
                State2AttackReady = false;
                State3HightnedAlert = false;

                if (enemyFOV.PlayerinFOV == true)
                {

                }

                break;

            case State.State1Alerted:
                //INSERT CODE FOR THIS STATE
                Patrol.isOnPatrol = true;

                

                State0Neutral = false;
                State1Alerted = true;
                State2AttackReady = false;
                State3HightnedAlert = false;

                break;

            case State.State2AttackReady:
                //INSERT CODE FOR THIS STATE

               

                //INSERT CODE FOR THIS STATE
        

                State0Neutral = false;
                State1Alerted = false;
                State2AttackReady = true;
                State3HightnedAlert = false;

                break;

            case State.State3HightnedAlert:
                //INSERT CODE FOR THIS STATE

                chase.isAttackRunning = true;
                chase.isIdleAlerted = false;
                chase.StartAlertWalk = false;
                enemyAlerted = false;

            
                State0Neutral = false;
                State1Alerted = false;
                State2AttackReady = false;
                State3HightnedAlert = true;

                break;


        }


        if (State0Neutral == true)
        {
            stateMarkerImage.sprite = state0Sprite;
        }

        if (State1Alerted == true)
        {
            stateMarkerImage.sprite = state1Sprite;
        }

        if (State2AttackReady == true)
        {
            stateMarkerImage.sprite = state2Sprite;
        }
        if (State3HightnedAlert == true)
        {
            stateMarkerImage.sprite = state0Sprite;
        }





        if (enemyAlerted == true)
        {
            StartCoroutine(EnemyAlerted());
        }

        if (enemyAlerted == false)
        {
            StopCoroutine(EnemyAlerted());
        }
    }


    //to change state state = State.Statexx


    IEnumerator EnemyChase()
    {                                   //Player IS STILL IN the FOV of the enemy
        yield return new WaitForSeconds(EnemyChaseDelay);
        if (Vector3.Distance(player.position, this.transform.position) <= viewRadius)
        {
            chase.StartChase = true;
            chase.Walking = true;
            // stopWalking = false;

        }
        //Player has ESCAPED the FOV of the enemy
        else if (Vector3.Distance(player.position, this.transform.position) >= viewRadius)
        {
            StopCoroutine("EnemyChase");
            print("End Chase");
            chase.StartChase = false;
            //chase.Walking = false;
            print("Player out of range");
            StopCoroutine("StartAlertWalking");
            //stopWalking = true;

        }
    }

    //Dodging
    IEnumerator Dodge()
    {

        if (dodging == true)
        {
            anim.SetBool("isDodging", true);
            transform.position += (transform.forward * -1) * Dodgespeed * Time.deltaTime;
            yield return new WaitForSeconds(1f);
            anim.SetBool("isDodging", false);
            dodging = false;
        }
    }

    void AlertCooldownTimer()
    {
        //
        // HightenedAlertActive = true;                                //The Enemy is now in a highted state for a time being.
        // StartCoroutine("EnemyAlerted");                             //Look for the player

        if (PlayerinFOV == false)
        {
            // ---- >START TIMER IN ALERT STATE

            AlertStateCountdown -= Time.deltaTime;
            // print(AlertStateCountdownMax);


            //When reaches 0 Move down states
            if (AlertStateCountdown <= 0)
            {

                //Go down chain
                if (State2AttackReady == true && startAlertCountdown == false)
                {
                    print("Moving from 2 - 3");
                    State2AttackReady = false;                          //If enemy in state 2 --> 3
                    State3HightnedAlert = true;                         //Enable State 3
                    AlertStateCountdown = AlertStateCountdownMax;       //Reset the timer
                    startAlertCountdown = true;                         //reset the timer
                }

                //else  if (State3HightnedAlert == true && startAlertCountdown == false)
                //{
                //    print("Moving from 3 - 1");
                //    State3HightnedAlert = false;                        //If enemy in state 3 --> 1
                //    State1Alerted = true;                               //Enable State 1
                //    AlertStateCountdown = AlertStateCountdownMax;       //Reset the timer
                //    startAlertCountdown = true;                         //reset the timer
                //}

                //else if (State1Alerted == true && startAlertCountdown == false)
                //{
                //    print("Moving from 1 - 0");
                //    State1Alerted = false;                              //If enemy in state 1 --> 0
                //    State0Neutral = true;                               //Enable State 0
                //    AlertStateCountdown = AlertStateCountdownMax;       //Reset the timer
                //    startAlertCountdown = true;                         //reset the timer
                //}




            }

        }

    }


    //CHANGE THIS TO A STEP COUNTER WHERE IS i + 1, to change the state from one state to another. 




    public IEnumerator DowngradeState()
    {
        print("Downgrade State");
        yield return new WaitForSeconds(0.01f);


        if (State0Neutral == true)                                          //if enemy is in neutral state move to state 1                                        
        {
            print("ERROR CODE : 1 CHECK STATE");
            state = State.State0Neutral;
            DowngradeAlertState = true;
            StopCoroutine(DowngradeState());
        }

        else if (State1Alerted == true)                                     //If in State 1 Move to state 3 
        {
            state = State.State0Neutral;
            DowngradeAlertState = true;
            StopCoroutine(DowngradeState());
        }

        else if (State3HightnedAlert== true)                                     //If in State 1 Move to state 3 
        {
            state = State.State1Alerted;
            DowngradeAlertState = true;
            StopCoroutine(DowngradeState());
        }

        else if (State2AttackReady == true)                                     //If in State 1 Move to state 3 
        {
            state = State.State3HightnedAlert;
            DowngradeAlertState = true;
            StopCoroutine(DowngradeState());
        }

    }






    //..Here we are in an alerted state.
    IEnumerator EnemyAlerted()
    {
        //Here we are BECOMING ALERT
        //checkState = false;
        //Start Alert Animation

        Patrol.isOnPatrol = false;                          //Stop patrol
        chase.Walking = false;                              //Stop Walking
        anim.SetBool("isAlerted", true);                    //Setting Alerted to true


        yield return new WaitForSeconds(EnemyAlertDelay);   // This will wait for x seconds before 'Going into of state 1'

        state = State.State1Alerted;

        //State0Neutral = false;                                //The Enemy is not neutral.
        //State1Alerted = true;                                 // Set the alert state to 1

        chase.Walking = true;                                 //Start Walking
        anim.SetBool("isAlerted", false);                    //Setting Alerted to true

        chase.StartAlertWalk = true;

        StopCoroutine(EnemyAlerted());                      //Stop This CoRoutine;
                                                            //AlteredAlerteState();
                                                            //TO DO : INSERT ICON FOR THE STATE OF THE ENEMY. 

    }




    
    }