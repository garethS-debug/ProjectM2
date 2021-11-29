using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Listen For CCTV Shout")]


public class ListenForCCTVShout : Decision
{
    public override bool Decide(StateController controller)
    {
        bool someoneIsCalling = WhoIsCalling(controller); //Returns the result of the look function
        return someoneIsCalling;
    }

    private bool WhoIsCalling(StateController controller)
    {
        //float distance = Vector3.Distance(controller.transform.position, controller.enemyFOV.distrctionOBJ.transform.position);

        if (Alarm.Instance.PlayerinCCTVFOV == true)
        {

            return true;
        }
        else
        {
            return false;
        }


    }
}
