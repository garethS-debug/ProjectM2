using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Distraction Decision")]

public class DistractionDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool distractionVisiable = Distracted(controller); //Returns the result of the look function
        return distractionVisiable;
    }

    private bool Distracted(StateController controller)
    {


        if (controller.enemyFOV.Distraction == true)
        {
            // If player in FOV
            Debug.Log("DISTRATION");
            return true;

        }
        else
        {
          //  Debug.Log("PLayerNOTInFOV");
            return false;       
        }


    }
}
