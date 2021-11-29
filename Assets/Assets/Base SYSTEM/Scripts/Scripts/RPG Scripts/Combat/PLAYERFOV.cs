using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERFOV : MonoBehaviour
{

    /*

    //CREATE SCRIPT FOR DETECTING THE PLAYERS FOV AND IF ITS INTERSECTING AN ENEMY

    private Transform enemy;

    // Use this for initialization
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        #region LOS
        Vector3 direction = enemy.position - this.transform.position; // getting direction to calculate angle 
        float angle = Vector3.Angle(direction, this.transform.forward);
        #endregion

        if (Vector3.Distance(enemy.position, this.transform.position) < DetectionRange && angle < FieldofVision) // if the distance between the player and the skeleton position is less than 10 
        { // calculate the diection from the player to direction.
            Debug.Log("Within Field of Vision");
        }
    }
    */
}
