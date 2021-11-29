using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyWeapon : MonoBehaviour {


    Vector3 pointA = new Vector3(-4.9f, 2.329837f, 19.6f);
    Vector3 pointB = new Vector3(7.6f, 2.3298f, 19.6f);

    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 1));
    }
}
