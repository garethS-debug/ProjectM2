using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ENEMY WEAPON HITTING PLAYER
//--------> Script for Enemy Weapon Hitting the Player
public class EnemyWeapon : MonoBehaviour {

    //Box Collider
    Collider m_Collider;

    //Reference to other script
    Combat combat; // reference to Enemy Attacking Player
    CharacterStats PlayersStats;
    Chase enemyChaseScript;

    CharacterStats dealingDamage;

    [Tooltip("Reference to the player")]
    public GameObject Player;

    [Tooltip("Reference to this enemy")]
    public GameObject thisEnemy;

    //Animation
    static Animator anim;

    public void Awake()
    {
         enemyChaseScript = thisEnemy.gameObject.GetComponent<Chase>();
    }
    public void Start()
    {
        combat =        Player.GetComponent<Combat>(); // reference to Enemy Attacking Player
       
        enemyChaseScript = thisEnemy.GetComponent<Chase>();

        //Getting the gae objects collider 
        m_Collider = GetComponent<BoxCollider>();
    }

    public void Update()
    {

       //if (enemyChaseScript.enabledWeaponCollider == true)
       // {
       //     // m_Collider.enabled = !m_Collider.enabled;
       //     m_Collider.enabled = true;
          
       // }
       // else if (enemyChaseScript.enabledWeaponCollider == false)
       // {
       //     // m_Collider.enabled = !m_Collider.enabled;
       //     m_Collider.enabled = false;

       // }


    }

    public void OnTriggerEnter(Collider collision)
    {

        //IF THE Enemy WEAPON COLLIDES WITH A PLAYER

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("MAIN CHARACTER HIT BY" + this.gameObject.transform.name);
            anim = collision.gameObject.GetComponent<Animator>();
            StartCoroutine("Damage");


            //combat.PlyerTakesDamage = true;

            ////This is the player's stats from EnemyFOV Script (PlayerStats = target.GetComponent<CharacterStats>();)
            //PlayersStats = Enemysight.PlayerStats;
            ////In character combat script 'Attack' the players' stats.
            //combat.Attack(PlayersStats); // reference to Enemy Attacking Player
            ////Set Dealing Damage = true
            //combat.dealingDamage = true;

            ////Referncing the character stats on the player
            //dealingDamage = collision.gameObject.GetComponent<CharacterStats>();
            //dealingDamage.PlayerTakesDamage = true;
            // m_Collider.enabled = !m_Collider.enabled;

        }




        else
        {

        }
    }

    public void OnTriggerExit(Collider collision)
    {

        //IF THE Enemy WEAPON COLLIDES WITH A PLAYER

        if (collision.gameObject.tag == "Player")
        {
            combat.dealingDamage = false;
        }
           
    }

 

    IEnumerator Damage()
    {
        anim.SetBool("isHit", true);
        combat.dealingDamage = true;
        yield return new WaitForSeconds(0.01f);
        combat.dealingDamage = false;
        anim.SetBool("isHit", false);

    }



    }





