using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class PathFollower : MonoBehaviour
{
    public PathCreator pathcreator;
    public float speed = 5;
    public float distanceTraveled;
    public float MaxDistance = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (distanceTraveled <= MaxDistance)
        {
            distanceTraveled += speed * Time.deltaTime;
            transform.position = pathcreator.path.GetPointAtDistance(distanceTraveled);
            transform.rotation = pathcreator.path.GetRotationAtDistance(distanceTraveled);
            break;
        }
    }
}
