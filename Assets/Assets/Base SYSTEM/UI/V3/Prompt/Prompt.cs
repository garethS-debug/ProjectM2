using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Prompt : MonoBehaviour
{
    private DistractableOBJ promptInfo;
    private ItemPickup2 promptText;
    private ItemPickup2 itemPickup2;
    private TextMeshProUGUI text;

    public void Start()
    {
        text = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUpOBJ" || other.gameObject.tag == "Distraction" )
        {
            if (other.gameObject.GetComponent<DistractableOBJ>() != null)
            {
                promptInfo = other.gameObject.GetComponent<DistractableOBJ>();

                if (promptInfo.PromptText != null)
                {
                    text.text = promptInfo.PromptText;
                }

            }

            if (other.gameObject.GetComponent<ItemPickup2>() )
            {
                promptText = other.gameObject.GetComponent<ItemPickup2>();

                if (promptText.PromptText != null)
                {
                    text.text = promptText.PromptText;
                }
            }


        }


        if (other.gameObject.tag == "HideBody")
        {

            promptInfo = other.gameObject.GetComponent<DistractableOBJ>();

            if (promptInfo.PromptText != null)
            {
                text.text = promptInfo.PromptText;
            }

            if (promptInfo.PromptText == null)
            {
                text.text = promptInfo.PromptText;
            }

        }



        if (other.gameObject.tag == "PickupBody")
        {
            
                promptInfo = other.gameObject.GetComponent<DistractableOBJ>();

                if (promptInfo.PromptText != null)
                {
                    text.text = promptInfo.PromptText;
                }

            if (promptInfo.PromptText == null)
            {
                text.text = promptInfo.PromptText;
            }

        }

        if (other.gameObject.tag == "InventoryItem")
        {
            itemPickup2 = other.gameObject.GetComponent<ItemPickup2>();
            text.text = itemPickup2.PickUpObjText;
        }



    }


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Door")
        {
            //  print("I am a door");
           // print("I am a door: I can say " + other.gameObject.GetComponent<DoorChecker>().PromptText);

            if (other.gameObject.GetComponent<DoorChecker>().isEventCompleted == false)
            {
                print("I am a door: I can say " + other.gameObject.GetComponent<DoorChecker>().PromptText);
                text.text = other.gameObject.GetComponent<DoorChecker>().PromptText;
            }
        }

    }


}
