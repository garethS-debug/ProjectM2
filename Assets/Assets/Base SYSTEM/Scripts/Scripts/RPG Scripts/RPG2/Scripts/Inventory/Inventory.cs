    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

#region Singleton

    public static Inventory instance;
    public static bool CheckQuestObj;


    [Header("Level Completed")]
    [Tooltip("Assigning the levelComplete variable in order to modify it")]
    public LevelCompleted levelcompleted;

    void Awake ()
    {
        levelcompleted = this.gameObject.GetComponent<LevelCompleted>();

        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
        }
        instance = this; // creating a static variable.



    }


    public void Start()
    {

    }


    public void Update()
    {
        QuestItemCheck();
    }



    #endregion

    #region Delegate
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback; 
    // Callback which is triggered when
    // an item gets added/removed.
    #endregion

    public int Inventoryspace = 20;


    public List<ItemBluePrint> items = new List<ItemBluePrint>();
    public List<GameObject> Prefab = new List<GameObject>();
    public List<int> QuestObjectID = new List<int>();


    public int [] example; 



    public bool Add (ItemBluePrint item) // Add a new item. If there is enough room
    {

                                                                                    //IF the item is a quest Item, Update Quest information. 
        if (!item.isDefaultItem)// Don't do anything if it's a default item
        {
            if (items.Count >= Inventoryspace) // Check if out of space
            {
                Debug.Log("Not Enough Room");
                return false;
            }

            //example.

            items.Add(item);
            Prefab.Add(item.ItemGameObj);   // Add prefab to list

            if (item.isQuestItem == true) // Check if item that was picked up is a quest item
            {

                QuestObjectID.Add(item.questID);  //Add quest ID to list

                //CHECK IF QUEST IS ALREADY IN CURRENT QUEST LIST
                foreach (Quests curQuest in QuestManager.questManager.currentQuestList)
                {
                    print("CurrentQuests" + curQuest.id);
                    if (curQuest.id == item.questID)
                    {
                        print("Youve Laready Got this quest");
                        QuestManager.questManager.AddtoQuestObjective(item.questID, item.AddXtoQuest);
                    }
                }

                //Update the Objective UI
                Objective.instance.UpdateObjectiveMarker(item.UIMarkerObjective);

            }

            //SAVE THE INVENTORY ITEM ID TO THE SAVE FILE
            InformationToSave.instance.SaveInventory();



           

            if (item.isMainObjective == true)
            {
                Objective.instance.levelInformation.hasKeyLocation = true;
            }





            if (onItemChangedCallback != null) 
                {
                onItemChangedCallback.Invoke(); // updating UI 
                }

            }



        return true;

    }

    public void Remove ( ItemBluePrint item)
    {
        

        items.Remove(item);
        Prefab.Remove(item.ItemGameObj); // Remove prefab to list

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke(); // updating UI 
        }

    }


    //CHECKING THE QUEST ITEM WHEN WE ACCEPT AND COMPLETE A QUEST
    public void QuestItemCheck()
    {
        if (CheckQuestObj == true)
        {
            foreach (Quests quest in QuestManager.questManager.questlist)
            {

                if (QuestObjectID.Contains(quest.id))
                {
                    if (quest.progress == Quests.QuestProgress.DONE)
                    {
                        //FIND A WAY TO DELETE THAT SPECIFIC QUEST ITEM
                        Debug.Log("DELETE THE QUEST ITEM");
                        //   instance.Remove();

                        levelcompleted.LevelComplete = true;            //Completing the level

                        foreach (ItemBluePrint item in items)
                        {

                            if (item.questID == quest.id)
                            {
                                Remove(item);                           //Remove the item
                               

                                break;
                            }
                        }
                    }
                }
            }

            //CHECK IF QUEST IS ALREADY IN CURRENT QUEST LIST 
            foreach (Quests curQuest in QuestManager.questManager.currentQuestList)
            {

                if (QuestObjectID.Contains(curQuest.id))
                {
                    if (curQuest.progress == Quests.QuestProgress.ACCEPTED)
                    {
                        //FIND A WAY TO DELETE THAT SPECIFIC QUEST ITEM
                        Debug.Log("Quest Accepted but you have the item already!");
                        //   instance.Remove();

                        foreach (ItemBluePrint item in items)
                        {

                            if (item.questID == curQuest.id)
                            {
                                QuestManager.questManager.AddtoQuestObjective(item.questID, item.AddXtoQuest);
                                Remove(item);
                                break;
                            }
                        }
                    }
                }


            }


            CheckQuestObj = false;

        }
    }



}
