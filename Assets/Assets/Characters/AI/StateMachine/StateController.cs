using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    //public enum Categories
    //{
    //    State0Neutral,
    //    State1BecomingAlertedAlarm,
    //    State2BecomingAlertedStaff,
    //    State3BecomingAlertedPlayer,
    //    State4AlertedLookingforPlayer,
    //    State5ChasingPlayer,
    //    State6CantFindPlayer,
    //    State7GoingBackToNeutral,
    //    State8Distraction,
    //    State9CallingForOtherGuards,
    //    State10
    //}

    [Header("States")]
    public State currentState;
    public State remainState;

    [Header("Icon")]
    public Image StateMarkerImage;//image on canvas

    [Header("Proximity")]
    [HideInInspector] public bool WithinProximity;

    [Header("StaffAlertingGuard")]
    public bool staffAlertingGuard;

    [Header("Temp Waypoint")]
    public GameObject tempWaypoint;
    public int maxWaypoint;
    public List<GameObject> TempWaypoints = new List<GameObject>();

    [Header("Character Stats")]
    [HideInInspector] public CharacterStats CharacterStats;

    [Header("Main Character")]
    public GameObject mainCharacher;

    [Header("DistractionEvent")]
    [HideInInspector] public bool distractionEventActive;
    [HideInInspector] public GameObject distractionEventLocation;


    [Header("List of Patrol Points")]
    //public Transform[] moveLocations;
    public List<Transform> moveLocations = new List<Transform>();
    //private bool aiActive;
    [HideInInspector] public int randomSpot;
    [TextArea]
    public string Notes = "Place patrol point in each room so the police move away from Murphy's hidden spots"; // Do not place your note/comment here. 
                                           // Enter your note in the Unity Editor.



    [Header("WaitTime (Patrol)")]
    public float waitTime;
    [HideInInspector] public float StartWaitTime;
    [HideInInspector] public float stateTimeElapsed;

    [Header("WaitTime (Cooldown)")]
    public float CooldownwaitTime;
    [HideInInspector] public float StartCooldowWaitTime;

    [Header("NavMesh")]
   public NavMeshAgent _navMeshAgent;
   public bool resetPath;

    [Header("FOV")]
    [HideInInspector] public EnemyFOV enemyFOV;
    [HideInInspector] public StaffFOV staffFOV;
    [HideInInspector] public PoliceFOV policeFOV;
    [HideInInspector] public CivilianFOV civilianFOV;
    [HideInInspector] public Vector3 LastKnownLocation;

    [Header("Witness Crime")]
    [HideInInspector] public bool witnessCrime;


    [Header("StealKill")]
    public GameObject RagDoll;
    [HideInInspector] public bool isBeingKilled;
    [HideInInspector] public bool spawnedRag;
    public GameObject ItemSpawnOnDeath;

    [Header("Weapons")]
    [Tooltip("Reference to weapoons to turn off by Murphy")]
    public GameObject[] Weapons;

    [Header("Alarm Source")]
    [Tooltip("Someone Shouting")]
   public Vector3 whoCalled;
   public bool someoneIsCalling;

    [Header("Anim")]
    [HideInInspector] public Animator anim;
    [Header("Speed/Dir")]
    public float speedDampTime = 0.09f;
    [Tooltip("Running speed of guard TIP: set very low e.g. 0.001")]
    private float direction = 0f;
    private float DirectionDampTime = .25f; //used to set delay direction variable in anim controller (mainly for piviting



    [Header("Stats")]
    [HideInInspector] public Combat combat;
    [Tooltip("Rate at which the enemy turns when chasing the player (increase number to increase difficulty)")]
    public float turningSpeed;


    [Header("Attacking")]
    [Tooltip("Attacking")]
    [HideInInspector] public bool isAttacking;
    public float AttackCounter;
    //public float AttackBufferTimeMax;
    //public float attackBufferTime;
    private float CoolDwnTimerMax;
    [Tooltip("This is the timer between attacks")]
    public float AtkCoolDownTimer;
    [HideInInspector] private float AtkCoolDownTimerMax;
    //public float AttackCoolDown;
    public bool pauseAfterAttack;
    public bool lookAtPlayer;


   
    //Feet grounder
    [HideInInspector] public Vector3 rightFootPosition, leftFootPosition, leftFootIkPosition, rightFootIkPosition;
    [HideInInspector] public Quaternion leftFootIkRotation, rightFootIkRotation;
    [HideInInspector] public float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;
    [Header("Feet Grounder")]
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





    // Start is called before the first frame update
    void Start()
    {
        resetPath = false;
        CharacterStats = this.gameObject.GetComponent<CharacterStats>();
        randomSpot = Random.Range(0, moveLocations.Count);

       
       // _navMeshAgent = GetComponent<NavMeshAgent>(); //REference to the Navmesh Agent


        if (this.gameObject.tag == "Enemy")
        {
            enemyFOV = gameObject.GetComponent<EnemyFOV>();
        }

        if (this.gameObject.tag == "Staff")
        {
            staffFOV = gameObject.GetComponent<StaffFOV>();
        }

        if (this.gameObject.tag == "Police")
        {
            policeFOV = gameObject.GetComponent<PoliceFOV>();

            
        }

        if (this.gameObject.tag == "Civilian")
        {
            civilianFOV = gameObject.GetComponent<CivilianFOV>();
        }
      
        anim = this.gameObject.GetComponent<Animator>(); // get the animator component attached to enemy
        StartWaitTime = waitTime;
        StartCooldowWaitTime = CooldownwaitTime;
        combat = this.gameObject.GetComponent<Combat>();
        pauseAfterAttack = false;
        isAttacking = false;

        AtkCoolDownTimerMax = AtkCoolDownTimer;

        mainCharacher = MurphyPlayerManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {

            currentState.UpdateState(this);
          OnDrawGizmosSelected();
        //where is the player

        //if (_navMeshAgent.hasPath)
        //{
        //    drawPath();
        //}


    }


    //CENTRAL REFERENCE FOR DIFFERENT FUNCTIONS
    public void TransitionToState(State nextState)                  //Check if we should proceed to the next state
    {
        if (nextState != remainState)
        {
            StateMarkerImage.sprite = nextState.stateMarker;        
            currentState = nextState;                               //Transition to next state
            OnExitState();
        }
    }

    #region Anims

    public void walking ()
    {
      //  float speed = float.Parse(CharacterStats.speed.ToString());

        anim.SetFloat("Speed", _navMeshAgent.speed - 0.9f, speedDampTime, Time.deltaTime);
        anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);

        anim.SetBool("isWalking", true);
        anim.SetBool("isAlerted", false);
        anim.SetBool("isLooking", false);
    }

    public void pauseInWalk()
    {
        anim.SetFloat("Speed", 0.0f, speedDampTime, Time.deltaTime);
        anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);

        if (_navMeshAgent != false || isBeingKilled != true)
        {
            _navMeshAgent.SetDestination(this.gameObject.transform.position);
        }
        
        anim.SetBool("isWalking", false);
        anim.SetBool("isAlerted", false);
        anim.SetBool("isLooking", false);
    }


    public void Running()
    {
       // float speed = float.Parse(CharacterStats.speed.ToString());

        anim.SetBool("isAlerted", false);
        //Anim Controller
        //Direction -- > Anim
       
        //Vector3 targetDir = transform.position - transform.position;
        //float angle = Vector3.Angle(targetDir, transform.forward);

        //speed = _navMeshAgent.speed;

        // print(angle);
     //   anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
        anim.SetFloat("Speed", _navMeshAgent.speed - 0.7f, speedDampTime, Time.deltaTime);
        anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);
        //Direction -- > Anim
        //  direction = angle / 360;


        //Moving Animation 
        anim.SetBool("isWalking", true);
        anim.SetBool("isAlerted", false);
        anim.SetBool("isLooking", false);

        // _navMeshAgent.speed = 0.1f;
    }
    #endregion

    #region Running to Las Known LOC

    public void RunningToLastKnownPlayerLOC()
    {
        waitTime = StartWaitTime;

        if (enemyFOV != null)
        {
            // if GUARD & player is in fov rotate to player
            if (enemyFOV.PlayerinFOV == true)
            {
                // Determine which direction to rotate towards
                Vector3 targetDirection = enemyFOV.LastKnowLOCTransform.position - transform.position;
                // The step size is equal to speed times frame time.
                float singleStep = turningSpeed * Time.deltaTime;
                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);
                //   transform.LookAt(enemyFOV.LastKnowLOCTransform);
            }


            // IF LAST KNOW LOC LIST HAPPENS TO BE LARGER THAN 1
            if (enemyFOV.LastKnownFOVLOC.Count >= 1)
            {
                int ListNumber = enemyFOV.LastKnownFOVLOC.Count;
                LastKnownLocation = enemyFOV.LastKnownFOVLOC[ListNumber - 1];
            }
        }
       



        if (policeFOV != null)
        {
            // if POLICE & player is in fov rotate to player
            if (policeFOV.PlayerinFOV == true)
            {
                // Determine which direction to rotate towards
                Vector3 targetDirection = policeFOV.LastKnowLOCTransform.position - transform.position;
                // The step size is equal to speed times frame time.
                float singleStep = turningSpeed * Time.deltaTime;
                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);

                //   transform.LookAt(enemyFOV.LastKnowLOCTransform);
            }


            // IF LAST KNOW LOC LIST HAPPENS TO BE LARGER THAN 1
            if (policeFOV.LastKnownFOVLOC.Count >= 1)
            {
                int ListNumber = policeFOV.LastKnownFOVLOC.Count;
                LastKnownLocation = policeFOV.LastKnownFOVLOC[ListNumber - 1];
            }

        }








        if (Vector3.Distance(transform.position, LastKnownLocation) > _navMeshAgent.stoppingDistance)
        {
            WithinProximity = false;
            _navMeshAgent.SetDestination(LastKnownLocation);
            Running();
        }

        if (Vector3.Distance(transform.position, LastKnownLocation) < 2.0f)
        {
            _navMeshAgent.isStopped = true;
            WithinProximity = true;
            //if (enemyFOV.PlayerinFOV == false)
            //{
            // //   resetPath = true;
            //}

            // enemyAIController.StartClimbDown = true;
            pauseInWalk();


        }
    }


    public void RunningToAuthority()
    {
        //ADD script to run to players last known LOC

        if (staffFOV.LastKnownGuardLOCList.Count >= 1)
        {
            int ListNumber = staffFOV.LastKnownGuardLOCList.Count;
            staffFOV.LastKnownGuardLocation = staffFOV.LastKnownGuardLOCList[ListNumber - 1];
        }


        if (Vector3.Distance(transform.position, staffFOV.LastKnownGuardLocation) > 1.0f)
        {
            //original-----  transform.position = Vector3.MoveTowards(transform.position, LastKnownLocation, Speed * Time.deltaTime);
            _navMeshAgent.SetDestination(staffFOV.LastKnownGuardLocation);

           Running();
            WithinProximity = false;
            staffAlertingGuard = false;
        }

        if (Vector3.Distance(transform.position, staffFOV.LastKnownGuardLocation) < 1.0f)
        {
            // Alarm.Instance.CallForHelp(this.gameObject);


        
            //ALERT THE GUARD
            anim.SetBool("isScared", true);

            WithinProximity = true;


            foreach (GameObject guard in Alarm.Instance.Guards)
            {
                if (Vector3.Distance(guard.transform.position, transform.position) < 30.0f)
                {
                    print("Guards in range");


                    foreach (StateController statecontroller in Alarm.Instance.guardStateController)
                    {
                        statecontroller.staffAlertingGuard = true;
                        print("transfering Murphy LOC");
                        statecontroller.LastKnownLocation = staffFOV.LastKnownMurphyLOC;
                    }



                }

                if (Vector3.Distance(guard.transform.position, transform.position) > 30.0f)
                {
                    print("Out of Range");
                }

            }





            //alertTheGuard = true;
            //guardAI.AlertedbyStaff = true;


            //Alerting the guard about seeing Murphy 
            //  Alarm.Instance.GuardsAlertedByStaff(staffFOV.LastKnownMurphyLOC);

            Alarm.Instance.RemoveCostumeProtection(); //Remove Costume protection

            //TALKING DIRECTLY WITH GUARD (whithout Going through alarm)
            //   GuardAI guardAIComponent = guard.gameObject.GetComponent<GuardAI>();
            //    guardAIComponent.WhereIsMurphy = staffFOV.LastKnownMurphyLOC;
            //   guardAIComponent.StaffAlertingGu   ard = true;

            pauseInWalk();

            //   staffAIController.StartClimbDown = true;
            //    Walking = false;
            //   PauseInWalk = true;
            //   Jogging = false;

        }
    }

    #endregion
    public void ResetPath()
    {
            _navMeshAgent.ResetPath();
            _navMeshAgent.SetDestination(moveLocations[randomSpot].position);
             return;
    }

    #region TIMERS
    public void Timer()
    {

        if (CooldownwaitTime<= 0.1f)
        {
            CooldownwaitTime = StartCooldowWaitTime;
        }
        else
        {

            //Timer ticks down
            CooldownwaitTime -= Time.deltaTime;
           
        }
    }


    //TIMER
    public bool CheckifCountdownTimeElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()  //When we chance state
    {
        stateTimeElapsed = 0;
 

        WithinProximity = false;

        staffAlertingGuard = false;

        if (anim.GetBool("isScared") == true)
        {
            anim.SetBool("isScared", false);
        }

        if (anim.GetBool("isAttacking") == true)
        {
            anim.SetBool("isAttacking", false);
        }


        distractionEventActive = false;
        // someoneIsCalling = false;
    }


    #endregion

    #region Attacking
    public void Attack()
    {
        transform.LookAt(MurphyPlayerManager.instance.player.transform);

        WaitAfterAttack();

        if (pauseAfterAttack == false)
        {
            isAttacking = true;
            anim.SetBool("isAttacking", true);
            AttackCounter = Mathf.Lerp(AttackCounter, 1, Time.deltaTime / 3);
            //Set the slider on Mechanim
            anim.SetFloat("AttackCounter", AttackCounter);
        }

        if (pauseAfterAttack == true)
        {
            isAttacking = false;
            anim.SetBool("isAttacking", false);
            anim.SetFloat("AttackCounter", 0);
        }

    }

    public void WaitAfterAttack()
    {
        if (AtkCoolDownTimer > 0.1f)
        {
            AtkCoolDownTimer -= Time.deltaTime;
           // pauseAfterAttack = true;
            
        }

        if (AtkCoolDownTimer <= 0.1f)
        {

           pauseAfterAttack = !pauseAfterAttack;
            AtkCoolDownTimer = AtkCoolDownTimerMax;
            //  pauseAfterAttack = false;

        }

    }

    #endregion

    #region Distraction

    public void isWalkingToTheDistraction(GameObject DistractionOBJ)
    {

            if (Vector3.Distance(transform.position, DistractionOBJ.transform.position) > 2.0f)
            {
                //original-----  transform.position = Vector3.MoveTowards(transform.position, LastKnownLocation, Speed * Time.deltaTime);
                _navMeshAgent.SetDestination(DistractionOBJ.transform.position);
            transform.LookAt(DistractionOBJ.transform);
            Running();
            WithinProximity = false;
        }

            if (Vector3.Distance(transform.position, DistractionOBJ.transform.position) < 2.0f)
            {
                pauseInWalk();
                WithinProximity = true;
            //if (enemyAIController.IveSeenTheDistraction == true)
            //{
            //    enemyAIController.DistractionTimers();
            //}

            //Walking = false;
            //PauseInWalk = true;
            //Jogging = false;
        }
       

    }




    #endregion


    #region Alarm

    public void RaisingAlarm()
    {
            witnessCrime = false;

            // Remove Costumer Protection
            Alarm.Instance.RemoveCostumeProtection();

            //IF ALARM IS NOT ALREADY RAISED
            if (Alarm.Instance.RaiseAlarm == false)
            {
                Alarm.Instance.RaiseAlarm = true;
                //   Alarm.Instance.PlayerinCCTVFOV = true;
            }

            // Call for help
            Alarm.Instance.CallForHelp(LastKnownLocation);
            Debug.Log("Last Known Loc =" + LastKnownLocation);

            // Call the police
            Alarm.Instance.callThePolice = true;

   

            if (this.gameObject.tag == "Civilian")
            {
            anim.SetBool("isScared", true);
            }
}


    public void IsRunningToTheShouting(Vector3 WhoShouted)
    {
        print("Im running to the shout");

       
            if (Vector3.Distance(transform.position, WhoShouted) < 1.9f)
            {
                pauseInWalk();
            //  Alarm.Instance.CivilianHasCalledPolice = false;  
                WithinProximity = true;
                someoneIsCalling = false;
            }

            if (Vector3.Distance(transform.position, WhoShouted) > 1.9f)
            {

            //transform.LookAt(WhoShouted.transform.position)


            Debug.Log("Who Shouted = " + WhoShouted);



               // _navMeshAgent.ResetPath();
                _navMeshAgent.SetDestination(WhoShouted);



              //  transform.LookAt(WhoShouted);
                Running();



                WithinProximity = false;

        }
        


    }


    public void isRunningToTheDistractionEvent(GameObject itemLocation)
    {
        print("Im running to the distraction event");
        

        if (Vector3.Distance(transform.position, itemLocation.transform.position) < 4f)
        {
            pauseInWalk();
            //  Alarm.Instance.CivilianHasCalledPolice = false;  
            WithinProximity = true;
        }

        if (Vector3.Distance(transform.position, itemLocation.transform.position) > 4f)
        {
            //transform.LookAt(WhoShouted.transform.position);
            _navMeshAgent.SetDestination(itemLocation.transform.position);
         //   transform.LookAt(itemLocation.transform.position);
            Running();
            WithinProximity = false;
        }
    }


    public void isBeingStealthKilled()
    {
        isBeingKilled = true;


   


        foreach (Collider c in gameObject.GetComponents<CapsuleCollider>())
        {
            c.enabled = false;
        }

        foreach (GameObject GO in Weapons)
        {
            GO.SetActive(false);
        }

        //enable enemy anim for being killed
        StartCoroutine("EnemyAnimRagDoll");

        if (enemyFOV != null)
        {
            if (enemyFOV.enabled == true)
            {
                enemyFOV.enabled = false;
            }

        }

        if (policeFOV != null)
        {
            if (policeFOV.enabled == true)
            {
                policeFOV.enabled = false;
            }
        }
        
  

        NewMurphyMovement mainCharScript= mainCharacher.GetComponent<NewMurphyMovement>();

        //Move Enemy Stealth Kill to Player && look away
        transform.position = mainCharScript.stealthKillLoc.transform.position;
        transform.LookAt(mainCharScript.stealthKillFocus);
        
    }

    IEnumerator EnemyAnimRagDoll()
    {
        NewMurphyMovement mainCharScript = mainCharacher.GetComponent<NewMurphyMovement>();

        anim.SetBool("isBeingStealthKilled", true);
        yield return new WaitForSeconds(2.5f);
        GetComponent<NavMeshAgent>().enabled = false;
        //enemyAnim.enabled = false;
        if (spawnedRag == false)
        {
            if (ItemSpawnOnDeath != null)
            {
                Instantiate(ItemSpawnOnDeath, mainCharScript.CostumeSpawnLoc.transform.position, transform.rotation);
            }
     
            Instantiate(RagDoll, transform.position, transform.rotation);
            spawnedRag = true;
        }
        
        Destroy(this.gameObject);
        anim.SetBool("isStealthKill", false);
       

    }

    #endregion

    #region searching aread 
    //public void SearchingTheArea(Vector3 SearchArea)
    //{

    //    //Instantiate patrol points around incident area 

    //    if (tempWaypoint != null)
    //    {

    //        ////Spawning Temp Waypoints
    //        if (TempWaypoints.Count >= maxWaypoint)
    //        {

    //        }

    //        if (TempWaypoints.Count <= maxWaypoint)
    //        {
    //            for (int x = 0; x <= maxWaypoint; x++)
    //            {
    //                GameObject SpawnWaypointPrefabs = Instantiate(tempWaypoint, SearchArea + Vector3.right * Random.Range(-3, 3), Quaternion.identity);
    //                TempWaypoints.Add(SpawnWaypointPrefabs);
    //            }
    //        }



    //    }
    //    //set to patrol around there


    //    //on exit state remove petrol points






    //}
    #endregion


