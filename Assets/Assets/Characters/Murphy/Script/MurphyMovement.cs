        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurphyMovement : MonoBehaviour
{
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
    [Tooltip("Sprint factor is the speed while running")]
    public float sprintSpeed;
    public float backwardsMovement;
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
    public bool isWalking;
    private bool isUpPressed;


    //Anim
    Animator anim;

    [Header("Keycodes")]
    public KeyCode AttackingButton = KeyCode.A;
    public KeyCode StealthButton = KeyCode.LeftShift;
    public KeyCode SprintButton = KeyCode.LeftControl;
    #endregion

    #region Feet Grounder
    //Feet grounder
    private Vector3 rightFootPosition, leftFootPosition, leftFootIkPosition, rightFootIkPosition;
    private Quaternion leftFootIkRotation, rightFootIkRotation;
    private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;

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

    [Header("Reference to other scripts")]
    private StealthNoiseMeasure stealthNoiseMeasure;
    #endregion

    private void Start()
    {
        stealthNoiseMeasure = GetComponent<StealthNoiseMeasure>();
        enabledPlayerWeaponCollider = false;
        #region movement
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        #endregion
        moveSpeed = defaultMoveSpeed;
    }
    void Update()
    {
        #region player speed
        if (Input.GetKeyDown(SprintButton) )
        {
            // anim.SetFloat("Speed", 0.5f);
            // anim.SetBool("isWalking", true);
            // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            moveSpeed = sprintSpeed;
            isSprinting = true;
           // break;
        }

        //if (Input.GetKeyDown(StealthButton)  )
        //{
        //    if (Input.GetKeyDown(KeyCode.UpArrow))
        //    {
        //        print("Im Sneaking");
        //        // anim.SetFloat("Speed", 0.5f);
        //        // anim.SetBool("isWalking", true);
        //        // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //        anim.SetBool("isSneaking", true);
        //        // moveSpeed = stealthSpeed;
        //        isStealthing = true;
        //        //  break;
        //    }

        //}

        if (Input.GetKeyDown(StealthButton))
        {
            if (isWalking == true)
            {
                isWalking = false;
            }
           
            isStealthing = true;
            moveSpeed = stealthSpeed;
            //  break;
        }
        if (Input.GetKeyUp(StealthButton))
        {
            // anim.SetBool("isCrouched", false);
            isStealthing = false;
            moveSpeed = defaultMoveSpeed;
            //  break;
        }


        if (Input.GetKeyUp(SprintButton))
        {
            // anim.SetFloat("Speed", 0.5f);
            // anim.SetBool("isWalking", true);
            // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            moveSpeed = defaultMoveSpeed;
            isSprinting = false;
            // break;
        }

        //if (Input.GetKeyUp(StealthButton))
        //{
        //    // anim.SetFloat("Speed", 0.5f);
        //    // anim.SetBool("isWalking", true);
        //    // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //    anim.SetBool("isSneaking", false);
        //    moveSpeed = defaultMoveSpeed;
        //    isStealthing = false;
        //    //  break;
        //}


        #endregion


        #region player movement

        if (isWalking == true)
        {
            isStealthing = false;
            anim.SetBool("isSneaking", false);
            anim.SetBool("isSneakingBackwards", false);
        }

        if (isStealthing == true)
        {
            isWalking = false;
            anim.SetBool("isWalking", false);
        }


        //WASD & Arrow Keys
        while (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            isUpPressed = true;
            anim.SetBool("isSneakingBackwards", false);

            if (isStealthing == false && isUpPressed == true)
            {
                // anim.SetFloat("Speed", 0.5f);
                anim.SetBool("isWalking", true);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                isWalking = true;
            }

            if (isStealthing == true && isUpPressed == true)
            {
                anim.SetBool("isSneaking", true);
               
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
                isWalking = false;
            }
          
            break;
        }

       

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            if (isStealthing == false)
            {
                //  anim.SetFloat("Speed", 0.0f);
                anim.SetBool("isWalking", false);
                isWalking = false;
                //ADD SLOW DOWN ANUME
                //anim.
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }

            if (isStealthing == true)
            {
                anim.SetBool("isSneaking", false);
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }


        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (isStealthing == true)
            {
                anim.SetBool("isSneakingBackwards", true);
                transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            }

            if (isStealthing == false)
            {
                anim.SetBool("isSneakingBackwards", false);
                //Turning the character 180, when backwards is pressed.
                anim.SetBool("isBackward", true);
                // transform.Translate(-Vector3.forward * backwardsMovement * Time.deltaTime);
                transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
                isWalking = true;
            }
          
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("isBackward", false);
            isWalking = false;

        }

        if (Input.GetKeyDown(AttackingButton))
        {

            if (stopButtonMashing == false)
            {
                enabledPlayerWeaponCollider = true;
                StartCoroutine("isAttacking0");                     //Attacking
                                                                    //anim.SetTrigger("isAttacking0");
                murphyAttacking = true;
                //  anim.SetTrigger("isAttacking");
            }

        }

        

        if (Input.GetKeyUp(AttackingButton))
        {
            //anim.SetBool("isAttacking", false);
            //Attacking
            murphyAttacking = false;
           // anim.SetTrigger("isAttacking");
        }



        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            
            anim.SetBool("Turning_Left", true);
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            isWalking = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("Turning_Left", false);
            isWalking = false;
        }


        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("Turning_Right", true);
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            isWalking = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("Turning_Right", false);
            isWalking = false;

        }

        #endregion
    }


    IEnumerator isAttacking0()
    {
        stopButtonMashing = true;
        //anim.SetTrigger("isAttacking0");
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("isAttacking", false);
        // anim.SetTrigger("isAttacking0");
        stopButtonMashing = false;
        enabledPlayerWeaponCollider = false;
        StopCoroutine("isAttacking0");
    }


    #region Feet Grounding
    /// <summary>
    /// We are updating the adjust feet target method and also finding the position of each foot inside our Solver position
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

        //Rightfoot IK position and rotation --- utilise pro features
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        if (useProIkFeature)
        {anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat(rightFootAnimVariableName));}

        MoveFeetToIkPoint(AvatarIKGoal.RightFoot, rightFootIkPosition, rightFootIkRotation, ref lastRightFootPositionY);

        //Left foot IK position and rotation --- utilise pro features
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

        if (useProIkFeature)
        { anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat(leftFootAnimVariableName)); }

        MoveFeetToIkPoint(AvatarIKGoal.LeftFoot, leftFootIkPosition, leftFootIkRotation, ref lastLeftFootPositionY);
    }
    #endregion

    #region Geet Grounding Methods
    /// <summary>
    /// Moves the feet to ik point.
    /// </summary>
    /// <param name="foot">Foot.</param>
    /// <param name="positionIkHolder">Position ik holder.</param>
    /// <param name="rotationIkHolder">Rotation ik holder.</param>
    /// <param name="lastFootPositionY">Last foot position y.</param>
    /// 
    void MoveFeetToIkPoint(AvatarIKGoal foot, Vector3 positionIkHolder, Quaternion rotationIkHolder, ref float lastFootPositionY)
    {
        Vector3 targetIKPosition = anim.GetIKPosition(foot);
        if (positionIkHolder != Vector3.zero)
        {
            targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
            positionIkHolder = transform.InverseTransformPoint(positionIkHolder);

            float yVariable = Mathf.Lerp(lastFootPositionY, positionIkHolder.y, feetToIkPositionSpeed);
            targetIKPosition.y += yVariable;

            lastFootPositionY = yVariable;

            targetIKPosition = transform.TransformPoint(targetIKPosition);

            anim.SetIKRotation(foot, rotationIkHolder);
        }

        anim.SetIKPosition(foot, targetIKPosition);
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
        //Raycast Handling Section
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

        feetIkPositions = Vector3.zero; //it didn't work 
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

