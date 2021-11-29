using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Neutral")]

public class Neutral : Action
{
    //* This is a Neutral action.

    public override void Act(StateController controller)
    {
        CooldownAction(controller);
    }
    private void CooldownAction(StateController controller)
    {
        if (controller._navMeshAgent != false || controller.isBeingKilled != true)
        {
            controller.pauseInWalk(); // Pause Walk.
                                      //   controller.ResetPath(); //Reset thenavmesh
        }

        if (controller.gameObject.tag == "Civilian")
        {
            controller.anim.SetBool("isScared", false);
        }


    }
}
