using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIPatrol : MonoBehaviour
{
    //Waypoints
    [Header("Waypoints")]
    [Tooltip("Max amount of waypoints in the area")]
    public List<float> MaxWaypointCount = new List<float>();
    [Tooltip("List of waypoints")]
    public List<Transform> Waypoints = new List<Transform>();
    [Tooltip("Max amount of waypoints")]
    public float MaxWaypoints = 6;
    [Tooltip("Timer for spacing out temp waypoints")]
    public bool NowYouCanAddAWaypoint;
    public bool startWaypointSpacingTimer;
    public GameObject WaypointGORef;

    [Header("Waypoint Timer")]
    [Tooltip("Timer for spacing out temp waypoints")]
    public float TempWaypointTime;
    [Tooltip("Timer for spacing out temp waypoints")]
    private float StartTempWaypointTime;



    [Header("Patrol")]
    public bool isOnPatrol;
    public bool AddWayPoint;

    [Header("Last Known Location")]
    public List<Vector3> LastKnownLocation = new List<Vector3>();//list of last known Location
    public Vector3 LastKnownLocationVector3;

  

    // Start is called before the first frame update
    void Awake()
    {
        NowYouCanAddAWaypoint = true;
    }

    // Update is called once per frame
    void Update()
    {



        //WAYPOINT ADDING
        // visibleTargetWaypoint.Add(target.transform.position);
        //If there are less than the max Waypoints allowed
        if (MaxWaypointCount.Count < MaxWaypoints && NowYouCanAddAWaypoint == true && AddWayPoint == true)
        {
            //---- > Add waypoint 
            Instantiate(WaypointGORef, LastKnownLocationVector3, transform.rotation);
            //add 1 to the temporary waypoint count
            MaxWaypointCount.Add(1);
            //Waypoint spacing timer
            NowYouCanAddAWaypoint = false;
            startWaypointSpacingTimer = true;
        }

    }


                                                        //Timer for removing this Waypoint from the temp Waypoint list
    public void TempWaypointTimer()
    {

        if (TempWaypointTime <= 0)                      //is the character's wait time at the patrol point finished
        {
            TempWaypointTime = StartTempWaypointTime;   // reset timer time
            NowYouCanAddAWaypoint = true;               //Allow plaing of waypoint
            startWaypointSpacingTimer = false;          //stop timer.
        }

        else //wait time countdown timer.
        {
            TempWaypointTime -= Time.deltaTime;

        }


    }

}
