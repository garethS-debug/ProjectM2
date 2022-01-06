/*  
    This component is for creating all items the player can pick up 
*/

using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]

public class ItemBluePrint : ScriptableObject 
{
    new public string name = "New Item";  // Name of the item
    public Sprite icon = null;              // Item icon
    public bool isDefaultItem = false;      // Is the item default wear?

    [Header("ItemID")]
    public int ItemID;

    [Header("Questing")]
    [Tooltip("Check box if Quest and place Quest ID")]
    public bool isQuestItem = false;        // Is the item default wear?
    public int questID;                     //quest Object Must match name in Quest Manager
    public int AddXtoQuest;                 //adds number to quest goal
    public GameObject UIMarkerObjective;    //Update the quest marker with the objective this item belong to. 

    [Header("Item Properties")]
    public bool isVaultKey;

    [Tooltip("These Bools will determine whether certain conditions are present in the main heist")]
    public bool isFoodEvent;
    public bool isMechanicEvent;
    public bool isStaffRota;
    public bool isMainObjective;

    [Header("Consumable")]
    [Tooltip("Is the Item a consumable")]
    public bool consumableItem;
    public int consumeHealth;

    [Header("Respawn G/O")]
    [Tooltip("Item's Game Object that will respawn when x is pressed")]
    public GameObject ItemGameObj;          //Item's Game Object that will respawn when x is pressed

    [Header("Shop Item Properties")]
    [Tooltip("If this is a shop item and its properties")]
    public bool isShopItem;
    public float itemPrice;
    public string itemDescription;


    [Header("Cash Item Pickup")]
    [Tooltip("If this is a Cash Pickup")]
    public float itemValue;
    public bool isCashPickup;


    // public Text ItemPickUpText;
    public virtual void Use()
    {
        //empty
        Debug.Log("using" + name);

        if (consumableItem == true)
        {
          
            
            if (consumeHealth > 0.1f )
            {
              
                PlayerStats TempHealthLog = SceneSettings.Instance.humanPlayer.gameObject.GetComponent < PlayerStats>();

                if (TempHealthLog.currentHealth > 0 && TempHealthLog.currentHealth < TempHealthLog.maxHealth)
                {
                    TempHealthLog.currentHealth += consumeHealth;
                    Debug.Log("PLACE HEALTH SCRIPT HERE"); //CONSUME HEALTH
                }
                else
                {
                    Debug.Log("Already at max health"); //CONSUME HEALTH
                }
                
            }

            Inventory.instance.Remove(this);
        }

        if(isCashPickup == true)
        {
            Debug.Log("ADD CASH MACHINE DISTANCE CHECK"); //CONSUME HEALTH
           // Objective.instance.Cash += itemValue;
           if (Objective.instance != null)
            {
                Objective.instance.CashPrize(itemValue + MurphyPlayerManager.instance.PlayerStats.wealthMod.GetValue()); //Update Level Cash Value
                Debug.Log("CASH PICKED UP" + itemValue + MurphyPlayerManager.instance.PlayerStats.wealthMod.GetValue());
            }
            
            InformationToSave.instance.SaveGlobalScore(itemValue + MurphyPlayerManager.instance.PlayerStats.wealthMod.GetValue());//Update Global Cash Value

            Debug.Log("ADD CASH Effect"); //CONSUME HEALTH
            Inventory.instance.Remove(this);
        }
    }



    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public void RemovefromInventory ()
    {
        Inventory.instance.Remove(this);
    }
}
