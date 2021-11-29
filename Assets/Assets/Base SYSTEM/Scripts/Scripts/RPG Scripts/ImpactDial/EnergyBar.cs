using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    public Image healthBar;
    public static float health;
    public float MaxHealth;
    private int potentialEnergy;
    public float waitTime = 1.0f;





    public void Start()
    {
        health = 0; // 0 energy stored

    }



    // Update is called once per frame
    void Update()//  Update our score continuously.
    {
        if (KEImpactDetection.isHealthy == true)
        {

            potentialEnergy = KEImpactDetection.PotentialEnergy / 10;
            TakeDamage(+potentialEnergy);
            KEImpactDetection.isHealthy = false;

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            healthBar.fillAmount = 0;
        }

    }


    public void TakeDamage(float amount)
    {
        health += amount;
        float PctHealth = health / MaxHealth;
        healthBar.fillAmount = PctHealth;
        //healthBar.fillAmount += PctHealth / Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

}

/*
 *     public Image healthBar;
    public static float health;
    public float MaxHealth;
   


    private void Start()
    {
        UpdateHealthBar(); //updte at start
    }


    void UpdateHealthBar()
    {
        float ratio = health / MaxHealth; // give value 0-1
        healthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
      

    }

    private void TakeDamage(float PotentialEnergy)
    {
        health += PotentialEnergy;
        UpdateHealthBar();
    }


    private void HealDamage(float Heal)
    {

    }

   






    /*
    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    private void Awake()
    {
        GetComponentInParent<ImpactRecorder>().OnEnergyPctChanged += HandleEnergyChanged;
    }

    private void HandleEnergyChanged (float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct (float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, preChangePct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct; 
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
    */

