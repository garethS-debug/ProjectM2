using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTileSpawner : MonoBehaviour {

    public GameObject Ship;
    public GameObject WaterTilePrefab;
    public GameObject WaterTileTransformLoc;
    // Use this for initialization
    void Start () 
    {
    
    }
	
	// Update is called once per frame
	void Update () 
    {


		
	}

    void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.tag == "Ship" && WaterFollowShip.SpawnNewWaterTile == true)
            if (collision.gameObject.tag == "Ship")
            {
            // a Gameobject tagged as "Ball" hit the player
            Debug.Log("Spawn New Water Tile HERE");

          //  WaterFollowShip.SpawnNewWaterTile = false;
            //Instantiate Game Object
            // GameObject go = Instantiate(WaterTilePrefab, WaterTileTransformLoc.transform.position, WaterTileTransformLoc.transform.rotation);

            WaterTilePrefab.gameObject.SetActive(true);
        //SetGameObject To True 

         
           }

    }
}
