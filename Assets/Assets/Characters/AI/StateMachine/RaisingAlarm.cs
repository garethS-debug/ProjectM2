using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/RaisingAlarm&CallPolice")]

public class RaisingAlarm : Action
{
    // Start is called before the first frame update
    public override void Act(StateController controller)
    {
        RaiseAlarm(controller);
    }

    private void RaiseAlarm(StateController controller)
    {


        // controller._navMeshAgent.SetDestination(LastKnownLocation);

        controller.RaisingAlarm();


    }
}
