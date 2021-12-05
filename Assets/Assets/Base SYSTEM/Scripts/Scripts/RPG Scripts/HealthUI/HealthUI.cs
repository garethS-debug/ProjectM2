using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    

    //Reference to character stats
    public int currentHealth;
    public float maxHealth;
    public float healthPercent;

    public GameObject uiPrefab;
    public Image HealthImage;
    public Transform target;

    Transform ui;
    Image healthbarSlider;


    Transform cam;
    float visibleTime = 5;
    float LastMadeVisiableTime;

    [Header("Failed Level Ref")]
    [Tooltip("reference to the game manager")]
    public GameObject gameManager;
    LevelFailed levelFailed;

    //[Header("Death")]
    //[Tooltip("Ragdoll Game Object")]
    //public GameObject RagdollGO;
    //private Animator anim;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        levelFailed = gameManager.gameObject.GetComponent<LevelFailed>();

        
        if (SceneSettings.Instance.HealthImage != null)
        {
            healthbarSlider = SceneSettings.Instance.HealthImage;
        }


        //anim = GetComponent<Animator>();

        if (this.gameObject.tag == "Enemy")
        {
                                        //THis was moved into the tag check from 1 line above. 
            maxHealth = gameObject.GetComponent<CharacterStats>().maxHealth;
           // Debug.Log("HealthUI ENEMY");
            Instantiate(uiPrefab,target);
            uiPrefab.SetActive(true);
                       //// healthbarSlider = ui.GetChild(0).GetComponent<Image>();
                       //// ui.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        

        if (this.gameObject.tag == "Enemy")
        {
            uiPrefab.transform.position = target.transform.position;
        }
        //

        currentHealth = gameObject.GetComponent<CharacterStats>().currentHealth;
        healthPercent = currentHealth/ maxHealth;
        if (healthbarSlider!= null)
        {
            healthbarSlider.fillAmount = healthPercent;
        }
     

         if (currentHealth <= 0)
        {
                                                    //If this is the main character
                  if (this.gameObject.tag == "Player")
            {
              //  levelFailed.FailedLevel();
            }

            if (this.gameObject.tag == "Enemy")
            {
               
                //StartCoroutine("EnemyDeath");

            }

            //                    Destroy(ui.gameObject);

        }


        



    }

    //public IEnumerable EnemyDeath()
    //{
    //    Debug.Log(transform.name + " died.");
    //    anim.SetBool("isDead", true);
    //    yield return new WaitForSeconds(1f);
    //    //enemyAnim.enabled = false;
    //    Instantiate(RagdollGO, transform.position, transform.rotation);
    //    Destroy(gameObject);

    //}



}

