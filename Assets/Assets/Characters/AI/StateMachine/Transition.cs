using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition 
{

    public Decision decision; //Decision evaluating
    public State trueState;  //Final Date
    public State falseState; // if decision returns false

}
