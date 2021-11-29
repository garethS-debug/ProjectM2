using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : MonoBehaviour
{
    private Animator anim;
    private Inventory inventoryManager;
    private EquipmentManager currentequipment;
    private GameObject gamecontroller;

    private bool isVaultKey;

    //public GameObject colliderPivot;
    private Vector3 currentRotation;
    //  public Transform VaultCollierEndPOS;

    public GameObject vaultDoorCollider;
    private Animator vaultDoorAnim;

    // Start is called before the first frame update
    void Start()
    {
        gamecontroller = GameObject.FindGameObjectWithTag("GameController");
        inventoryManager = gamecontroller.gameObject.GetComponent<Inventory>();
        currentequipment = gamecontroller.gameObject.GetComponent<EquipmentManager>();
        anim = this.gameObject.GetComponent<Animator>();
        vaultDoorAnim = vaultDoorCollider.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            foreach (ItemBluePrint item in inventoryManager.items)
            {
                if (item.isVaultKey == true)
                {
                    print("OPEN THE VAULT");
                    anim.SetBool("isOpen", true);
                    vaultDoorAnim.SetBool("isOpen", true);
                    ////colliderPivot.transform.eulerAngles = new Vector3(0, 90, 90);
                    //colliderPivot.transform.rotation = Quaternion.Lerp(colliderPivot.transform.rotation, VaultCollierEndPOS.transform.rotation, 1.0f * Time.deltaTime);
                    ////  colliderPivot.transform.position = VaultCollierEndPOS.transform.position;
                    //colliderPivot.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                }
                break;
            }



            if (currentequipment.currentEquipment != null)
            {
                foreach (Equipment elem in currentequipment.currentEquipment)
                {
                    if (elem == null)
                    {
                        print("current equipment: Null");
                        break;
                    }

                    if (elem != null)
                    {
                        print("current equipment: Not Null");
                        for (int i = 0; i < currentequipment.currentEquipment.Length; i++)
                        {
                            print("current equipment: Checking list");

                            if (currentequipment.currentEquipment[i].isVaultKey == true)
                            {
                                print("current equipment: VaultKey");
                                print("VAULT: ACTIVATE"); // dot.SetActive(true);
                                anim.SetBool("isOpen", true);
                                vaultDoorAnim.SetBool("isOpen", true);
                            }
                            else
                            {
                                break;
                                //print("VAULT: DONT ACTIVATE");//  dot.SetActive(false);
                            }

                            break;
                        }
                        break;
                    }

                    break;
                }

                  //  print("current equipment: Not Null");

                
            }

           



            //foreach (ItemBluePrint equipeditem in currentequipment.currentEquipment)
            //{
            //    if (currentequipment.currentEquipment.Length > -1) 
            //    {
            //        if (equipeditem.isVaultKey == true)
            //        {
            //            print("OPEN THE VAULT");
            //            anim.SetBool("isOpen", true);
            //            vaultDoorAnim.SetBool("isOpen", true);
            //        }


            //    }
            //    else
            //    {
            //        break;
            //    }

            //    break;
            //}

        }
    }

    private void OnTriggerExit(Collider other)
    {

        //   print("Waypoint:" + other.name);

        if (other.gameObject.tag == "Player")
        {

           
        }
    }
}
