using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Listen For Distraction Event")]

public class DistractionEventDecision : Decision
{
    // Start is called before the first frame update
    public override bool Decide(StateController controller)
    {
        bool eventIsCalling = IsEventCalling(controller); //Returns the result of the look function
        return eventIsCalling;
    }

    private bool IsEventCalling(StateController controller)
    {
        //float distance = Vector3.Distance(controller.transform.position, controller.enemyFOV.distrctionOBJ.transform.position);

        if (controller.distractionEventActive == true)
        {

            return true;
        }
        else
        {
            return false;
        }


    }
}
