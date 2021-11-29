using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/ActiveState")]


public class InFOV : Decision
{
    //
    public override bool Decide(StateController controller)
    {
        //is player in FOV
        // bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;
        bool TargetIsActive = controller.enemyFOV.PlayerinFOV;
        return TargetIsActive;
    }
}
