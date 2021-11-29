using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    //--------> Script for Player Weapon Hitting Enemy
    static Animator anim;

    Collider m_Collider;
    public Combat combat; // reference to Enemy Attacking Player
    CharacterStats PlayersStats;
    CharacterStats dealingDamage;
    Combat MainCharacterCombat;
    Combat EnemyCombat;
    //Reference to the enemy
    private GameObject TheMainCharacter;
    MurphyMovement murphyScript;
    


    //public GameObject ThePlayer;
    //private EnemyFOV Enemysight;

    //private CharacterStats TheEnemysStats;    //Here we reference the enemies stats
    //private CharacterStats ThePlayersStates; //Here we reference the players own stats
    //private Combat PlayersCombatScript;
    ////Animation
    //public Animator anim;

    //////Reference to other script
    ////CharacterCombat combat; // reference to Enemy Attacking Player
    ////CharacterStats EnemyStats;
    ////FieldOfView Playersight;
    //CharacterStats dealingDamagetoEnemy;


    // Use this for initialization
    void Start () 
    {

                                                    //Find The player
        TheMainCharacter = GameObject.FindGameObjectWithTag("Player");
                                                    //Set the charactercombat script to the one on the player.
        MainCharacterCombat = TheMainCharacter.GetComponent<Combat>();
        m_Collider = GetComponent<BoxCollider>();
        murphyScript = TheMainCharacter.gameObject.GetComponent<MurphyMovement>();

        ////Get the Players combat script
        //PlayersCombatScript = ThePlayer.gameObject.GetComponent<Combat>();


        //Playersight = gameObject.GetComponentInParent<FieldOfView>();
        //combat = gameObject.GetComponentInParent<CharacterCombat>(); // reference to Enemy Attacking Player
    }
	
	// Update is called once per frame
	void Update () 
    {

        //if (enemyChaseScript.enabledPlayerWeaponCollider == true)
        //{
        //    // m_Collider.enabled = !m_Collider.enabled;
        //    m_Collider.enabled = true;

        //}
        //else if (enemyChaseScript.enabledPlayerWeaponCollider == false)
        //{
        //    // m_Collider.enabled = !m_Collider.enabled;
        //    m_Collider.enabled = false;

        //}

    }

    //IF THE PLAYER WEAPON COLLIDES WITH A Enemy


    private void OnTriggerEnter(Collider collision)
    {

        //                                                            //IF THE Enemy WEAPON COLLIDES WITH A The Enemy. Gather its stats
        //TheEnemysStats = collision.gameObject.GetComponent<CharacterStats>();
        //                                                            //Now we want to pass this information to this Player's Combat Script
        //PlayersCombatScript.TheEnemyStats = TheEnemysStats;

        if (collision.gameObject.tag == "Enemy")
        {
                                                            //Setting the Enemy GO as this on on this instance.
            MainCharacterCombat.Enemy = collision.gameObject;
                                                            //reference to the enemy combat. 
             EnemyCombat = collision.gameObject.GetComponent<Combat>();
             Debug.Log("Enemy Hit By" + this.gameObject.transform.name);
                                                                        //Here we set the combat script enemy to be this one we just hit. So we only deal damage to them. 
            anim = collision.gameObject.GetComponent<Animator>();
            StartCoroutine("Damage");
            





            //combat.PlyerTakesDamage = true;
            // Enemysight = collision.gameObject.GetComponent<EnemyFOV>();
            // EnemyStats = Playersight.EnemyStats;
            // combat.Attack(EnemyStats); // reference to Enemy Attacking Player
            //// CharacterCombat.dealingDamage = true;
            // dealingDamagetoEnemy = collision.gameObject.GetComponent<CharacterStats>();
            // dealingDamagetoEnemy.EnemyTakesDamage = true;
        }


        else
        {

        }

    }

    public void OnTriggerExit(Collider collision)
    {

        //IF THE Enemy WEAPON COLLIDES WITH A PLAYER

        if (collision.gameObject.tag == "Enemy")
        {
            EnemyCombat.dealingDamage = false;
        }
        
    }

    IEnumerator Damage()
    {
        Debug.Log("Set Hit Bool to WOW");
        anim.SetBool("isHit", true);
        EnemyCombat.dealingDamage = true;
        yield return new WaitForSeconds(0.01f);
        EnemyCombat.dealingDamage = false;
        anim.SetBool("isHit", false);

    }
}
