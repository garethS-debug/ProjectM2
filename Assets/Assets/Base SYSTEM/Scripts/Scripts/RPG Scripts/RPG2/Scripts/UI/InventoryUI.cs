using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This object updates the inventory UI.
*/

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;   // The parent object of all the items
    public GameObject inventoryUI;  // The entire UI
    public KeyCode InventoryButton = KeyCode.I;

    [Header("Check for Hidden")]
    private bool ShowingInventory;
    public static bool closeOtherUI;
    public NewMurphyMovement murphyMovement;

    [HideInInspector]
    public GameObject player;

    Inventory inventory;    // Our current inventory

    Inventoryslot[] slots;  // List of all the slots

    public static InventoryUI instance;

    // public GameObject ShowScrollOBJ;

    void Awake()
    {
 

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
        }


        instance = this; // creating a static variable.
      
  


    }



    void Start()
    {
        if (SceneSettings.Instance.humanPlayer != null)
        {
            player = SceneSettings.Instance.humanPlayer;
            murphyMovement = player.GetComponent<NewMurphyMovement>();
        }


        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;    // Subscribe to the onItemChanged callback

        ShowingInventory = true;

        // Populate our slots array
        slots = itemsParent.GetComponentsInChildren<Inventoryslot>();
    }

    void Update()
    {

        // Check to see if we should open/close the inventory
        if (Input.GetKeyDown(InventoryButton) )
        { 
            if (murphyMovement == null)
            {
                 player = SceneSettings.Instance.humanPlayer;
                murphyMovement = player.GetComponentInChildren<NewMurphyMovement>();
            }

            if (murphyMovement.IsHidden == false)
            {

    
            ShowingInventory = !ShowingInventory;

            if (InformationToSave.instance.savedInformation.InventoryList != null)
            {
                InformationToSave.instance.LoadInventory();
                print("Checking Inventory");
            }



            //  InformationToSave.instance.LoadInventory();

            if (ShowingInventory == true)
            {
                //Change to the inventory screen
                ShowScroll.Instance.HideBars();
                closeOtherUI = false;
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                murphyMovement.isInInventory = false;

                
             

            }

            if (ShowingInventory == false)
            {
                closeOtherUI = true;
                StartCoroutine(ShowInventory());
                //Change to the inventory screen
                ShowScroll.Instance.ShowBars();
                murphyMovement.isInInventory = true;

            }

            }


        }





    }
    private IEnumerator ShowInventory()
    {
        if (murphyMovement.anim != null )
        {
        murphyMovement.anim.SetBool("isKneeling", true);
        }
        yield return new WaitForSeconds(2.2f);
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }


        // Update the inventory UI by:
        //      - Adding items
        //      - Clearing empty slots
        // This is called using a delegate on the Inventory.


    public void UpdateUI()
    {
        // Loop through all the slots
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)  // If there is an item to add
            {
                slots[i].AddItem(inventory.items[i]);   // Add it
            }
            else
            {
                // Otherwise clear the slot
                slots[i].ClearSlot();
            }
        }
    }
}