using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    /*
     * This script is for dealing with damage & hit animations
     */

    //Animator
    static Animator anim;

    [Header("Control Bools")]
    [Tooltip("Bools for controling Damage")]
    //Bool
    public bool dealingDamage;
    public bool damageTimer;
    public bool EnemyTakesDamage;
    public bool playerTakesDamage;
    // public bool PlyerTakesDamage;
    [Space(10)]


    //Reference to script
    private CharacterStats ThePlayer;
    private CharacterStats TheEnemy;
    private MurphyPlayerManager playerManager;
    private Chase chase;
    EnemyFOV enemyFOV;

    [Header("Reference to Player / Enemy")]
    [Tooltip("References to the player / enemy")]

    //Reference to the player
  [HideInInspector]  public GameObject Player;
    [Tooltip("Reference this enemy character here")]
    public GameObject Enemy;
    //Damage
    public int damage;

    [Space(10)]

    //Timers
    private float CoolDwnTimerMax;
    [Tooltip("This is the timer between attacks")]
    public float AtkCoolDownTimer;

    // Start is called before the first frame update
    void Start()
    {

        //Referencing the global GO of player
//        Player = playerManager.player;
        //Informing that 'The Player' refers to that characters Stats
      


        if (Enemy != null)
        {
            //THIS NEEDS NEEDS to be passed from the weapon. 
            TheEnemy = Enemy.gameObject.GetComponent<CharacterStats>();
        }
       

        if (this.gameObject.tag == "Enemy")
        {
         
            TheEnemy = this.gameObject.GetComponent<CharacterStats>();
            print("I AM THE ENEMY" + TheEnemy.gameObject.name);
        }


        chase = this.gameObject.GetComponent<Chase>();

        //set up the timers
        CoolDwnTimerMax = AtkCoolDownTimer;

        //Get the animator
        anim = GetComponent<Animator>(); // get the animator component attached to the character
    }

    // Update is called once per frame
    void Update()
    {


        //If the instance of Chase on this gameobject i.e if THIS character is attacking
        if (damageTimer == true)
        {

            if (AtkCoolDownTimer <= 0)
            {
                chase.isAttacking = true;                 //Set attack to true
                
                AtkCoolDownTimer = CoolDwnTimerMax;     // reset the timer.
            }

            else
            {
                AtkCoolDownTimer -= Time.deltaTime;
                chase.isAttacking = false;
            }


        }

        // ---- ADD TIMER FOR ATTACKS
        //Here one character (sec guard) is dealing damage to another (player)
        if (this.gameObject.tag == "Enemy" & dealingDamage == true)
        {
            ThePlayer = this.gameObject.GetComponent<StateController>().playerInAttackRange.gameObject.GetComponent<CharacterStats>();

            if (ThePlayer != null)
            {
                //Here we want to say that damage is equal to this characters damage stats
                damage = this.gameObject.GetComponent<CharacterStats>().damage.GetValue();

                //  HitCounter = damage / 10;

                ThePlayer.PlayerTakesDamage = true;
                ThePlayer.DamageRecieved = damage;
                ThePlayer.TakeDamage(damage);
                dealingDamage = false;
            }
       
        }

        if (this.gameObject.tag == "Player" & dealingDamage == true)
        {
          
            print("Error Code Y");
            //Here we want to say that damage is equal to this characters damage stats
            damage = this.gameObject.GetComponent<CharacterStats>().damage.GetValue();
            TheEnemy.PlayerTakesDamage = true;
            TheEnemy.DamageRecieved = damage;
            TheEnemy.TakeDamage(damage);
            dealingDamage = false;
        }

        //if (this.gameObject.tag == "Player")
        //{
        //    if (PlyerTakesDamage == true)
        //    {
        //        anim.SetBool("isHit", true);
        //    }

        //    else
        //    {
        //        anim.SetBool("isHit", false);
        //    }

        //}



    }
}
