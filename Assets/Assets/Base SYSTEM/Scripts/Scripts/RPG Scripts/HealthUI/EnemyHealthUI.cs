using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class EnemyHealthUI : MonoBehaviour
{
    //Reference to animator 
    static Animator anim;

    //Reference to character stats
    public int currentHealth;
    public float maxHealth;
    public float healthPercent;
    public float ZeroHealth;

    //Reference to Game Objects
    public GameObject uiPrefab;
    public Image HealthImage;
    public Transform target;

    //Bool
    public static bool startEnemyDeath;

    //Private References
    Transform ui;
    Image healthbarSlider;
    Chase chase;
    EnemyFOV enemyFOV;
    CharacterCombat characterCombat;


    Transform cam;
    float visibleTime = 5;
    float LastMadeVisiableTime;


    private void Start()
    {
        healthbarSlider = HealthImage;
        maxHealth = gameObject.GetComponent<CharacterStats>().maxHealth;
        anim = GetComponent<Animator>(); // get the animator component attached to enemy

        //Ending compnents when dead
        chase = GetComponent<Chase>();
        enemyFOV = GetComponent<EnemyFOV>();
        characterCombat = GetComponent<CharacterCombat>();


      
        if (this.gameObject.tag == "Enemy")
        {
//            Debug.Log("HealthUI ENEMY");
            Instantiate(uiPrefab,target);
            uiPrefab.SetActive(true);
                       //// healthbarSlider = ui.GetChild(0).GetComponent<Image>();
                       //// ui.gameObject.SetActive(false);
        }


    }

    private void Update()
    {
        currentHealth = gameObject.GetComponent<CharacterStats>().currentHealth;
        healthPercent = currentHealth/ maxHealth;

        //NULL REFERENCE ERROR
       healthbarSlider.fillAmount = healthPercent;

        //if (currentHealth == 100)
        //{
        //    startEnemyDeath = true;
        //    anim.SetBool("isDying", true);
        //}

        if (currentHealth <= 0.00f)
                {

          
            // Destroy(ui.gameObject);
                    startEnemyDeath = true;
                    anim.SetBool("isDying", true);
                    //INSERT DEATH ANIMATION  
                    StartCoroutine("EnemyDeath");

            chase.enabled = false;
            enemyFOV.enabled = false;
            characterCombat.enabled = false;

                    }


    }


    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("isDying", false);
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    //private void Start()
    //{
    //    //cam = Camera.main.transform;

    //    healthbarSlider = HealthImage;

    //    //foreach (Canvas c in FindObjectsOfType<Canvas>())
    //    //{

    //    //    if (c.renderMode == RenderMode.WorldSpace)
    //    //    {

    //    //        ui = Instantiate(uiPrefab, c.transform).transform;
    //    //        healthbarSlider = ui.GetChild(0).GetComponent<Image>();
    //    //        ui.gameObject.SetActive(false);
    //    //        break;
    //    //    }

    //    //}
    //    ui = Instantiate(uiPrefab, c.transform).transform;
    //    this.GetComponent<CharacterStats>().OnhealthChanged += OnHealthchanged;
    //}


    ////private void LateUpdate()
    ////{
    ////    /*
    ////    ui.position = target.position;
    ////    ui.forward = -cam.forward;
    ////    */       


    ////    if (ui != null)
    ////    {
    ////        ui.position = target.position;
    ////        ui.forward = -cam.forward;

    ////        if (Time.time - LastMadeVisiableTime > visibleTime)
    ////        {
    ////            ui.gameObject.SetActive(false);
    ////        }
    ////    }


    ////}

    //void OnHealthchanged(int maxHealth, int currentHealth)
    //{
    //if (ui != null)
    //{
    //    {
    //        Debug.Log("HealthUI Activated");
    //        //ui.gameObject.SetActive(true);
    //        //LastMadeVisiableTime = Time.time;
    //        float healthPercent = currentHealth / (float)maxHealth;
    //        healthbarSlider.fillAmount = healthPercent;
    //        if (currentHealth <= 0)
    //        {
    //            Destroy(ui.gameObject);
    //        }
    //        //}
    //    }


    //}





}

