using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemPrompt : MonoBehaviour
{

    [Header("PikcupButton")]
    
    public TextMeshProUGUI PickUpButton;


    // Start is called before the first frame update
    void Start()
    {
        PickUpButton.text = PickupThrow.Instance.PickUpButton.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
