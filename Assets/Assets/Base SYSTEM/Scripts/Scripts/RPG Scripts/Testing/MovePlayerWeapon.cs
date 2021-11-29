using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerWeapon : MonoBehaviour {

 Vector3 pointA = new Vector3(1.1f, 2.329837f, 44.8f);
     Vector3 pointB = new Vector3(-12.1f, 2.329837f, 44.8f);

     void Update()
     {
         transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 1));
     }
 }

