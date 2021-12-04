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

        //--- > foreach loop of every human player in scene
 

        if (controller.enemyFOV.playerPOS <= controller.enemyFOV.attackRange && controller.enemyFOV.inPeriphVision == true)


            {
            
            // If player in close proximity
            controller.pauseInWalk();
        //    Debug.Log("PLayerInAttack Range");
            return true;
        }


 
        
        else
        { 
          //  Debug.Log("player POS : " + controller.enemyFOV.playerPOS);
         //   Debug.Log("Attack Range : " + controller.enemyFOV.attackRange);
           // Debug.Log("PLayerNOTInAttackRange");
            return false;
        }


    }

    
}
