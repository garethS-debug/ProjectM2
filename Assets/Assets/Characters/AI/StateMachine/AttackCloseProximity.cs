using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Attack Close Proximity")]

public class AttackCloseProximity : Decision
{



    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller); //Returns the result of the look function
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        if (controller.playerWithinAttackrange == true)
        {
            //--- > foreach loop of every human player in scene
            if (controller.enemyFOV.playerPOS <= controller.enemyFOV.attackRange)


            {
             //   Debug.Log("player IN close proximity ".Bold().Color("green") + "attack range " + controller.enemyFOV.attackRange + "Player POS " + controller.enemyFOV.playerPOS);
                // If player in close proximity
                controller.pauseInWalk();
                return true;
            }




            if (controller.enemyFOV.playerPOS >= controller.enemyFOV.attackRange)
            {
             //   Debug.Log("player not close proximity ".Bold().Color("red") + "attack range " + controller.enemyFOV.attackRange + "Player POS " + controller.enemyFOV.playerPOS);
                //  Debug.Log("player POS : " + controller.enemyFOV.playerPOS);
                //   Debug.Log("Attack Range : " + controller.enemyFOV.attackRange);
                // Debug.Log("PLayerNOTInAttackRange");
                return false;
            }

            else
            {
                return false;
            }
        }
  

         else
        {
            return false;
        }

    }

    
}
