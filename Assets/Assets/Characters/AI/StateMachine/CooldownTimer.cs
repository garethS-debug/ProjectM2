using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decisions/Timer")]

public class CooldownTimer : Decision
{

    public float CooldownwaitTime;
    public float StartCooldowWaitTime;

    
    public void OnEnable ()
    {
        if (CooldownwaitTime < StartCooldowWaitTime)
        {
            CooldownwaitTime = StartCooldowWaitTime;
        }

    }

    public override bool Decide(StateController controller)
    {
        bool timerAt0 = CountDownTimer(controller); //Returns the result of the timer function
        return timerAt0;
    }

    private bool CountDownTimer(StateController controller)
    {

        Timer();


        //was      if (controller.waitTime <= 0.1f && controller.enemyFOV.PlayerinFOV == false)
        if (CooldownwaitTime <= 0.1f)
        {
            return true;
        }
        else
        {
            
            return false;
        }


    }

    public void Timer()
    {

        if (CooldownwaitTime <= 0.1f)
        {
            CooldownwaitTime = StartCooldowWaitTime;
        }
        else
        {

            //Timer ticks down
            CooldownwaitTime -= Time.deltaTime;

        }
    }


}
