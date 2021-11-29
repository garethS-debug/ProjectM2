using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{

    public PathCreator pathCreator;
    public float speed = 5;
    float distancetravelled;
    public GameObject LookAtObj;


    // Update is called once per frame
    void Update()
    {
        distancetravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distancetravelled);
        transform.LookAt(LookAtObj.transform);

        //Vector3 relativePos = LookAtObj.transform.position - transform.position;
        //// the second argument, upwards, defaults to Vector3.up
        //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //transform.rotation = rotation;


    }
}
