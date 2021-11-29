using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWaypointToList : MonoBehaviour
{

    public List<Transform> visibleWaypointTargets = new List<Transform>();//list of visiable targets
    [Range(0, 360)]
    public float viewRadius;    //viewing radius of player
    [Range(0, 360)]              //clampting to 360
    public float viewAngle;     //viewing angle
    //Layer Mask
    public LayerMask targetMask;    //layer mask for target
    public LayerMask obstacleMask;  //Layer ask for obsticle

    //Bool
    public bool AlertStatus;
    public bool TurnoffScript;
    public bool removeandDestroy;
   // public bool WaypointwithinView;

    //Reference to other script
    private Patrol Patrol;

    //int
    public float WaypointDestructionTime; // Timer for waypoint destruction
    private float StartingWaypointDestructionTime;
   

    // Start is called before the first frame update
    void Start()
    {
        StartingWaypointDestructionTime = WaypointDestructionTime;
       
        //WaypointwithinView = true;
    }

    // Update is called once per frame
    void Update()
    {
        FindVisiableTargets();
        // TurnOffSript();
        //if (WaypointwithinView == false)
        //{
        //    TempWaypointTimer();
        //}
    }

    void FindVisiableTargets()
    {
        print("Waypoiny is finding Targets");

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
                  
                   if (target.gameObject.tag == "Enemy")
                    {
                        AlertStatus = target.gameObject.GetComponent<EnemyFOV>().AlertStatusActive;
                        Timer();
                        Patrol = target.GetComponent<Patrol>(); // reference to the patrol script

                        if (target.gameObject.GetComponent<EnemyFOV>().AlertStatusActive == true && TurnoffScript == false)
                        {
                            //Test
                            //visibleWaypointTargets.Add(target); // Add the target's position to list
                            print("ADD THIS Transform POSITION TO YOUR LIST ");
                            target.gameObject.GetComponent<Patrol>().Waypoints.Add(this.transform);
                            TurnoffScript = true;
                            
                        }

                        if (removeandDestroy == true)
                        {
                            Patrol.MaxWaypointCount.Remove(1);
                            target.gameObject.GetComponent<Patrol>().Waypoints.Remove(this.transform);
                            Destroy(this.gameObject);
                        }


                        //ADD this waypoint's location to enemy transform List
                        //Delete this script / stop it producing more transforms (e.g. set bool to off)

                       // WaypointwithinView = true;

                    }

                    //else
                    // {
                    //     WaypointwithinView = false;
                    // }


                    //If ther is another waypoint around this one then add 1 to a 'temp' waypoint list
                    //

                    //if (target.gameObject.tag == "Waypoint")
                    //{

                    //    Patrol.TempWaypointCount.Add(1);
                    //    print("Waypoiny Found");
                    //    ////    removeandDestroy = true;
                    //}



                }


            }
        }

    }


    

    public void Timer()
    {
     
            if (WaypointDestructionTime <= 0) //is the character's wait time at the patrol point finished
            {
            removeandDestroy = true;
            }

            else //wait time countdown timer.
            {
                WaypointDestructionTime -= Time.deltaTime;

            }

        
    }

//    public void TurnOffSript()
//    {
//if (TurnoffScript == true)
//        {
//            this.gameObject.GetComponent<AddWaypointToList>().enabled = false;
//        }
//    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) //if not global angle
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        //converting the agles to unity angles
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
