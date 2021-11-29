using UnityEngine;
using UnityEngine.UI;


public class ItemPickup : Interacting
{
    public ItemBluePrint itemProperties;
    public ItemPickup item;   // Item to put in the inventory if picked up

   

    #region ItemUI
    //Game Object
    [SerializeField]
    public GameObject PickupText;
    public GameObject PickUpTextBox;
    //String
    public string PickUpObjText;

    //Bool 
    private bool pickUpAllowed;


    private void Start()
    {
      

        PickUpObjText = PickupText.gameObject.GetComponent<Text>().text;

        //if (PickupText != null)
        //{
        // //   PickupText.gameObject.SetActive(false);
        //}
    }

    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.P))// CHANGE THE PICKUP BUTTON HERE
            Pickup();

        PickUpTextBox = GameObject.FindGameObjectWithTag ( "ItemPickUpText" );

    }
    #endregion



    #region Colliders Detecting Player 
    //Collision Detection function
    //---------------------->
    private void OnTriggerEnter(Collider collider)
    {
        //collider = Colider of the game object
        if ( (collider.gameObject.tag == "Player"))
        {
            PickUpTextBox.gameObject.SetActive(true);
            PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = PickUpObjText;
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //collider = Colider of the game object
        if ( collider.gameObject.tag == "Player")
        {
            PickUpTextBox.gameObject.SetActive(false);
            pickUpAllowed = false;

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
        
        Debug.Log("Picking up item." + itemProperties.name);
       
       // PickupText.gameObject.SetActive(false);
        bool wasPickedUp = Inventory.instance.Add(itemProperties);
         if (wasPickedUp)
        {
           
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
       
    }

    



}


