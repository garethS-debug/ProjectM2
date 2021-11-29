using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAttack : MonoBehaviour {



    static Animator anim;
    public KeyCode AttackButton = KeyCode.Y;


    //bool
    public static bool isAttacking = false;
    private bool startComboTimer;
    public bool CombiTimerBool;
    public static  bool CharacterAttacking;

    //int
    private int attackCombo;

    //float
    public float ComboDelay = 2.0f;
    public float CombiTimer;
    public float Delay = 1.0f;
    public float AttackDelay = 1.0f;
    public float defaultCombiTimer;
    public float CombiActionTimerDefault;
    public float CombiActionTimer;


    //int
    private int attackCounter;

    //TEST
    CharacterCombat combat;
    Transform target;   // Reference to the player



    // Use this for initialization
    void Start () 
    {
        anim = GetComponent<Animator>(); // get the animator component attached to player
        CombiTimer = defaultCombiTimer;
        CombiActionTimer = CombiActionTimerDefault;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (attackCounter > 0)
        {
            CharacterAttacking = true;
            print("CharacterAttackingBool");
        }

        if (attackCounter < 1)
        {
            CharacterAttacking = false;
        }

        #region Attacking
        if (Input.GetKeyDown(AttackButton))
        {
            if (CombiTimer == defaultCombiTimer)
            {
                startComboTimer = true;
                StartCoroutine("AttackEnemy");
//                print("Timer Started");      
            }

            CombiActionTimer = CombiActionTimerDefault;

        }

       
        //TIMER FOR STOPPING ATTACK BUTTON SPAM
        if (startComboTimer == true)
        {

            CombiTimer -= Time.deltaTime;

            if (CombiTimer <= 0)
            {
                CombiTimer = defaultCombiTimer;
                startComboTimer = false;
//                print("Next Y button press allowex");
            }
        }

        //TIMER FOR ENDING COMBINATION ATTACK AFTER IDLE TIME
        if (CombiTimerBool == true)
        {
            CombiActionTimer -= Time.deltaTime;

            if (CombiActionTimer <= 0)
            {
                attackCounter = 0;
                anim.SetInteger("AttackCombo", 0);
                CombiActionTimer = CombiActionTimerDefault;
                CombiTimerBool = false;
            }
        }


        else
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isIdle", true);
        }

        #endregion


    }

    #region Attacking

    IEnumerator AttackEnemy()
    {
        yield return new WaitForSeconds(0);
      
          isAttacking = true;
       // anim.SetBool("isAttacking", true);
       // StartCoroutine("ComboTrigger");


          ComboTrigger();


        //yield return new WaitForSeconds(ComboDelay);
        ////Reseting the attack
        //isAttacking = false;
        //attackCounter = 0;
        //anim.SetInteger("AttackCombo", 0);

    }
    #endregion

    public void ComboTrigger()
    {
        //attackCounter = 0;
        //This is the combo 
        #region Combo
       
            attackCounter++;
            print(attackCounter);

     
        #endregion

        if (attackCounter == 1)
        {
            CombiTimerBool = true;
            CombiActionTimer = 0.8f;
            anim.SetInteger("AttackCombo", 1);
            print("attackCombo 1");
        }

        if (attackCounter == 2)
        {
            CombiTimerBool = true;
            anim.SetInteger("AttackCombo", 2);
            print("attackCombo 2");
        }

        if (attackCounter == 3)
        {
            CombiTimerBool = true;
            anim.SetInteger("AttackCombo", 3);
            print("attackCombo 3");

        }


        if (attackCounter > 3)
        {
            attackCounter = 0;
            anim.SetInteger("AttackCombo", 0);
        }

     
    }



    #region Being Attacked_Gut_Hit1


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyWeapon")
        {

            StartCoroutine("TakeDamage");
        }
       // anim.SetBool("TakingDamage", false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "EnemyWeapon")
        {
            //anim.SetBool("isHit_Gut", true);
            anim.SetBool("TakingDamage", false);
           
        }

    }



    IEnumerator TakeDamage()
    {
      
            //anim.SetBool("isHit_Gut", true);
            anim.SetBool("TakingDamage", true);
            Bounce();
            yield return new WaitForSeconds(Delay);
            anim.SetBool("TakingDamage", false);


    }

    public void Bounce()
    {
        //while (anim.GetBool("TakingDamage", true))
        //{

        //}
        //ADD MORE DETAILED SCRIPT FOR BOUNCE AND ANIMATION FOR BOUNCE
        this.transform.position += this.transform.forward * -1 *3;

    }

    #endregion


}



