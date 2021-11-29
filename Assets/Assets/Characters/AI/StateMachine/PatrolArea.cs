using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateMachine/Actions/Patrol Incident Area")]


public class PatrolArea : Action
{
    int randomSpot;


  

    public override void Act(StateController controller)
    {
        PatrolingArea(controller);
    }

 

    private void PatrolingArea(StateController controller)
    {
      
            controller.someoneIsCalling = false;
        
   
     //  randomSpot = Random.Range(0, controller.TempWaypoints.Count);

        // controller._navMeshAgent.SetDestination(LastKnownLocation);
        // controller.SearchingTheArea(controller.whoCalled);


        //Instantiate patrol points around incident area 
        if (controller.tempWaypoint != null)
        {

            ////Spawning Temp Waypoints
            if (controller.TempWaypoints.Count >= controller.maxWaypoint)
            {

            }

            if (controller.TempWaypoints.Count <= controller.maxWaypoint)
            {
                for (int x = 0; x <= controller.maxWaypoint; x++)
                {
                    GameObject SpawnWaypointPrefabs = Instantiate(controller.tempWaypoint, controller.whoCalled + Vector3.right * Random.Range(-6, 6), Quaternion.identity);
                    controller.TempWaypoints.Add(SpawnWaypointPrefabs);
                    randomSpot = Random.Range(0, controller.TempWaypoints.Count);
                }
            }
        }
        //set to patrol around there

        if (Vector3.Distance(controller.transform.position, controller.TempWaypoints[randomSpot].transform.position) < 3.0f)
        {
            controller.pauseInWalk();
            //is it time for the enemy to move to the next patrol point 
            if (controller.waitTime <= 0)
            {
                //change the random location
                randomSpot = Random.Range(0, controller.TempWaypoints.Count);
                controller.waitTime = controller.StartWaitTime;
            }
            else
            {
                controller.waitTime -= Time.deltaTime;
            }
        }

        if (Vector3.Distance(controller.transform.position, controller.TempWaypoints[randomSpot].transform.position) > 1.0f)
        {
            controller.walking();
            controller._navMeshAgent.SetDestination(controller.TempWaypoints[randomSpot].transform.position);
            Debug.Log("Walking to Patrol Point");
        }




        //on exit state remove petrol points






    }


}
