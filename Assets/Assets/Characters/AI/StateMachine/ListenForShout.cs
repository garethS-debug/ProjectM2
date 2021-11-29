using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Listen For Shout")]

public class ListenForShout : Decision
{
    public override bool Decide(StateController controller)
    {
        bool someoneIsCalling = WhoIsCalling(controller); //Returns the result of the look function
        return someoneIsCalling;
    }

    private bool WhoIsCalling(StateController controller)
    {
        //float distance = Vector3.Distance(controller.transform.position, controller.enemyFOV.distrctionOBJ.transform.position);

        if (controller.someoneIsCalling)
        {

            return true;
        }
        else
        {
            return false;
        }


    }
}
