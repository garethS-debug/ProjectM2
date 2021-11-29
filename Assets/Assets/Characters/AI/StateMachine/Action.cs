using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    //used as a base class for actions we will create
    public abstract void Act(StateController controller);
}
