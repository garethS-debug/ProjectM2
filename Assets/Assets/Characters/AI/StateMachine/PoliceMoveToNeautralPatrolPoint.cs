using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/MoveToNeutralPatrolPoint")]

public class PoliceMoveToNeautralPatrolPoint : Action
{
   // int randomSpot;




    public override void Act(StateController controller)
    {
        MoveToNeautralPOS(controller);
    }



    private void MoveToNeautralPOS(StateController controller)
    {

        if (controller.moveLocations != null)
        {


            //Find the closest move location
            Transform GetClosestEnemy(List<Transform> enemies)
            {
                Transform bestTarget = null;
                float closestDistanceSqr = Mathf.Infinity;
                Vector3 currentPosition = controller.transform.position;
                foreach (Transform potentialTarget in enemies)
                {
                    Vector3 directionToTarget = potentialTarget.position - currentPosition;
                    float dSqrToTarget = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < closestDistanceSqr)
                    {
                        closestDistanceSqr = dSqrToTarget;
                        bestTarget = potentialTarget;
                    }
                }

                return bestTarget;
            }

            //  Debug.Log(GetClosestEnemy(moveLocations));






            //set to patrol around there

            if (Vector3.Distance(controller.transform.position, GetClosestEnemy(controller.moveLocations).transform.position) < 1.0f)
            {
                controller.WithinProximity = true;
                controller.pauseInWalk();
                //is it time for the enemy to move to the next patrol point 
               
            }

            if (Vector3.Distance(controller.transform.position, GetClosestEnemy(controller.moveLocations).transform.position) > 1.0f)
            {
                controller.walking();
                controller._navMeshAgent.SetDestination(GetClosestEnemy(controller.moveLocations).transform.position);
                Debug.Log("Walking to Neautral Point");
            }




            //on exit state remove petrol points


        }



    }
}
