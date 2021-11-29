using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Dialog : MonoBehaviour {

    private Text _textComponent;                    //gets the text component

   // public string[] DialogueStrings;                //string containing the dialog


    public float SecondsBetweenCharacters = 0.15f;  //seconds between characters displaying
    public float CharacterRateMultiplier = 0.5f;    //

    public KeyCode DialogueInput = KeyCode.Return;

    public static bool _isStringBeingRevealed = false;
    public static bool _isDialoguePlaying = false;
    private bool _isEndOfDialogue = false;

    public GameObject ContinueIcon;
    public GameObject StopIcon;

    //Bool For Quest Dialog
    public static Dialog dialog;

    //TESTING WITH SCRIPTABLE OBJECTS

    public string[] characterDialogtext;
    private bool isQuestDialog;

    //Bool for turning panel off
    public static bool endoftheDialog = false;




    //public bool setQuestfromDialog;

    // Use this for initialization

    public static DialogsQueue dialogsQueue;

    //Default DIALOG
    public DialogMenu dialogDefaultDialog;

    //QUEST AVAILABLE DIALOG
    public DialogMenu dialogQuestAvailable;

    //QUEST RUNNING DIALOG
    public DialogMenu dialogQuestRunning;

    //QUEST FINISHED DIALOG
    public DialogMenu dialogQuestFinished;



    void Start()
    {

        _textComponent = GetComponent<Text>();      //Get the text component
        _textComponent.text = "";                   //Blanking the screen on start
        HideIcons();

        StartNewDialog();

        //------ > Dialog setup (Use this to change the dialog)
        characterDialogtext =  dialogDefaultDialog.DialogueCharacterStrings;
        isQuestDialog = dialogDefaultDialog.isCharacterQuestDialog;
       
        //--------->

    }

    // Update is called once per frame
    void Update()
    {
     
        DialogSequencing();


        if (Input.GetKeyDown(KeyCode.Return)) //if player presses button co-ruitine will play
        {
            if (!_isDialoguePlaying)
            {
                _isDialoguePlaying = true;
                StartCoroutine(StartDialogue());
            }

            //--------> CLOSE DIALOG BOX IF END OF TEXT
            if (_isEndOfDialogue == true)
            {
                //PleaseCloseDialogbox = true;
                ResettheDialog();
            }

            //--------> CLOSE DIALOG BOX IF END OF TEXT

        }

        if (NPC_FOV.DialogReset == true )
        {
            StartCoroutine(ResetDialogue());

        }

      
    }

    private IEnumerator StartDialogue()
    {

        int dialogueLength = characterDialogtext.Length;        
        int currentDialogueIndex = 0;                       

        while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed) //is string being revealed

        {
            if (!_isStringBeingRevealed)
            {
                _isStringBeingRevealed = true;
                StartCoroutine(DisplayString(characterDialogtext[currentDialogueIndex++]));

                if (currentDialogueIndex >= dialogueLength)
                {
                    _isEndOfDialogue = true;
                    //REMOVE THIS TO THE DIALOG MANAGER
                    if (isQuestDialog == true)
                    {
                        DialogManager.passQuestfromDialog = true;
                    }
                    //REMOVE THIS TO THE DIALOG MANAGER
                }
            }

            yield return 0;
        }

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }



            yield return 0;
        }

        HideIcons();
        _isEndOfDialogue = false;
        _isDialoguePlaying = false;
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        //ADD PAUSE FUNCTION HERE

        int stringLength = stringToDisplay.Length;      // Length of the string
        int currentCharacterIndex = 0;                  //Current Character Index. 0 = start of string
        HideIcons();
        _textComponent.text = "";                       //clear string

    

        while (currentCharacterIndex < stringLength)    
        {
            _textComponent.text += stringToDisplay[currentCharacterIndex];      //add characters to string display
            currentCharacterIndex++;                                            //increment the character index

            if (currentCharacterIndex < stringLength)                           //breaking out of loop  
            {
                if (Input.GetKey(DialogueInput))
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters * CharacterRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters);

                }
            }
            else
            {
                break;
            }
        }

        // GET THIS TO PAUSE / STOP THE TEXT WHEN OUTSIDE DETECTION RANGE
        // GET THIS TO PAUSE / STOP THE TEXT WHEN OUTSIDE DETECTION RANGE
        // GET THIS TO PAUSE / STOP THE TEXT WHEN OUTSIDE DETECTION RANGE
      


        ShowIcon();

        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

        HideIcons();

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
            return;
           
        }
        else
        {
            ContinueIcon.SetActive(true);
        }

    } 

    public void ResetDialog( )
    {

        Debug.Log("Empty");

    }


    private IEnumerator ResetDialogue()
    {
        if (NPC_FOV.DialogReset == true)
        {
            _isStringBeingRevealed = false;
            Debug.Log("Dialog is reset");
            HideIcons();
            _textComponent.text = "";                       //clear string 
            NPC_FOV.DialogReset = false;
            _isDialoguePlaying = false; 
            yield break;
        }


    }

    public void ResettheDialog()
    {
        endoftheDialog = true;
        _isStringBeingRevealed = false;
        Debug.Log("Dialog is at its end");
        HideIcons();
        _textComponent.text = "";                       //clear string
        NPC_FOV.DialogReset = false;
        _isDialoguePlaying = false;
       

    }

   

    //-------> This sets the dialog based on the current quest dialog

    public void DialogSequencing ()
    {
        //----> DIALOG PROPERTIES MUST = DIALOGS QUEUE NOT WORKING ATM

        if (FieldOfView.RecievableQuestDialog == true)
        {
            Debug.Log("Recieveable quest Dialog here");
            characterDialogtext = dialogQuestAvailable.DialogueCharacterStrings;
            isQuestDialog = dialogQuestAvailable.isCharacterQuestDialog;
        }

        //QUEST ACCEPTED STATUS = DIALOG AVAILABLE 
        else if (FieldOfView.AcceptedQuestDialog == true)
        {
            //currentDialog = dialogQuestRunning;
            Debug.Log("Accepted quest Dialog here");
            characterDialogtext = dialogQuestRunning.DialogueCharacterStrings;
            isQuestDialog = dialogQuestRunning.isCharacterQuestDialog;


        }

        //QUEST FINISHED STATUS = DIALOG AVAILABLE
        else if (FieldOfView.CompletedQuestDialog == true)
        {
            Debug.Log("Completed quest Dialog here");
            characterDialogtext = dialogQuestFinished.DialogueCharacterStrings;
            isQuestDialog = dialogQuestFinished.isCharacterQuestDialog;
        }


        //IF THERE IS NO QUEST THEN CURRENT DIALOG = STANDARD DIALOG 
        else
        {
            Debug.Log("Default Dialog here");
            characterDialogtext = dialogDefaultDialog.DialogueCharacterStrings;
            isQuestDialog = dialogDefaultDialog.isCharacterQuestDialog;
        }
        ////----> DIALOG PROPERTIES MUST = DIALOGS QUEUE NOT WORKING ATM
        //dialogProperties = DialogsQueue.dialogsQueue.currentDialog;



    }

    public void StartNewDialog()
    {
        _textComponent = GetComponent<Text>();      //Get the text component
        _textComponent.text = "";                   //Blanking the screen on start
        HideIcons();
    }




}

