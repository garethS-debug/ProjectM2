using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Police Run To Crime")]


public class PoliceRunToCrimeAction : Action
{
    public override void Act(StateController controller)
    {
        RunToCrime(controller);
    }

    private void RunToCrime(StateController controller)
    {


        // controller._navMeshAgent.SetDestination(LastKnownLocation);

        controller.IsRunningToTheShouting(controller.whoCalled);


    }
}
