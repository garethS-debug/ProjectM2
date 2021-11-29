using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractableOBJ : MonoBehaviour
{

    public bool TurnOriginalMaterial;
    public Material OriginalMaterial;


    [Header("Prompt Material")]
    public string PromptText;

    public bool TurnUndistractable;

    [Header("Prompt Material")]
    [HideInInspector] public GameObject dumpLocation;

    // Start is called before the first frame update
    void Start()
    {
        dumpLocation = this.gameObject;

        TurnOriginalMaterial = false;

        if (gameObject.GetComponent<MeshRenderer>() != false)
        {
            OriginalMaterial = this.gameObject.GetComponent<MeshRenderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnUndistractable == true)   //Turn off TAG 
        {
            this.gameObject.tag = "PickUpOBJ";
            this.gameObject.layer = 0;
        }

        if (TurnOriginalMaterial == true)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = OriginalMaterial;
            TurnOriginalMaterial = false;
        }
    }

  


    }
