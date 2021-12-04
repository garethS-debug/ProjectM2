using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Handles interaction with the Enemy */
/* This makes our enemy interactable. */


[RequireComponent(typeof(CharacterStats))]


public class Enemy : Interacting
{
    MurphyPlayerManager playerManager;
    CharacterStats myStats;
    public static bool StartEnemyAttack = false;

    static Animator anim;

    void Start()
    {
        playerManager = MurphyPlayerManager.instance; //Rerence to the player
        myStats = GetComponent<CharacterStats>();
    }



    //THIS NEEDS A FIXING ------- > becuase the 'interaction' script is broken. 
    //THIS IS NOT BEING DETECTED


    //COMBAT SYSTEM------->  /*MAIN CHARACTER ATTACK SYSTEM FOR AN ENEMY*/
    public override void Interact() // when we interact with the enemy. 
    {
        if (Input.GetKeyDown(KeyCode.Y)  && CombatInteracting == true) // when Keypress down Y & Main Char Has LOS
        {
            CharacterStats.damageTrigger = true; //THIS IS THE BOOL FOR DOING DAMANGE TO A CHARACTER
            print("Y was pressed"); 
            base.Interact(); //Interact Method from interaction.cs
            CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>(); //Initiate combat?
            playerCombat.Attack(myStats); //attacking Enemy stats
            Chase.damageAnim = true;
        }
        //COMBAT SYSTEM------->  /*MAIN CHARACTER ATTACK SYSTEM FOR AN ENEMY*/









        //if (Vector3.Distance(player.position, this.transform.position) <= InteractRadius)
        //{
        //    StartEnemyAttack = true;
        //}

    }








    /*END OF MAIN CHARACTER ATTACKS ENEMY*/

    /*
   CharacterStats myStats; // enemies stats
   PlayerManager playerManager; // 
   //TEST
   CharacterCombat combat; // reference to Enemy Attacking Player
   Transform target;   // Reference to the player

   void Start()
   {
       playerManager = PlayerManager.instance;
       myStats = GetComponent<CharacterStats>(); //The enemies stats
       myStats.OnHealthReachedZero += Die;
       //test
       combat = GetComponent<CharacterCombat>(); // reference to Enemy Attacking Player
       target = PlayerManager.instance.player.transform;
   }


   // When we interact with the enemy: We attack it.
   public override void Interact()
   {
       base.Interact();
       StartCoroutine(Detectingenemyhit());
       StartCoroutine(Detectingplayerhit());
   }


   IEnumerator Detectingenemyhit()
   {
       //METHOD for ENEMY ATTACKING the PLAYER
       while (DetectHit.enemyhitDetected == true)
       {
           //Debug.Log("Enemy Hit Detected. Attacking Stats");
           //print("DetectHit.enemyhitDetected == true");
           //test
           CharacterStats targetStats = target.GetComponent<CharacterStats>(); // reference to Enemy Attacking Player
           combat.Attack(targetStats); // reference to Enemy Attacking Player
           yield return null;
       }
   }

   IEnumerator Detectingplayerhit()
   {
       //Main Character attacking the enemy and dmanagin stats
       //THIS IS THE MAIN SWITCHBOARD FOR ATTACKING STATS
       while (DetectHit.playerhitDetected == true)
       {
           print("DetectHit.playerhitDetected == true");
           //print("DetectHit.playerhitDetected == true");
           //Debug.Log("Player Hit Detected. Attacking Stats");
           //THIS IS WHERE THE ISSUE IS
           CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
           playerCombat.Attack(myStats); // attacking target stats of the enemy myStats = Enemy Stats
           yield return null;

       }
   }


   void Die()
   {
       //DEAT METHOD

   }


}





   PlayerManager playerManager;
   CharacterStats myStats;
   //private bool InteractingWithEnemy;

   void Start()
   {
       playerManager = PlayerManager.instance;
       myStats = GetComponent<CharacterStats>();
   }

   private void Update()
   {


   }

   #region //Collision Detection function

    private void OnTriggerEnter(Collider collider)
   {
       //collider = Colider of the game object
       if (collider.gameObject.tag == "Player")
       {
           InteractingWithEnemy = true;
       }
   }

   #endregion


   public override void Interact() // whenever we interact with an enemy we want to attack it. 
   {

       base.Interact();
       CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
       if (playerCombat != null)
       {
           playerCombat.Attack(myStats);
       }
   }

   */

}