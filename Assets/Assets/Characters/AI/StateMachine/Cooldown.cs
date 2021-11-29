using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Cooldown")]

public class Cooldown : Action
{

    //* This is a cooldown in between states. Here we can reset the navmesh and play unique animations//

    public override void Act(StateController controller)
    {
        CooldownAction(controller);
    }
    private void CooldownAction(StateController controller)
    {
        controller.pauseInWalk(); // Pause Walk.
        controller.ResetPath(); //Reset thenavmesh


    }
}
