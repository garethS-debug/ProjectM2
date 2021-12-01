using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
using Steamworks;

//[RequireComponent(typeof(SteamManager))]

public class SteamLobbyManager : MonoBehaviour
{
    protected Callback<LobbyCreated_t> steam_lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;


    private const string HostAddressKey = "HostAddress";

    public LobbyManager lobbyManager;

    private SteamManager steamManager;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Im here: Steam Lobby".Color("white"));

        //    lobbyManager = this.gameObject.GetComponent<LobbyManager>();
        steamManager = gameObject.GetComponent<SteamManager>();


        // if steam not initialized then return
        if (!SteamManager.Initialized) 
        {
            Debug.Log("Steam Manager not initialized".Color("red"));
            return; 
        }



        Debug.Log("Steam Lobby intiialized".Color("white"));



        steam_lobbyCreated = Callback<LobbyCreated_t>.Create(steam_OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);


    }





    /// <summary>
    /// create lobby in steam (input paramaters of Lobby type (friends/Public). 
    /// call this when setting up the lobby
    /// when pressing button we host a lobby 
    /// </summary>
    public void steam_HostLobby(ELobbyType eLobbyType, int maxConnections)
    {
        //on steam servers it will make us a lobby of steam users
        SteamMatchmaking.CreateLobby(eLobbyType, maxConnections);
        Debug.Log("Steam Set up Lobby".Bold().Color("green"));
    } 


    /// <summary>
    /// Callback for when the lobby is created
    /// </summary>
    /// <param name="callback"></param>
    private void steam_OnLobbyCreated(LobbyCreated_t callback)
    {

        //Lobby not created
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            return;
        }

        //Insert Logic here for photon
                                                                                        // START HOSTING - we become a host
                                                                                        //networkManager.StartHost();

        Debug.Log("Steam Lobby Created".Bold().Color("green"));
        //for this lobby steam has created, this is the key and this is the value, giving back the steam ID
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
    }


    /// <summary>
    /// when someone requests to join. Extra logic can be added when joining. 
    /// </summary>
    /// <param name="callback"></param>
    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
        Debug.Log("Join Request Recieved".Bold().Color("green"));
    }



    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        //check if this is the host talking 
       // if (NetworkServer.active) { return; } //exclude host
       if (lobbyManager.CheckIfHost() == true) { return; }


        //if client
        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

        //  networkManager.networkAddress = hostAddress;
        //  networkManager.StartClient();

        Debug.Log("Lobby has been entered".Bold().Color("green"));

        //    buttons.setActive(false);

    }


}
