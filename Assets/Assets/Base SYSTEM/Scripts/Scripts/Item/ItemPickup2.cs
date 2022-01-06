using UnityEngine;
using UnityEngine.UI;


public class ItemPickup2 : Interacting
{

    [Header("ItemProperties")]
    [Tooltip("3D object and reference to properties")]
    public ItemBluePrint itemProperties;    // Item Properties. 
    public ItemPickup2 item;                // Item to put in the inventory if picked up
    public GameObject preFab;               //Prefab to be placed in Equipment Manager when picked up;
    EquipmentManager equipmentManager;      //reference to equipment Manager
    [Space(10)]

    #region ItemUI
    //Game Object
    [Header("Text Box")]
    [Tooltip("Reference to text and text Box")]
    [SerializeField]
    public GameObject PickupText;
    public GameObject PickUpTextBox;
    public string PickUpObjText; // Text to be placed in text box
    [Space(10)]
    private bool pickUpAllowed;   //Bool to determine if picking up obj allowed


    [Header("Prompt Material")]
    public string PromptText;


    private void Start()
    {
        if (SceneSettings.Instance.humanPlayer != null)
        {
            player = SceneSettings.Instance.humanPlayer.transform;
        }

        interactionTransform = this.gameObject.transform;
        //if (PickupText != null)
        //{
        // //   PickupText.gameObject.SetActive(false);
        //}



   //     PickUpTextBox = GameObject.FindGameObjectWithTag("ItemPickUpText");

      
    }

    private void Update()
    {
        // player = PlayerManager.instance.player.transform;           //Reference to the player
        // PickUpTextBox = GameObject.FindGameObjectWithTag ( "ItemPickUpText" );

        if (pickUpAllowed && Input.GetKeyDown(KeyCode.P))// CHANGE THE PICKUP BUTTON HERE
            Pickup();

      

        //if (EquipmentManager.ItemToReveal.name == item.transform.name)
        //{
        //    print("REVEAL MAG GLASS");
        //}


    }
    #endregion



    #region Colliders Detecting Player 
    //Collision Detection function
    //---------------------->
    private void OnTriggerEnter(Collider collider)
    {
        //collider = Colider of the game object
        if ( (collider.gameObject.tag == "PlayerPickup"))
        {
          //  PickupText.gameObject.SetActive(true);
                            //  PickUpTextBox.GetComponent<Text>().text = PickUpObjText;
            pickUpAllowed = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        //collider = Colider of the game object
        if ((collider.gameObject.tag == "PlayerPickup"))
        {
            //  PickupText.gameObject.SetActive(true);
                                //   PickUpTextBox.GetComponent<Text>().text = PickUpObjText;
            pickUpAllowed = true;
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        //collider = Colider of the game object
        if ( collider.gameObject.tag == "PlayerPickup")
        {
          //  PickupText.gameObject.SetActive(false);
            pickUpAllowed = false;
                                     // PickUpTextBox.GetComponent<Text>().text = "";
        }
    }
    //Collision Detection function
    #endregion


    public override void Interact()
    {
        base.Interact();
        Pickup();
    }


    void Pickup()
    {
        //  PickupThrow.Instance.DestroyPrompt();

        PickupThrow.Instance.itemPickupDestroyPrompt = true;

        Debug.Log("Picking up item." + itemProperties.name);


       // equipmentManager.InventoryGameObjects.Add(GameObject.);//Add Item to equipment Manager List

        // PickupText.gameObject.SetActive(false);
        bool wasPickedUp = Inventory.instance.Add(itemProperties);

         if (wasPickedUp)
        {
        
                                     //  PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = "";
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
       
    }

    



}


