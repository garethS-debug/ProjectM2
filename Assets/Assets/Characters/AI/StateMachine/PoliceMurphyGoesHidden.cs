using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Murphy goes hidden")]

public class PoliceMurphyGoesHidden : Decision
{
    public override bool Decide(StateController controller)
    {
        bool someoneIsCalling = MurphyisHidden(controller); //Returns the result of the look function
        return someoneIsCalling;
    }

    private bool MurphyisHidden(StateController controller)
    {
        //float distance = Vector3.Distance(controller.transform.position, controller.enemyFOV.distrctionOBJ.transform.position);

        if (controller.policeFOV.PlayerinFOV == true)
        { 
            if (MurphyPlayerManager.instance.player.GetComponent<NewMurphyMovement>().IsHidden == true)
            {
                // If player in FOV
                Debug.Log("POLICE: PLayerIsGoingHidden");
                return true;

            }
            else
            {
                return false;
            }


        }
        else
        {
            //  Debug.Log("PLayerNOTInFOV");
            return false;
        }
    }
}
