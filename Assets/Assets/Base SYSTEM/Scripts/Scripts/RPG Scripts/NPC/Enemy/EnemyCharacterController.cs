using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Handles interaction with the Enemy */

[RequireComponent(typeof(CharacterStats))]

public class EnemyCharacterController : MonoBehaviour {

    PlayerManager playerManager;
    CharacterStats myStats;
    public static bool StartEnemyAttack;
    public static bool WithinEnemyAttackRange;
    static Animator anim;

    // Use this for initialization
    void Start () 
    {
        //playerManager = PlayerManager.instance; //Rerence to the player
        //myStats = GetComponent<CharacterStats>();

    }
	
	// Update is called once per frame
	void Update () 
    {





        //if (WithinEnemyAttackRange == true)
        //{
        //StartEnemyAttack = true;
        //}



    }


    //public void WalkingAnim()
    //{
    //    //anim.SetBool("isWalking", true);
    //    //anim.SetBool("isAwake", false);
    //    //anim.SetBool("isIdle", false);
    //    //anim.SetBool("isIdle", false);
    //    //anim.SetBool("isHit", false);
    //    //anim.SetInteger("atk", 0);
    //}
}
