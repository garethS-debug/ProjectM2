using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "StateMachine/Decisions/Police Close Proximity")]

public class PoliceProximity : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller); //Returns the result of the look function
        return targetVisible;
    }

    private bool Look(StateController controller)
    {

        if (controller.policeFOV.playerPOS <= 2)


        {

            // If player in close proximity
            controller.pauseInWalk();
            Debug.Log("POlice in Proximity");
            //    Debug.Log("PLayerInAttack Range");
            return true;
        }




        else
        {
            Debug.Log("POlice NOT in Proximity");
            //  Debug.Log("player POS : " + controller.enemyFOV.playerPOS);
            //   Debug.Log("Attack Range : " + controller.enemyFOV.attackRange);
            // Debug.Log("PLayerNOTInAttackRange");
            return false;
        }


    }
}
