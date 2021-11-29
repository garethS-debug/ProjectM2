using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Being Stealthed")]

public class isBeingStealthed : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = stealthed(controller); //Returns the result of the look function
        return targetVisible;
    }

    private bool stealthed(StateController controller)
    {
        //float distance = Vector3.Distance(controller.transform.position, controller.enemyFOV.distrctionOBJ.transform.position);

        if (controller.isBeingKilled == true)
        {

            return true;
        }
        else
        {
            return false;
        }

    }
}
