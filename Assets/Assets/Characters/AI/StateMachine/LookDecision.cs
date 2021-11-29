using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Player In GuardFOV")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller); //Returns the result of the look function
        return targetVisible;
    }

    private bool Look(StateController controller)
    {


        if (controller.enemyFOV.PlayerinFOV == true   )
        {
            // If player in FOV
            Debug.Log("PLayerInFOV");
            return true;
            
        }

    
        else
        {
          //  Debug.Log("PLayerNOTInFOV");
            return false;
        }


    }
}


