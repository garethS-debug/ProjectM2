using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CivilianPatrol : MonoBehaviour
{

    //[Header("Speed")]
    //public float Speed;

    [Header("WaitTime")]
    private float waitTime;
    public float StartWaitTime;
    // private WaypointInfo waypointInfo;

    [Header("List of Transforms")]
    //public Transform[] moveLocations;
    public List<Transform> moveLocations = new List<Transform>();

    [Header("Enenmy Ai Controller")]
    public bool isOnPatrol;
    public bool isAlerted;
    public bool isWalkingToLastKnownLOC;
    public bool isAlert;

    private int randomSpot;

    [Header("Player")]
    private GameObject player;
    private CivilianAI civilianAIController;

    [Header("Last Known LOC")]
    public List<Vector3> LastKnownGuardLOC = new List<Vector3>();
    public Vector3 LastKnownGuardLocation;

    [Header("Alerting")]
    private CivilianFOV civilianFOV;
    public bool inAlertRange;
    public bool alertTheGuard;

    [Header("Guard")]
    public GameObject guard;
    private GuardAI guardAI;


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

    [Header("Distraction")]
    public bool isWalkingToDistraction;
    public GameObject distraction;

    [Header("Sitting")]
    public Transform sitLocation;
    public Transform sitViewLocation;
    public Transform resetPOS;
    public bool isSitting;


    [Header("Food Time")]
    public bool isRunningToFood;
    public List<Transform> FoodGatheringLocations = new List<Transform>();
    private int randomFoodLOC;


    void Start()
    {
        player = SceneSettings.Instance.humanPlayer; 
        randomSpot = Random.Range(0, moveLocations.Count);

        civilianAIController = gameObject.GetComponent<CivilianAI>();
        civilianFOV = gameObject.GetComponent<CivilianFOV>();

        anim = this.gameObject.GetComponent<Animator>(); // get the animator component attached to enemy
        _navMeshAgent = GetComponent<NavMeshAgent>(); //REference to the Navmesh Agent
    }


    void Update()
    {
        float playerPOS = Vector3.Distance(player.transform.position, this.transform.position);
        //   print(playerPOS);




        if (playerPOS <= civilianFOV.alertRange && civilianFOV.inPeriphVision == true)
        {
            print("In attack range");
            //Player is in attack range
            isWalkingToLastKnownLOC = false;
            isAlerted = false;
            isOnPatrol = false;

            Walking = false;
            Startled = false;
            Jogging = false;

            isAlert = true;

            inAlertRange = true;
            // break;
        }

        //if (civilianAIController.State2 == true && playerPOS <= civilianFOV.alertDistance)
        //{
        //    inAlertRange = true;
        //}

        //if (civilianAIController.State2 == true && playerPOS >= civilianFOV.alertDistance)
        //{
        //    inAlertRange = false;
        //    _navMeshAgent.SetDestination(LastKnownGuardLocation);
        //    Walking = false;
        //    PauseInWalk = false;
        //    Jogging = true;
        //    anim.SetBool("isAttacking", false);

        //}
        //


        //Moving Function while on patrol
        while (isOnPatrol == true && isRunningToFood == false)
        {
            //moving function 
            // original --- transform.position = Vector3.MoveTowards(transform.position, moveLocations[randomSpot].position, Speed * Time.deltaTime);
            _navMeshAgent.SetDestination(moveLocations[randomSpot].position);


            // waiting once we have reached a location close to the target position
            if (Vector3.Distance(transform.position, moveLocations[randomSpot].position) <= 0.7f || isSitting == true )
            {
                Walking = false;
                PauseInWalk = true;

                //is it time for the enemy to move to the next patrol point 
                if (waitTime < 0)
                {
                    //change the random location
                    randomSpot = Random.Range(0, moveLocations.Count);
                    waitTime = StartWaitTime;

                    if (isSitting == true)
                    {
                        isSitting = false;
                        this.transform.position = resetPOS.position;

                    }



                }



                else
                {
                    waitTime -= Time.deltaTime;
                    //ADD animation of looking around while waiting


                }
            }

            if (Vector3.Distance(transform.position, moveLocations[randomSpot].position) > 0.7f)
            {
                Walking = true;
            }

            break;
        }

        while (isAlerted == true)
        {
            //ADD script to run to player
            //moving function 
            if (Vector3.Distance(transform.position, LastKnownGuardLocation) > 2.0f)
            {
                // original  ---- transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
                _navMeshAgent.SetDestination(LastKnownGuardLocation);
                //transform.LookAt(player.transform.position);
                Walking = false;
                PauseInWalk = false;
                Jogging = true;

            }




            ////grab the last vector in the last known LOC
            //int ListNumber = LastKnownGuardLOC.Count;
            //LastKnownGuardLocation = LastKnownGuardLOC[ListNumber - 1];

            //ADD Function here for RAISING THE ALARM the GUARDS
            //  anim.SetBool("isScared", true);
            //_navMeshAgent.SetDestination(this.transform.position);



            break;
        }

        while (isWalkingToDistraction == true)
        {
            Startled = false;
            if (distraction.gameObject != null)
            {

                while (Vector3.Distance(transform.position, distraction.gameObject.transform.position) > 1.5f)
                {
                    transform.LookAt(distraction.gameObject.transform);
                    break;
                }

                if (Vector3.Distance(transform.position, distraction.gameObject.transform.position) > 1.0f)
                {
                    //Walking to distraction
                    _navMeshAgent.SetDestination(distraction.gameObject.transform.position);
                    //transform.LookAt(player.transform.position);
                    Walking = true;
                    PauseInWalk = false;
                    Jogging = false;


                }

                if (Vector3.Distance(transform.position, distraction.gameObject.transform.position) <= 1.0f)
                {
                    civilianAIController.StartClimbDown = true;
                    //stop
                    _navMeshAgent.SetDestination(this.transform.position);
                    //transform.LookAt(player.transform.position);
                    Walking = false;
                    PauseInWalk = true;
                    Jogging = false;

                }
            }

            break;
        }




        while (isRunningToFood == true)
        {
            isOnPatrol = false;
            Walking = false;

            if (randomFoodLOC <= 0)
            {
                randomFoodLOC = Random.Range(0, Alarm.Instance.FoodGatheringLocations.Count);

            }

            // waiting once we have reached a location close to the target position
            if (Vector3.Distance(transform.position, FoodGatheringLocations[randomFoodLOC].position) < 0.9f)
            {
                Walking = false;

                gameObject.GetComponent<NavMeshAgent>().isStopped = true;

                anim.SetFloat("Speed", 0.0f, speedDampTime, Time.deltaTime);
                PauseInWalk = true;
                anim.SetBool("isEating", true);

            }

            if (Vector3.Distance(transform.position, FoodGatheringLocations[randomFoodLOC].position) > 0.9f)
            {
                _navMeshAgent.SetDestination(FoodGatheringLocations[randomFoodLOC].position);

                anim.SetFloat("Speed", 0.4f, speedDampTime, Time.deltaTime);
                PauseInWalk = false;
                isOnPatrol = false;
                Jogging = false;

            }


            break;
        }













        while (isWalkingToLastKnownLOC == true)
        {
            //ADD script to run to players last known LOC

            if (LastKnownGuardLOC.Count >= 1)
            {
                int ListNumber = LastKnownGuardLOC.Count;
                LastKnownGuardLocation = LastKnownGuardLOC[ListNumber - 1];
            }


            if (guard != null)
            {
                guardAI = guard.gameObject.GetComponent<GuardAI>();
            }


            if (Vector3.Distance(transform.position, LastKnownGuardLocation) > 1.0f)
            {
                //original-----  transform.position = Vector3.MoveTowards(transform.position, LastKnownLocation, Speed * Time.deltaTime);
                _navMeshAgent.SetDestination(LastKnownGuardLocation);
                Walking = false;
                PauseInWalk = false;
                Jogging = true;

                anim.SetBool("isScared", false);
                alertTheGuard = false;

            }

            if (Vector3.Distance(transform.position, LastKnownGuardLocation) < 1.0f)
            {
                //ALERT THE GUARD
                anim.SetBool("isScared", true);
                alertTheGuard = true;
                guardAI.AlertedbyStaff = true;
                civilianAIController.StartClimbDown = true;
                Walking = false;
                PauseInWalk = true;
                Jogging = false;
            }

            //Once player is within x distance of last Known LOC. Start Timer to 'FORGET' 
            break;
        }



        //while (civilianAIController.State2 == true && civilianFOV.PlayerinFOV == true)
        //{
        //    while (Vector3.Distance(transform.position, player.transform.position) > 2.0f)
        //    {
        //        //original ---  transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
        //        _navMeshAgent.SetDestination(player.transform.position);


        //        break;
        //    }
        //    break;
        //}





        if (Walking == true)
        {

            //Anim Controller
            //Direction -- > Anim
            //  Vector3 targetDir = LastKnownGuardLocation - transform.position;
            //  float angle = Vector3.Angle(targetDir, transform.forward);

            //speed = _navMeshAgent.speed;

            // print(angle);
            // anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
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
            //  Vector3 targetDir = LastKnownGuardLocation - transform.position;
            //   float angle = Vector3.Angle(targetDir, transform.forward);

            //  speed = _navMeshAgent.speed;

            // print(angle);
            //  anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
            anim.SetFloat("Speed", 0.0f, speedDampTime, Time.deltaTime);
            anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);
            //Direction -- > Anim
            //  direction = angle / 360;

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
            Vector3 targetDir = LastKnownGuardLocation - transform.position;
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
            direction = angle / 360;

            //Moving Animation 
            anim.SetBool("isWalking", false);
            anim.SetBool("isAlerted", true);
            anim.SetBool("isLooking", false);
            // _navMeshAgent.speed = 0.1f;

        }

        if (Jogging == true)
        {
            anim.SetBool("isAlerted", false);
            //Anim Controller
            //Direction -- > Anim
            Vector3 targetDir = player.transform.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            //speed = _navMeshAgent.speed;

            // print(angle);
            anim.SetFloat("Angle", angle, speedDampTime, Time.deltaTime);
            anim.SetFloat("Speed", 0.4f, speedDampTime, Time.deltaTime);
            anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);
            //Direction -- > Anim
            direction = angle / 360;


            //Moving Animation 
            anim.SetBool("isWalking", true);
            anim.SetBool("isAlerted", false);
            anim.SetBool("isLooking", false);

            // _navMeshAgent.speed = 0.1f;

        }

        if (isSitting == true)
        {

            this.transform.position = sitLocation.position;
            transform.LookAt(sitViewLocation);

            if (waitTime > 2.5f)
            {
                anim.SetBool("isSitting", true);
            }

            if (waitTime < 2.5f)
            {
                anim.SetBool("isSitting", false);

            }
        }

        if (isSitting == false)
        {
            anim.SetBool("isSitting", false);
            //this.transform.position = resetPOS.position;
        }



    }

    private void OnTriggerEnter(Collider other)
    {

        //   print("Waypoint:" + other.name);

        if (other.gameObject.tag == "PatrolPoint")
        {

            //Disable Bool
            if (other.gameObject.name == "TableSit")
            {
                waitTime = other.gameObject.GetComponent<WaypointInfo>().WaypointWaitTime;
                isSitting = true;
            }

            
        }
    }

    private void OnTriggerExit(Collider other)
    {

        //   print("Waypoint:" + other.name);

        if (other.gameObject.tag == "PatrolPoint")
        {

            //Disable Bool
            if (other.gameObject.name == "Sit")
            {
                isSitting = false;
            }

           
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

        if (collision.gameObject.tag == "Waypoint")
        {
            print("Waypoint:" + collision.gameObject.name);
            //Disable Bool
            if (collision.gameObject.name == "Sit")
            {
                print("Waypoint:" + collision.gameObject.name);
                anim.SetBool("isSitting", true);
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
}
