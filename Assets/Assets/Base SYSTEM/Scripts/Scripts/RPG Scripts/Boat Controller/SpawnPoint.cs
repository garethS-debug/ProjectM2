using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public Transform boat;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 newspawnPOs = new Vector3(boat.position.x, boat.position.y, boat.position.z);
        transform.position = newspawnPOs;
        transform.rotation = Quaternion.Euler(new Vector3(boat.transform.rotation.x, boat.transform.rotation.y, boat.transform.rotation.z));
//        Debug.Log("new Spawn" + newspawnPOs);
    }
}
