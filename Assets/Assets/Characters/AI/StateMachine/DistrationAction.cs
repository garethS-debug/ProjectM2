using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Distracted")]


public class DistrationAction : Action
{
    public override void Act(StateController controller)
    {
        Distraction(controller);
    }
    private void Distraction(StateController controller)
    {

        controller.isWalkingToTheDistraction(controller.enemyFOV.distrctionOBJ);

    }

}
