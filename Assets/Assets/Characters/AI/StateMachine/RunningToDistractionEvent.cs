using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateMachine/Actions/Run To event")]

public class RunningToDistractionEvent : Action
{
    public override void Act(StateController controller)
    {
        DistractionEvent(controller);
    }
    private void DistractionEvent(StateController controller)
    {
        controller.isRunningToTheDistractionEvent(controller.distractionEventLocation);
    }
}
