using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealItem : MonoBehaviour
{

    public static bool RevealTheItem;

    [Header("InventoryRef")]
    [Tooltip("Reference to inventory Information")]
    public Equipment itemProperties;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }


    private void Update()
    {

        if (itemProperties.RenderMesh == false)
        {
            print("Falsey");
        }

        if (itemProperties.RenderMesh == true)
        {
            print("Truey");
        }




        if (itemProperties.RenderMesh == true)
        {
            Debug.Log("Reveal Item Whoosh");
            this.gameObject.SetActive(true);
        }

        if (itemProperties.RenderMesh == false)
        {
            Debug.Log("Un-Reveal Item Whoosh");
            this.gameObject.SetActive(false);
        }

    }

  
   



}
