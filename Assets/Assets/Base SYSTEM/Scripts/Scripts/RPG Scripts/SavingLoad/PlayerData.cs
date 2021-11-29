using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public struct SerializableVector3
//{
//    public float x;
//    public float y;
//    public float z;
//}

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    //-------> Internal Save Hooks
    //Save Player Health
    public float health; //Players Health
    //Save Player Position
    public float[] positionVector3;
    //public SerializableVector3 pos;

    
  

    public PlayerData (PlayerInformation player)
    {
        //Player Health
        health = player.PlayerHealth;

        //Player Position
        positionVector3 = new float[3];
        positionVector3[0] = player.transform.position.x;
        positionVector3[1] = player.transform.position.y;
        positionVector3[3] = player.transform.position.z;

        //pos.x = player.transform.position.x;
        //pos.y = player.transform.position.y;
        //pos.z = player.transform.position.z;
    }
}
