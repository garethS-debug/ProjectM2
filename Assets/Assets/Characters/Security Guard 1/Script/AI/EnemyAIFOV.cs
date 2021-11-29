using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *
 * This script will determine the state 'eyes' of the enemy ,   WHAT CAN WE SEE
 *
 *
 *
 * 
 * */

public class EnemyAIFOV : MonoBehaviour
{

    [Header("Visiable Targets")]
    [Tooltip("List of visiable targets")]
    public List<Transform> visibleTargets = new List<Transform>();//list of visiable targets
    public float viewRadius;                                        //viewing radius of player
    public LayerMask targetMask;                                    //layer mask for target
    public LayerMask obstacleMask;                                  //Layer ask for obsticle
    [Range(0, 360)]                                                 //clampting to 360  
    public float viewAngle;                                         //viewing angle

    [Header("Bools")]
    [Tooltip("Override to control the character")]
    public bool PlayerinFOV = false;
    public bool checkTheCostume;

    [Header("Costumes")]
    [Tooltip("WHO IS SHE. Used to determine what costumes fool this character")]
    public string WhoAmI;
    [Tooltip("Is this enemy fooled by this")]
    public bool fooledByCostume;

    [Header("CheckingState")]
    public bool checkState;

    [Header("Patrol")]
    public EnemyAIPatrol enemyAIPatrol;
  



    private EquipmentManager equipmentManager;

    void Start()
    {
        //Get the equipment Manager
        equipmentManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EquipmentManager>();
        enemyAIPatrol = this.gameObject.GetComponent<EnemyAIPatrol>();
    }


    void Update()
    {
        FindVisiableTargets();      //Looking for visable targets
    }


    //Looking for targets
    void FindVisiableTargets()
    {
        PlayerinFOV = false;        //Reset the FOV as we are looking for the player
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

                    if (fooledByCostume == false)        //If WE ARE NOT fooled by the costume
                    {
                        checkState = true;              //When player enters FOV (and is not fooled by the costume) Check the enemy's current state
                        PlayerinFOV = true;             //Player is in FOV
                        enemyAIPatrol.isOnPatrol = false;      //Pause the patrol.
                    }


                    //While the player is in the FOV add their waypoints
                    visibleTargets.Add(target);
                    //Last Known Location List
                   enemyAIPatrol.LastKnownLocation.Add(target.transform.position);
                   enemyAIPatrol.LastKnownLocationVector3 = target.transform.position;

                    enemyAIPatrol.AddWayPoint = true;

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
                        if (equipment.WhoDoIFool.Contains(WhoAmI))
                        {
                            fooledByCostume = true;
                            Debug.Log("IM A BLIND SECURITY GUARD");
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
}
