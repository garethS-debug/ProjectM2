using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Attack")]

public class AttackAction : Action
{

    public override void Act(StateController controller)
    {
        Attack(controller);
    }
    private void Attack(StateController controller)
    {

       
            Debug.Log("Within Attack Range");
                controller.Attack();
           // }

        

    }

}
