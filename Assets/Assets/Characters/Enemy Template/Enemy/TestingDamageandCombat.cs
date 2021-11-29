using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDamageandCombat : MonoBehaviour
{

    public bool Enemy;
    public bool player;
    public bool takeDamage;


    public GameObject Player;
    public GameObject enemy;

    public int damage;

    private CharacterStats ThePlayer;
    private CharacterStats TheEnemy;

    private Animator anim;
    private Animator enenmyAnim;

    // Start is called before the first frame update
    void Start()
    {
        ThePlayer = Player.gameObject.GetComponent<CharacterStats>();
        TheEnemy = enemy.gameObject.GetComponent<CharacterStats>();

        enenmyAnim = TheEnemy.gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy == true && takeDamage == true)
        {
            float randomNumber = Random.Range(0.0f, 1.0f); //Picks a number 0 - 1
            enenmyAnim.SetFloat("damage", randomNumber);


            TheEnemy.EnemyTakesDamage = true;
            TheEnemy.DamageRecieved = damage;
            TheEnemy.TakeDamage(damage);
            takeDamage = false;

        }

        if (player == true && takeDamage == true)
        {
            ThePlayer.PlayerTakesDamage = true;
            ThePlayer.DamageRecieved = damage;
            ThePlayer.TakeDamage(damage);
            takeDamage = false;


        }
    }
}
