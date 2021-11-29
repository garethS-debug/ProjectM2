using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    /*
     * This script deals with how the enemy deals with the player being within eyesight. 
     */


    public static Chase chase;
    static Animator anim;

    [Header("References to Character")]
    [Tooltip("Reference to the main character transform")]
    public Transform player; // exposed variable in inspector.
    [Tooltip("Reference to the main character")]
    public GameObject MainCharacter;
    //transform
    private Transform playerTransform;
    [Space(10)]



    public float startattackDistance = 3f;
    [TextArea]
    public string Notes = "Comments here."; // note on code
                                            /*
                                             * Start attack distance is the distance the player needs to get within for the enemy to automatically start attacking
                                             */

    [Header("Speed / Direction")]
    public float defaultMoveSpeed = 1.8f;
    public float moveSpeed = 1.8f;
    private float speed = 0.0f;
    [Tooltip("test")]
    private float speedDampTime = 0.09f;
    public float defaultSpeed;
    [Tooltip("Running speed of guard TIP: set very low e.g. 0.001")]
    public float runningSpeed; //speed of the guard running
    private float direction = 0f;
    private float DirectionDampTime = .25f; //used to set delay direction variable in anim controller (mainly for piviting



    [Header("Floats")]
    [Tooltip("Time the enemy remains 'looking' for the character after being alerted to their presence")]
    public float idleAlertedTime = 10.1f;
    //public float Alertbufferdistance; // commented out
    [Tooltip("Buffer distance to stop enemy reaching exact location of player")]
    public float attackbufferdistance;
    [Tooltip("This is the distance the player needs to be within for the attack naimation to play")]
    public float attackRange; //This is the distance the player needs to be within for the attack naimation to play
    [Tooltip("Buffer distance to stop enemy reaching exact location of a normal location")]
    public float bufferdistance;
    public float IdleAndAlertedTime;
    public float IdleAndAlertedTimeMax;

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

    [Space(10)]

    [Header("Control Bools")]
    //BOOLS
    [SerializeField]
    public bool StartChase = false;
    // public static bool WithinAttackRange = false;
    public static bool damageAnim = false;
    public bool StartAlertWalk;
    public static bool enemyisAttacking;
    public bool AttackingCooldown;
    public bool Walking;
    public bool Running;
    public bool isAttacking;
    public bool lookatPlayer;
    public bool pauseAfterAttack;
    public bool isIdleAlerted;
    public bool isAttackRunning;
    [Tooltip("Control's Weapon Collider on Enemy")]
    // public bool enabledWeaponCollider;
    [Space(10)]

    [Header("Last Known Loc")]
    //Other
    public Vector3 lastknownlocation;
    private Vector3 EnemyVector;
    // Use this for initialization
    [Space(10)]

    [Header("References to other scripts")]
    //References to other scripts
    public Patrol patrol;
    private NavMeshAgent _navMeshAgent; // reference t0 navmesh
    private EnemyFOV enemyFOV;
    private Combat combat;
    public EnemyAIController enemyAIController;

    [Header("RagDoll GO")]
    public GameObject RagdollGO;



    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>(); //REference to the Navmesh Agent
                                                      ////attackbufferdistance = _navMeshAgent.radius; //REference to the Navmesh's buffer distance

        combat = this.gameObject.GetComponent<Combat>();

        anim = GetComponent<Animator>(); // get the animator component attached to enemy

       // attackBufferTime = AttackBufferTimeMax;
        IdleAndAlertedTimeMax = IdleAndAlertedTime;
        AtkCoolDownTimerMax = AtkCoolDownTimer;
        //CoolDwnTimerMax = AtkCoolDownTimer;
        //AttackCoolDown = CoolDwnTimerMax / 2;

        playerTransform = this.transform;

        //reference to patrol script
        patrol = GetComponent<Patrol>(); // reference to the patrol script
        enemyFOV = GetComponent<EnemyFOV>(); // reference to enemy FOV script
        enemyAIController = GetComponent<EnemyAIController>(); // reference to enemy controllerd script
                                                               // defaultSpeed = _navMeshAgent.speed;

        // enabledWeaponCollider = false;

       // defaultSpeed = _navMeshAgent.speed;

    }

    // Update is called once per frame
    void Update()
    {
        //Anim Controller
        //Direction -- > Anim
        Vector3 targetDir = patrol.WaypointPOS - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
       // print(angle);
        anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
        speed = _navMeshAgent.speed ;
        anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
        //Direction -- > Anim
        direction = angle / 360;

        ////This determines what angle the PLAYER is in relation to the security guard
        //Vector3 targetDir = player.position - transform.position;
        //Vector3 forward = transform.forward;
        //float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        //if (angle < -5.0F)
        //    print("turn left");
        //else if (angle > 5.0F)
        //    print("turn right");
        //else
        //    print("forward");



        #region LOS

        //Getting the last know location of the player. 
        lastknownlocation = gameObject.GetComponent<EnemyFOV>().LastKnownLocationVector3;
        EnemyVector = this.transform.position;
        #endregion

        // if (Vector3.Distance(player.position, this.transform.position) < DetectionRange) 

        // Distance is checked to see if the player is in 'Attack Range' 
        // if (Vector3.Distance(player.position, this.transform.position) < FollowRange && StartChase == true)

        if (Walking == true)
        {
            //Moving Animation 
            anim.SetBool("isWalking", true);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isLooking", false);

            _navMeshAgent.speed = 0.1f;

            anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
            anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);


        }

        else if (Walking == false)
        {
            //Moving Animation 
            anim.SetBool("isWalking", false);
            _navMeshAgent.speed = 0;
        }


        if (isIdleAlerted == true)
        {
            StartCoroutine(IdleAlerted());
        }

        if (isIdleAlerted == false)
        {
            StopCoroutine(IdleAlerted());
        }



      
        if (isAttackRunning == true)
        {
            StartCoroutine(StartAttackWalking());
        }

        if(isAttackRunning == false)
        {
            StopCoroutine(StartAttackWalking());
        }

        //if (enemyFOV.stopWalking == true)
        //{

        //    Walking = false;
        //}

        if (lookatPlayer == true)
        {
            transform.LookAt(player);
        }





        //Attack Animation -- Insert doing damage here
        if (isAttacking == true)
        {
            Vector3 direction = player.position - this.transform.position;          // getting direction to calculate angle between enemy & playr
            direction.y = 0;                                                        //// calculate the diection from the player to direction.
            float dist = Vector3.Distance(player.position, transform.position);     ////measureing distance to objective

            Running = false;


            if (dist <= attackRange)
            {
                Attacking();
                print("Start Attacking");
            }

            lookatPlayer = true;
        }





        else if (isAttacking == false)
        {
            AttackCounter = Mathf.Lerp(AttackCounter, 0.001f, Time.deltaTime * 3);

            lookatPlayer = false;

                                        //-----> Animator 
            Vector3 direction = player.position - this.transform.position;  // getting direction to calculate angle between enemy & playr
            direction.y = 0;                                                //// calculate the diection from the player to direction.
            float dist = Vector3.Distance(player.position, transform.position);////measureing distance to objective



            if (dist >= attackRange)
            {
                // enabledWeaponCollider = false ;
                anim.SetBool("isAttacking", false);
                //  combat.dealingDamage = false;
            }


        }


        if (StartChase == true)
        {
            // lookatPlayer = true;
            anim.SetBool("isAlerted", false);                    //Stopping the alert animation
            Walking = true;                        //Enabling walking                          
            // Walking = true;
            //            print("StartCoroutine(\"StartAttackWalking\");");
            StartCoroutine("StartAttackWalking");
            StartAlertWalk = false;

        }

        if (StartChase == false)
        {
            // Walking = false;
            StopCoroutine("StartAttackWalking");
            isAttacking = false;
            // lookatPlayer = false;

        }

        if (Running == true)
        {
            _navMeshAgent.speed = _navMeshAgent.speed + runningSpeed;
        }

        if (Running == false)
        {
            //revert to normal speed
            _navMeshAgent.speed = defaultMoveSpeed;
        }



        if (StartAlertWalk == true)
        {
                StartCoroutine("StartAlertWalking");
        }

        if (StartAlertWalk == false)
        {
            StopCoroutine("StartAlertWalking");

            //  Walking = false;
        }

        
    }



    //Start walking to last known player location
    //THIS IS CAUSING ISSUES WHEN WALKING ---- IDLE ALERTED STILL PLAYING
    //LOOK AT THE BUFFER DISTANCE

    IEnumerator StartAlertWalking()
    {
        print("Start Alert Walk ");
        float dist = Vector3.Distance(lastknownlocation, this.transform.position);  //measureing distance to objective

        //Anim Controller
        //New anim Direction -- > Anim
        Vector3 targetDir = lastknownlocation - transform.position;
        //float angle = Vector3.Angle(targetDir, transform.forward);
        //anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
        //direction = angle / 360;



        yield return new WaitForSeconds(0.1f);                                      //Wait


        //WHEN WE GET TO THE 'LAST KNOWN LOCATION IT CAUSES ISSUES

        if (dist >= bufferdistance)                                                 //if enemy is more than the buffer distance from th objective
        {
            anim.SetBool("isLooking", false);                                       //stopping other scripts that might conflict
           // isIdleAlerted = false;                                           //stopping other scripts that might conflict
            Walking = true;                                                         // Walk is true
            _navMeshAgent.SetDestination(lastknownlocation);                        //Set location to player
           // anim.SetBool("isAlerted", false);                                     //we are not looking for player
           // anim.SetBool("isLooking", false);                                     //stopping other scripts that might conflict
   
        }

        if (dist <= bufferdistance)                                              //If we REACHED THE OBJECTIVE distance stops us
        {

            Walking = false;                                                    //stop walking
            patrol.pauseWalk = true;
            _navMeshAgent.isStopped = true;                                     // StopCoroutine("StartAlertWalking");                                 //stop alert walking
            isIdleAlerted = true ;
            StartAlertWalk = false;
            StopCoroutine(StartAlertWalking());                                 //Stop This CoRoutine;
            // anim.SetBool("isLooking", true);
            // StartAlertWalk = false;                                             // End the script
            //Add in here a 'random' chance of the enemy player doing a small patrol of the area
            //IdleAndAlertedTimer();                                  //Set the 'alerted' animation to play so that the enemy is looking for the player
        }





    }
    //Player is standing and looking for the player after walking to the last known location. After a time then leaves
    IEnumerator IdleAlerted()
    {
        print("Idle Alerted Start");
        Walking = false;                                       //Setting the walk to false           
        anim.SetBool("isLooking", true);                        //Looking for the player

        yield return new WaitForSeconds(idleAlertedTime);       //Waiting a few seconds looking for the player. Then if we have not found the player, move back to patrol. 

        //enemyAIController.AlertCooldownTimer();

        Walking = true; //Setting the walk to false             //Walking is true
        anim.SetBool("isLooking", false);                       //Stop Looking

        print("Stop Idle Alerted ");                            //anim.SetBool("isIdle", true);

        // enemyAIController.StopCoroutine("DowngradeState");      //Downgrade State

        isIdleAlerted = false;

       
       // enemyAIController.DowngradeAlertState = true;
       // StopCoroutine("IdleAlerted");                       //Stop Idle Alerted
       //enemyFOV.State1Alerted = false;                     //Enemy Not alerted
    }




    IEnumerator StartAttackWalking()
    {
       
        yield return new WaitForSeconds(0.01f);

        Vector3 direction = player.position - this.transform.position;  // getting direction to calculate angle between enemy & playr
        direction.y = 0;                                                //// calculate the diection from the player to direction.
        float dist = Vector3.Distance(player.position, transform.position);////measureing distance to objective

        //Buffer distance is set to a large number as we are move to a far point
        if (dist >= attackbufferdistance)
        {
            isAttacking = false;
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(player.position);
                print("error code 1: moving to P pos");

        }

        else if (dist <= attackbufferdistance)
        {
        
            _navMeshAgent.isStopped = true;
            _navMeshAgent.speed = 0;

            print("error code 2:i should be stopped walking");
            isAttacking = true;
            
        }

    }





    public void IdleAndAlertedTimer()
    {
        IdleAndAlertedTime -= Time.deltaTime;
        Walking = false;
        anim.SetBool("isLooking", true);

      

        if (IdleAndAlertedTime <= 0)
        {
            IdleAndAlertedTime = IdleAndAlertedTimeMax; //Reset the timer
            StartAlertWalk = false;

        }


    }

    IEnumerator PauseAfterAttack()
    {
        yield return new WaitForSeconds(PauseBetweenAttacks);
        // enabledWeaponCollider = false;
        pauseAfterAttack = true;
        yield return new WaitForSeconds(PauseBetweenAttacks);
        pauseAfterAttack = false;

        // enabledWeaponCollider = true;
     
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


    public void Attacking()
    {
        print("error code 7: am attacking");
        //enemyAIController.State2AttackReady = true;

        Walking = false;
            //Set the attacking to true. 
        anim.SetBool("isAttacking", true);
        StartChase = false;
        StartAlertWalk = false;


        //Anim Controller
        //Direction -- > Anim
        Vector3 targetDir = player.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        // print(angle);
        anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
        speed = _navMeshAgent.speed;
        anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
        //Direction -- > Anim
        direction = angle / 360;



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
}


  





