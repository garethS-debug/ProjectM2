using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour {

    //-------> Internal Save Hooks
    //Save Player Health
    public float PlayerHealth; //Players Health
   


    //------->Reference to Scripts / player
    public GameObject player;
    public HealthUI healthUI;

    public void Start()
    {
        //Reference to Player
        player = GameObject.FindGameObjectWithTag("Player");

        //Reference to HealthUI
        healthUI = player.GetComponent<HealthUI>();
    }

    // Update is called once per frame
    void Update () 
    {
        //Player Health
        PlayerHealth = healthUI.currentHealth;

    }
}
