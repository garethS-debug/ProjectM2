using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PEBar : MonoBehaviour {

    public Image healthBar;
    public static float health;
    public float MaxHealth;
    public float startHealth = 0;


    public void Start()
    {
        health = startHealth;
    }

    public void TakeDamage (float amount)
    {
        healthBar.fillAmount = health / MaxHealth;
    }

   
}
