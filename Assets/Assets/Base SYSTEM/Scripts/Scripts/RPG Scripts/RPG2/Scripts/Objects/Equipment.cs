using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]

public class Equipment : ItemBluePrint 
{
    [Header("Equipment Options")]
    [Tooltip("Reveal item when equiping?")]
    public bool isEquipableItem = true;



    [Header("Equipment Mesh")]
    [Tooltip("Reveal item MeshRenderer")]
   // public MeshFilter ItemMeshRend;
    public bool RenderMesh = false;

    public EquipmentSlot equipSlot;  // Slot to store equipment in

    [Header("Stats")]
    [Tooltip("Item Stats")]
    public int armorModifier;                     // Increase/decrease in armor
    public int damageModifier;                    // Increase/decrease in damage
    public int stealthModifier;
    public int lockpickingModifier;                    // Increasing/drecrease lockpicking skill
    public int speedModifier;                     // Increase / decrease Murphy's speed;
    public int wealthModifier;                    //Increase/decrease value of items around you

   

    [Header("Stealth Stats")]
    [Tooltip("Stats specfically effecting stealth % of the object")]
    public int stealth;                           //How much stealth does the object have


    [Header("Costume")]
    public bool isCostume;
    [Tooltip("what characters are fooled by this disguise?")]
    public string WhoDoIFool;      // Who does this effect
    public List<string> WhoDoIFools = new List<string>();
    public bool IFoolTheGuards;
    public bool IFoolTheCamera;
    public bool IFoolTheStaff;

    public static Equipment instance;


    [Header("Mesh")]
    [Tooltip("Tip: Update Murphy Blender file, Duplicate and break the Murphy pre-fab, drag the object (with skinned mesh render component) into the inspector as a prefab. Make sure there is skinned mesh render component on the prefab. Then just drag over the prefab.")]
    public SkinnedMeshRenderer mesh; // the objects mesh

    void Awake()
    {
       
            instance = this;
    }

    // When pressed in inventory
    public override void Use()
    {
        base.Use();

        //Enable Item Mesh
        //MeshRenderer m = ItemMeshRend.GetComponent<MeshRenderer>();
        //m.enabled = true;

        Debug.Log("Equipment.CS use");
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();                  // Remove it from inventory
    }

}


public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }