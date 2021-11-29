using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportFromStairs : MonoBehaviour
{
    //[Header("Reference to player")]
    //[Tooltip("Reference to player controller to judge distance")]
    //private PlayerManager playerManager;
    //public GameObject player;

    [Header("InteractionProperties (AutoSet)")]
    [Tooltip("references to player and interaction point")]

    public Transform player;
    public Transform interactionTransform;
    public GameObject PickUpTextBox;

    [Space(10)]

    [Header("TextBox")]
    [Tooltip("What message do you want to send to the text box")]
    public string MessageBoxText; // Text to be placed in text box

    [Space(10)]

    [Header("Interaction")]
    [Tooltip("Whats the 'GoDownstairs' Button")]
    public KeyCode GoDownStairs = KeyCode.L;
    [Tooltip("Whats the floor to load")]
  //  public string FloorToLoad;
    public bool GoDownstairs;

    [Header("Camera")]
    [Tooltip("Whats the Camera's new position")]
    public GameObject TheCamera;
    public Transform newCamPos;

    [Header("Where to Teleport the PLayer")]
    [Tooltip("Where do you want to releport the player to")]
    public Transform TeleportTo;

    private void Start()
    {

        player = PlayerManager.instance.player.transform;
        interactionTransform = this.gameObject.transform;
        PickUpTextBox = GameObject.FindGameObjectWithTag("ItemPickUpText");


    }

    private void Update()
    {
        // player = PlayerManager.instance.player.transform;           //Reference to the player
        // PickUpTextBox = GameObject.FindGameObjectWithTag ( "ItemPickUpText" );

        if (GoDownstairs == true && Input.GetKeyDown(GoDownStairs))// CHANGE THE PICKUP BUTTON HERE

        {
            player.transform.position = TeleportTo.transform.position;
            TheCamera.transform.position = newCamPos.transform.position;

            //SceneManager.LoadScene(levelToLoad);
        }
  

        //if (EquipmentManager.ItemToReveal.name == item.transform.name)
        //{
        //    print("REVEAL MAG GLASS");
        //}


    }


    #region Colliders Detecting Player 
    //Collision Detection function
    //---------------------->
    private void OnTriggerEnter(Collider collider)
    {
        //collider = Colider of the game object
        if ((collider.gameObject.tag == "Player"))
        {
            //  PickupText.gameObject.SetActive(true);
            PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = MessageBoxText;
            GoDownstairs = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //collider = Colider of the game object
        if (collider.gameObject.tag == "Player")
        {
            //  PickupText.gameObject.SetActive(false);
            GoDownstairs = false;
            PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = "";
        }
    }
    //Collision Detection function
    #endregion


}
