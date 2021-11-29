using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateMachine/Actions/RunToLastKnowLOC")]


public class RunToLastKnowLOC : Action
{
    // Start is called before the first frame update
    public override void Act(StateController controller)
    {
        RunToLOC(controller);
    }

    private void RunToLOC(StateController controller)
    {


        // controller._navMeshAgent.SetDestination(LastKnownLocation);

        controller.RunningToLastKnownPlayerLOC();


    }
}
