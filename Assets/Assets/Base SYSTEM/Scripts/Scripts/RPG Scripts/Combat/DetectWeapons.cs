using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS THE SCRIPT ON THE ENEMY FOR DETECTING PLAYER HITS

public class DetectWeapons : MonoBehaviour
{

    //Float
    public float hitCooldown;
    public float hitCooldownMax;
    public static int hitCounter;
    public int hitCntr;
    public int hitCntrDflt;
    public int hitsB4Dodge;

    //BOOL
    public static bool enemyhitDetected = false;
    public static bool playerhitDetected = false;
    public bool starthitCooldown;

    //OTHER
    public GameObject playerObject;
    public Transform player;                        // declare the character. 


    //COMBAT 
    PlayerManager playerManager;
    CharacterStats myStats;
    public static bool StartEnemyAttack = false;
    static Animator anim;


    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerhitDetected = false;
        enemyhitDetected = false;

        //COMBAT
        playerManager = PlayerManager.instance; //Rerence to the player
        myStats = GetComponent<CharacterStats>();

        //Animator
        anim = GetComponent<Animator>(); // get the animator component attached to enemy

        //timer
        hitCooldown = hitCooldownMax;

        //hitCounter
        hitCounter = hitCntrDflt;
    }

    void Update()
    {
        hitCntr = hitCounter;

        if (anim.GetBool("isHit") == true)
        {
            //Make sure hit resets
                starthitCooldown = true;
                hitCooldown -= hitCooldownMax/3;
                if (hitCooldown <= 0)
                {
                    anim.SetBool("isHit", false);
                    hitCooldown = hitCooldownMax;
                    starthitCooldown = false;
                }
            //Force the enemy to stop attacking

            //counting how many hits


        }

        //If hit too many times then dodge
        if (hitCounter >= hitsB4Dodge)
        {
            EnemyAIController.dodging = true;
            anim.SetBool("isHit", false);
            hitCounter = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {




        if (other.gameObject.tag == "PlayerWeapon")
        {
//            print("Sword HIT");
            anim.SetBool("isHit", true);

            hitCounter++;
            Debug.Log("hit no." + hitCounter);

            PlayerAttack();

        }
        else
        {
            playerhitDetected = false;
            anim.SetBool("isHit", false);
        }

    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "PlayerWeapon")
    //    {
    //        print("Sword HIT");
    //        anim.SetBool("isHit", true);
    //        PlayerAttack();

    //    }
    //    else
    //    {
    //        playerhitDetected = false;
    //        anim.SetBool("isHit", false);
    //    }

    //}


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {

            anim.SetBool("isHit", false);

        }

    }



    public void PlayerAttack ()
    {
     
        Debug.Log("MAIN CHARACTER HAS HIT THE ENEMY");

        playerhitDetected = true;              //Player hit is true
        //CharacterStats.damageTrigger = true;   //attack the enemies stats. THIS IS THE BOOL FOR DOING DAMANGE TO A CHARACTER
                                               // WAITING FOR SECONDS
                                               // THEN SWITCH 
                                               //enemyhitDetected = false;
        //CharacterStats.damageTrigger = true; //
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>(); //Initiate combat?
        playerCombat.Attack(myStats); //attacking Enemy Character stats

        //Chase.damageAnim = true;
    }


}









    