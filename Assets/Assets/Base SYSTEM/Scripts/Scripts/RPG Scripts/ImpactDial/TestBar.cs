using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBar : MonoBehaviour {



    public float FillAmnt;
    public int MaxHealth;
    private float potentialEnergy;



    public GameObject uiPrefab;
    public Transform target;
    Transform ui;
    Image energybarSlider;
    Transform cam;
    float visibleTime = 5;
    float LastMadeVisiableTime;

    private void Awake()
    {
         
    }

    // Use this for initialization
    void Start () 
    {
        LastMadeVisiableTime = Time.time;
        FillAmnt = 0;
        cam = Camera.main.transform;
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {

                ui = Instantiate(uiPrefab, c.transform).transform;
                energybarSlider = ui.GetChild(0).GetComponent<Image>();
                //ui.gameObject.SetActive(false);
                break;
            }
        }
    }

	
	// Update is called once per frame
	void Update () 
    {
        if (KEImpactDetection.isHealthy == true)
        {
            potentialEnergy = KEImpactDetection.PotentialEnergy/MaxHealth;
            //potentialEnergy = KEImpactDetection.KEForce / MaxHealth;
            StartCoroutine("FillEnergy");
            print("ENERGY BAR FILLING");
            KEImpactDetection.isHealthy = false;

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            energybarSlider.fillAmount = 0;
            FillAmnt = 0;
            potentialEnergy = 0;
        }
    }


    IEnumerator FillEnergy()
    {
        for (float i=0; i < potentialEnergy; i++)
        {
            energybarSlider.fillAmount = FillAmnt;
            FillAmnt += Time.deltaTime * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void LateUpdate()
    {
        /*
        ui.position = target.position;
        ui.forward = -cam.forward;
        */

        if (ui != null)
        {
            ui.position = target.position;
            ui.forward = -cam.forward;


            if (Time.time - LastMadeVisiableTime > visibleTime)
            {
                ui.gameObject.SetActive(false);
            }

        }


    }

}










      

