using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

    EquipmentManager equipmentManager;


	// Use this for initialization
	void Start () 
    {
        equipmentManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<EquipmentManager>();
		equipmentManager.onEquipmentChanged += OnEquipmentChanged;
    }

    //every time an item is added or removed this method gets called for adding the stats 
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            Debug.Log("Apply Modifier");
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            stealth.AddModifier(newItem.stealthModifier);
            lockPick.AddModifier(newItem.lockpickingModifier);
            speed.AddModifier(newItem.speedModifier);
            wealthMod.AddModifier(newItem.wealthModifier);

        }
       if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            stealth.AddModifier(oldItem.stealthModifier);
            lockPick.AddModifier(oldItem.lockpickingModifier);
            speed.AddModifier(oldItem.speedModifier);
            wealthMod.AddModifier(oldItem.wealthModifier);
        }
    }

    
    public override void Die()
    {
        base.Die();
        //kill the player
        MurphyPlayerManager.instance.KillPlayer();
    }


}
