/*  
    This component is for creating all dialog that an NPC can give
*/

    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/Add New Dialog")]

public class DialogMenu : ScriptableObject {


    public string[] DialogueCharacterStrings;                //string containing the dialog
    public bool isCharacterQuestDialog = false;
    public bool isCharacterQuestCompleteDialog = false;

    //public bool setCharacterQuestfromDialog = false;
    public int QuestID;
}
