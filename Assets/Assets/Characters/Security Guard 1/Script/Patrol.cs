using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;



    //public Transform[] Waypoints;
    private int randomSpot;
    private int currentControlPointIndex = 0;

    [Header("Bools")]
    //Bool
    public bool isOnPatrol;
    public bool pauseWalk;
    [Tooltip("Timer for spacing out temp waypoints")]
    public bool NowYouCanAddAWaypoint;
    public bool startWaypointSpacingTimer;


    [Header("Waypoints")]
    //Game Object
    public GameObject WayPointGO;
    //Waypoints
    public List<Transform> Waypoints = new List<Transform>();
   // public List<Transform> TempWaypoints = new List<Transform>();

    [Tooltip("Max amount of waypoints in the area")]
    public List<float> MaxWaypointCount = new List<float>();
    [Tooltip("Timer for spacing out temp waypoints")]
    public float TempWaypointTime;
    [Tooltip("Timer for spacing out temp waypoints")]
    private float StartTempWaypointTime;
    public Vector3 WaypointPOS;


    //Float
    private float ObsticeAvoidanceRadious;
    [SerializeField]
    private float WaypointWaitTime;
    private float startWaitTime;
    [SerializeField]
    private float PauseinWalkTime;
    private float StartPauseinWalkTime;

    [Header("Reference to Other Script")]
    //Reference to other scripts
    EnemyFOV enemyFOV;
    Chase chase;
    private Animator anim;

    public void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>(); //REference to the Navmesh Agent
        NowYouCanAddAWaypoint = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        ObsticeAvoidanceRadious = _navMeshAgent.radius; //telling the script the waypoint has been reached / = navmesh distance
        startWaitTime = WaypointWaitTime; //resetting countdown timer
        StartPauseinWalkTime = PauseinWalkTime; //resetting pause time
        randomSpot = Random.Range(0, Waypoints.Count);
        //Refernece to other scripts
        anim = GetComponent<Animator>(); // get the animator component attached to enemy
        enemyFOV = GetComponent<EnemyFOV>(); //refernece to other script
        chase = GetComponent<Chase>();//refernece to  chase script
        StartTempWaypointTime = TempWaypointTime;
    }

    void Update()
    {
        WaypointPOS = Waypoints[randomSpot].transform.position;

        //whislt 'on patrol' is active and 'Alert Status' is not active
        while (isOnPatrol == true  )
        {
            _navMeshAgent.SetDestination(Waypoints[randomSpot].transform.position);
            anim.SetBool("isLooking", false);
            chase.Walking = true;
            break;
        }

        while (startWaypointSpacingTimer == true)
        {
            TempWaypointTimer();
            break;

        }

        //else
        //{
        //    _navMeshAgent.enabled = true;
        //}


        //If the distance of the enemy on patrol is less than 0.5f then walk to next position
        if (Vector3.Distance(transform.position, Waypoints[randomSpot].position) <= ObsticeAvoidanceRadious)
        {
            if (WaypointWaitTime <= 0)                          //is the character's wait time at the patrol point finished
            {
                randomSpot = Random.Range(0, Waypoints.Count);
                WaypointWaitTime = startWaitTime;

            }

            else //wait time countdown timer.
            {
                WaypointWaitTime -= Time.deltaTime;
                chase.Walking = false;


            }

        }

        if (pauseWalk == true)
        {
            if (PauseinWalkTime <= 0) //is the character's wait time at the patrol point finished
            {
                pauseWalk = false;
                PauseinWalkTime = StartPauseinWalkTime;
                _navMeshAgent.isStopped = false;
                anim.SetBool("isAlerted", false);

            }

            else //wait time countdown timer.
            {
                PauseinWalkTime -= Time.deltaTime;
                _navMeshAgent.isStopped = true;
                chase.Walking = false;
                anim.SetBool("isAlerted", true);

            }

        }



    }

    //Timer for removing this Waypoint from the temp Waypoint list
    public void TempWaypointTimer()
    {

        if (TempWaypointTime <= 0) //is the character's wait time at the patrol point finished
        {
            TempWaypointTime = StartTempWaypointTime; // reset timer time
            NowYouCanAddAWaypoint = true;           //Allow plaing of waypoint
            startWaypointSpacingTimer = false;      //stop timer.
        }

        else //wait time countdown timer.
        {
            TempWaypointTime -= Time.deltaTime;

        }


    }

}
