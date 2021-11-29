using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Patrol")]

public class PatrolAction : Action
{




    public override void Act(StateController controller)
    {
        Patrol(controller);
    }       

    private void Patrol(StateController controller)
    {
        //controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        //controller.navMeshAgent.Resume();

        //if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        //{
        //    controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
        //}


        if (controller.TempWaypoints.Count > 0)
        {
            foreach (GameObject tempWaypoint in controller.TempWaypoints)
            {

               
                Destroy(tempWaypoint);
                controller.TempWaypoints.Remove(tempWaypoint);
            }
        }


        Debug.Log("Working Patrol");

        // waiting once we have reached a location close to the target position
        if (Vector3.Distance(controller.transform.position, controller.moveLocations[controller.randomSpot].position) < 1.0f)
        {
            controller.pauseInWalk();

            //is it time for the enemy to move to the next patrol point 
            if (controller.waitTime <= 0)
            {
                //change the random location
                controller.randomSpot = Random.Range(0, controller.moveLocations.Count);
                controller.waitTime = controller.StartWaitTime;

            }
            else
            {
                controller.waitTime -= Time.deltaTime;
                //ADD animation of looking around while waiting
            }
        }

        if (Vector3.Distance(controller.transform.position, controller.moveLocations[controller.randomSpot].position) > 1.0f)
        {
            controller.walking();
          
            controller._navMeshAgent.SetDestination(controller.moveLocations[controller.randomSpot].position);

            // transform.LookAt(moveLocations[randomSpot].position);
            // _navMeshAgent.destination = moveLocations[randomSpot].position;

            Debug.Log("Walking to Patrol Point");
        }



    }
}
