using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogInteraction : Interacting {

    public static DialogInteraction dialogInteraction;

    //CHANGE THE BELOW CODE FROM ITEM INTERACTION TO PLAYERINTERACTION 

    

    private ItemBluePrint itemProperties;
    private ItemPickup item;   // Item to put in the inventory if picked up
    public KeyCode DialogueInput = KeyCode.P;

    //Dialog Manage Bool Signal
    public static bool DialogBoxisOpen;

    #region ItemUI
    [SerializeField]
    private Image Dialog_Panel;

    private void Start()
    {
        Dialog_Panel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (NPC_FOV.DialogReady == true && Input.GetKeyDown(DialogueInput))// CHANGE THE PICKUP BUTTON HERE
        {
            StartDialog();
            Debug.Log("Starting Dialog");
        }

        if (NPC_FOV.DialogReady == false || DialogManager.DialogManagerSaysClose == true)
        {

            Dialog_Panel.gameObject.SetActive(false); //set panel to close
            DialogBoxisOpen = false;
        }

      
        if (NPC_FOV.DialogReady == true)
        {

            Dialog_Panel.gameObject.SetActive(true);//set panel to open
                                                    //Debug.Log("dialog started");
            DialogBoxisOpen = true;
        }


        else

        { 
        Dialog_Panel.gameObject.SetActive(false); //set panel to close
            //Debug.Log("dialog closed");
        }
       
        while (DialogManager.DialogManagerSaysClose == true)
        {
            Debug.Log("DialogManagerSaysClose while loop still rnning");
            Dialog_Panel.gameObject.SetActive(false); //set panel to close
            DialogManager.DialogManagerSaysClose = false;
            break;
        }


        if (DialogManager.ResetDialogBool == true)
        {
            Debug.Log("ResetDialogBool while loop still rnning");
            Dialog_Panel.gameObject.SetActive(false); //set panel to close

        }
              

    }
    #endregion

    /*

    #region Colliders Detecting Player 
    //Collision Detection function
    //---------------------->
    private void OnTriggerEnter(Collider collider)
    {
        //collider = Colider of the game object
        if (collider.gameObject.tag == "Player")
        {
           
            //DialogUpAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //collider = Colider of the game object
        if (collider.gameObject.tag == "Player")
        {
            Dialog_Panel.gameObject.SetActive(false);
            //DialogUpAllowed = false;
        }
    }
    //Collision Detection function
    #endregion
    */

    public override void Interact()
    {
        base.Interact();
        StartDialog();
    }


    void StartDialog()
    {

        //Debug.Log("Picking up item." + itemProperties.name);
        Dialog_Panel.gameObject.SetActive(true);
        //bool wasPickedUp = Inventory.instance.Add(itemProperties);
        DialogBoxisOpen = true;
    }

    public void CloseDialog()
    {

        //Debug.Log("Picking up item." + itemProperties.name);
        Dialog_Panel.gameObject.SetActive(false);
        //bool wasPickedUp = Inventory.instance.Add(itemProperties);
    }



}
