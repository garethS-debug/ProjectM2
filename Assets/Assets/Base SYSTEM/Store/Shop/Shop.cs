using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop instance;



    [Header("Shop Items")]
    [Tooltip("Create Items and place them in here")]
    public List<ItemBluePrint> shopItems = new List<ItemBluePrint>();



    private bool isScoreHighEnough;

    //[Header("Cameras")]
    //public GameObject MainCamera;
    //public GameObject ShopCamera;

    [Header("ShopUI")]
    public GameObject ShopUI;
    public Image itemImage;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI ItemPrice;
    public Button BuyButton;
    public TextMeshProUGUI globalCash;

    public ItemBluePrint tempItem;

    [Header("Spawn")]
    public GameObject SpawnPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        itemImage.gameObject.SetActive(false);
        ItemName.text = null;
        ItemDescription.text = null;
        ItemPrice.SetText("");
        BuyButton.gameObject.SetActive(false);
        globalCash.text = InformationToSave.instance.savedInformation.GlobalTotalCash.ToString();
    }

    //Check to see if there is enough 'Money'
    public void CheckScore(ItemBluePrint iteminfo)
    {
        //STORE GLOBAL CASH AMOUNT / TOTAL SCORE IN ITS OWN LIST


        //foreach (HeistLevelListInfo heistLevelInfo in InformationToSave.instance.savedInformation.HeistLevel)
        //{
            if (InformationToSave.instance.savedInformation.GlobalTotalCash >= iteminfo.itemPrice)
            {
                print("You Have Enough");
                BuyButton.gameObject.SetActive(true);
                isScoreHighEnough = true;
                
            }


            else if (InformationToSave.instance.savedInformation.GlobalTotalCash < iteminfo.itemPrice)
            {
                BuyButton.gameObject.SetActive(false);
                isScoreHighEnough = false;
                Debug.Log("Get more cash");
            }
          
            
        //}


        
    }


    //Spawn Purchased Item
    public void PurchasedItem(GameObject purchasedItem)
    {
        Debug.Log("ItemPurchased");
    }





    //Button Click
    public void ClickToDisplayItem(ItemBluePrint itemInformation)
    {
        tempItem = itemInformation ;

        CheckScore(itemInformation);

        itemImage.gameObject.SetActive(true);
        Debug.Log("Click Registered" + itemInformation.name);
        itemImage.sprite = itemInformation.icon;
        ItemName.text = itemInformation.name;
        ItemDescription.text = itemInformation.itemDescription;
        ItemPrice.SetText("Price: $" + itemInformation.itemPrice.ToString());
        
    }



    //Button Click
    public void ClickToPurchaseGadget()
    {
        Debug.Log("Click Registered");

        CheckScore(tempItem);

        if (isScoreHighEnough == true)
        {
            InformationToSave.instance.savedInformation.GlobalTotalCash -= tempItem.itemPrice;
            Debug.Log("ItemPurchased");
            Instantiate(tempItem.ItemGameObj, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
            ShopUI.gameObject.SetActive(false);
            globalCash.text = InformationToSave.instance.savedInformation.GlobalTotalCash.ToString();
        }

    }

    public void OpenShop()
    {
        ShopUI.SetActive(true);
    }

    public void CloseShop()
    {
        ShopUI.SetActive(false);
    }

    //private void OnTriggerEnter(Collider collider)
    //{
    //    //collider = Colider of the game object
    //    if ((collider.gameObject.tag == "Player"))
    //    {
    //        //MainCamera.SetActive(false);
    //        //ShopCamera.SetActive(true);
    //        ShopUI.SetActive(true);
    //    }
    //}

    ////private void OnTriggerStay(Collider collider)
    ////{
    ////    //collider = Colider of the game object
    ////    if ((collider.gameObject.tag == "Player"))
    ////    {
    ////        ShopUI.SetActive(true);
    ////    }
    ////}

    private void OnTriggerExit(Collider collider)
    {
        //collider = Colider of the game object
        if ((collider.gameObject.tag == "Player"))
        {
            //MainCamera.SetActive(true);
            //ShopCamera.SetActive(false);

            ShopUI.SetActive(false);
        }
    }
}
