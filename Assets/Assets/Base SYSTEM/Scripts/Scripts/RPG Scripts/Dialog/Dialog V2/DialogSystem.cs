using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DialogSystem : MonoBehaviour {


    //-------> Setting dialog as Scriptable Object

    public static DialogSystem dialogSystem;
    //Standard DIALOG
    public DialogMenu dialogStandardDialog;

    //QUEST AVAILABLE DIALOG
    public DialogMenu dialogQuestAvailable;

    //QUEST RUNNING DIALOG
    public DialogMenu dialogQuestRunning;

    //QUEST FINISHED DIALOG
    public DialogMenu dialogQuestFinished;

    //Default DIALOG
    public DialogMenu defaultDialog;

    //TESTING WITH SCRIPTABLE OBJECTS
    public string[] characterDialogtext;
    private bool isQuestDialog = false;
    [Tooltip("Is there an item to delete")]
    private bool isQuestItem = false;
    public static bool isCompletedQuest = false;

    public bool isEndingofDialog;
    public bool deleteQuestObject;

    //---------->

    //------ > Dialog Components
    private Text _textComponent;
    //private string[] DialogueStrings;
    //------> DialogueStrings has been changed to DialogProperties. THIS IS THE STRING TEXT
    public float SecondsBetweenCharacters = 0.15f;
    public float CharacterRateMultiplier = 0.5f;

    public KeyCode DialogueInput = KeyCode.Return;

    private bool _isStringBeingRevealed = false;
    public static bool _isDialoguePlaying = false;
    private bool _isEndOfDialogue = false;

    public GameObject ContinueIcon;
    public GameObject StopIcon;


    //---> reset dialog
    public static bool DialogReset;

    public bool StringBeingRev = false;

    // Use this for initialization
    void Start()
    {
        _textComponent = GetComponent<Text>();
        _textComponent.text = "";
        HideIcons();

        //-------> Setting up the standard dialog as Scriptable Object
        characterDialogtext = defaultDialog.DialogueCharacterStrings;
        isQuestDialog = defaultDialog.isCharacterQuestDialog;
       
        //-------->




    }

    // Update is called once per frame
    void Update()
    {
        //-------> Running through IF statements to change dialog based on coniditions 
        DialogChange();
        //-------->
        DialogUpdate();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!_isDialoguePlaying)
            {
                _isDialoguePlaying = true;
                StartCoroutine(StartDialogue());
            }

        }

        if (DialogReset == true)
        {
          //  Debug.Log("Dialog Is being reset");
            StartCoroutine(ResetDialogue());
        }
    }

    private IEnumerator StartDialogue()
    {
        int dialogueLength = characterDialogtext.Length;
        int currentDialogueIndex = 0;

        while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed)
        {
          //  StringBeingRev = true;

            if (!_isStringBeingRevealed)
            {
                _isStringBeingRevealed = true;

                StartCoroutine(DisplayString(characterDialogtext[currentDialogueIndex++]));

                if (currentDialogueIndex >= dialogueLength)
                {
                    _isEndOfDialogue = true;
                   
                }
            }

            yield return 0;
        }

        while (true)
        {
//            Debug.Log("BUFFING TESTING");

            if (Input.GetKeyDown(DialogueInput) && StringBeingRev == false) // BUG Identified, only reset while loop after text has finished
            {
              //  Debug.Log("BUF TESTING");
                break;
            }

            yield return 0;
        }

        HideIcons();
        _isEndOfDialogue = false;
        _isDialoguePlaying = false;
        isEndingofDialog = false;
        StringBeingRev = false;
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;
        HideIcons();
        _textComponent.text = "";


     

        while (currentCharacterIndex < stringLength)
        {
            _textComponent.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;
          

            if (currentCharacterIndex < stringLength)
            {


                if (Input.GetKey(DialogueInput))
                {
                    StringBeingRev = true;
                    //IF DIALOG BUTTON PRESSED WHILE DIALOG PLAYING
                   // Debug.Log("SkipText");
                    yield return new WaitForSeconds(SecondsBetweenCharacters * CharacterRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters);

                }
            }
            else
            {
                StringBeingRev = false;
                break;
            }
        }

        ShowIcon();

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {

                break;
            }

            yield return 0;
        }

        //HideIcons();

        _isStringBeingRevealed = false;
        _textComponent.text = "";
    }

    private void HideIcons()
    {
        ContinueIcon.SetActive(false);
        StopIcon.SetActive(false);
    }

    private void ShowIcon()
    {
        if (_isEndOfDialogue)
        {
            StopIcon.SetActive(true);

           // Debug.Log("THIS IS THE END OF THE DIALOG");
            //-------------------->GIVE QUEST FROM END OF DIALOG
            //----------> Set the Quest Dialog Box Open
            if (isQuestDialog == true)
            {
                QuestObject.StartQuestFromDialog = true;
                Debug.Log("Pass Quest from Dialog");
            }

            // if completed quest dialog
            if (isCompletedQuest == true)
            {
  //             Debug.Log("Quest End DIALOG");
                QuestObject.CompleteQuestFromDialog = true;
            }
            //-------------------->GIVE QUEST FROM END OF DIALOG
            //---------->

            return;
        }

        else
        {
            ContinueIcon.SetActive(true);
        }
       
    }

    /// ------------ <summary>
    /// Reset Dialog Coruitine
    ///  ----------- </summary>

    private IEnumerator ResetDialogue()
    {
        if (DialogReset == true)
        {
            _isStringBeingRevealed = false;
          //  Debug.Log("Dialog is reset");
            HideIcons();
            _textComponent.text = "";                       //clear string 
            DialogReset = false;
            _isDialoguePlaying = false;
            yield break;
        }


    }




    public void DialogUpdate()
    {
        /*
        dialogStandardDialog = FieldOfView.dialogStandardDialog;
        dialogQuestAvailable = FieldOfView.dialogQuestAvailable;
        dialogQuestRunning = FieldOfView.dialogQuestRunning;
        dialogQuestFinished = FieldOfView.dialogQuestFinished;
        defaultDialog = FieldOfView.defaultDialog;
        */       
    }



    /// ------------ <summary>
    /// Changes the Dialog based on a set of conditions
    ///  ----------- </summary>

    public void DialogChange()
    {

        //----- > THIS IS NOT CHANGING. 
        //dialogStandardDialog = FieldOfView.fieldOfView.dialogStandardDialog;
        //dialogQuestAvailable = FieldOfView.fieldOfView.dialogQuestAvailable;
        //dialogQuestRunning = FieldOfView.fieldOfView.dialogQuestRunning;
        //dialogQuestFinished = FieldOfView.fieldOfView.dialogQuestFinished;
        //defaultDialog = FieldOfView.fieldOfView.defaultDialog;
        //----- > THIS IS NOT CHANGING. 


        //Recieve NPC Information from FieldOfView
        //if that NPC holds dialog then update the dialog. 
        //e.g. if ("NPC1".QuestObject.RecievableQuestDialog == true)
        //QuestObject is under 'NPC MECHANICS'
 

        if (QuestObject.RecievableQuestDialog == true)
        {

          //  Debug.Log("Recieveable quest Dialog here");
            characterDialogtext = FieldOfView.dialogQuestAvailable.DialogueCharacterStrings;
            isQuestDialog = FieldOfView.dialogQuestAvailable.isCharacterQuestDialog;
            isCompletedQuest = FieldOfView.dialogQuestAvailable.isCharacterQuestCompleteDialog;
        }

        //QUEST ACCEPTED STATUS = DIALOG AVAILABLE 
        else if (QuestObject.AcceptedQuestDialog == true)
        {
            //currentDialog = dialogQuestRunning;
          //  Debug.Log("Accepted quest Dialog here");
            characterDialogtext = FieldOfView.dialogQuestRunning.DialogueCharacterStrings;
            isQuestDialog = FieldOfView.dialogQuestRunning.isCharacterQuestDialog;
            isCompletedQuest = FieldOfView.dialogQuestRunning.isCharacterQuestCompleteDialog;

        }

        //QUEST FINISHED STATUS = DIALOG AVAILABLE
        else if (QuestObject.CompletedQuestDialog == true)
        {
//            Debug.Log("Completed quest Dialog here");
            characterDialogtext = FieldOfView.dialogQuestFinished.DialogueCharacterStrings;
            isQuestDialog = FieldOfView.dialogQuestFinished.isCharacterQuestDialog;
            isCompletedQuest = FieldOfView.dialogQuestFinished.isCharacterQuestCompleteDialog;
        }


        //IF THERE IS NO QUEST THEN CURRENT DIALOG = STANDARD DIALOG 
        else
        {
          //  Debug.Log("Default Dialog here");
            characterDialogtext = FieldOfView.dialogStandardDialog.DialogueCharacterStrings;
            isQuestDialog = FieldOfView.dialogStandardDialog.isCharacterQuestDialog;

        }
         
    }


}
