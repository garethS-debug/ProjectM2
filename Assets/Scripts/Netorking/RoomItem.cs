using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


//Displaying the correct room name for each room item and joining the room on clic 

public class RoomItem : MonoBehaviour
{


    public TMP_Text roomName;

    LobbyManager manager;

    public TMP_InputField password;
    public GameObject PasswordBox;
    private string passwordtext;
    public bool passwordRequired;

    public RoomInfo roomInfo;
    public string thePasswordDummy;



    private void Start()
    {
        manager = FindObjectOfType<LobbyManager>();

        if (passwordRequired)
        {
            PasswordBox.gameObject.SetActive(true);

            if (roomInfo.CustomProperties["Password"].ToString() != "")
            {
                thePasswordDummy = roomInfo.CustomProperties["Password"].ToString();
            }
        }

        if (!passwordRequired)
        {
            PasswordBox.gameObject.SetActive(false);
        }

       

    }


    // Start is called before the first frame update
    public void SetRoomName(string _roomName)
    {

        roomName.text = _roomName;

    }

    public void WrittenPassword (string _password)
    {
        PasswordBox.gameObject.SetActive(true);
        passwordtext = _password;

    }
  
    public void OnClickItem()
    {

        if (passwordRequired)
        {
            passwordtext = password.text;

            if (passwordtext == thePasswordDummy)
            {
                Debug.Log("WELL DONE PASSWORD CORRECT");
                manager.JoinRoom(roomName.text);
            }

            else
            {
                Debug.Log("PASSWORD INCORRECT");
                Debug.Log("You Entered " + passwordtext);
            }
           
        }
        if (!passwordRequired)
        {
            manager.JoinRoom(roomName.text);

        }


        }



   
}
