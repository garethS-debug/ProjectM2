using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/TurnUndistractible")]

public class TurnUndistractibleAction : Action
{
    public override void Act(StateController controller)
    {
        TurnUnDistraction(controller);
    }
    private void TurnUnDistraction(StateController controller)
    {
        controller.enemyFOV.distractableObject.TurnUndistractable = true;
        controller.enemyFOV.Distraction = false;
        // controller.pauseInWalk();
        //  controller.isWalkingToTheDistraction(controller.enemyFOV.distrctionOBJ);
        controller.pauseInWalk();
    }
}
