using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System;
using ExitGames.Client.Photon;
using Steamworks; 
public class LobbyManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public TMP_Text roomName;

    public TMP_Text MaxPlayerUI;
    public int maxPlayerCount = 1;

    [Header("UI Room Button - in Lobby")]
    public RoomItem RoomItemPrefab; //Storing roomitem prefeb
    List<RoomItem> roomItems = new List<RoomItem>();   //List of room items

    [Header("TIME BETWEEN UPDATES")]
    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;
   

    public Transform contentObject; //Scrool view parent


    [Header("Player Item Cards")]
    public List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefeb;
    public Transform playerItemParent;

    [Header("Button")]
    public GameObject playButton;

    [Header("SceneSelect")]
    public GameObject levelSelectPanel;


    [Header("Room Info")]
    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, RoomItem> roomListEntries;
    private Dictionary<int, GameObject> playerListEntries;

    [Header("KickPlayer")]
    [SerializeField]
    private TMP_InputField nicknameInputField;
    private const byte KICK_PLAYER_EVENT = 0;

    [Header("Password")]
    public GameObject passwordBox;
    [SerializeField]
    Toggle public_Toggle;
    [SerializeField]
    Toggle private_Toggle;
    [SerializeField]
    private TMP_InputField passwordField;

    ExitGames.Client.Photon.Hashtable table = new ExitGames.Client.Photon.Hashtable();

    /// <summary>
    /// steam 
    /// </summary>
    [Header("Steam")]
    public SteamLobbyManager steamLobbyManager;


    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        cachedRoomList = new Dictionary<string, RoomInfo>();
        roomListEntries = new Dictionary<string, RoomItem>();
    }

 

    //When lobby scene loads up we need a lobby to create a room
    public void Start()
    {
     

        /// <summary>
        /// steam 
        /// </summary>
      
       // steamLobbyManager = this.gameObject.GetComponent<SteamLobbyManager>();


        // if (!PhotonNetwork.IsConnected)
        // {
        PhotonNetwork.JoinLobby();
       // }


        maxPlayerCount = 1;
    }


    //When the player accepts the invite
    //Create a void to join the game. Then start the void with the connection string.



    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)                                    //If there is not a blank room name
        {



            //create room with photon
            CreateRoom(roomInputField.text, maxPlayerCount, passwordField.text);

            if (SceneSettings.Instance.enableSteamSetttings == true)
            {


                //create room with steam  -- TO DO ADD ROOM TYPE
                steamLobbyManager.steam_HostLobby(ELobbyType.k_ELobbyTypePublic, maxPlayerCount);
            }


        }


    }

    public void CreateRoom(string _name, int _maxplayers, string _password)
    {



        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = System.Convert.ToByte(_maxplayers), IsOpen = true, IsVisible = true };

        ExitGames.Client.Photon.Hashtable custProps = new ExitGames.Client.Photon.Hashtable();
        custProps.Add("Password", _password);

        roomOptions.CustomRoomProperties = custProps;
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "Password" };

        PhotonNetwork.CreateRoom(_name, roomOptions, TypedLobby.Default);



    }




    public void IncreaseMaxPlayerCount()
    {
        maxPlayerCount++;
        MaxPlayerUI.text = maxPlayerCount.ToString();
    }

    public void DecreaseMaxPlayerCount()
    {
        if (maxPlayerCount > 1)
        {
            maxPlayerCount--;
            MaxPlayerUI.text = maxPlayerCount.ToString();
        }
    
    }

    /// <summary>
    /// from asteriod Demo
    /// </summary>
    public override void OnJoinedLobby()
    {
        // whenever this joins a new lobby, clear any previous room lists
        cachedRoomList.Clear();
        ClearRoomListView();
    }

    //called when joining a room
    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);

        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        UpdatePlayerList();
    }

    //Called automaticlaly when there is a change in room list
    public override void OnRoomListUpdate(List<RoomInfo> _roomList)
    {
        //when a room is created, modified or destroyed
        print("OnRoomList Update");
        /*
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(_roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
        */

        ClearRoomListView();

        UpdateCachedRoomList(_roomList);
        UpdateRoomListView();

    }

    /// <summary>
    /// From Asteriods Demo
    /// </summary>
    private void ClearRoomListView()
    {
        foreach (RoomItem item in roomItems)
        {
            Destroy(item.gameObject);
        }


        roomItems.Clear();
    }



    /// <summary>
    /// From Asteriods Demo
    /// </summary>
    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // Remove room from cached room list if it got closed, became invisible or was marked as removed
            if (!info.IsOpen || !info.IsVisible || info.RemovedFromList)
            {
                if (cachedRoomList.ContainsKey(info.Name))
                {
                    cachedRoomList.Remove(info.Name);
                }

                continue;
            }

            // Update cached room info
            if (cachedRoomList.ContainsKey(info.Name))
            {
                cachedRoomList[info.Name] = info;
            }
            // Add new room info to cache
            else
            {
                cachedRoomList.Add(info.Name, info);
            }
        }
    }


    /// <summary>
    /// From ASteriod demo
    /// </summary>
    /// <param name="_list"></param>

    private void UpdateRoomListView()
    {
        foreach (RoomInfo info in cachedRoomList.Values)
        {
            print("Room Name = " + info.Name);
            RoomItem newRoom = Instantiate(RoomItemPrefab, contentObject);         //Instantiate the object
            newRoom.SetRoomName(info.Name);                                         //Set toom name in the item
            newRoom.roomInfo = info;
            roomItems.Add(newRoom);                                                 //AddComponentMenu new to list
            print("Add Rooms");



           // Password
            Debug.Log(info.ToStringFull());
            Debug.Log("------");
            if (info.CustomProperties["Password"] != null)
            {
                Debug.Log("Password is : " + info.CustomProperties["Password"]);

                newRoom.passwordRequired = true;
            }

            if (info.CustomProperties["Password"].ToString() == "")
            {
                Debug.Log("No password on room " + info.Name);
                newRoom.passwordRequired = false;
            }

            //  roomListEntries.Add(info.Name, newRoom);
        }
    }

    /*
    private void UpdateRoomList(List<RoomInfo> _list)
    {
        print("DestroyRooms");
        //destroy room items and re-popualte with new
        foreach (RoomItem item in roomItems)
        {
            Destroy(item.gameObject);
        }

        roomItems.Clear();

        //INstantiate room items for each room and add our new list
        print("Populate with new rooms");
        foreach (RoomInfo room in _list)
        {
            print("Room Name = " + room.Name);
            RoomItem newRoom =  Instantiate(RoomItemPrefab, contentObject);         //Instantiate the object
            newRoom.SetRoomName(room.Name);                                         //Set toom name in the item
            roomItems.Add(newRoom);                                                 //AddComponentMenu new to list
            print("Add Rooms");
        }
    }
    */


    public void  JoinRoom(string roomName)              //Join room
    {

        //UpdateRoomList(_list);
        //UpdateCachedRoomList(_list);

        PhotonNetwork.JoinRoom(roomName);
    }



    public void OnClickLeaveRoom()                      //leave room button
    {
        PhotonNetwork.LeaveRoom();      
        //refresh rooms
    }

    //Runs autoamtically when leaving room

    public override void OnLeftRoom()

    {
        if (roomPanel != null)
        {
            roomPanel.SetActive(false);
        }

        if (lobbyPanel != null)
        {
            lobbyPanel.SetActive(true);
        }
     
    }

    public override void OnConnectedToMaster()      //Join master server when leaving a lobby. 
    {
        PhotonNetwork.JoinLobby();
    }


    void UpdatePlayerList()
    {
        //divided into 2 steps, 1st delete all items and delete, then populate for each player in the room
        foreach (PlayerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
            
        }

        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)      //If the room is clear, exist out of code
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players) //looping through the current room players, retrieving the curret players in the room. It comes inthe form of a KVP (dictionary)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefeb, playerItemParent);
            newPlayerItem.Setplayerinfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer) //checking if this is the local player in question
            {
                newPlayerItem.ApplyLocalChanges();
            }
            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)              //when player enters room
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)           //when player leaves room
    {
        UpdatePlayerList();
    }



    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= maxPlayerCount) //is Host? less than room limit
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }

        //PhotonNetwork.GetCustomRoomList

       

        if (public_Toggle.isOn)
        {
            passwordBox.gameObject.SetActive(false);
            passwordField.text = "";
        }


         if (private_Toggle.isOn == true)
        {
            passwordBox.gameObject.SetActive(true);

        }


        }

   

    //-------THIS NEEDS TO BE MADE EXPANDABLE
    //--This should take into account locked levels - based on whoever is the host
    //--A UI interface should be created to select levels in the lobby
    //--Save data

    public void OnClickPlayButton()
    {
        PhotonNetwork.LoadLevel("Game"); //Load the 'Game' scene when master cliet clicks
    }

    public void OnClickLevelSelect()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(false);
        levelSelectPanel.SetActive(true);

    }

    public void OnClickLeaveLevelSelect()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        levelSelectPanel.SetActive(false);

    }




    public void OnClick_CallKick()
    {
        Kick(nicknameInputField);
        print("Kick Button Clicked");
    }

    private void Kick(TMP_InputField inputField)
    {
        if (inputField == null)
        {
            return; // log error?
        }
        string nickname = inputField.text;
        Kick(nickname);
    }

    private void Kick(string nickname)
    {
        if (string.IsNullOrEmpty(nickname))
        {
            return; // log error?
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players) //looping through the current room players, retrieving the curret players in the room. It comes inthe form of a KVP (dictionary)
        {
            if (player.Value != PhotonNetwork.LocalPlayer) //checking if this is the local player in question
            {
              
                if (nickname.Equals(player.Value.NickName))
                {
                    print("Kicking " + player.Value);
                    KickingPlayer(player.Value);
                    return;
                }


            }
 


        }
        // log error? player is local or not found?
    }

    private void KickingPlayer(Player playerToKick)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return; // log error?
        }
        print("Closing connection for " + playerToKick.NickName);




        //not working
        PhotonNetwork.EnableCloseConnection = true;
         PhotonNetwork.CloseConnection(playerToKick);

  
    }


    public bool CheckIfHost()
    {

        if (PhotonNetwork.IsMasterClient == true)
        {
            return true;
        }

        else
        {
            return false;
        }
    }



}

    
