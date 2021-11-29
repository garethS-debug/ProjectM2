                using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffFOV : MonoBehaviour
{


    /*
     * This is the Eyesight of the Enemy Character & determines the character's 'states
     */

    //Animation
    static Animator anim;
    public static StaffFOV staffFOV;

    //FLOAT / INTS
    public float viewRadius;    //viewing radius of player
    [Range(0, 360)]              //clampting to 360  
    public float viewAngle;     //viewing angle
    [Range(0, 360)]              //clampting to 360  
    public float alertViewAngle;     //viewing angle
                                      // public float ChaseDistances = 10.0f;

    public float alertDistance;
    public float alertRange;

    [Header("Costumes")]
    [Tooltip("WHO IS SHE. Used to determine what costumes fool this character")]
    public string WhoAmI;

    [Header("Last Known Guard LOC")]
    public List<Vector3> LastKnownGuardLOCList = new List<Vector3>();
    public Vector3 LastKnownGuardLocation;


    //INT
    // public int EnemyID;
    [Header("Layer Masks / list of target")]
    [Tooltip("What can this character see!")]
    //Layer Mask
    public LayerMask targetMask;    //layer mask for target
    public LayerMask obstacleMask;  //Layer ask for obsticle
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();//list of visiable targets
 
                                                                 //public List<Transform> WaypointLocation = new List<Transform>();//list of visiable targets
    [Space(10)]

    [Header("Waypoints")]
    [Tooltip("Waypoints added")]
    public GameObject WaypointGORef;
    public Vector3 LastKnownGuardLocationVector3;
    public Transform LastKnownGuardLOC;
    [Tooltip("Max amount of waypoints")]
    public float MaxWaypoints = 6;


    [Header("Reference to other script")]
    private CharacterCombat combat; // reference to Enemy Attacking Player
    public CharacterStats PlayerStats;
    private Chase chase;
    public EquipmentManager equipmentManager;
    private StaffPatrol patrol;
    private StaffAIController staffAIController;
    private DistractableOBJ distractableObject;
    private FieldOfView playerFOV;
    private NewMurphyMovement newMurphyMovement;

    //Enemyattack bubble
    //public float variance;    // This much variance 
    //public float offsetdistance; // at this distance

    [Header("Who is this character")]
    [Tooltip("Override to control the character")]
    //public bool stopWalking;
    // public static bool enemySleeping;
    public bool AlertStatusActive = false;
    public bool HightenedAlertActive;
    public bool AlertToHightenedAlert;
    public bool PlayerinFOV = false;
    public bool GuardinFOV = false;
    // public bool AttackStatusActive; //An attack status to engage combat if player exceeds alter status 
    private bool checkTheCostume;
    public bool fooledByCostume;


    [Header("Distraction")]
    public bool Distraction;
    public GameObject distraction;

    [Header("in Alert FOV")]
    public bool inPeriphVision;

    [Header("MurphyLOCATION")]
    public List<Vector3> LastKnownMurphyLocation = new List<Vector3>();//list of last known Location
    public Vector3 LastKnownMurphyLOC;

    [Header("Guard")]
    public GameObject guard;
    private GuardAI guardAI;

    [Header("Crime")]
    public bool SeenCrime;
    public bool IsBeingStealthKilled;

    [Header("Viewer for FOV")]
    public float meshResolution;
    public MeshFilter viewMeshFilter;
    private Mesh viewMesh;
    public int edgeResolveIterations;
    public float edgeDstThreshold;


    private void Start()
    {
        anim = GetComponent<Animator>();            // get the animator component attached to enemy
        chase = GetComponent<Chase>();              //refernece to  chase script
        combat = GetComponent<CharacterCombat>();   // reference to Enemy Attacking Player
        patrol = GetComponent<StaffPatrol>();            // reference to the patrol script
        staffAIController = GetComponent<StaffAIController>();


        // anim.SetBool("isAwake", false);             //Animators            
        //anim.SetBool("isIdle", false);              //Animators
        anim.SetBool("isWalking", false);           //Animators
        anim.SetBool("isHit", false);               //Animators
                                                    //anim.SetInteger("atk", 0);

        checkTheCostume = false;


        // StartCoroutine("LookingOutForTargets", .2f); //start looking for targets


        equipmentManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EquipmentManager>();

        //  attackRange = chase.attackRange;

        //Mesh View
        viewMesh = new Mesh();
        viewMesh.name = "ViewMesh";
        viewMeshFilter.mesh = viewMesh;
    }

    private void Update()
    {

        FindVisiableTargets();

    }

    private void LateUpdate()
    {
        DrawFieldofView();
    }

    //Looking for targets
    public void FindVisiableTargets()
    {
        PlayerinFOV = false;        //Reset the FOV as we are looking for the player
        GuardinFOV = false;         //Reset the FOV as we are looking for the guard
        SeenCrime = false;          //Reset the crime 

        visibleTargets.Clear();     //Reset the visiable targets as we are looking for the player
                                    /////visibleTargetWaypoint.Clear();


        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask); // get an array of all the colliders in FOV
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) //check if target falls in view angle
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); // if there is an obsticle between outselves and target
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) // if we dont collider with anything
                {
                    checkCostume();                 //continuiusly check the costume while in sight.

                    playerFOV = target.gameObject.GetComponent<FieldOfView>();
                    newMurphyMovement = target.gameObject.GetComponent<NewMurphyMovement>();

                    if (fooledByCostume == false)        //If we are not fooled by the costume
                    {
                        if (playerFOV != false)
                        {
                            if(target.transform.tag == "Player")
                            {
                                if (playerFOV.isSitting == false || newMurphyMovement.IsHidden == false)
                                {

                                    // checkState = true;
                                    PlayerinFOV = true;             //Player is in FOV
                                                                    //CheckState();                    //When player enters FOV Check the enemy's current state
                                    patrol.isOnPatrol = false;      //Pause the patrol.
                                                                    //Last Known Location List
                                                                    //LastKnownLocation.Add(target.transform.position);
                                                                    //LastKnownLocationVector3 = target.transform.position;

                                    // patrol.LastKnownGuardLOC.Add(target.transform.position);
                                    // enemyAIController.AddTempWaypoint = true; // start adding waypoints
                                    LastKnownMurphyLocation.Add(target.transform.position);
                                    int ListNumber = LastKnownMurphyLocation.Count;
                                    LastKnownMurphyLOC = LastKnownMurphyLocation[ListNumber - 1];
                                    LastKnownMurphyLocation.Clear();

                                }
                            }
                            

                        }


                    }


                    if (target.gameObject.tag == "Enemy")
                    {
                        GuardinFOV = true;             //Guardis in FOV
                        print("STAFF SEES GUARD");
                     //   patrol.LastKnownGuardLOC.Add(target.transform.position);
                        LastKnownGuardLOCList.Add(target.transform.position);
                        guard = target.gameObject;
                        guardAI = target.gameObject.GetComponent<GuardAI>();
                       // patrol.guard = guard;

                    }

                    if (target.gameObject.tag == "Crime")
                    {
                        //Stops the victim seeing their own crime
                        if (IsBeingStealthKilled == false)
                        {
                            print("I Can See Crime");
                            SeenCrime = true;
                        }


                    }

                    visibleTargets.Add(target);



                    //Check to see if the object in the FOV is a 'Object'
                    while (target.gameObject.tag == "Distraction")

                    {

                        print("I can see distraction");
                        distractableObject = target.gameObject.GetComponent<DistractableOBJ>();
                        patrol.distraction = target.gameObject;

                        if (staffAIController.CantSeeDistraction == false) //if the time runs down. The object in FOV becomes blank
                        {
                            Distraction = true;
                            //patrol.LastKnownMurphyLOC.Add(target.transform.position);                                 //THIS HAS BEEN REMOVED
                        }

                        if (staffAIController.CantSeeDistraction == true) //if the time runs down. The object in FOV becomes blank
                        {
                            distractableObject.TurnUndistractable = true;
                            Distraction = false;
                            staffAIController.CantSeeDistraction = false;
                        }

                        break;

                    }

                }

            }
        }


        inPeriphVision = false;

        //AUTO ATTACK PERIFERAL VISION
        Collider[] targetsInImmiadiateViewRadius = Physics.OverlapSphere(transform.position, alertRange, targetMask); // get an array of all the colliders in FOV
        for (int i = 0; i < targetsInImmiadiateViewRadius.Length; i++)
        {
            Transform target = targetsInImmiadiateViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < alertViewAngle / 2) //check if target falls in view angle
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); // if there is an obsticle between outselves and target
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) // if we dont collider with anything
                {

                    playerFOV = target.gameObject.GetComponent<FieldOfView>();



                    if (playerFOV != false)
                    {
                        if (playerFOV.isSitting == false)
                        {
                            //Player is in the periferal vision so Its ok to Auto Attack
                            inPeriphVision = true;
                        }

                    }




                }

            }
        }


    }




    public void checkCostume()
    {
        checkTheCostume = true;

        if (checkTheCostume == true)
        {
            //Check the equipemnt manager for any equipment that may blind enemy to player
            if (equipmentManager.currentEquipment.Length > 1)
            {
                foreach (Equipment equipment in equipmentManager.currentEquipment)
                {
                    if (equipment != null)
                    {


                        // generate a random number between 1-10, take away 'stealth' bonus, if number above e.g. 5 then Not Fooled by costume.
                        float StealthCheck = Random.Range(0.0f, 10.0f);
                        StealthCheck -= equipment.stealth;
                      //  print("Stealth" + StealthCheck);


                        if (equipment.IFoolTheStaff == true && Alarm.Instance.tempStaffCostumeRemoved!= true)
                        {
                            fooledByCostume = true;
                            Debug.Log("IM A BLIND STAFF");
                            checkTheCostume = false;
                        }

                        else
                        {
                            fooledByCostume = false;
                            Debug.Log("I can see you ");
                            checkTheCostume = false;
                            break;
                        }
                        break;
                    }

                    if (equipment == null)
                    {
                        fooledByCostume = false;
                        break;
                    }


                    break;
                }

            }
        }


    }



    void DrawFieldofView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;

            //  Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * viewRadius, Color.red);
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }

            }


            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;

        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }


        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();

    }


    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }



    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }



    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) //if not global angle
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        //converting the agles to unity angles
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
