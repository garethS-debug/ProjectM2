using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImpactRecorder : MonoBehaviour {

    /*
   public Image energyBar;
   public float startPotEnergy = 0;
   private float Potenergy;

   public void Start()
   {
       Potenergy = startPotEnergy;
   }

   public void onKEupdate ()
   {
       energyBar.fillAmount = Potenergy / startPotEnergy;
   }
   */


    [SerializeField]
    private int MaxEnergy = 1000;
    public int startEnergy = 0;
    [SerializeField]
    private int currentEnergy;

    public event Action<float> OnEnergyPctChanged = delegate { };

    private void OnEnable()
    {
        currentEnergy = startEnergy;
    }


    public void ModifyEnergy(int amount)
    {
        currentEnergy += amount;

        float currentEnergyPct = (float)currentEnergy / (float)MaxEnergy;
        OnEnergyPctChanged(currentEnergy);
    }

   
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.K))
            ModifyEnergy(+100);
	}
}
