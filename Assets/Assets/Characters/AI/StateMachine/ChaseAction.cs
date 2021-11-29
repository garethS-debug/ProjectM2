using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Chase")]

public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase (StateController controller)
    {


        // controller._navMeshAgent.SetDestination(LastKnownLocation);

     controller.RunningToLastKnownPlayerLOC();


    }
}