public void drawPath()
    {
        var line = GetComponent<LineRenderer>();
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
        line.positionCount = 0;
        line.positionCount = _navMeshAgent.path.corners.Length;
        line.SetPosition(0, transform.position);

       

        if (_navMeshAgent.path.corners.Length <2)
        {
            return;
        }

        for (int i = 0; i < _navMeshAgent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(_navMeshAgent.path.corners[i].x, _navMeshAgent.path.corners[i].y, _navMeshAgent.path.corners[i].z);
            line.SetPosition(i, pointPosition);
        }
    }


void OnDrawGizmosSelected()
 {
 
     var nav = GetComponent<NavMeshAgent>();
     if( nav == null || nav.path == null )
         return;
 
     var line = this.GetComponent<LineRenderer>();
     if( line == null )
     {
         line = this.gameObject.AddComponent<LineRenderer>();
         line.material = new Material( Shader.Find( "Sprites/Default" ) ) { color = Color.yellow };
            //  line.SetWidth( 0.5f, 0.5f );
            line.startWidth = 0.15f;
            line.endWidth = 0.15f;
         line.SetColors( Color.yellow, Color.yellow );
     }
 
     var path = nav.path;
 
     line.SetVertexCount( path.corners.Length );
 
     for( int i = 0; i < path.corners.Length; i++ )
     {
         line.SetPosition( i, path.corners[ i ] );
     }
 
 }



    private void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColor; // See what state the agent is in by color of gizo
                                                         // Gizmos.DrawWireSphere(gameObject.transform.position, wireCastRadious);
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



}
