using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using UnityEngine;

public class DoorChecker : MonoBehaviour
{
    [Header("What door is this")]
    [Tooltip("Determine what door this is?")]
    public bool issingleDoor;
    public bool isdoubleDoorR;
    public bool isdoubleDoorL;


    [Header("Dingle Door GO")]
    [Tooltip("Go for single door?")]
    public GameObject singleDoorGO;

    [Header("Double Door GO")]
    [Tooltip("Go for Double door?")]
    public GameObject DoubleDoorGOL;
    public GameObject DoubleDoorGOR;


    [Header("State")]
    public bool isOpen;


    [Header("Speed")]
    public float openSpeed;

    public Transform OpenTartget;
    public Transform ClosedTarget;


   
    [HideInInspector] public bool isEventCompleted;
    [Header("Event Check")]
    public bool isItemEquiped;
    public bool isEventCheck;
    public LootableItem eventItem;
    public Equipment requiredEquipedItem;

    [Header("Prompt")]
    public string PromptText;

    private bool okToProceed;
    private bool OpenDoor;
    private bool closeDoor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (OpenDoor == true)
        {
            if (issingleDoor == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, OpenTartget.position, openSpeed);

            }

            if (isdoubleDoorL == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, OpenTartget.position, openSpeed);

            }

            if (isdoubleDoorR == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, OpenTartget.position, openSpeed);
            }

        }

        if (closeDoor == true)
        {

        }

        if (isEventCheck == true)
        {

            if (eventItem.startEvent == true)
            {
                OpenDoor = true;
            }

        }

    }


    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {

            CheckForRequirement();

            if (okToProceed == true)
            {
                OpenDoor = true;
              
            }

        }
    }


    public void CheckForRequirement()
    {

        if (isItemEquiped == true)
        {
            for (int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
            {
                
                if (EquipmentManager.instance.currentEquipment[i] == requiredEquipedItem)
                {
                    isEventCompleted = true;
                    okToProceed = true;
                    break;
                }
            }
        }

     

    }

    //private void OnTriggerExit(Collider collider)
    //{

    //    if (collider.gameObject.tag == "Player")
    //    {
    //        if (issingleDoor == true)
    //        {
    //            transform.position = Vector3.MoveTowards(transform.position, ClosedTarget.position, openSpeed);
    //        }

    //        if (isdoubleDoorL == true)
    //        {
    //            transform.position = Vector3.MoveTowards(transform.position, ClosedTarget.position, openSpeed);
    //        }

    //        if (isdoubleDoorR == true)
    //        {
    //            transform.position = Vector3.MoveTowards(transform.position, ClosedTarget.position, openSpeed);
    //        }
    //    }

    //}
}
