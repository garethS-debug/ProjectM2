using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurphyWeaponScript : MonoBehaviour
{

    public GameObject ThePlayer;

    private NewMurphyMovement murphyMovement;
    private PlayerStats PlayerStats;
    private CharacterStats TheEnemy;
    private Animator enenmyAnim;
    public bool dealDamage;
    public int damage;

    SphereCollider m_Collider;
    
    public void Start()
    {
        PlayerStats = ThePlayer.gameObject.GetComponent<PlayerStats>();
        damage = PlayerStats.damage.GetValue();
        m_Collider = GetComponent<SphereCollider>();
        murphyMovement = ThePlayer.gameObject.GetComponent<NewMurphyMovement>();
    }

    public void Update()
    {
        if (dealDamage == true)
        {
            TheEnemy.EnemyTakesDamage = true;
            TheEnemy.DamageRecieved = damage;
            TheEnemy.TakeDamage(damage);

            float randomNumber = Random.Range(0.0f, 1.0f); //Picks a number 0 - 1
            enenmyAnim.SetFloat("damage", randomNumber);

        }

        if (murphyMovement.enabledPlayerWeaponCollider == true)
        {
            m_Collider.enabled = true;
        }

        else if (murphyMovement.enabledPlayerWeaponCollider == false)
        {
            m_Collider.enabled = false;
        }


    }

  

    public void OnTriggerEnter(Collider collision)
   {
        Debug.Log(collision.gameObject.name + " registered collision by " + this.gameObject.transform.name);

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log(collision.gameObject.name + " hit by " + this.gameObject.transform.name);
            TheEnemy = collision.gameObject.GetComponent<CharacterStats>();
            enenmyAnim = TheEnemy.gameObject.GetComponent<Animator>();
            StartCoroutine("Damage");
        }
    }

    IEnumerator Damage()
    {
        dealDamage = true;
        enenmyAnim.SetBool("isHit", true);
        yield return new WaitForSeconds(0.001f);
        enenmyAnim.SetBool("isHit", false);
        dealDamage = false;
       

    }

    //  ____------____--___

    //Collider m_Collider;

    ////Animation
    //static Animator anim;

    //public bool takeDamage;


    //public GameObject Player;
    //public GameObject theEnemy;

    //public int damage;

    //public CharacterStats TheEnemy;
    //private NewMurphyMovement murphyMovement;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    damage = Player.gameObject.GetComponent<CharacterStats>().damage.GetValue();
    //    murphyMovement = Player.gameObject.GetComponent<NewMurphyMovement>();
    //    m_Collider = GetComponent<SphereCollider>();

    //}

    //public void OnTriggerEnter(Collider collision)
    //{
    //    TheEnemy = collision.gameObject.GetComponent<CharacterStats>();
    //    //IF THE Enemy WEAPON COLLIDES WITH an enemy

    //    Debug.Log(collision.gameObject.name + " registered collision by " + this.gameObject.transform.name);

    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log(collision.gameObject.name + " hit by " + this.gameObject.transform.name);

    //        anim = collision.gameObject.GetComponent<Animator>();

    //     //   float randomNumber = Random.Range(0.0f, 1.0f); //Picks a number 0 - 1
    //        anim.SetFloat("damage", 0.5f);

    //        StartCoroutine("Damage");

    //    }
    //}

    //IEnumerator Damage()
    //{
    //    anim.SetBool("isHit", true);
    //    takeDamage = true;
    //    yield return new WaitForSeconds(0.001f);
    //    takeDamage = false;
    //    anim.SetBool("isHit", false);

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    if (murphyMovement.enabledPlayerWeaponCollider == true)
    //    {
    //        m_Collider.enabled = true;
    //    }

    //    else if  (murphyMovement.enabledPlayerWeaponCollider == false)
    //    {
    //        m_Collider.enabled = false;
    //    }


    //    if (takeDamage == true)
    //    {
    //        TheEnemy.EnemyTakesDamage = true;
    //        TheEnemy.DamageRecieved = damage;
    //        TheEnemy.TakeDamage(damage);
    //        takeDamage = false;
    //    }
    //}




}
