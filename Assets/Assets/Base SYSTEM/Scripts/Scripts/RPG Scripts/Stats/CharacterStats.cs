using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    /*
     * this script determines damage done to the character.
     *
     */

    [Header("Health")]
    [Tooltip("reference to the character's health")]
    // Health
    public int maxHealth; // Maximum amount of health
    public int currentHealth { get; set; }// Current amount of health (changed from private set to set
    [Space(10)]


    //Bool
    public bool PlayerTakesDamage;
    public bool EnemyTakesDamage;

    [Header("Character Stats Ref")]
    [Tooltip("reference to the character's stats")]
    //stats
    public Stat damage;
    public Stat armor;
    public Stat stealth; //Stealth Stat
    public Stat lockPick;
    public Stat speed;
    public Stat wealthMod;

    [Space(10)]

    [Header("Hit Counter")]
    [Tooltip("Hit Counter for damage anim")]
    public float HitCounter;
    private Animator anim;

    [Header("Damage Recieved")]
    [Tooltip("Amount of Damage Recieved")]
    //int
    public int DamageRecieved;

    [Header("Death")]
    [Tooltip("Ragdoll Game Object")]
    public GameObject RagdollGO;


    [Header("CameraShake")]
    public CameraShake cameraShake;
    //public Camera cam;

    //reference to charactercombatscript
    CharacterCombat characterCombat;

   // public event System.Action OnHealthReachedZero;
    public event System.Action<int, int> OnhealthChanged;

    public static bool damageTrigger = false;

    CameraShake camShake;

    // Set current health to max health
    // when starting the game.
    void Awake()
    {
        currentHealth = maxHealth;
        characterCombat = GetComponent<CharacterCombat>();
        anim = GetComponent<Animator>();
        
        
    }

    public void Update()
    {
        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }

       if (characterCombat.dealingDamage == true || PlayerTakesDamage == true || EnemyTakesDamage == true)
        {
            //HERE THE PLAYER WILL TAKE DAMAGE BASED ON 'CHARACTER COMBAT'S'

          
          //  StartCoroutine(cameraShake.Shake(.10f, .2f));   //This is the line for shaking the camera on hit.
            DamageRecieved = characterCombat.damageDealt;   // This is the line for determining what damage is taken. Damage recieved by the enemy is the damage recieved from combat.
            TakeDamage(DamageRecieved);                     // This is the line for taking damage.
            Debug.Log("Damage Recieved" + DamageRecieved);  // Debug Log
        }

    }

    // Damage the character
    public void TakeDamage(int damage)
    {
        //&& this.gameobject.tag == will direct this attack at the player. 
        //This attack method attacks ALL Health that has this script attached. This should be character specific. 
        if (characterCombat.dealingDamage == true || PlayerTakesDamage == true || EnemyTakesDamage == true) 
        {

            if (HitCounter < 1)
            {
                HitCounter = Mathf.Lerp(HitCounter, 1, Time.deltaTime);
                anim.SetBool("isHit", true);
                anim.SetFloat("HitCounter",HitCounter);
            }

            if (HitCounter >= 1)
            {
               
                HitCounter = 0.01f;
            }


            //If this is the player Get the correspondaning Camera
            if (this.gameObject.tag == "Player")
            {
                if (camShake == null)
                {
                    //this.gameObject.GetComponent<murphyPlayerController>().CameraPrefab.GetComponent<CameraShake>();
                  //  camShake = SceneSettings.Instance.humanPlayer.gameObject.GetComponentInChildren<murphyPlayerController>().CameraPrefab.GetComponent<CameraShake>();
                    Debug.Log("Finding Camera".Bold());
                 camShake = GameObject.FindGameObjectWithTag("CamFollow").GetComponent<CameraShake>();
                }

                else if (camShake != null)
                {
                    camShake.ShakeCam = true;
                }
             
            }
         

            // Subtract the armor value
            damage -= armor.GetValue();
            damage = Mathf.Clamp(damage, 0, int.MaxValue);
            // Damage the character
            // StartCoroutine(cameraShake.Shake(.15f, .25f));   //This is the line for shaking the camera on hit. Duration, Magnitude
           

            currentHealth -= damage;
            Debug.Log(transform.name + " takes " + damage + " damage.");
            //Start take damage animation
            Debug.Log(this.gameObject.name + "Current health now" + currentHealth);

       

            DetectHit.enemyhitDetected = false;

            if (OnhealthChanged != null)
        {
            OnhealthChanged(maxHealth, currentHealth);  //Update the  characters health
        }

        }

        EnemyTakesDamage = false;
        PlayerTakesDamage = false;
    }

  
    public virtual void Die()
    {
        if (this.gameObject.tag == "Enemy")
        {
            this.gameObject.tag = "SleepingEnemy";
            anim.SetBool("isDead", true);
            StartCoroutine("EnemyDeath");
            Debug.Log( "Dieing now");
        }

      

    }

    public IEnumerator EnemyDeath()
    {
        
            // Die in some way
            // This method is meant to be overwritten
            Debug.Log(transform.name + " died X");
            yield return new WaitForSeconds(0.9f);
        //enemyAnim.enabled = false;
             Instantiate(RagdollGO, transform.position, transform.rotation);
            Destroy(gameObject);
       
    }





  

}