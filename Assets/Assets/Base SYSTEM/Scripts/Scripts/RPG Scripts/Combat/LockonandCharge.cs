using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*=================
  Lock-on to Enemy
=================*/

public class LockonandCharge : MonoBehaviour {

    public KeyCode LockOn = KeyCode.LeftShift;

    //Raycast Layer Mask
    public LayerMask EnemyMask;  //Layer ask for obsticle

   // public Camera MainCamera;
   // public Camera TargetingCamera;

    //Vector3
    Vector3 offset;


    //Transform
    public Transform target;                //Enemy Transform
    public Transform plyrPivotPoint;        //Player Pivot Point
   // public Transform TargetingCam;          //Targeting Cam POint
    private Transform CamTransform;

    //Float
    public float radius;                    //Radious of Lock on
    public float speed = 3.0f;             //Speed
    public float distToTarget;             //Distance to target
    public float attackDistance;            //Attack Distance
    public float MoveTwdsEnemySpeed;        //Move towards enemy speed
    public float Enemybufferdistance;
    //Bool
    public bool LookingAtYou;
    private bool resetplayerpos;
    public bool LetsRotate;
    public bool MainCharAttacking;
    public bool withinAttackDistance;
 

    //GameObj

    public GameObject MainCamera;
    public GameObject TargetingCam;
    public GameObject TrgtCamPoint;
    public GameObject OriginalCamaraParent;
    public GameObject CameraTarget;
    public GameObject Player;
    public GameObject lockonCube;
 

   // ThirdPersonOrbitCamBasic CameraObject;

    // Angle of player
    //[SerializeField]
    //float eulerAngY;



    void Start () 
    {

		//CameraObject = Camera.GetComponent<ThirdPersonOrbitCamBasic>();
        //Identifying which camera to turn on and off
        //CamTransform = MainCamera.transform;


    }

    ///*=================
    //  TO DO:
    // CAMERA FOLLOW PLAYER        
    //=================*/


    void Update()
    {
        MainCharAttacking = CharacterAttack.CharacterAttacking;

        //Move to attack
        MoveToAttack();

        //RADAR SYSTEM
        RayCastFromPivotPoint();


        FindDisttoTarget();

        CameraTarget.transform.position = TargetingCam.transform.position;
       
        //CameraTarget.transform.Rotate(0, speed * Time.deltaTime, 0);// Rotate around pivot point

        //eulerAngY = plyrPivotPoint.localEulerAngles.y;                //Setting the eular angle of the player

        FindTargets();                                              //Find Enemy Targets

        if (Input.GetKeyDown(LockOn))
        {
            // CameraTarget.transform.parent = TargetingCam.transform;     //Setting the camera target as child
            //CameraObject.enabled = false;                                       //DisableOriginal Cam Script
            MainCamera.gameObject.SetActive(false);                             //Disable Original Cam 
            TargetingCam.gameObject.SetActive(true);                            //Enable Targeting Cam
            LookingAtYou = true;
            resetplayerpos = true;                                  //Enable Look at target

        }


        if (Input.GetKeyUp(LockOn))
        {
            //CameraTarget.transform.parent = OriginalCamaraParent.transform;     //Reverting pareting
                                                                                //CameraObject.enabled = true;                                        //Enable Original Cam Script
            MainCamera.gameObject.SetActive(true);                              //enaable Original Cam 
            TargetingCam.gameObject.SetActive(false);                            //disable Targeting Cam
            resetplayerpos = false;
            LookingAtYou = false;                                               //Dissable Look at target
        }

        if (LookingAtYou == true)
        {
            //Player.transform.LookAt(target);
            //plyrPivotPoint.transform.LookAt(target) ;                    //CAMERATarget LOOKS AT TARGET
            TargetingCam.transform.LookAt(target);                    //CAMERA LOOKS AT TARGET
            LockingOn();
            //CameraTarget.transform.rotation = Quaternion.LookRotation(playerObject.position - target.position);
            //CameraTarget.transform.Rotate(0, speed * Time.deltaTime, 0);
            //    CameraTarget.transform.RotateAround(playerObject.forward, Vector3.up, 20 * Time.deltaTime);
        }

        if (resetplayerpos == true)
        {
            //Player.transform.rotation = Quaternion.Euler(0, 180, 0);

            resetplayerpos = false;
        }



       



    }

    public void FindTargets()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider col in cols)
        {
            if (col && col.tag == "Enemy")
            { // if object has the right tag...
              // assuming the enemy script is called EnemyScript
                target = col.transform;
            }
        }
    }


    /*=================
        Radar SYSTEM          
     =================*/

    public void RayCastFromPivotPoint()
    {

        Rotate();

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(TrgtCamPoint.transform.position, TrgtCamPoint.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, EnemyMask))
        {
            LetsRotate = false;
            Debug.DrawRay(TrgtCamPoint.transform.position, TrgtCamPoint.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
          //  Debug.Log("Did Hit");
          //  Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            LetsRotate = true;
            Debug.DrawRay(TrgtCamPoint.transform.position, TrgtCamPoint.transform.TransformDirection(Vector3.forward) * radius, Color.green);
           // Debug.Log("Did not Hit");
        }
    }

    public void Rotate()
    {

        if (LetsRotate == true)
        {
           // TargetingCam.transform.LookAt(Player.transform);
          //  plyrPivotPoint.transform.Rotate(0, speed * Time.deltaTime, 0);
          if (target != null)
            {
                Vector3 targetDir = target.position - plyrPivotPoint.transform.position;
                // The step size is equal to speed times frame time.
                float step = speed * Time.fixedDeltaTime;

                Vector3 newDir = Vector3.RotateTowards(plyrPivotPoint.transform.forward, targetDir, step, 0.0f);
                Debug.DrawRay(plyrPivotPoint.transform.position, newDir, Color.red);

                // Move our position a step closer to the target.
                plyrPivotPoint.transform.rotation = Quaternion.LookRotation(newDir);
            }
            


        }

        else
        {
            //IF RAYLINE HITS TARGET THEN STOP MOVEMENT
            plyrPivotPoint.transform.Rotate(0, 0, 0);
        }

    }

    public void LockingOn()
    {
        lockonCube.transform.LookAt(target);


    }

    public void FindDisttoTarget ()
    {
        if (target != null)
        {
            distToTarget = Vector3.Distance(target.position, Player.transform.position);
           // print(distToTarget);
        }
 
    }
    public void MoveToAttack()
    {
        if (MainCharAttacking == true)
        {
            if (distToTarget <= attackDistance)
            {
                withinAttackDistance = true;
            }
            else if (distToTarget >= attackDistance)
            {
                withinAttackDistance = false;
            }
        }

        if (withinAttackDistance == true)
        {
          while (distToTarget >= Enemybufferdistance)
            {
                Player.transform.position = Vector3.MoveTowards(Player.transform.position, target.position, MoveTwdsEnemySpeed);//move to attack position 
                withinAttackDistance = false;
                break;
            }
        
        }
    }



}
