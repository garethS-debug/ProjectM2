using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Attach this to the player to define when 
 the player interacts with an object. 


 */

public class PlayerInteract : MonoBehaviour {

    public Interacting focus;

    private void Start()
    {
     
    }

    private void OnTriggerEnter(Collider col)
    {
        //collider = Colider of the game object
        if (col.gameObject.tag == "Interactable")
        {
            GetComponent<Interacting>();
           
        }
    }
    //Collision Detection function

    void SetFocus(Interacting newFocus)
    {
        focus = newFocus;
    }
}
