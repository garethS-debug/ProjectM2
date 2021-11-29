using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EquipedItemsUI : MonoBehaviour
{

    public Image ItemImageContainer;
    public TextMeshProUGUI itemStatContainer;
    public Image ItemCategoryContainer;
    public bool isBoxEmpty;

    public string ItemName;
    public int SlotIndexNumber;
    // Start is called before the first frame update
    void Start()
    {
        isBoxEmpty = true;
        // this.gameObject.SetActive(false);
        //Set opacity to 0%
        Color c = ItemImageContainer.color;
        c.a = 0;
        ItemImageContainer.color = c;

        ////Set Category opacity to 0%
        Color cat = ItemCategoryContainer.color;
        cat.a = 0;
        ItemCategoryContainer.color = cat;
    }

  
    public void UpdateEquipedItemUI( Equipment newItem)
    {
      //  this.gameObject.SetActive(true);
        // Set the new icon in EquipmentUIContainer
        // EquipedItemUIContainer[i].sprite = NewItem.icon;
        ItemImageContainer.sprite = newItem.icon;



        //Set opacity to 0%
        Color c = ItemImageContainer.color;
        c.a = 1;
        ItemImageContainer.color = c;

        //Set Category opacity to 0%
        Color cat = ItemCategoryContainer.color;
        cat.a = 1;
        ItemCategoryContainer.color = cat;

        ItemName = newItem.name;

       // this.gameObject.SetActive(true);

        //Update Wealth Stat
        if (newItem.wealthModifier > 0.1f)
        {
            c.a = 1;
            cat.a = 1;
            ItemImageContainer.color = c;
            ItemCategoryContainer.color = cat;

            //ItemCategoryContainer.IsActive();
            itemStatContainer.text = newItem.wealthModifier.ToString();
            ItemCategoryContainer.sprite = EquipmentManager.instance.itemCatWealth;
        }

        if (newItem.armorModifier > 0.1f)
        {
            c.a = 1;
            cat.a = 1;
            ItemImageContainer.color = c;
            ItemCategoryContainer.color = cat;
            itemStatContainer.text = newItem.armorModifier.ToString();
            ItemCategoryContainer.sprite = EquipmentManager.instance.itemCatArmor;
        }

        if (newItem.damageModifier > 0.1f)
        {
            c.a = 1;
            cat.a = 1;
            ItemImageContainer.color = c;
            ItemCategoryContainer.color = cat;
            itemStatContainer.text = newItem.damageModifier.ToString();
            ItemCategoryContainer.sprite = EquipmentManager.instance.itemCatDamage;
        }


        if (newItem.lockpickingModifier > 0.1f)
        {
            c.a = 1;
            cat.a = 1;
            ItemImageContainer.color = c;
            ItemCategoryContainer.color = cat;

            itemStatContainer.text = newItem.lockpickingModifier.ToString();
            ItemCategoryContainer.sprite = EquipmentManager.instance.itemCatLockPick;
        }

        if (newItem.speedModifier > 0.1f)
        {
            c.a = 1;
            cat.a = 1;
            ItemImageContainer.color = c;
            ItemCategoryContainer.color = cat;

            itemStatContainer.text = newItem.speedModifier.ToString();
            ItemCategoryContainer.sprite = EquipmentManager.instance.itemCatSpeed;
        }

        if (newItem.stealthModifier > 0.1f)
        {
            c.a = 1;
            cat.a = 1;
            ItemImageContainer.color = c;
            ItemCategoryContainer.color = cat;

            itemStatContainer.text = newItem.stealthModifier.ToString();
            ItemCategoryContainer.sprite = EquipmentManager.instance.itemCatStealth;
        }

        isBoxEmpty = false;
        //  print("Quick Menu Equipment UI Updated " + EquipedItemUIContainer[i].name);
        // break;

    }

    public void ClearEquipedItemUI()
    {
        // Set the new icon in EquipmentUIContainer
        

        ItemImageContainer.sprite = null;

        //Set opacity to 00%
        Color c = ItemImageContainer.color;
        c.a = 0;
        ItemImageContainer.color = c;

        //Set Category opacity to 100%
        Color cat = ItemCategoryContainer.color;
        cat.a = 0;
        ItemCategoryContainer.color = cat;

        itemStatContainer.text = "";

        ItemName = null;


        //    EquipmentManager.instance.Unequip(slotIndex);

        for (int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
        {
            EquipmentManager.instance.Unequip(SlotIndexNumber);
        }



        isBoxEmpty = true;

        this.gameObject.SetActive(false);
    }



    }
