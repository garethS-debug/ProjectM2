using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class InformationToSave : MonoBehaviour
{
                                                                                        //IMPORTANT PLEASE POPULATE HEIST LEVEL RAW DATA WITH LEVEL I'D's
    public static InformationToSave instance;

    // public LevelInformation levelInformation; //Scriptiable object container to save.


    [Header("Save Scriptable OBJ")]
    public SaveInformation savedInformation; //Scriptiable object container to save.   

                            
    [Header("Level Information List")]
    [Tooltip("IMPORTANT PLEASE POPULATE HEIST LEVEL RAW DATA WITH LEVEL I'D's!!!")]
    //IMPORTANT PLEASE POPULATE HEIST LEVEL RAW DATA WITH LEVEL I'D's
    public List<LevelInformation> LevelInfo = new List<LevelInformation>();

  


    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Information to Save found");
        }
        instance = this; // creating a static variable. 
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    //public void SaveData()
    //{
    //    SaveSystem.SaveInformation(this);

    //    //NEW SAVE DATA

    //}


    //Saving Inventory Information.    
    public void SaveInventory ()
    {
        //   savedInformation.inventoryItems.Length = Inventory.instance.items.Count;

        //int arrayLength = savedInformation.inventoryItems.Length;

        //for (int i = 0; i <= arrayLength; i++)
        //{
        //    savedInformation.inventoryItems[i] = Inventory.instance.items[i];

        //}


        // savedInformation.inventoryItems = Inventory.instance.items.ToArray();

        //akldk 
        //foreach (ItemBluePrint ArrayItems in savedInformation.inventoryItems)
        //{
        //    print("Array item" + ArrayItems.name);
        //    savedInformation.InventoryList.Add(ArrayItems);


        //}

        savedInformation.InventoryList = Inventory.instance.items;


    }


    public void LoadInventory ()
    {
        CheckInventory(savedInformation.InventoryList, Inventory.instance.items);

        //foreach (ItemBluePrint items in savedInformation.inventoryItems)    
        //{
            
        //    print(items.name);
        //    // Inventory.instance.items.Add(items);
        //    Inventory.instance.Add(items);

        //}



    }


    //LIST 1 = SAVE FILE, List 2 = Active INventory
    void CheckInventory(List<ItemBluePrint> _listOne, List<ItemBluePrint> _listTwo)
    {
        foreach (ItemBluePrint _GO in _listOne)
        {
            if (!_listTwo.Contains(_GO))
               // _listTwo.Add(_GO);
                Inventory.instance.Add(_GO);
                Inventory.instance.onItemChangedCallback();
                print(_GO.name);
        }
    }




    public void SaveGlobalScore(float Value)
    {
            savedInformation.GlobalTotalCash += Value;
            savedInformation.GlobalTotalScore += Value;
    }



    public void SaveLevelInformation()
    {
            print("Saving Level info");

            if (Objective.instance != null)
        {
            //THIS IS RESETTING TO 0
            LevelInfo.Add(Objective.instance.levelInformation);

        }

        if (Objective.instance != null)
        {
            if (!savedInformation.LevelInformationFiles.Contains(Objective.instance.levelInformation))
            {
                savedInformation.LevelInformationFiles.Add(Objective.instance.levelInformation);
                print("Add Level information to Level Info Files List in Saved Information");
            }
        }

            





            //THIS ACCESSES THE SAVE INFORMATION FILE AND 'ADDS' IN THE RAW HEIST LEVEL LIST THE INFORMATION FROM THE LEVEL INFO SO IN THE OBJECTIVE INSTANCE
            //REPLACE FOREARCH WITH A CHECK TO SEE IF INFO IS AVAILABLE FOR THAT SPECIFIC LEVELID 
            foreach (HeistLevelListInfo info in savedInformation.HeistLevel)
            {

            

            print("Saving Level info");

            if (Objective.instance != null)
            {
                //Checks to see if there is a Level ID available to populate 
                if (info.LevelID == Objective.instance.levelInformation.LevelID)
                {
                    print("Level ID Match");

                    info.LevelID = Objective.instance.levelInformation.LevelID;
                    info.hasKeyLocation = Objective.instance.levelInformation.hasKeyLocation;
                    info.Score = Objective.instance.levelInformation.Score;
                    info.SpottedPenetlies = Objective.instance.levelInformation.SpottedPenetlies;
                    info.TotalScore = Objective.instance.levelInformation.TotalScore;
                    info.SpottedNo = Objective.instance.levelInformation.SpottedNo;
                    info.Cash = Objective.instance.levelInformation.Cash;


             
                    info.eventID = Objective.instance.levelInformation.eventID;


                    // break;
                }
                if (info.LevelID != Objective.instance.levelInformation.LevelID)
                {
                    print("Level ID Dont Match");

                   


                  //  break;
                }

            }

        

            }



        //ADD this to script to save level information
       // InformationToSave.instance.SaveLevelInformation();


        ////savedInformation.HeistLevel.Add()
        //foreach (HeistLevelListInfo info in SaveData)
        //{


        //    info.LevelID 
        //    info.hasKeyLocation;
        //    info.hasGuardRota;
        //    info.hasMechanicEvent;
        //    info.hasFoodEvent;
        //    info.GuardAmountWithRota;
        //    info.GuardAmountWithoutRota;
        //    info.Score;
        //    info.SpottedPenetlies;
        //    info.TotalScore;
        //    info.SpottedNo;
        //}



    }




    //CHECK FOR Existing SAVE

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }


    //FUNCTION FOR SAVING
    public void SaveData()
    {

        SaveLevelInformation();
        SaveInventory();



        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

       if (!Directory.Exists(Application.persistentDataPath + "/game_save/save_data"))
        {
            //creates folder if it doesnt exist
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/save_data");
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/save_data/character_data.txt");
        var json = JsonUtility.ToJson(savedInformation);
        binaryFormatter.Serialize(file, json); //turns into binary format.
        file.Close();

        //Close quick menu
        if (QuickMenu.instance.ShowingQuickMenu == true)
        {
            print("closing menu");
            QuickMenu.instance.ShowingQuickMenu = false;
            QuickMenu.instance.QuickMenuUI.SetActive(!QuickMenu.instance.QuickMenuUI.activeSelf);
           
        }
    }


    //FUNCTION FOR LOADING

    public void LoadGame()
    {

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/save_data"))
        {
            //creates folder if it doesnt exist
            // Directory.CreateDirectory(Application.persistentDataPath + "/game_save/save_data");
            return;
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/game_save/save_data/character_data.txt"))//if file exists
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/save_data/character_data.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), savedInformation);
            file.Close();
        }


        LoadInventory();

        //Close quick menu
        if (QuickMenu.instance.ShowingQuickMenu == true)
        {
            print("closing menu");
            QuickMenu.instance.ShowingQuickMenu = false;
            QuickMenu.instance.QuickMenuUI.SetActive(!QuickMenu.instance.QuickMenuUI.activeSelf);
        }   
    }




    //public void LoadData()
    //{
    //    SavedData data = SaveSystem.LoadInformation();

    //    //LOAD IN INFORMATION 

    //    for (int i = 0; i <= InventoryIDList.Count; i++)
    //    {
    //        Debug.Log(InventoryIDList[i]);

    //    }




        //level = data.level;
        //health = data.health;

    }

