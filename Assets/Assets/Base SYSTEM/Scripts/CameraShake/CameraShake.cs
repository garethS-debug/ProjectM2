using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{


    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;



    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPOS = transform.localPosition;

        float elapsed = 0.0f;
         while (elapsed < duration )
        {

            //float x = Random.Range(-0.01f, 0.01f) * magnitude;
            //float y = Random.Range(-0.001f, 0.001f) * magnitude;
            //float z = Random.Range(-0.01f, 0.01f) * magnitude;

            //  transform.localPosition = new Vector3(originalPOS.x, y, originalPOS.z);

            //   transform.position = new Vector3(Mathf.PingPong(Time.deltaTime, 6f), originalPOS.y, originalPOS.z);


            this.transform.localPosition = originalPOS + Random.insideUnitSphere * shakeAmount;


            elapsed += Time.deltaTime;

            

            yield return null;
        }

        transform.localPosition = originalPOS;
    }
}
