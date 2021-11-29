/*  
    This component is for creating a Save File
*/


using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "Save Data", menuName = "Save/Save Information")]
public class SaveInformation : ScriptableObject
{
    [Header("Score")]
        public float GlobalTotalScore;
        public float GlobalTotalCash; 

    [Header("Inventory")]
   // public ItemBluePrint[] inventoryItems;
    public List<ItemBluePrint> InventoryList = new List<ItemBluePrint>();


    [Header("Level Information Files")]
    [Tooltip("//IMPORTANT PLEASE POPULATE HEIST LEVEL RAW DATA WITH LEVEL I'D's")]
    public List<LevelInformation> LevelInformationFiles = new List<LevelInformation>();
                                                            //IMPORTANT PLEASE POPULATE HEIST LEVEL RAW DATA WITH LEVEL I'D's

    [Header("Level Information Raw Data")]
    //IMPORTANT PLEASE POPULATE HEIST LEVEL RAW DATA WITH LEVEL I'D's
    public List<HeistLevelListInfo> HeistLevel = new List<HeistLevelListInfo>();

    [Header("Tutorial")]
    public bool tutorialCompleted;


}
