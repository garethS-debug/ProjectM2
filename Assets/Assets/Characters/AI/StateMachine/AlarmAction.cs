using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Alarm Action")]
public class AlarmAction : Action
{
    public override void Act(StateController controller)
    {
        Alarm(controller);
    }

    private void Alarm(StateController controller)
    {


        // controller._navMeshAgent.SetDestination(LastKnownLocation);

        controller.IsRunningToTheShouting(controller.whoCalled);


    }
}
