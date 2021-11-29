using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "State", menuName = "StateMachine/State")]


public class State : ScriptableObject   
{
    public Action[] actions;
    public Transition[] transitions;
    public Sprite stateMarker;//image on canvas
    public Color sceneGizmoColor = Color.gray;

    [TextArea]
    public string Notes = "Comment Here."; // Do not place your note/comment here. 
                                           // Enter your note in the Unity Editor.


    public void UpdateState(StateController controller)
    {
        //evaluate each of the actions
        DoActions(controller);

        //evaluate each of the transitions
        CheckTransitions(controller);
     
            //Marker Image
          //  MarkerImage(controller);
    
    }

    //private void MarkerImage(StateController controller)
    //{
       
    //        controller.StateMarkerImage.sprite = stateMarker;
        

    //}

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller); // Loop through actions
           // Debug.Log(actions[i].name);
        }


    }

    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller); //Loop over transitions and evaluate each decision store bool in decision sucessed

            if (decisionSucceeded)                                                  //If State controller gives yes, then transition to next state 
            {
                controller.TransitionToState(transitions[i].trueState);             // If we hit a valid decision, transition to the next state
                break;
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);            // If we DONT hit a valid decision, transition to a false state
                
            }
            
        }
    }
}
