using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFollowShip : MonoBehaviour {

    //public Transform player; // exposed variable in inspector.
    public Transform ship; // exposed variable in inspector.
    public float DetectionRange = 5f;
    public Transform CentralPoint;
    public static bool SpawnNewWaterTile = false; 

    // Use this for initialization
    void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        #region LOS
        Vector3 direction = ship.position - transform.position; // getting direction to calculate angle between water & playr
        float angle = Vector3.Angle(direction, transform.forward);
        #endregion

        Debug.DrawRay(transform.position, direction, Color.red);

        if (Vector3.Distance(ship.position, CentralPoint.transform.position) > DetectionRange)
        {
            SpawnNewWaterTile = true;
            Debug.Log("SpawnNewWaterTile  is active");

        }

        else
        {
            SpawnNewWaterTile = false;
        }

    }
}
