using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // unity to serialise a custom class. Fields show up in ispector

public class Stat {

    [SerializeField]
    private int baseValue;

    //list of all the modifiers currently on this stat
    public List<int> modifiers = new List<int>();
   //public List<bool> stealthMods = new List<bool>();


    public int GetValue ()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue; // returns the base value 
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            Debug.Log("modifier != 0");
            modifiers.Add(modifier);
        }

        else if (modifier == 0)
        {
            Debug.Log("modifier = 0");
        }
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }
}
