using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    

    public MeshRenderer mEyeRender;             //Eye    
        
    private SkinnedMeshRenderer mLidRender = null;     //Lid

    private int mFrameCount = 8;                //Frame Rate

    private void Awake()
    {
        mLidRender = GetComponent<SkinnedMeshRenderer>();

    }

    public void Start()
    {

        StartCoroutine(Blink());                //Start Blinking
    }

    private IEnumerator Blink()
    {
        float speed = 0.03f;
        while (gameObject.activeSelf)       //gameObject object active in scene
        {
            //Time Between Blinks
            yield return new WaitForSeconds(Random.Range(5, 10));       //random eye blink wait time

            //Count Up
            for (int i=0; i < mFrameCount; i++)         //scrll through texture sheet
            {
                yield return new WaitForSeconds(speed * Time.deltaTime);
                SetNewFrame(i);
            }

            //Shift Eye
            Vector2 eyeShift = new Vector2(Random.Range(-0.04f, 0.04f), Random.Range(-0.04f, 0.04f));
            mEyeRender.material.SetTextureOffset("_MainTex", eyeShift);

            //wait
            yield return new WaitForSeconds(speed * 0.25f);

            //Count Down (repeat first loop in reverse)
            for (int j = mFrameCount - 1; j >= 0; j--)
            {

                yield return new WaitForSeconds(speed * Time.deltaTime);
                SetNewFrame(j);
            }


        }
    }

    private void SetNewFrame(int frameIndex)
    {
        //Create New Offset, move texture
        Vector2 newOffset = new Vector2(frameIndex * (1.0f / mFrameCount), 0);
        mLidRender.material.SetTextureOffset("_MainTex", newOffset); 
    }
}
