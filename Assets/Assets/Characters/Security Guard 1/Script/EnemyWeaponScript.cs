using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponScript : MonoBehaviour
{
    Collider m_Collider;

    //Animation
    static Animator anim;
    static Animator enemyAnim;

    public bool takeDamage;


    [Header("Reference to player or enemy")]
    [Tooltip("reference to the players")]
    public GameObject Player;
    public GameObject theEnemy;

    [Header("Damage")]
    [Tooltip("damage")]
    public int damage;
    public float HitCounter;

    private CharacterStats ThePlayer;
    private Combat combat;
    private Chase chase;

    //[Header("CameraShake")]
    //public CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        if (theEnemy.gameObject.GetComponent<CharacterStats>().damage.GetValue() > 0)
        {
            damage = theEnemy.gameObject.GetComponent<CharacterStats>().damage.GetValue();
        }
       
        ThePlayer = PlayerManager.instance.player.GetComponent<CharacterStats>();
        chase = theEnemy.gameObject.GetComponent<Chase>();
        enemyAnim = this.gameObject.GetComponent<Animator>();
        combat = PlayerManager.instance.player.gameObject.GetComponent<Combat>();
       // HitCounter = combat.HitCounter;

    }

    // Update is called once per frame
    void Update()
    {

        //if (enemyAnim.GetBool("isAttacking") == true)
        //{
        //    m_Collider.enabled = true;
        //}

        //if (enemyAnim.GetBool("isAttacking") == false)
        //{
        //    m_Collider.enabled = false;
        //}



        if (takeDamage == true)
        {
         //  StartCoroutine (cameraShake.Shake(.10f,.2f));
            ThePlayer.PlayerTakesDamage = true;
            ThePlayer.DamageRecieved = damage;
            ThePlayer.TakeDamage(damage);
            takeDamage = false;

        }
    }



    public void OnTriggerEnter(Collider collision)
    {

        //IF THE Enemy WEAPON COLLIDES WITH A PLAYER

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("MAIN CHARACTER HIT BY" + this.gameObject.transform.name);
            anim = collision.gameObject.GetComponent<Animator>();
            StartCoroutine("Damage");

        }
    }


    IEnumerator Damage()
    {
        anim.SetBool("isHit", true);
        takeDamage = true;
       // HitCounter = HitCounter + 1.0f;
        yield return new WaitForSeconds(0.001f);
        takeDamage = false;
        anim.SetBool("isHit", false);

    }

}
