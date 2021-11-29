using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterfollow : MonoBehaviour {


    public float yAxisDepth;
    public Transform Ship;
    public int offsetx;
    public int offsetz;

    void Start()
    {
      
    }

    void Update()
    {
        transform.position = new Vector3(Ship.position.x-offsetx, yAxisDepth, Ship.position.z-offsetz);
    }
}

