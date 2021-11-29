using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealthNoiseMeasure : MonoBehaviour
{
    private NewMurphyMovement murphyMovement;

    [Header("Noise Level")]
    public float noiseLevel;
   // public float increaseNoiseBy;
  //  public float noiseMin;
    public float noiseMax;
    public float noisePercent;
    public float noiseCoolDwnTimerMax;

    [Header("Noise Bar")]
    public Image stealthbarSlider;

    // Start is called before the first frame update
    void Start()
    {
        murphyMovement = GetComponent<NewMurphyMovement>();
       // noiseLevel = Mathf.Clamp(noiseLevel, noiseMin, noiseMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (murphyMovement.isStealthing == true && noiseLevel < noiseMax)
        {
            noiseLevel += 5 * Time.deltaTime; // Cap at some max value too
        }

        if (murphyMovement.isSprinting == true && noiseLevel < noiseMax)
        {
            noiseLevel += 20 * Time.deltaTime; // Cap at some max value too
        }

        if (murphyMovement.isWalkingForward == true || murphyMovement.isWalkingBackward == true || murphyMovement.isMovingLeft == true || murphyMovement.isMovingRight == true && noiseLevel < noiseMax)
        {
            noiseLevel += 10 * Time.deltaTime; // Cap at some max value too
        }

        noisePercent = noiseLevel / noiseMax;

        stealthbarSlider.fillAmount = noisePercent;

        Timer();
    }

    public void Timer()
    {
        //If the instance of Chase on this gameobject i.e if THIS character is attacking
        
            if (noiseLevel <= 0)
            {

                noiseLevel = noiseCoolDwnTimerMax;     // reset the timer.
            }

            else
            {
                 noiseLevel -= 7 * Time.deltaTime;
               
            }


        }
    
}
