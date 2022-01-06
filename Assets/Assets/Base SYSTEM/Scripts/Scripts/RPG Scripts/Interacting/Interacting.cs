using UnityEngine;
using UnityEngine.AI;

/*  
    Attach to interactable. 
 
    This is a base class component that all objects that 
    the player can interact with such as enemies, items etc. 
    derives from. It is meant to be used as a base class.

    Attach this script to an interactable object. Then define
    where that object's interacttion point is and who the
    player is.

    The interactable target must have a box collider & Tag 
    'Interactable'   
*/

public class Interacting : MonoBehaviour {


    public float InteractRadius = 3f;               // How close do we need to be to interact?
    public Transform interactionTransform;  // The transform from where we interact in case you want to offset it
    bool isFocus = false;   // Is this interactable currently being focused?
    bool hasInteracted = false; // Have we already interacted with the object?
    [HideInInspector]
    public Transform player; // Reference to the player transform

    //public Interaction focus; // what is currently being focused 
    public bool interactingwithNPC = false; //BOOL FOR INTERACTION 
    public static bool CombatInteracting = false;//Enabling Interacting

    private void Start()
    {
        player = SceneSettings.Instance.humanPlayer.transform;
       
    }

    void Update()
    {
       
  
        if (CombatInteracting == true) // If we are close enough to the interactable (REPLACE WITH FOV)
        {
            Interact();// Interact with the object
            hasInteracted = true;
            //interactingwithNPC = true;
            Debug.Log("Interacting");
        }

    }

    public virtual void Interact()
    {
        // This method is meant to be overwritten
     // code is inserted here from other codes
    }


   
    #region Interaction Shere
    // Draw our interaction radius in the editor
    void OnDrawGizmosSelected()
    {
      
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(interactionTransform.position, InteractRadius);
    }
    #endregion



}
