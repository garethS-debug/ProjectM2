using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Steamworks;
public class GameSetup : MonoBehaviour
{

	//Check for save game

	// if no save game, then ask player to select character. 

	// Save that characte choice


	//Lobby check's save file for player choice and uses that info

	[Header("Static Variables")]
	public static int PlayerCharacter;
	public static SaveGameManager.SaveFile playerSaveFile;
	public static PlayerSO staticPlayerData;
	public static bool staticexistingSaveGame;
	public static string playerName;


	[Header("UI")]
	public GameObject selectCharacterUI;
	public GameObject ContinueUI;
	public GameObject NewGameUI;
	//public TMP_InputField GhostNameInput;
	public TMP_InputField CharacterNameInput;



	[Header("Scene Ref")]
	public SceneReference newGame_levelToLoad;
	public SceneReference continue_levelToLoad;

	[Header("SO")]
	public PlayerSO playerSOData;


	// Start is called before the first frame update
	void Start()
    {
		staticPlayerData = playerSOData;

		/*
				print("example text in bold".Bold());
				print("example text in italics".Italic());
				print("example in Color".Color("red"));
				print("combine two or more in one".Bold().Color("yellow"));
				print("size also but be careful".Size(20));
		*/


		//check for existing game (UI)
		staticexistingSaveGame = SaveGameManager.CheckforSaveGame();
			
		if (staticexistingSaveGame == true)
        {
			ContinueUI.SetActive(true);

		}

		else
        {
			ContinueUI.SetActive(false);
		}

	}

    // Update is called once per frame
    void Update()
    {
        
    }



	// Note: The Awake() on this script must run before the SaveGameManager.Awake().
	internal static void LoadDataFromSaveFile(SaveGameManager.SaveFile saveFile)                                                                                        //Method for loading information from the save file. 
	{

		//print("Selected character number " +  saveFile.slectedCharacter);

		if (saveFile!= null)
        {
			playerSaveFile = saveFile;
			PlayerCharacter = saveFile.slectedCharacter;
		}

		


		/*
		// Handle StepRecords - x1 for loop
		foreach (StepRecord sRec in saveFile.stepRecords)                                                                                               //1 Foreach Loop using a dictionary
		{                                                                                                                                               //accessing the information using a dictionary to 'target' the info
			if (STEP_REC_DICT.ContainsKey(sRec.type))
			{

				STEP_REC_DICT[sRec.type].num = sRec.num;                                                                                                //Cannot be accessed if the 'record' is a struct. Has to be a class
			}
		}

		// Handle Achievements - x2 for loops
		foreach (Achievement achievementSave in saveFile.achievements)                                                                                  //2 nested foreach loops (an alternative way to access the information)
		{
			//	This nested loop is not an efficient way to do this, but the number of 
			//  Achievements is so small that it will work fine. I could have made
			//  a Dictionary<string,Achievement> for Achievements as I did with 
			//  StepRecords, but I wanted to show both ways of doing this.
			foreach (Achievement achievement in Instance.achievements)
			{
				if (achievementSave.name == achievement.name)                                                                                           // This is the same Achievement
				{

					achievement.completed = achievementSave.completed;                                                                                  //Information can only be altered if its a 'class'. Struct inforamtion cannot be altered
				}
			}
		}
		*/

	}


	public void SAVE_BUTTON()
	{
		SaveGameManager.Save();
	}

	public void LOAD_BUTTON()
	{
		SaveGameManager.Load();
	}


	public void SELECT_CHARACTER()
    {

		PlayerCharacter = 1;                                            //Human = 1
		playerSOData.PlayerCharacterChoise = PlayerCharacter;
		//Debug.Log("Saving....");
		playerName = CharacterNameInput.text;
		SaveGameManager.Save();
		SceneManager.LoadScene(newGame_levelToLoad);

		//TO DO -------- ADD TUTORIAL REQUIREMENT TO SCRIPTABLE OBJ
	}



	public void m_NEW_GAME()
	{
		/*
		bool isSave = SaveGameManager.CheckforSaveGame();

		if (isSave)
		{
			//	Debug.Log("Continue to game with your character...");
			SaveGameManager.Load();

			UpdatePlayerSaveSO(SceneSettings.Instance.connectionType);                      //1 = multiplayer 2 = single player
			SceneManager.LoadScene(newGame_levelToLoad);
		}

		*/
		if (SceneSettings.Instance.enableSteamSetttings == true)
		{

			if (SteamManager.Initialized)
			{
				string name = SteamFriends.GetPersonaName(); // Get your name as a string
				Debug.Log("Steam Set up and grabbing name : ".Bold().Color("green") + name);

				PlayerCharacter = 1;                                            //Human = 1
				playerSOData.PlayerCharacterChoise = PlayerCharacter;
				//Debug.Log("Saving....");
				playerName = name;
				SaveGameManager.Save();
				SceneManager.LoadScene(newGame_levelToLoad);


				//TO DO -------- ADD TUTORIAL REQUIREMENT TO SCRIPTABLE OBJ
			}




		}





		else
		{
			//	Debug.Log("Choose A Character");
			selectCharacterUI.SetActive(true);
			ContinueUI.SetActive(false);

		}

	}







	public void m_CONTINUE_GAME()
    {
	 bool isSave =	SaveGameManager.CheckforSaveGame();

	if (isSave)
        {
		//	Debug.Log("Continue to game with your character...");
			SaveGameManager.Load();

			UpdatePlayerSaveSO(1);                   //1 = multiplayer 2 = single player

			SceneManager.LoadScene(continue_levelToLoad);
		}
	else
        {
		//	Debug.Log("Choose A Character");
			selectCharacterUI.SetActive(true);
			ContinueUI.SetActive(false);

		}

	}






	public  static void UpdatePlayerSaveSO(int SingleorMulti)
    {
		
		//Insert other variable information -- e.g. level progression
		staticPlayerData.PlayerCharacterChoise = playerSaveFile.slectedCharacter;
		staticPlayerData.PlayerName = playerSaveFile.playerName;
		staticPlayerData.SingleOrMultiPlayer = SingleorMulti;

		Debug.Log("Updating SO......." + playerSaveFile.slectedCharacter);

		

	}
}
