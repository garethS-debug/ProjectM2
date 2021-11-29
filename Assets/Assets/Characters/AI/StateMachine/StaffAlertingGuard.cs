using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Staff Alerting Guard - Proximity")]


public class StaffAlertingGuard : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Proximity(controller); //Returns the result of the look function
        return targetVisible;
    }

    private bool Proximity(StateController controller)
    {
        //float distance = Vector3.Distance(controller.transform.position, controller.enemyFOV.distrctionOBJ.transform.position);

        if (controller.staffAlertingGuard == true)
        {
            Debug.Log("Staff has alerted the guard");
            return true;
        }


        else
        {
            return false;
        }

    }
}
