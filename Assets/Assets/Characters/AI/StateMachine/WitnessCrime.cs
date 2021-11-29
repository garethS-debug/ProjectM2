using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Witness Crime")]

public class WitnessCrime : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = WitnessingCrime(controller); //Returns the result of the look function
        return targetVisible;
    }

    private bool WitnessingCrime(StateController controller)
    {
        //float distance = Vector3.Distance(controller.transform.position, controller.enemyFOV.distrctionOBJ.transform.position);

        if (controller.witnessCrime == true)
        {
            Debug.Log("Civ: Hey I can see crime");
            return true;

        }
        else
        {
            return false;
        }

    }
}
