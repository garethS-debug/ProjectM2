using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIslandRockingMotion : MonoBehaviour {

    // "Bobbing" animation from 1D Perlin noise.

    // Range over which height varies.
    public float heightScale = 1.0f;

    //Float side to side
   // public float heightScale = 1.0f;

    // Distance covered per second along X axis of Perlin plane.
    public float xScale = 1.0f;


    public float rotspeed = 2f;
    public float maxRotation = 45f;


    void Update()
    {
        //Float up
        float height = heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f);

        //Float side
        //float height = heightScale * Mathf.PerlinNoise(Time.time * xScale, 0.0f);

        transform.rotation = Quaternion.Euler(maxRotation * Mathf.Sin(Time.time *  rotspeed), 0f, 0f);

        Vector3 pos = transform.position;
        pos.y = height;
        transform.position = pos;
    }

   
}
