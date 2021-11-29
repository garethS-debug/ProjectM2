using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This resorts combat for all characters. */
// this sits on ALL our characters
// So if Enenmy Attacks player it will

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    
    public event System.Action OnAttack; //???
   
    //float
    public float attackDelay = .6f;
    public float attackSpeed = 1f;  //set an attack speed, influenced by char stats

    //int
    public int damageDealt;

    // Bool
    public bool dealingDamage;

    //reference to other scripts
    CharacterStats myStats;         //Reference to this character's stats
    CharacterStats takeDamage;      //Taking damage


    void Start()
    {
        myStats = GetComponent<CharacterStats>();       // reference to this character's stats. 
        takeDamage = GetComponent<CharacterStats>();    // reference to this character's stats. 
    }


    #region Attacking
    //PUBLIC METHOD FOR ATTACKING. 
    public void Attack(CharacterStats targetStats) //this will take in stats of the character we want to attack. 
    {
        if (dealingDamage == true)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
                OnAttack();
            //attackCooldown = 1f / attackSpeed;

        }

        else if (dealingDamage == false)
        {
            StopCoroutine("DoDamage");
        }
    }
    #endregion


    //THIS IS CONSTANTLY PLAYING IN THE BACKGROUND
    IEnumerator DoDamage(CharacterStats stats, float attackdelay)
    {
        if (dealingDamage == true)
        {
            Debug.Log("Start Combat");
            yield return new WaitForSeconds(0.1f);
            stats.TakeDamage(myStats.damage.GetValue()); //dealing damage (take damage) based on 'myStats'
            Debug.Log(transform.name + " swings for " + myStats.damage.GetValue() + " damage");

            //WE HAVE THE ATTACKERS NAME AND DAMAGE DEALT
            //WE NEED KNOW WHO THE VICTIM IS SO THAT WE CAN PASS THE DAMAGE TO 'CHARACTER STATS'
            //storing the damage dealt
            damageDealt = myStats.damage.GetValue();
            dealingDamage = false;
      

            //We have the ENEMIES NAME AND DAMAGE. NOW WE NEED THE TARGET
            //myStats.damage.GetValue() = The damage that this object can deal 
            //stats.TakeDamage is sapposed to be the 'take damage' function in the 'Character Stats' script. 
            //transform.name = this characters deets
        }
    }



}
