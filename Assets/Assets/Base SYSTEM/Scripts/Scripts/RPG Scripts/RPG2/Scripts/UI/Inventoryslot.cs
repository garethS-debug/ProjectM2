using UnityEngine;
using UnityEngine.UI;

public class Inventoryslot : MonoBehaviour
{

    //This is the script running the inventory that an item resides in. 

    public Image icon;          // Reference to the Icon image
    public Button removeButton; // Reference to the remove button

    public GameObject SpawnLoc; // Reference to the Main Character

    ItemBluePrint item;  // Current item in the slot
    ItemPickup2 itemPickup2; //Reference to ItemPickup2 Script

    public static GameObject StoredItem;//Item's Mesh

    [Header("SpawnDistance")]
    [Tooltip("How far away the object spawns away from the player?")]
    public float SpawnDistance = 1.0f;

    public void Start()
    {
      //  MainCharacter = GameObject.FindGameObjectWithTag("Player");
    }
    // Add item to the slot
    public void AddItem(ItemBluePrint newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;

        //check if quest Item 
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    // Called when the remove button is pressed
    public void OnRemoveButton()
    {
        //REWORK THIS CODE ---- > Instantiated object not the same size / rotation
        //Instantiate item back into the game
        Instantiate(item.ItemGameObj, SpawnLoc.transform.position + (transform.forward * -SpawnDistance), transform.rotation);

        Inventory.instance.Remove(item);

    }

    // Called when the item is pressed
    public void UseItem()
    {
        if (item != null)
        {
            item.Use();

          
                if (Objective.instance != null)
                {
                    //Update the UI With What item has been used
                    ObjectiveBar.Instance.checkCostume();
                }
               
            


        }
    }

}