using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/PlayerDeathDecision")]

public class PlayerDeathDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool isPlayerDead = playerDeath(controller); //Returns the result of the look function
        return isPlayerDead;
    }




    private bool playerDeath(StateController controller)
    {
        if (controller.enemyFOV.bestTarget.GetComponent<murphyPlayerController>().isDead)
        {
            Debug.Log("I can see you are dead  ".Bold().Color("white") + controller.enemyFOV.bestTarget.name) ;
            //--- > foreach loop of every human player in scene
          
            return true;
           
        }


        else
        {
            Debug.Log("player not dead ".Bold().Color("red") );
            return false;
        }

    }
}
