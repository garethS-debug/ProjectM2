using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBoard : MonoBehaviour
{
    [Header("InteractionProperties")]
    [Tooltip("references to player and interaction point")]

    private Transform player;
    private Transform interactionTransform;
    public GameObject PickUpTextBox;
    public GameObject MessageBox;

    [Space(10)]

    [Header("Main TextBox")]
    [Tooltip("What message do you want to send to the text box")]
    public string MessageBoxText; // Text to be placed in text box

    [Header("Planning TextBox")]
    [Tooltip("What message do you want to send to the text box")]
    public string PlanningBoxText; // Text to be placed in text box

    [Header("Shop TextBox")]
    [Tooltip("What message do you want to send to the text box")]
    public string ShopBoxText; // Text to be placed in text box



    [Space(10)]


    [Header("Is this planning / Main heist")]
   // public bool PlanningHeist;
    public bool MainHeist;
    public bool isShop;

    [Header("Level Select")]
    [Tooltip("Whats the level select Button")]
    public KeyCode levelSelectCode = KeyCode.L;
    [Tooltip("Whats the level to load")]
    public Object levelToLoad;
    //public string PlanningLevelToLoad;
    public bool SelectLevel;
    public bool openShop;



   


    private void Start()
    {

        player = MurphyPlayerManager.instance.player.transform;
        interactionTransform = this.gameObject.transform;
      //  PickUpTextBox = GameObject.FindGameObjectWithTag("ItemPickUpText");
        MessageBox.gameObject.SetActive(false);



    }

    private void Update()
    {
        // player = PlayerManager.instance.player.transform;           //Reference to the player
        // PickUpTextBox = GameObject.FindGameObjectWithTag ( "ItemPickUpText" );


        //if (EquipmentManager.ItemToReveal.name == item.transform.name)
        //{
        //    print("REVEAL MAG GLASS");
        //}

        if (SelectLevel == true && Input.GetKeyDown(levelSelectCode))// CHANGE THE PICKUP BUTTON HERE

        {
            EquipmentManager.instance.UnequipAll();

            if (MainHeist == true) //Check to see if tutorial completed
            {
                //if (InformationToSave.instance.savedInformation.tutorialCompleted == true)
                //{
                    SceneManager.LoadScene(levelToLoad.name);
                //}
            }

            //if (PlanningHeist == true) //Check to see if tutorial completed
            //{
               
            //        SceneManager.LoadScene(levelToLoad.name);
                
            //}

        }


        if (Input.GetKeyDown(levelSelectCode) && openShop==true)// CHANGE THE PICKUP BUTTON HERE

        {

            Shop.instance.OpenShop();

        }

    }


    #region Colliders Detecting Player 
    //Collision Detection function
    //---------------------->
    private void OnTriggerEnter(Collider collider)
    {
        //collider = Colider of the game object
        if ((collider.gameObject.tag == "Player"))
        {
            if (MainHeist == true)
            {
                MessageBox.gameObject.SetActive(true);
                //  PickupText.gameObject.SetActive(true);
                PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = MessageBoxText;
                SelectLevel = true;


                //if (InformationToSave.instance.savedInformation.tutorialCompleted == false)
                //{
                //    //popup saying complete tutorial
                //    MessageBox.gameObject.SetActive(true);
                //    //  PickupText.gameObject.SetActive(true);
                //    PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = "Complete the tutorial first at the planning board";
                //    SelectLevel = true;
                //}

              

            }

            //if (PlanningHeist == true)
            //{
            //    MessageBox.gameObject.SetActive(true);
            //    //  PickupText.gameObject.SetActive(true);
            //    PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = PlanningBoxText;
            //    SelectLevel = true;



            //}

            if (isShop == true)
            {
                MessageBox.gameObject.SetActive(true);
                //  PickupText.gameObject.SetActive(true);
                PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = ShopBoxText;
                openShop = true;
              

            }

        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //collider = Colider of the game object
        if (collider.gameObject.tag == "Player")
        {
            openShop = false;
            //  PickupText.gameObject.SetActive(false);
            SelectLevel = false;
            PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = "";
            MessageBox.gameObject.SetActive(false);
            
        }
    }
    //Collision Detection function
    #endregion


   

    }

