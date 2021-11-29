using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//THIS IS THE SCRIPT ON THE PLAYER FOR DETECTING ENEMY HITS

public class DetectHit : MonoBehaviour {



    //Bool
    public static bool enemyhitDetected = false;

    //Reference to other scripts 
   



    //INT

    public static string weaponDetected;  //reference to enemyID




    public void Start()
    {
        //weaponStats = WeaponsMaster.GetComponent<CharacterStats>();
    }


    private void OnTriggerEnter(Collider collision)
    {

        //NEED TO ALSO CHECK I THE COLLIDER IS WITHIN ANOTHER COLLIDER 
        // BECAUSE THE COLLIDER ONLY DETECTS IMPACT ON ETNERING. 
        //if (collider.bounds.Intersects(currentHeader.boxCollider.bounds)

                
        if (collision.gameObject.tag == "EnemyWeapon")
        {

            //THIS TELLS US THAT THE MAIN CHARACTER HAS BEEN HIT
             enemyhitDetected = true; //THIS IS THE BOOL FOR DOING DAMANGE TO THE M_CHARACTER
             Debug.Log("enemy Weapon Hit" + transform.name);
           
            //THIS TELLS US THE NAME OF THE weapon attacking the Main Character
            weaponDetected = collision.gameObject.transform.name;
        
  
        }


        else 
        {
           // playerhitDetected = false;
            enemyhitDetected = false;
            CharacterStats.damageTrigger = false; //THIS IS THE BOOL FOR DOING DAMANGE TO A CHARACTER
        }

    }

    private void OnTriggerExit(Collider collision)
    {

       // CharacterCombat.dealingDamage = false;
        CharacterStats.damageTrigger = false;    //THIS IS THE BOOL FOR DOING DAMANGE TO THE M_CHARACTER
         //  enemyhitDetected = false;
       // weaponDetected = "";

    }


   

}






