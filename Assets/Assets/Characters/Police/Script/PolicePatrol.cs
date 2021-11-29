using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PolicePatrol : MonoBehaviour
{
    //[Header("Speed")]
    //public float Speed;

    [Header("WaitTime")]
    public float waitTime;
    public float StartWaitTime;

    [Header("List of Transforms")]
    //public Transform[] moveLocations;
    public List<Transform> moveLocations = new List<Transform>();


    [Header("Enenmy Ai Controller")]
    public bool isOnPatrol;
    public bool isAlerted;
    public bool isWalkingToLastKnownLOC;
    public bool isAttack;

    private int randomSpot;

    [Header("Player")]
    public GameObject player;
    private PoliceAI enemyAIController;

    [Header("Police")]
    public bool isRunningBackToVan;

    [Header("Last Known LOC")]
    public List<Vector3> LastKnownLOC = new List<Vector3>();
    public Vector3 LastKnownLocation;
    public bool isWalkingtoLastKnownLoc;

    [Header("Attacking")]
    private PoliceFOV enemyFOV;
    public bool inAttackRange;

    [Header("Alarm Raised")]
    public List<Transform> AlarmRaisedLocations = new List<Transform>();
    public bool isRunningToAlarmRaisedPatrolPoints;
    public bool isRunningToShout;
    private int randomAlarmLOC;

    [Header("Navmesh Agent Reset")]
    public bool resetPath;


    [Header("Food Time")]
    public bool isRunningToFood;
    public List<Transform> FoodGatheringLocations = new List<Transform>();
    private int randomFoodLOC;

    [Header("AlertedByStaff")]
    public bool HasBeenAlertedByStaff;

    [Header("Distraction")]
    public bool isWalkingToDistraction;

    [Header("NavMesh")]
    private float ObsticeAvoidanceRadious;
    private NavMeshAgent _navMeshAgent;



    [Header("Walking  Anim")]
    public bool Walking;
    public bool PauseInWalk;
    private Animator anim;
    public float defaultMoveSpeed = 1.8f;
    public float moveSpeed = 1.8f;
    private float speed = 0.0f;
    [Tooltip("test")]
    public float speedDampTime = 0.09f;
    public float defaultSpeed;
    [Tooltip("Running speed of guard TIP: set very low e.g. 0.001")]
    public float runningSpeed; //speed of the guard running
    private float direction = 0f;
    private float DirectionDampTime = .25f; //used to set delay direction variable in anim controller (mainly for piviting

    [Header("Startled")]
    public bool Startled;

    [Header("Jogging")]
    public bool Jogging;

    [Header("Crime Scene")]
    public bool SearchArea;
    public int MaxAmountOfPolice = 2;
    public GameObject policeSpawnPrefab;
    private int randomSearchSpot;
    private bool MaxSpawnsReached = false;
    public List<GameObject> TempPoliceOfficerSpawn = new List<GameObject>();

    [Header("Feet Grounder")]
    //Feet grounder
    private Vector3 rightFootPosition, leftFootPosition, leftFootIkPosition, rightFootIkPosition;
    private Quaternion leftFootIkRotation, rightFootIkRotation;
    private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;
    public bool enableFeetIk = true;
    [Range(0, 2)] [SerializeField] private float heightFromGroundRaycast = 1.14f;
    [Range(0, 2)] [SerializeField] private float raycastDownDistance = 1.5f;
    [SerializeField] private LayerMask environmentLayer;
    [SerializeField] private float pelvisOffset = 0f;
    [Range(0, 1)] [SerializeField] private float pelvisUpAndDownSpeed = 0.28f;
    [Range(0, 1)] [SerializeField] private float feetToIkPositionSpeed = 0.5f;
    public string leftFootAnimVariableName = "LeftFootCurve";
    public string rightFootAnimVariableName = "RightFootCurve";
    public bool useProIkFeature = false;
    public bool showSolverDebug = true;


    void Start()
    {
        randomAlarmLOC = Random.Range(0, AlarmRaisedLocations.Count);

        randomSpot = Random.Range(0, moveLocations.Count);

        //randomSearchSpot = Random.Range(0, searchingTheAreaLOC.Count);

        player = PlayerManager.instance.player;

        enemyAIController = gameObject.GetComponent<PoliceAI>();
        enemyFOV = gameObject.GetComponent<PoliceFOV>();
        anim = this.gameObject.GetComponent<Animator>(); // get the animator component attached to enemy
        _navMeshAgent = GetComponent<NavMeshAgent>(); //REference to the Navmesh Agent
    }


    void Update()
    {
        float playerPOS = Vector3.Distance(player.transform.position, this.transform.position);
        //   print(playerPOS);


        if (LastKnownLOC.Count >= 10.00f)
        {
            LastKnownLOC.RemoveRange(2, 8);
        }

        if (resetPath == true)
        {
            enemyAIController.pauseAfterAttack = false;
            Walking = false;
            Jogging = false;
            PauseInWalk = true;
            _navMeshAgent.ResetPath();
            _navMeshAgent.SetDestination(moveLocations[randomSpot].position);
            resetPath = false;
        }


        if (playerPOS <= enemyFOV.attackRange && enemyFOV.inPeriphVision == true)
        {
            print("In attack range");
            //Player is in attack range
            isWalkingToLastKnownLOC = false;
            isAlerted = false;
            isOnPatrol = false;

            Walking = false;
            Startled = false;
            Jogging = false;

            isAttack = true;

            inAttackRange = true;
            // break;
        }

        if (enemyAIController.State2 == true && playerPOS <= enemyFOV.attackDistance)
        {
            inAttackRange = true;
        }

        if (enemyAIController.State2 == true && playerPOS >= enemyFOV.attackDistance)
        {
            inAttackRange = false;
            _navMeshAgent.SetDestination(LastKnownLocation);
            Walking = false;
            PauseInWalk = false;
            Jogging = true;
            anim.SetBool("isAttacking", false);

        }



        //


        //Moving Function while on patrol
        while (isOnPatrol == true)
        {
            isWalkingToLastKnownLOC = false;

            //moving function 
            // original --- transform.position = Vector3.MoveTowards(transform.position, moveLocations[randomSpot].position, Speed * Time.deltaTime);

            if (this.gameObject.tag == "Police" && isRunningBackToVan == false)
            {

                // waiting once we have reached a location close to the target position
                if (Vector3.Distance(transform.position, moveLocations[randomSpot].position) < 1.0f)
                {
                    Walking = false;
                    PauseInWalk = true;

                    //is it time for the enemy to move to the next patrol point 
                    if (waitTime <= 0)
                    {
                        //change the random location
                        randomSpot = Random.Range(0, moveLocations.Count);
                        waitTime = StartWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                    }
                }

                if (Vector3.Distance(transform.position, moveLocations[randomSpot].position) > 1.0f)
                {
                    Walking = true;
                    _navMeshAgent.SetDestination(moveLocations[randomSpot].position);
                    // transform.LookAt(moveLocations[randomSpot].position);
                    // _navMeshAgent.destination = moveLocations[randomSpot].position;
             
                }
            }
       

            if (this.gameObject.tag == "Enemy")
            {
                print("is On Patrol");

                // waiting once we have reached a location close to the target position
                if (Vector3.Distance(transform.position, moveLocations[randomSpot].position) < 1.0f)
                {
                    Walking = false;
                    PauseInWalk = true;

                    //is it time for the enemy to move to the next patrol point 
                    if (waitTime <= 0)
                    {
                        //change the random location
                        randomSpot = Random.Range(0, moveLocations.Count);
                        waitTime = StartWaitTime;

                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                        //ADD animation of looking around while waiting
                    }
                }

                if (Vector3.Distance(transform.position, moveLocations[randomSpot].position) > 1.0f)
                {
                    Walking = true;
                    _navMeshAgent.SetDestination(moveLocations[randomSpot].position);

                    // transform.LookAt(moveLocations[randomSpot].position);
                    // _navMeshAgent.destination = moveLocations[randomSpot].position;

                    print("Walking to Patrol Point");
                }
            }



            break;
        }

        while (isAlerted == true)
        {
            //ADD script to run to player
            //moving function 
            if (Vector3.Distance(transform.position, player.transform.position) > 2.0f)
            {
                if (LastKnownLOC.Count >= 1)
                {
                    //grab the last vector in the last known LOC
                    int ListNumber = LastKnownLOC.Count;
                    if (LastKnownLOC != null)
                    {
                        LastKnownLocation = LastKnownLOC[ListNumber - 1];
                    }



                }

                // original  ---- transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
                _navMeshAgent.SetDestination(LastKnownLocation);
                //transform.LookAt(player.transform.position);
                Walking = false;
                PauseInWalk = false;
                Jogging = true;

            }





            break;
        }

        while (isWalkingToLastKnownLOC == true)
        {
            //ADD script to run to players last known LOC
            isWalkingToLastKnownLOC = true;

            if (LastKnownLOC.Count >= 1)
            {
                int ListNumber = LastKnownLOC.Count;
                LastKnownLocation = LastKnownLOC[ListNumber - 1];
            }

            if (LastKnownLOC.Count <= 1)
            {

                int ListNumber = LastKnownLOC.Count;
                LastKnownLocation = LastKnownLOC[ListNumber - 2];

            }


            //if (enemyAIController.IveSeenTheDistraction == true)
            //{
            //    //grab the last vector in the last known LOC
            //    int ListNumber = LastKnownLOC.Count;
            //    LastKnownLocation = LastKnownLOC[ListNumber - 1];
            //}


            if (Vector3.Distance(transform.position, LastKnownLocation) > 2.0f)
            {
                //original-----  transform.position = Vector3.MoveTowards(transform.position, LastKnownLocation, Speed * Time.deltaTime);
                _navMeshAgent.SetDestination(LastKnownLocation);
                Walking = false;
                PauseInWalk = false;
                Jogging = true;
            }

            if (Vector3.Distance(transform.position, LastKnownLocation) < 2.0f)
            {
                _navMeshAgent.isStopped = true;
                resetPath = true;
                enemyAIController.StartClimbDown = true;
                Walking = false;
                PauseInWalk = true;
                Jogging = false;
            }

            //Once player is within x distance of last Known LOC. Start Timer to 'FORGET' 
            break;
        }

        while (isRunningToAlarmRaisedPatrolPoints == true && enemyAIController.ICanHearTheAlarm == false)
        {
            //  _navMeshAgent.SetDestination(AlarmRaisedLocations[randomAlarmLOC].position);


            // waiting once we have reached a location close to the target position
            if (Vector3.Distance(transform.position, AlarmRaisedLocations[randomAlarmLOC].position) < 0.9f)
            {
                Jogging = false;
                Walking = false;
                PauseInWalk = true;


                //is it time for the enemy to move to the next patrol point 
                if (waitTime <= 0)
                {
                    //change the random location
                    randomAlarmLOC = Random.Range(0, AlarmRaisedLocations.Count);
                    waitTime = StartWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                    //ADD animation of looking around while waiting
                }
            }

            if (Vector3.Distance(transform.position, AlarmRaisedLocations[randomAlarmLOC].position) > 0.9f)
            {
                _navMeshAgent.SetDestination(AlarmRaisedLocations[randomAlarmLOC].position);
                Walking = false;
                Jogging = true;
                PauseInWalk = false;

            }


            break;
        }









        while (isRunningToFood == true)
        {
            //  _navMeshAgent.SetDestination(AlarmRaisedLocations[randomAlarmLOC].position);

            isOnPatrol = false;
            //  randomFoodLOC = Random.Range(0, FoodGatheringLocations.Count);
            Walking = false;

            if (randomFoodLOC <= 0)
            {
                randomFoodLOC = Random.Range(0, Alarm.Instance.FoodGatheringLocations.Count);

            }



            // waiting once we have reached a location close to the target position
            if (Vector3.Distance(transform.position, FoodGatheringLocations[randomFoodLOC].position) < 0.9f)
            {
                // Jogging = false;
                Walking = false;
                //PauseInWalk = true;

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;

                anim.SetFloat("Speed", 0.0f, speedDampTime, Time.deltaTime);
                //anim.SetBool("isWalking", false);
                PauseInWalk = true;
                anim.SetBool("isEating", true);
                //Walking = false;
                ////is it time for the enemy to move to the next patrol point 
                //if (waitTime <= 0)
                //{
                //    //change the random location
                //    randomFoodLOC = Random.Range(0, Alarm.Instance.FoodGatheringLocations.Count);
                //    waitTime = StartWaitTime;
                //}
                //else
                //{
                //    waitTime -= Time.deltaTime;
                //    //ADD animation of looking around while waiting
                //}
            }

            if (Vector3.Distance(transform.position, FoodGatheringLocations[randomFoodLOC].position) > 0.9f)
            {
                _navMeshAgent.SetDestination(FoodGatheringLocations[randomFoodLOC].position);
                //  Walking = true;
                //Jogging = true;
                //anim.SetBool("isWalking", true);
                anim.SetFloat("Speed", 0.4f, speedDampTime, Time.deltaTime);
                PauseInWalk = false;
                isOnPatrol = false;
                Jogging = false;

                // Walking = true;

                //anim.SetFloat("Speed", 0.2f, speedDampTime, Time.deltaTime);
                //anim.SetBool("isWalking", true);

            }


            break;
        }


        while (enemyAIController.State2 == true && enemyFOV.PlayerinFOV == true)
        {
            while (Vector3.Distance(transform.position, player.transform.position) > 2.0f)
            {
                //original ---  transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
                _navMeshAgent.SetDestination(player.transform.position);


                break;
            }
            break;
        }


        //while (isWalkingToDistraction == true)
        //{
        //    isWalkingToTheDistraction();
        //    break;
        //}


        if (Walking == true)
        {
            PauseInWalk = false;
            Jogging = false;
            //Anim Controller
            //Direction -- > Anim
            //  Vector3 targetDir = LastKnownLocation - transform.position;
            //  float angle = Vector3.Angle(targetDir, transform.forward);

            //speed = _navMeshAgent.speed;

            // print(angle);
            //  anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
            anim.SetFloat("Speed", 0.1f, speedDampTime, Time.deltaTime);
            anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);
            //Direction -- > Anim
            //  direction = angle / 360;


            //Moving Animation 
            anim.SetBool("isWalking", true);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isLooking", false);

            // _navMeshAgent.speed = 0.1f;

        }

        if (PauseInWalk == true)
        {

            //Anim Controller
            //Direction -- > Anim
            //    Vector3 targetDir = LastKnownLocation - transform.position;
            //      float angle = Vector3.Angle(targetDir, transform.forward);
            //
            //  speed = _navMeshAgent.speed;

            // print(angle);
            //  anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
            anim.SetFloat("Speed", 0.0f, speedDampTime, Time.deltaTime);
            anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);
            //Direction -- > Anim
            //   direction = angle / 360;

            //Moving Animation 
            anim.SetBool("isWalking", false);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isLooking", false);
            // _navMeshAgent.speed = 0.1f;

        }

        if (Startled == true)
        {

            //Anim Controller
            //Direction -- > Anim
            Vector3 targetDir = LastKnownLocation - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            //  speed = _navMeshAgent.speed;
            PauseInWalk = true;
            Walking = false;

            _navMeshAgent.SetDestination(this.gameObject.transform.position);

            //// print(angle);
            //anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
            //anim.SetFloat("Speed", 0.0f, speedDampTime, Time.deltaTime);
            //anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);
            //Direction -- > Anim
            //   direction = angle / 360;

            //Moving Animation 
            anim.SetBool("isWalking", false);
            anim.SetBool("isAlerted", true);
            anim.SetBool("isLooking", false);
            // _navMeshAgent.speed = 0.1f;

        }

        if (Jogging == true)
        {
            PauseInWalk = false;

            anim.SetBool("isAlerted", false);
            //Anim Controller
            //Direction -- > Anim
            Vector3 targetDir = player.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            //speed = _navMeshAgent.speed;

            // print(angle);
            anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
            anim.SetFloat("Speed", runningSpeed, speedDampTime, Time.deltaTime);
            anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);
            //Direction -- > Anim
            //  direction = angle / 360;


            //Moving Animation 
            anim.SetBool("isWalking", true);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isLooking", false);

            // _navMeshAgent.speed = 0.1f;

        }
    }

    public void RunningBackToVan(GameObject Van)
    {
        isRunningBackToVan = true;
        isRunningToShout = false;
        isOnPatrol = false;

        // waiting once we have reached a location close to the target position
        if (Vector3.Distance(transform.position, Van.transform.position) < 0.3f)
        {
            Jogging = false;
            Walking = false;
            PauseInWalk = true;
            enemyAIController.PoliceCalledAlarmTime = enemyAIController.PoliceCalledAlarmTimeMax;


        }

        if (Vector3.Distance(transform.position, Van.transform.position) > 0.3f)
        {
            _navMeshAgent.SetDestination(Van.transform.position);
            Walking = false;
            Jogging = true;

        }


    }


    //Searching the area
    public void SearchingTheArea(GameObject enemySearching)
    {
        Startled = false;
        SearchArea = true;
        isRunningToShout = false;


        if (enemyFOV.SeenCrime == false && enemyFOV.PlayerinFOV == false)
        {


            // waiting once we have reached a location close to the target position
            if (Vector3.Distance(transform.position, moveLocations[randomSpot].position) < 1.0f)
            {
                Walking = false;
                PauseInWalk = true;

                //is it time for the enemy to move to the next patrol point 
                if (waitTime <= 0)
                {
                    resetPath = true;
                    //change the random location
                    randomSpot = Random.Range(0, moveLocations.Count);
                    waitTime = StartWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                    //ADD animation of looking around while waiting
                }
            }

            if (Vector3.Distance(transform.position, moveLocations[randomSpot].position) > 1.0f)
            {
                Walking = true;
                _navMeshAgent.SetDestination(moveLocations[randomSpot].position);

                //   transform.LookAt(moveLocations[randomSpot].position);
                // _navMeshAgent.destination = moveLocations[randomSpot].position;

                print("POlice:  Walking to Patrol Point");
            }
        }

        if (enemyFOV.SeenCrime == true && enemyFOV.PlayerinFOV == false)
        {
            if (enemyAIController.CrimeScene != false)
            {
                if (Vector3.Distance(transform.position, enemyAIController.CrimeScene.transform.position) > 3.0f)
                {
                    Walking = true;
                    _navMeshAgent.SetDestination(enemyAIController.CrimeScene.transform.position);
                    print("POlice: Walking to crime scene");
                }

                if (Vector3.Distance(transform.position, enemyAIController.CrimeScene.transform.position) < 3.0f)
                {
                    Walking = false;
                    PauseInWalk = true;
                    resetPath = true;
                    //CALLING FOR EXTRA POLICE OFFICERS
                    //if (policeSpawnPrefab != null)
                    //{
                    //    print("POlice:  Spawing Extra Help");
                    //    ////Spawning Temp Waypoints
                    //    if (TempPoliceOfficerSpawn.Count >= MaxAmountOfPolice)
                    //    {
                    //        MaxSpawnsReached = true;
                    //    }

                    //    if (TempPoliceOfficerSpawn.Count <= MaxAmountOfPolice)
                    //    {
                    //        MaxSpawnsReached = false;
                    //    }


                    //    while (MaxSpawnsReached == false)
                    //    {
                    //        for (int x = 0; x <= MaxAmountOfPolice; x++)
                    //        {
                    //            GameObject policeSpawnPrefabs = Instantiate(policeSpawnPrefab, enemyAIController.CrimeScene.transform.position + Vector3.right * Random.Range(-3, 3), Quaternion.identity);
                    //            TempPoliceOfficerSpawn.Add(policeSpawnPrefabs);
                    //        }
                    //        break;
                    //    }
                    //}
                }
            }

        }



    }


    //}





    //RUNNING TO SHOUT
    public void IsRunningToTheShouting(GameObject WhoShouted)
    {
        print("Im running to the shout");

        while (isRunningToShout == true)
        {
//            if (fov)
             if(enemyFOV.PlayerinFOV == true)
            {
             //   sfa
            }      

            Startled = false;
            // waiting once we have reached a location close to the target position
            if (Vector3.Distance(transform.position, WhoShouted.transform.position) < 2f)
            {
                Jogging = false;
                Walking = false;
                PauseInWalk = true;
                Alarm.Instance.CivilianHasCalledPolice = false;
                enemyAIController.StartClimbDown = true;
                //SearchingTheArea(this.gameObject);
                isRunningToShout = false;

            }

            if (Vector3.Distance(transform.position, WhoShouted.transform.position) > 2f)
            {
                //transform.LookAt(WhoShouted.transform.position);
                _navMeshAgent.SetDestination(WhoShouted.transform.position);
                Walking = false;
                Jogging = true;

            }
            break;
        }


    }


    //RUNNING TO Distraction
    public void isWalkingToTheDistraction(GameObject DistractionOBJ)
    {

        Startled = false;

        if (HasBeenAlertedByStaff == false)
        {
            if (Vector3.Distance(transform.position, DistractionOBJ.transform.position) > 2.0f)
            {
                //original-----  transform.position = Vector3.MoveTowards(transform.position, LastKnownLocation, Speed * Time.deltaTime);
                _navMeshAgent.SetDestination(DistractionOBJ.transform.position);
                Walking = false;
                PauseInWalk = false;
                Jogging = true;
            }

            if (Vector3.Distance(transform.position, DistractionOBJ.transform.position) < 2.0f)
            {
                //   _navMeshAgent.isStopped = true;
                //  enemyAIController.StartClimbDown = true;
                PauseInWalk = true;

                if (enemyAIController.IveSeenTheDistraction == true)
                {
                    enemyAIController.DistractionTimers();
                }

                Walking = false;
                PauseInWalk = true;
                Jogging = false;
            }
        }


    }


    //RUNNING TO Where Staff Saw Murphy
    public void IsRunningToMurphysLOC(Vector3 MurphysLOC)
    {
        print("Alerted by staff, is running to murph");
        while (HasBeenAlertedByStaff == true && enemyAIController.ICanHearTheAlarm == false && enemyAIController.ICanHearSomeoneShouting == false)
        {
            // waiting once we have reached a location close to the target position
            if (Vector3.Distance(transform.position, MurphysLOC) < 1.9f)
            {
                Jogging = false;
                Walking = false;
                // PauseInWalk = true;
                //  _navMeshAgent.isStopped = true;
                //   Alarm.Instance.CivilianHasCalledPolice = false;



                if (enemyFOV.PlayerinFOV == false)
                {
                    enemyAIController.StaffAlertingGuard = false;
                    enemyAIController.StartClimbDown = true;
                    resetPath = true;
                }

            }

            if (Vector3.Distance(transform.position, MurphysLOC) > 1.9f)
            {
                // resetPath = true;
                _navMeshAgent.SetDestination(MurphysLOC);
                Walking = false;
                Jogging = true;
                // PauseInWalk = false;

            }
            break;
        }


    }


    #region FeetGrounding

    /// <summary>
    /// We are updating the AdjustFeetTarget method and also find the position of each foot inside our Solver Position.
    /// </summary>
    private void FixedUpdate()
    {
        if (enableFeetIk == false) { return; }
        if (anim == null) { return; }

        AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
        AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

        //find and raycast to the ground to find positions
        FeetPositionSolver(rightFootPosition, ref rightFootIkPosition, ref rightFootIkRotation); // handle the solver for right foot
        FeetPositionSolver(leftFootPosition, ref leftFootIkPosition, ref leftFootIkRotation); //handle the solver for the left foot

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (enableFeetIk == false) { return; }
        if (anim == null) { return; }

        MovePelvisHeight();

        //right foot ik position and rotation -- utilise the pro features in here
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        if (useProIkFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));
        }

        MoveFeetToIkPoint(AvatarIKGoal.RightFoot, rightFootIkPosition, rightFootIkRotation, ref lastRightFootPositionY);

        //left foot ik position and rotation -- utilise the pro features in here
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

        if (useProIkFeature)
        {
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName));
        }

        MoveFeetToIkPoint(AvatarIKGoal.LeftFoot, leftFootIkPosition, leftFootIkRotation, ref lastLeftFootPositionY);
    }

    #endregion



    #region FeetGroundingMethods

    /// <summary>
    /// Moves the feet to ik point.
    /// </summary>
    /// <param name="foot">Foot.</param>
    /// <param name="positionIkHolder">Position ik holder.</param>
    /// <param name="rotationIkHolder">Rotation ik holder.</param>
    /// <param name="lastFootPositionY">Last foot position y.</param>
    void MoveFeetToIkPoint(AvatarIKGoal foot, Vector3 positionIkHolder, Quaternion rotationIkHolder, ref float lastFootPositionY)
    {
        Vector3 targetIkPosition = anim.GetIKPosition(foot);

        if (positionIkHolder != Vector3.zero)
        {
            targetIkPosition = transform.InverseTransformPoint(targetIkPosition);
            positionIkHolder = transform.InverseTransformPoint(positionIkHolder);

            float yVariable = Mathf.Lerp(lastFootPositionY, positionIkHolder.y, feetToIkPositionSpeed);
            targetIkPosition.y += yVariable;

            lastFootPositionY = yVariable;

            targetIkPosition = transform.TransformPoint(targetIkPosition);

            anim.SetIKRotation(foot, rotationIkHolder);
        }

        anim.SetIKPosition(foot, targetIkPosition);
    }
    /// <summary>
    /// Moves the height of the pelvis.
    /// </summary>
    private void MovePelvisHeight()
    {

        if (rightFootIkPosition == Vector3.zero || leftFootIkPosition == Vector3.zero || lastPelvisPositionY == 0)
        {
            lastPelvisPositionY = anim.bodyPosition.y;
            return;
        }

        float lOffsetPosition = leftFootIkPosition.y - transform.position.y;
        float rOffsetPosition = rightFootIkPosition.y - transform.position.y;

        float totalOffset = (lOffsetPosition < rOffsetPosition) ? lOffsetPosition : rOffsetPosition;

        Vector3 newPelvisPosition = anim.bodyPosition + Vector3.up * totalOffset;

        newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);

        anim.bodyPosition = newPelvisPosition;

        lastPelvisPositionY = anim.bodyPosition.y;

    }

    /// <summary>
    /// We are locating the Feet position via a Raycast and then Solving
    /// </summary>
    /// <param name="fromSkyPosition">From sky position.</param>
    /// <param name="feetIkPositions">Feet ik positions.</param>
    /// <param name="feetIkRotations">Feet ik rotations.</param>
    private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIkPositions, ref Quaternion feetIkRotations)
    {
        //raycast handling section 
        RaycastHit feetOutHit;

        if (showSolverDebug)
            Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (raycastDownDistance + heightFromGroundRaycast), Color.yellow);

        if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetOutHit, raycastDownDistance + heightFromGroundRaycast, environmentLayer))
        {
            //finding our feet ik positions from the sky position
            feetIkPositions = fromSkyPosition;
            feetIkPositions.y = feetOutHit.point.y + pelvisOffset;
            feetIkRotations = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;

            return;
        }

        feetIkPositions = Vector3.zero; //it didn't work :(

    }
    /// <summary>
    /// Adjusts the feet target.
    /// </summary>
    /// <param name="feetPositions">Feet positions.</param>
    /// <param name="foot">Foot.</param>
    private void AdjustFeetTarget(ref Vector3 feetPositions, HumanBodyBones foot)
    {
        feetPositions = anim.GetBoneTransform(foot).position;
        feetPositions.y = transform.position.y + heightFromGroundRaycast;

    }

    #endregion

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            foreach (CapsuleCollider c in GetComponents<CapsuleCollider>())
            {
                c.enabled = false;
            }
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Stairs")
        {
            foreach (CapsuleCollider c in GetComponents<CapsuleCollider>())
            {
                c.enabled = true;
            }
        }

    }


    void OnDrawGizmosSelected()
    {

        var nav = GetComponent<NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
            line.SetWidth(0.5f, 0.5f);
            line.SetColors(Color.yellow, Color.yellow);
        }

        var path = nav.path;

        line.SetVertexCount(path.corners.Length);

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }
}
