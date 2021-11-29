using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    

public class SavedData 
{
    //serialisable information can only be taken from INT, BOOL, FLOAT, STRING and [] array.


    public int[] InventoryitemID;
    //public float health;
    //public bool  LevelInfo;
    //public int[] Vector3;
    //public float floating;




    //Saving Inventory Information.     
    public SavedData (InformationToSave inventory)
    {
      

        int arrayLength = Inventory.instance.items.Count;
       

        //for (int i = 0; i <= inventory.InventoryIDList.Count; i++)
        //{
        //    InventoryitemID[i] = inventory.InventoryIDList[i];

        //}


    
    }






    ////USE THIS TO SAVE DATA -- ATTACH TO OBJECTS/Buttons whatever


    //public void SaveData()
    //{
    //      SaveSystem.SaveInformation(this);
    //}



    //public void LoadData()
    //{
    //    SavedData data = SaveSystem.LoadInformation();

    //    //level = data.level;
    //    //health = data.health;

    //}



}
