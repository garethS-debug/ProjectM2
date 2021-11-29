using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentManager : MonoBehaviour
{


    #region Singleton
    public static EquipmentManager instance;
    //public GameObject Item2Reveal;
   // List<GameObject> Item2Reveal;

    // public static Equipment ItemToReveal;


    void Awake ()
    {
        //Item2Reveal = new List<GameObject>();
        instance = this;
    }
    #endregion

    [Header("Equipment Mesh")]
    public Equipment[] defaultItems;//default items
    public SkinnedMeshRenderer targetMesh;
    public Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;//skinned mesh renderer array 

    [Header("Equipment UI")]
    //EquipmentUI
    public EquipedItemsUI[] EquipedItemUIContainer;
    [Header("Equipment Category Images")]
    public Sprite itemCatArmor;
    public Sprite itemCatDamage;
    public Sprite itemCatStealth;
    public Sprite itemCatLockPick;
    public Sprite itemCatSpeed;
    public Sprite itemCatWealth;

   

    [Header("Costume UI")]
    //Costumer UI Container
    public ObjectiveBar[] CostumeUIContainer;

    // Callback for when an item is equipped/unequipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
                                // Initialize currentEquipment based on number of equipment slots
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //use this number to initialise array 
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

      
        EquipDefaultItems();//equip default items
    }

    //-------> EQUIP
    public void Equip (Equipment newItem)
    {

      // // ItemToReveal = newItem;
      ////if (newItem.name == Item2Reveal.name)
        ////{
        ////    Debug.Log("SUCCESS, ITEM ATTACHED = " + newItem.name);
        ////}

        //-------MAKE THIS MORE GENERIC
        if (newItem.isEquipableItem == true)
        {
            newItem.mesh.gameObject.SetActive(true);
            print(newItem.mesh.gameObject.name);
            //RevealItem.RevealTheItem = true;
            Debug.Log("SUCCESS, ITEM ATTACHED = " + newItem.name);

            
            //Update Equiped Item UI
            if(newItem.isCostume != true)
            {
                UpdateQuickMenuUI(newItem, (int)newItem.equipSlot);

                
            }
           
        }
        //-------MAKE THIS MORE GENERIC


        Debug.Log("ITEM ATTACHED = " + newItem.name);
        int slotIndex = (int)newItem.equipSlot;
        Unequip(slotIndex);                         //unquip old item
        Equipment oldItem = Unequip(slotIndex);     //old item




        /*
        if (currentEquipment[slotIndex] != null) // if something is already there
        {
            oldItem = currentEquipment[slotIndex]; //add item back into inventory
            inventory.Add(oldItem);

        }
        */       
        // An item has been equipped so we trigger the callback
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;//instantiate object
        newMesh.bones = targetMesh.bones;//deform based on bones
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
        //Update UI
      //  EquipmentUIContainer[slotIndex] = newItem.icon;
    }



    //-------> UNEQUIP 
    public Equipment Unequip (int slotIndex)
    {
       

        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null) 
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }//check that the current mesh at slot index isnt null
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

          

            return oldItem;

            

        }
        return null;
    }



    //-------> UNEQUIP ALL
    public void UnequipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        ClearAllQuickMenuUI();  //Clear the equiped items UI
        EquipDefaultItems(); // if all items are unequiped, the default items are placed there. 
       
        if (ObjectiveBar.Instance == true)
        {
            if (currentEquipment.Length >= 1)
            {
                ObjectiveBar.Instance.UnequipAllUI();
            }
         
        }
    }

    void EquipDefaultItems() 
    { 
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }//equip default item 


    //UPDATING THE Equiped Item MENU CURRENT ITEM UI
    public void UpdateQuickMenuUI(Equipment NewItem, int ItemSlotIndex)
    {
     //   print("Slot Index" + ItemSlotIndex);
        // Loop through all the Image slots
        for (int i = 0; i < EquipedItemUIContainer.Length; i++)
        {
            EquipedItemUIContainer[i].enabled = true;
          //  EquipedItemUIContainer[i].gameObject.SetActive(true);

            if (EquipedItemUIContainer[i].isBoxEmpty == true) 
            {
               
                EquipedItemUIContainer[i].UpdateEquipedItemUI(NewItem);
                EquipedItemUIContainer[i].SlotIndexNumber = ItemSlotIndex;
               // EquipedItemUIContainer[i].SlotIndexNumber = 
                //print("This [i] is " + EquipedItemUIContainer[i].name);
                break;
            }
        }

    }

    //Clearing THE Equiped Item MENU CURRENT ITEM UI
    public void ClearAllQuickMenuUI()
    {
        // Loop through all the Image slots
        for (int i = 0; i < EquipedItemUIContainer.Length; i++)
        {
            if (EquipedItemUIContainer[i].isBoxEmpty == false)
            {
                EquipedItemUIContainer[i].ClearEquipedItemUI();
           
            }
        }
    }






    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }
}
