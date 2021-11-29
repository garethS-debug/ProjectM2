using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Player In StaffFOV")]


public class StaffLookDecision : Decision
{
    // Start is called before the first frame update
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller); //Returns the result of the look function
        return targetVisible;
    }

    private bool Look(StateController controller)
    {


        if (controller.staffFOV.PlayerinFOV == true)
        {
            // If player in FOV
            Debug.Log("PLayerInStaffFOV");
            return true;

        }
        else
        {
            // Debug.Log("PLayerNOTInStaffFOV");
            return false;
        }

    }
}
