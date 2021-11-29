using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Player In Police FOV")]

public class PoliceFOVDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller); //Returns the result of the look function
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        if (controller.policeFOV.PlayerinFOV == true )
        {
            // If player in FOV
            Debug.Log("PLayerInPoliceFOV");
            return true;
        }
        else
        {
            //  Debug.Log("PLayerNOTInFOV");
            return false;
        }
    }
}
