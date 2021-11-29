//Copyright Filmstorm (C) 2018 - Movement Controller for Root Motion and built in IK solver
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewMurphyMovement : MonoBehaviour
{
    
        
    /// UnityTutorials - A Unity Game Design Prototyping Sandbox
    /// <copyright>(c) John McElmurray and Julian Adams 2013</copyright>

    #region movement
    //float
    [Header("Floats")]
    private int xtimesHit = 0;
    [Tooltip("defult Speed")]
    public float defaultMoveSpeed = 1.8f;
    public float moveSpeed = 1.8f;
    public float turnSpeed = 50f;
    [Tooltip("Stealth speed is the speed while sneaking")]
    public float stealthSpeed;
    public float stealthTurnSpeed;
    [Tooltip("Sprint factor is the speed while running")]
    public float sprintSpeed;
    public float backwardsMovement;

    

    // private float direction = 0f;
    [Header("Effects")]
    public ParticleSystem Dust;

    //RB
    private Rigidbody rb;

    [Header("Bools")]
    //Bool
    public bool moving = false;
    public bool murphyAttacking;
    private bool stopButtonMashing;
    public bool enabledPlayerWeaponCollider;
    public bool isStealthing;
    public bool isSprinting;
    public bool isWalkingForward;
    public bool isWalkingBackward;
    public bool isMovingLeft;
    public bool isMovingRight;

    [Header("Third Person Control")]
    private float DirectionDampTime = .25f; //used to set delay direction variable in anim controller (mainly for piviting
    private float speed = 0.0f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    [SerializeField]
    private float directionSpeed = 1.5f;
    [SerializeField]
    private CameraOnRails gamecam;
    private float direction = 0f;
    private float charAngle = 0f;
    // Hashes
    private int m_LocomotionId = 0;
    private int m_LocomotionPivotLId = 0;
    private int m_LocomotionPivotRId = 0;
    private int m_LocomotionPivotLTransId = 0;
    private int m_LocomotionPivotRTransId = 0;
    [SerializeField]
    private float speedDampTime = 0.05f;


    private float rotationDegreePerSecond = 120f;
    [SerializeField]
    private float directionDampTime = 0.25f;
    [SerializeField]
    private float fovDampTime = 3f;
    [SerializeField]
    private float jumpMultiplier = 1f;
    [SerializeField]
    private CapsuleCollider capCollider;
    [SerializeField]
    private float jumpDist = 1f;

    // Private global only
    private float leftX = 0f;
    private float leftY = 0f;
    private const float SPRINT_SPEED = 2.0f;
    private const float WALK_SPEED = 0.3f;
    private const float IDLE_SPEED = 0.00f;
    private const float SPRINT_FOV = 75.0f;
    private const float NORMAL_FOV = 60.0f;
    private float capsuleHeight;
    public KeyCode Jump = KeyCode.J;
    public KeyCode Sprint = KeyCode.H;

    [Header("Sneak")]
    public float sneak;
    private bool StopStealthSpam;
    public GameObject EnemyStealthKillGO;
    private GameObject EnemyRagdollGO;
    private GameObject EnemyCostumeGO;
    private GuardChase enemyChaseScript;
    private EnemyFOV enemyFOVSript;
    private GuardPatrol enemyPatrol;
    public Transform stealthKillLoc;
    public Transform stealthKillFocus;
    private CapsuleCollider enemyCapsuleCollider;
    private bool stealthKillLOCUpdate;
    private FieldOfView murphyFOV;
    public KeyCode GoHidden = KeyCode.H;
    public bool IsHidden;
    public bool isInInventory;

    [Header("Attack")]
    public float AttackCounter;
    private Animator enemyAnim;
    private Animator staffAnim;
    private Animator civilianAnim;
    public GameObject CostumeSpawnLoc;

    [Header("Sitting")]
    public bool Sitting;

    [Header("Dragging")]
    private PickupThrow pickupThrow;
   [HideInInspector] public bool isDragging;
    public bool letsHidetheBody;

   
    #region Properties (public)

    //public Animator Animator
    //{
    //    get
    //    {
    //        return this.animator;
    //    }
    //}

    public float Speed
    {
        get
        {
            return this.speed;
        }
    }

    public float LocomotionThreshold { get { return 0.2f; } }

    #endregion

    //Anim State
    private AnimatorStateInfo stateInfo;
    private AnimatorStateInfo transInfo;

    //Anim
    public Animator anim;

    [Header("Keycodes")]
    public KeyCode AttackingButton = KeyCode.A;
    public KeyCode StealthButton = KeyCode.LeftShift;
    public KeyCode SprintButton = KeyCode.LeftControl;
    public KeyCode stealthAttackButton = KeyCode.K;
    public KeyCode SittingButton = KeyCode.C;
    #endregion

    #region Feet Grounder
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

    [Header("Reference to other scripts")]
    private StealthNoiseMeasure stealthNoiseMeasure;
    public GuardAI enemyAIController;
    private StaffAIController staffAIController;
    private StaffFOV staffFOV;
    private StaffPatrol staffPatrol;        
    public CapsuleCollider staffCapsuleCollider { get; private set; }


    #endregion

    private void Start()
    {

        pickupThrow = gameObject.GetComponent<PickupThrow>();

        //HomeBrew
        stealthNoiseMeasure = GetComponent<StealthNoiseMeasure>();
        enabledPlayerWeaponCollider = false;
        #region movement
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        #endregion
        //moveSpeed = defaultMoveSpeed;

        murphyFOV = this.gameObject.GetComponent<FieldOfView>();
        EnemyStealthKillGO = null;


        //Locomotion script
        capCollider = GetComponent<CapsuleCollider>();
        capsuleHeight = capCollider.height;
        if (anim.layerCount >= 2)
        {
            anim.SetLayerWeight(1, 1);
        }

        // Hash all animation names for performance
        m_LocomotionId = Animator.StringToHash("Base Layer.Locomotion");
        m_LocomotionPivotLId = Animator.StringToHash("Base Layer.LocomotionPivotL");
        m_LocomotionPivotRId = Animator.StringToHash("Base Layer.LocomotionPivotR");
        m_LocomotionPivotLTransId = Animator.StringToHash("Base Layer.Locomotion -> Base Layer.LocomotionPivotL");
        m_LocomotionPivotRTransId = Animator.StringToHash("Base Layer.Locomotion -> Base Layer.LocomotionPivotR");
    }
 
    
    void Update()
    {


    


        //Determine if there is a gameObject for Enemy Stealth Kill
        //resetting the attack counter over time
        // AttackCounter = Mathf.Lerp(AttackCounter, 0, Time.deltaTime / 20);
        while (stealthKillLOCUpdate == true && EnemyStealthKillGO != false)
        {
            EnemyStealthKillGO.transform.position = stealthKillLoc.transform.position;
            break;
        }


        //Pull values from keyboard / controller
        leftX = Input.GetAxis("Horizontal");
        leftY = Input.GetAxis("Vertical");


        //Set this when using controller
        // Press A to jump
        if (Input.GetKeyDown(Jump) || Input.GetButton("Jump"))
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        // Press B to sprint
        if (Input.GetKeyDown(Sprint))
        {
            speed = Mathf.Lerp(speed, SPRINT_SPEED, Time.deltaTime);
            gamecam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(gamecam.GetComponent<Camera>().fieldOfView, SPRINT_FOV, fovDampTime * Time.deltaTime);
        }
        else
        {
           // speed = speed;
            gamecam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(gamecam.GetComponent<Camera>().fieldOfView, NORMAL_FOV, fovDampTime * Time.deltaTime);
        }


        while (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {

            //   speed = Mathf.Lerp(speed, WALK_SPEED, Time.deltaTime);

            isWalkingForward = true;
      
            break;
        }



        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            isWalkingForward = false;


            if (gameObject.GetComponent<Animator>().GetBool("isDragWalk") == true)
            {
                anim.SetBool("isDragWalk", false);
            }

        }


        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            isWalkingBackward = true;
            //anim.SetBool("isBackward", true);

        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            isWalkingBackward = false;
            //anim.SetBool("isBackward", false);

            if (gameObject.GetComponent<Animator>().GetBool("isDragWalk") == true)
            {
                anim.SetBool("isDragWalk", false);
            }
        }

        if (Input.GetKeyDown(AttackingButton))
        {

            if (stopButtonMashing == false)
            {
                enabledPlayerWeaponCollider = true;
                StartCoroutine("isAttacking0");                     //Attacking
                                                                    //anim.SetTrigger("isAttacking0");
                murphyAttacking = true;
                // anim.SetTrigger("isAttacking");
            }

        }



        if (Input.GetKeyUp(AttackingButton))
        {
            //anim.SetBool("isAttacking", false);
            //Attacking
            murphyAttacking = false;
            enabledPlayerWeaponCollider = false;
            // anim.SetTrigger("isAttacking");

        }


        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {

            isMovingLeft = true;
            ////Determines Angle of Player 
            //Vector3 forward = transform.forward;
            //forward.y = 0;
            //float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
            //print(headingAngle);

            //////IF ANGLE BETWEEN 160 & 200
            //if (headingAngle > 170f && headingAngle < 190f)
            //{
            //    print(headingAngle);
            //    isMovingRight = true;
            //}

            //else
            //{
            //    print("Turn Left");
            //    isMovingLeft = true;
            //}



        }

        //if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(SprintButton) &&  isWalkingForward == true || Input.GetKeyDown(KeyCode.A) && isSprinting == true && isWalkingForward == true)
        //{
        //    //   isMovingLeft = true;
        //    Debug.Log("Signal dettected");
        //}



        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            if (isMovingLeft == true)
            {
                isMovingLeft = false;
            }

            if (isMovingRight == true)
            {
                isMovingRight = false;
            }

            if (gameObject.GetComponent<Animator>().GetBool("isDragWalk") == true)
            {
                    anim.SetBool("isDragWalk", false);
            }

        }


        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {

            //Determines Angle of Player
            //   Vector3 forward = transform.forward;
            //  forward.y = 0;
            //  float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
            //  print(headingAngle);

            //////IF ANGLE BETWEEN 160 & 200
            //if (headingAngle > 170f && headingAngle < 190f)
            //{
            //    print(headingAngle);

            //    isMovingLeft = true;
            //}

            //else
            //{
            //    print("Turn Left");
            //    isMovingRight = true;
            //}


            isMovingRight = true;

        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            if (isMovingLeft == true)
            {
                isMovingLeft = false;
            }

            if (isMovingRight == true)
            {
                isMovingRight = false;
            }

            
            if (gameObject.GetComponent<Animator>().GetBool("isDragWalk") == true)
            {
                    anim.SetBool("isDragWalk", false);
            }
        }


        //REVERT THIS BACK FOR CONTROLLER CONTROLl
        // Translate controls stick coordinates into world/cam/character space
        //   StickToWorldspace(this.transform, gamecam.transform, ref direction, ref speed, ref charAngle, IsInPivot());

        // speed = new Vector2 (horizontal,vertical).sqrMagnitude;



        if (isSprinting == false)
        {

         


            if (isWalkingForward == true && isMovingLeft == true || isWalkingForward == true && isMovingRight == true || isWalkingBackward == true && isMovingLeft == true || isWalkingBackward == true && isMovingRight == true)
            {
              
               // anim.SetFloat("Speed", speed - 1.4f, speedDampTime, Time.deltaTime);

                transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
                Dust.Play();
            }


            if (isWalkingForward == true )
            {
                //CHANGE THE -0.5f to something more streamlined ---- >> E>G> IF SHIFT & MOVE THEN SPEED - 0.2f;
                anim.SetFloat("Speed", speed + moveSpeed, speedDampTime, Time.deltaTime);


                if (isDragging == false)
                {
                    // Move the object forward along its z axis 1 unit/second.
                    transform.Translate(Vector3.forward * Time.deltaTime);
                    anim.SetBool("isDragWalk", false);
                }

                if (isDragging == true)
                {
                    // Move the object forward along its z axis 1 unit/second.
                    transform.Translate(Vector3.back * Time.deltaTime * 0.4f);
                    anim.SetBool("isDragWalk", true);
                }


            

          
                Dust.Play();

            }

            if (isWalkingBackward == true)
            {
                //CHANGE THE -0.5f to something more streamlined ---- >> E>G> IF SHIFT & MOVE THEN SPEED - 0.2f;
                anim.SetFloat("Speed", speed - moveSpeed, speedDampTime, Time.deltaTime);
                anim.SetBool("isBackward", true);

                if (isDragging == false)
                {
                    // Move the object forward along its z axis 1 unit/second.
                    transform.Translate(Vector3.back * 0.7f*  Time.deltaTime);
                     anim.SetBool("isDragWalk", false);
                }

                if (isDragging == true)
                {
                    // Move the object forward along its z axis 1 unit/second.
                    transform.Translate(Vector3.back * Time.deltaTime * 0.4f);
                    anim.SetBool("isDragWalk", true);
                }


               

                Dust.Play();

            }

            if (isWalkingBackward == false )
            {
                anim.SetBool("isBackward", false);

            }



            if (isMovingLeft == true )
            {
                if (isDragging == true)
                {
                    anim.SetBool("isDragWalk", true);
                }

                //CHANGE THE -0.5f to something more streamlined ---- >> E>G> IF SHIFT & MOVE THEN SPEED - 0.2f;
                anim.SetFloat("Speed", speed + moveSpeed, speedDampTime, Time.deltaTime);

                if (isStealthing == false)
                {
                    transform.Rotate(0, -turnSpeed, Time.deltaTime * turnSpeed);
                }

                if (isStealthing == true)
                {
                    transform.Rotate(0, -stealthTurnSpeed, Time.deltaTime * turnSpeed);
                }


                ////IF ANGLE BETWEEN 160 & 200
                //if (headingAngle > 160f && headingAngle < 200f)
                //{
                //   // print("ChangePlace");
                //    transform.Rotate(0, 7f, Time.deltaTime * turnSpeed);
                //}

                //else
                //{
                //    // print("Normal");
                //    transform.Rotate(0, -7f, Time.deltaTime * turnSpeed);
                //}



                Dust.Play();
            }

            if (isMovingRight == true)
            {
                if (isDragging == true)
                {
                    anim.SetBool("isDragWalk", true);
                }


                anim.SetFloat("Speed", speed + moveSpeed, speedDampTime, Time.deltaTime);


                if (isStealthing == false)
                {
                    transform.Rotate(0, turnSpeed, Time.deltaTime * turnSpeed);
                }

                if (isStealthing == true)
                {
                    transform.Rotate(0, stealthTurnSpeed, Time.deltaTime * turnSpeed);
                }


                Dust.Play();
            }

       

            //else
            //        {
            //    Dust.Stop();
            //}s

            if (letsHidetheBody == true)
            {
                anim.SetBool("isHidingBody", true);

              
                Collider[] colls = pickupThrow.bodyToHide.GetComponentsInChildren<Collider>();
                foreach (Collider c in colls)
                {
                   // print(c.name);
                    c.enabled = false;
                }


               // Collider colls2 = pickupThrow.bodyHideLOC.GetComponent<Collider>();
               // colls2.enabled = false;


                pickupThrow.bodyToHide.transform.position = pickupThrow.leftHand.transform.position;
                pickupThrow.bodyToHide.transform.rotation = pickupThrow.leftHand.transform.rotation;

                //this.gameObject.transform.LookAt(pickupThrow.bodyHideLOC.transform);
                transform.rotation = Quaternion.LookRotation(transform.position - pickupThrow.bodyHideLOC.transform.position);

                // The step size is equal to speed times frame time.
                float singleStep = 1.5f * Time.deltaTime;
                // Determine which direction to rotate towards
                Vector3 targetDirection = pickupThrow.bodyHideLOC.transform.position - transform.position;
                // Rotate the forward vector towards the target direction by one step
                Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);



                Invoke("SetLiftBoolBack", 4.2f);


                //print(pickupThrow.bodyToHide.transform.root.gameObject);
              
            }

            if (letsHidetheBody == false)
            {
                anim.SetBool("isHidingBody", false);
            }


            if (isStealthing == true)
            {
               
                if (isWalkingForward == true && isMovingLeft == true || isWalkingForward == true && isMovingRight == true || isWalkingBackward == true && isMovingLeft == true || isWalkingBackward == true && isMovingRight == true)
                {
                    anim.SetBool("isSneakWalking", true);
                
                    anim.SetFloat("Speed", speed - stealthSpeed, speedDampTime, Time.deltaTime);
                  //  transform.Rotate(Vector3.up * Time.deltaTime );
                }



                else if (isWalkingForward == true || isWalkingBackward == true)
                {
                    anim.SetBool("isSneakWalking", true);
                   anim.SetFloat("Speed", speed - stealthSpeed, speedDampTime, Time.deltaTime);

                }

                else if (isMovingLeft == true)
                {
                    anim.SetBool("isSneakWalking", true);
                    //CHANGE THE -0.5f to something more streamlined ---- >> E>G> IF SHIFT & MOVE THEN SPEED - 0.2f;
                    anim.SetFloat("Speed", speed - stealthTurnSpeed, speedDampTime, Time.deltaTime);
                 //   transform.Rotate(0, -3, Time.deltaTime * turnSpeed);

                }

                else if (isMovingRight == true)
                {
                     anim.SetBool("isSneakWalking", true);
                     anim.SetFloat("Speed", speed - stealthTurnSpeed, speedDampTime, Time.deltaTime);
                 //   transform.Rotate(0, 3, Time.deltaTime * turnSpeed);
                }

                else
                {
                    anim.SetBool("isSneakWalking", false);
                }

             


            }



            if (isWalkingForward == false && isMovingLeft == false || isWalkingForward == false && isMovingRight == false || isWalkingBackward == false && isMovingLeft == false || isWalkingBackward == false && isMovingRight == false)
            {
                  anim.SetFloat("Speed", 0f, speedDampTime, Time.deltaTime);
                //anim.SetBool("isSneakWalking", false);


            }

            if (isWalkingForward == false || isMovingRight == false || isMovingLeft == false || isWalkingBackward == false || isStealthing == false)
            {
               // anim.SetBool("isSneakWalking", false);
                //CHANGE THE -0.5f to something more streamlined ---- >> E>G> IF SHIFT & MOVE THEN SPEED - 0.2f;
                anim.SetFloat("Speed", 0f, speedDampTime, Time.deltaTime);

                
            }






        }

        if (isSprinting == true)
        {
            //CHANGE THE -0.5f to something more streamlined ---- >> E>G> IF SHIFT & MOVE THEN SPEED - 0.2f;
           // anim.SetFloat("Speed", speed + 1f, speedDampTime, Time.deltaTime);


            if (isWalkingForward == true && isMovingLeft == true || isWalkingForward == true && isMovingRight == true || isWalkingBackward == true && isMovingLeft == true || isWalkingBackward == true && isMovingRight == true)
            {
                // anim.SetFloat("Speed", speed - 1.4f, speedDampTime, Time.deltaTime);
                transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
            }


            if (isWalkingForward == true || isWalkingBackward == true)
            {
                //CHANGE THE -0.5f to something more streamlined ---- >> E>G> IF SHIFT & MOVE THEN SPEED - 0.2f;
                anim.SetFloat("Speed", speed + sprintSpeed, speedDampTime, Time.deltaTime);

                // Move the object forward along its z axis 1 unit/second.
                transform.Translate(Vector3.forward * sprintSpeed * Time.deltaTime);
            }

            if (isMovingLeft == true)
            {
                //CHANGE THE -0.5f to something more streamlined ---- >> E>G> IF SHIFT & MOVE THEN SPEED - 0.2f;
                anim.SetFloat("Speed", speed + turnSpeed *0.4f, speedDampTime, Time.deltaTime);
                transform.Rotate(0, -5f, Time.deltaTime * turnSpeed);

            }

            if (isMovingRight == true)
            {
                anim.SetFloat("Speed", speed + turnSpeed * 0.4f, speedDampTime, Time.deltaTime);
                transform.Rotate(0, 5f, Time.deltaTime * turnSpeed);
            }




        }



        if (Input.GetKey(SprintButton))
        {
            //  moveSpeed = sprintSpeed;
            isSprinting = true;
            // break;

        }

        //if (Input.GetKeyUp(SprintButton))
        //{
        //    //  moveSpeed = sprintSpeed;
        //    isSprinting = false;
        //    // break;

        //}


        //Set this when using controller
        anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);

       // anim.SetFloat("Direction", horizontal, DirectionDampTime, Time.deltaTime);


        if (speed > LocomotionThreshold)    // Dead zone
        {
            if (!IsInPivot())
            {
                anim.SetFloat("Angle", charAngle);
            }
        }
        if (speed < LocomotionThreshold && Mathf.Abs(leftX) < 0.05f)    // Dead zone
        {
            anim.SetFloat("Direction", 0f);
            anim.SetFloat("Angle", 0f);
        }



        charAngle = 0f;
        direction = 0f;





        #region player speed



        #region player stealth

        if (Input.GetKey(StealthButton))
        {
            isStealthing = true;
            anim.SetBool("isSneaking", true);
//            CinematicBars.Instance.ShowBars();

            print("Stealth Button Check");



           
            while (Input.GetKey(GoHidden))                     //MurphyBecomes Undetectible
            {
                IsHidden = true;
                anim.SetBool("GoHidden", true);
               
                break;
            }

            if (Input.GetKeyUp(GoHidden))                     //MurphyBecomes Undetectible
            {
                IsHidden = false;
                anim.SetBool("GoHidden", false);
              
            }


            if (Input.GetKeyDown(stealthAttackButton))
            {
                if (EnemyStealthKillGO != null && StopStealthSpam == false)
                {
                    StopStealthSpam = true;

                    if (EnemyStealthKillGO.gameObject.tag == "Civilian")
                    {

                        //SPAWN CRIME CUBE
                        Alarm.Instance.GenerateCrimeCube(EnemyStealthKillGO.gameObject);

                        if (EnemyStealthKillGO.gameObject.tag == "Civilian")
                        {
                            CivilianFOV civilianFOV = EnemyStealthKillGO.gameObject.GetComponent<CivilianFOV>();
                            civilianFOV.IsBeingStealthKilled = true;

                        }

                        //if (EnemyStealthKillGO.gameObject.tag == "Enemy")
                        //{
                        //    Debug.Log("Guard is being stealth Killed");
                        //    EnemyFOV enemyFOV = EnemyStealthKillGO.gameObject.GetComponent<EnemyFOV>();
                        //    enemyFOV.IsBeingStealthKilled = true;

                        //}



                        anim.SetBool("isStealthKill", true);
                        print("Civilian: Dying Now");
                        EnemyRagdollGO = EnemyStealthKillGO.gameObject.GetComponent<CivilianAI>().RagdollGO;
                        EnemyCostumeGO = EnemyStealthKillGO.gameObject.GetComponent<CivilianAI>().Costume;
                        civilianAnim = EnemyStealthKillGO.gameObject.GetComponent<Animator>();
                        //enable enemy anim for being killed
                        StartCoroutine("CivilianAnimRagDoll");
                        EnemyStealthKillGO.transform.position = stealthKillLoc.transform.position;
                        stealthKillLOCUpdate = true;
                        EnemyStealthKillGO.transform.LookAt(stealthKillFocus);
                    }


                    if (EnemyStealthKillGO.gameObject.tag == "Police")
                    {
                        //SPAWN CRIME CUBE
                        Alarm.Instance.GenerateCrimeCube(EnemyStealthKillGO.gameObject);

                        if (EnemyStealthKillGO != false)
                        {
                            stealthKillLOCUpdate = true;
                        }
   


                        anim.SetBool("isStealthKill", true);
                        print("Police: Dying Now");
                        EnemyStealthKillGO.GetComponent<StateController>().isBeingStealthKilled();
                    }


                        if (EnemyStealthKillGO.gameObject.tag == "Enemy")
                    {
                        
                        //SPAWN CRIME CUBE
                        Alarm.Instance.GenerateCrimeCube(EnemyStealthKillGO.gameObject);

                        if (EnemyStealthKillGO != false)
                        {
                            stealthKillLOCUpdate = true;
                        }

                        anim.SetBool("isStealthKill", true);
                        print("Guard: Dying Now");
                        EnemyStealthKillGO.GetComponent<StateController>().isBeingStealthKilled();

                        ////ENTER OPTIONS FOR STAFF MEMBERS 
                        //EnemyStealthKillGO.SetActive



                        //enemyAIController = EnemyStealthKillGO.gameObject.GetComponent<GuardAI>();
                        //enemyFOVSript = EnemyStealthKillGO.gameObject.GetComponent<EnemyFOV>();

                        //Debug.Log("Guard is being stealth Killed");
                        //EnemyFOV enemyFOV = EnemyStealthKillGO.gameObject.GetComponent<EnemyFOV>();
                        //enemyFOV.IsBeingStealthKilled = true;


                        //if (EnemyStealthKillGO != null && enemyAIController.State0 == true)
                        //{


                        //    anim.SetBool("isStealthKill", true);
                        //    //Get and Disable enemy script
                        //    // enemyFOVSript = EnemyStealthKillGO.gameObject.GetComponent<EnemyFOV>();
                        //    // enemyChaseScript = EnemyStealthKillGO.gameObject.GetComponent<GuardChase>();
                        //    enemyAnim = EnemyStealthKillGO.gameObject.GetComponent<Animator>();
                        //    enemyPatrol = EnemyStealthKillGO.gameObject.GetComponent<GuardPatrol>();
                        //    enemyCapsuleCollider = EnemyStealthKillGO.gameObject.GetComponent<CapsuleCollider>();

                        //    foreach (Collider c in EnemyStealthKillGO.gameObject.GetComponents<CapsuleCollider>())
                        //    {
                        //        c.enabled = false;
                        //    }

                        //    foreach (GameObject GO in EnemyStealthKillGO.gameObject.GetComponent<GuardAI>().Weapons)
                        //    {
                        //        GO.SetActive(false);
                        //    }



                        //    EnemyRagdollGO = EnemyStealthKillGO.gameObject.GetComponent<GuardAI>().RagdollGO;
                        //    EnemyStealthKillGO.GetComponent<NavMeshAgent>().enabled = false;


                        //    //   enemyChaseScript.enabled = false;
                        //    enemyFOVSript.enabled = false;
                        //    enemyPatrol.enabled = false;
                        //    enemyCapsuleCollider.enabled = false;

                        //    //enable enemy anim for being killed
                        //    StartCoroutine("EnemyAnimRagDoll");



                        //    //Move Enemy Stealth Kill to Player

                        //    EnemyStealthKillGO.transform.position = stealthKillLoc.transform.position;
                        //    stealthKillLOCUpdate = true;
                        //    EnemyStealthKillGO.transform.LookAt(stealthKillFocus);

                        //    //Rotate the enemy away from the player

                        //    //Wait 2s

                        //    //Deal 105% damage

                        //    //Ragdoll
                        //}
                    }

                    if (EnemyStealthKillGO.gameObject.tag == "Staff")
                    {
                        //ENTER OPTIONS FOR STAFF MEMBERS 

                        staffAIController = EnemyStealthKillGO.gameObject.GetComponent<StaffAIController>();
                        staffFOV = EnemyStealthKillGO.gameObject.GetComponent<StaffFOV>();




                        if (EnemyStealthKillGO != null /*&& staffAIController.State0 == true*/)
                        {


                            anim.SetBool("isStealthKill", true);
                            //Get and Disable enemy script
                            // enemyFOVSript = EnemyStealthKillGO.gameObject.GetComponent<EnemyFOV>();
                            // enemyChaseScript = EnemyStealthKillGO.gameObject.GetComponent<GuardChase>();
                            staffAnim = EnemyStealthKillGO.gameObject.GetComponent<Animator>();
                            staffPatrol = EnemyStealthKillGO.gameObject.GetComponent<StaffPatrol>();
                            staffCapsuleCollider = EnemyStealthKillGO.gameObject.GetComponent<CapsuleCollider>();

                            foreach (Collider c in EnemyStealthKillGO.gameObject.GetComponents<CapsuleCollider>())
                            {
                                c.enabled = false;
                            }

                            //foreach (GameObject GO in EnemyStealthKillGO.gameObject.GetComponent<GuardAI>().Weapons)
                            //{
                            //    GO.SetActive(false);
                            //}



                            EnemyRagdollGO = EnemyStealthKillGO.gameObject.GetComponent<StaffAIController>().RagdollGO;
                            EnemyStealthKillGO.GetComponent<NavMeshAgent>().enabled = false;


                            //   enemyChaseScript.enabled = false;
                            staffFOV.enabled = false;
                            staffPatrol.enabled = false;
                            staffCapsuleCollider.enabled = false;

                            //enable enemy anim for being killed
                            StartCoroutine("StaffAnimRagDoll");



                            //Move Enemy Stealth Kill to Player

                            EnemyStealthKillGO.transform.position = stealthKillLoc.transform.position;
                            stealthKillLOCUpdate = true;
                            EnemyStealthKillGO.transform.LookAt(stealthKillFocus);

                            //Rotate the enemy away from the player

                            //Wait 2s

                            //Deal 105% damage

                            //Ragdoll
                        }
                    }

                    Invoke("SetBoolBack", 3.0f);
                }







                //    if (Input.GetKey("up") || Input.GetKeyDown(KeyCode.W))
                //    {
                //        print("SneakingForward");
                //        anim.SetBool("isSneakingBackwards", false);
                //        anim.SetBool("isSneakWalking", true);



                //        //anim.SetFloat("Speed", 0.1f, speedDampTime, Time.deltaTime);
                //        //anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);

                //        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
                //    }

                //    if (Input.GetKeyUp("up") || Input.GetKeyUp(KeyCode.W))
                //    {
                //        anim.SetBool("isSneakWalking", false);
                //    }


                //if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
                //{
                //    Quaternion.Euler(0, 45, 0) * Vector3.forward;
                //}



                //    //if (Input.GetKey("left")  || Input.GetKeyDown(KeyCode.A) )
                //    //{
                //    //    anim.SetBool("isSneakWalking", true);

                //    //    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                //    //}

                //    //if (Input.GetKeyUp("left") || Input.GetKeyUp(KeyCode.A) )
                //    //{
                //    //    anim.SetBool("isSneakWalking", false);
                //    //}


                //    //if (Input.GetKey("right") || Input.GetKeyDown(KeyCode.D))
                //    //{
                //    //    anim.SetBool("isSneakWalking", true);
                //    //    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                //    //}

                //    //if ( Input.GetKeyUp("right") || Input.GetKeyUp(KeyCode.A))
                //    //{
                //    //    anim.SetBool("isSneakWalking", false);
                //    //}

               

            }

            if (IsHidden == false && isInInventory == false)
            {
                // Get the horizontal and vertical axis.
                // By default they are mapped to the arrow keys.
                // The value is in the range -1 to 1
                float translation = Input.GetAxis("Vertical") * stealthSpeed;
                float rotation = Input.GetAxis("Horizontal") * rotationDegreePerSecond;

                // Make it move 10 meters per second instead of 10 meters per frame...
                translation *= Time.deltaTime;
                rotation *= Time.deltaTime;

                // Move translation along the object's z-axis
                transform.Translate(0, 0, translation);

                // Rotate around our y-axis
                transform.Rotate(0, rotation, 0);
            }
           


            //// Translate controls stick coordinates into world/cam/character space
            //StickToWorldspace(this.transform, gamecam.transform, ref direction, ref speed, ref charAngle, IsInPivot());

            //if (Input.GetAxis("Horizontal") > 0.5f || Input.GetAxis("Horizontal") < -0.5f)
            //{
            //    transform.Translate(new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            //    anim.SetBool("isSneakWalking", true);

            //}

            //if (Input.GetAxis("Vertical") > 0.5f || Input.GetAxis("Vertical") < -0.5f)
            //{
            //    transform.Translate(new Vector3(0f, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime), 0f);
            //    anim.SetBool("isSneakWalking", true);

            //}
            if (Input.GetKey("down") || Input.GetKeyDown(KeyCode.S))
            {
               // anim.SetBool("isSneakWalking", false);
                anim.SetBool("isSneakingBackwards", true);

                print("IsSneakingBackwards");

                // transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

                //  anim.SetFloat("Speed", 0.1f, speedDampTime, Time.deltaTime);
                //  anim.SetFloat("Direction", direction, DirectionDampTime, Time.deltaTime);
            }

            if (Input.GetKeyUp("down") || Input.GetKeyUp(KeyCode.S))
            {
                anim.SetBool("isSneakingBackwards", false);
            }
            // moveSpeed = stealthSpeed;
            //  break;
            sneak = Mathf.Lerp(sneak, 1, Time.deltaTime);
            anim.SetFloat("Sneak", sneak);
            //  break;


            //}
        }

        else
        {
            anim.SetBool("isSneakingBackwards", false);
          //  anim.SetBool("isSneakWalking", false);
          if (CinematicBars.Instance != null)
            {
                CinematicBars.Instance.HideBars();
            }
        
            anim.SetBool("isSneaking", false);
            anim.SetFloat("Sneak", sneak);
            sneak = Mathf.Lerp(sneak, 0, Time.deltaTime);
            if (sneak <= 0.01)
            {
                sneak = 0;
            }
        }



        if (Input.GetKeyUp(StealthButton))
        {
            // anim.SetBool("isCrouched", false);
            isStealthing = false;

        }





        if (Input.GetKeyUp(SprintButton))
        {
            isSprinting = false;
            // break;
        }

        #endregion

        #endregion


        #region player movement

       // speed = Mathf.Lerp(speed, IDLE_SPEED, Time.deltaTime);

        //if (isWalkingForward == true)
        //{
        //    isStealthing = false;

        //}

        //if (isStealthing == true)
        //{
        //    isWalkingForward = false;
        //    anim.SetBool("isWalking", false);
        //}


        //WASD & Arrow Keys
       

        #endregion
    }

    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }


    private void SetBoolBack()
    {
        StopStealthSpam = false;
    }


    IEnumerator EnemyAnimRagDoll()
    {
        enemyAnim.SetBool("isBeingStealthKilled", true);
        yield return new WaitForSeconds(2.5f);
        //enemyAnim.enabled = false;
        Instantiate(EnemyRagdollGO, transform.position, transform.rotation);
        stealthKillLOCUpdate = false;
        Destroy(EnemyStealthKillGO);
        anim.SetBool("isStealthKill", false);


    }

    IEnumerator StaffAnimRagDoll()
    {
        staffAnim.SetBool("isBeingStealthKilled", true);
        yield return new WaitForSeconds(1.5f);
        //enemyAnim.enabled = false;
        Instantiate(EnemyRagdollGO, transform.position, transform.rotation);
        stealthKillLOCUpdate = false;
        Destroy(EnemyStealthKillGO);

        anim.SetBool("isStealthKill", false);

    }

    IEnumerator CivilianAnimRagDoll()
    {
        print("Civilian: Being Stealth Killed");
        civilianAnim.SetBool("isBeingStealthKilled", true);
        yield return new WaitForSeconds(1.5f);
        //enemyAnim.enabled = false;
        Instantiate(EnemyRagdollGO, transform.position, transform.rotation);
        Instantiate(EnemyCostumeGO, CostumeSpawnLoc.transform.position, transform.rotation);

        stealthKillLOCUpdate = false;
        Destroy(EnemyStealthKillGO);

        anim.SetBool("isStealthKill", false);

    }

    IEnumerator isAttacking0()
    {
        stopButtonMashing = true;
        //anim.SetTrigger("isAttacking0");
        anim.SetBool("isAttacking", true);

        if (AttackCounter < 1.0f)
        {
            AttackCounter = Mathf.Lerp(AttackCounter, 1, Time.deltaTime * 3);
            anim.SetFloat("AttackCounter", AttackCounter);
        }
       if (AttackCounter >= 0.99f)
        {
            AttackCounter = 0.001f;
        }

        yield return new WaitForSeconds(0.1f);
        anim.SetBool("isAttacking", false);
        // anim.SetTrigger("isAttacking0");
        stopButtonMashing = false;
        
        StopCoroutine("isAttacking0");
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

    //is character animator controller in either one of these two states    
    public bool IsInPivot()
    {
        //   return (anim.GetCurrentAnimatorStateInfo(0).IsName("Base.LocomotionPivotLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("Base.LocomotionPivotRight"));
        return stateInfo.fullPathHash == m_LocomotionPivotLId ||
                 stateInfo.fullPathHash == m_LocomotionPivotRId ||
                 transInfo.fullPathHash == m_LocomotionPivotLTransId ||
                 transInfo.fullPathHash == m_LocomotionPivotRTransId;

    }

    public bool IsInLocomotion()
    {
        return stateInfo.fullPathHash == m_LocomotionId;
    }

    public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut, ref float angleOut, bool isPivoting)
    {
        Vector3 rootDirection = root.forward;

        Vector3 stickDirection = new Vector3(leftX, 0, leftY);

        speedOut = stickDirection.sqrMagnitude;

        // Get camera rotation
        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f; // kill Y
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, Vector3.Normalize(CameraDirection));

        // Convert joystick input in Worldspace coordinates
        Vector3 moveDirection = referentialShift * stickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), stickDirection, Color.blue);
        Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2.5f, root.position.z), axisSign, Color.red);

        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
        if (!isPivoting)
        {
            angleOut = angleRootToMove;
        }
        angleRootToMove /= 180f;

        directionOut = angleRootToMove * directionSpeed;
    }


    void createDust()
    {
        Dust.Play();
    }

  

    private void SetLiftBoolBack()
    {
        if (pickupThrow.bodyToHide.transform.root.gameObject != null)
        {
           pickupThrow.bodyToHide.transform.root.gameObject.SetActive(false);
        }
      
        letsHidetheBody = false;

    }

}
