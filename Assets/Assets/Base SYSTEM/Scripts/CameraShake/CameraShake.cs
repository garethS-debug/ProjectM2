using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public bool ShakeCam;

   public  float elapsed;

    public float duration = 1.5f;


    private void OnEnable()
    {
        CharacterStats.OnCamShake += EnableCamShake;
    }

    private void OnDisable()
    {
        CharacterStats.OnCamShake -= EnableCamShake;
    }

    public void Start()
    {
        ShakeCam = false;
    }

    public void Update()
    {
        if (ShakeCam == true)
        {

           


            if (elapsed < duration)
            {
                Vector3 originalPOS = transform.localPosition;
                this.transform.localPosition = originalPOS + Random.insideUnitSphere * shakeAmount;
                elapsed += Time.deltaTime;
//                Debug.Log("Shaking ".Bold().Color("white"));
            }

            if (elapsed >= duration)
            {
                elapsed = 0;
                ShakeCam = false;
          //      Debug.Log("Not Shaking ".Bold().Color("white"));
            }
               
        }
    }

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;



    public IEnumerator Shake (float duration, float magnitude)
    {
       // Vector3 originalPOS = transform.localPosition;

        float elapsed = 0.0f;
         while (elapsed < duration )
        {

            //float x = Random.Range(-0.01f, 0.01f) * magnitude;
            //float y = Random.Range(-0.001f, 0.001f) * magnitude;
            //float z = Random.Range(-0.01f, 0.01f) * magnitude;

            //  transform.localPosition = new Vector3(originalPOS.x, y, originalPOS.z);

            //   transform.position = new Vector3(Mathf.PingPong(Time.deltaTime, 6f), originalPOS.y, originalPOS.z);


            // this.transform.localPosition = originalPOS + Random.insideUnitSphere * shakeAmount;

            ShakeCam = true;
           


      

            elapsed += Time.deltaTime;

            

            yield return null;
        }

//        transform.localPosition = originalPOS;
    }

    void EnableCamShake()
    {
        ShakeCam = true;
    }
}
