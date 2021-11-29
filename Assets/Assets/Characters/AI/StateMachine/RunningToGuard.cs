using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Running To Authority")]



public class RunningToGuard : Action
{
    public override void Act(StateController controller)
    {
        RunToAuthority(controller);
    }

    private void RunToAuthority(StateController controller)
    {

        controller.RunningToAuthority(); 



    }
}
