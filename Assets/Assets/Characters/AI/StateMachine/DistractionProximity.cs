using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Distraction Proximity")]



public class DistractionProximity : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller); //Returns the result of the look function
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        float distance = Vector3.Distance(controller.transform.position, controller.enemyFOV.distrctionOBJ.transform.position);

        Debug.Log("CanSeeDistraction" + distance);

        if (distance <= 2)
        {
            
 
            return true;
        }
        else
        {
            return false;
        }


    }
}
