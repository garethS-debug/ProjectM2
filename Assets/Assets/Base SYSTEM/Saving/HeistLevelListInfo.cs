using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeistLevelListInfo 
{

    //public GameObject gameObject;
    //public int ID;
    //public string name;

    [Header("Level ID")]
    [Tooltip("A Unique Level ID to determine whether a level has been completed when refering to the Save File")]
    public int LevelID;


    [Header("Level Information")]
    [Tooltip("Information gathered from planning stage")]

    public bool hasKeyLocation;

    [Header("Score")]
    public float Score;
    public float SpottedPenetlies;
    public float TotalScore;
    public float SpottedNo;
    public float Cash;

    [Header("Mastery")]
    public int[] eventID;


    public HeistLevelListInfo (int LevelID, bool hasKeyLocation,   float Score, float SpottedPenetlies, float TotalScore, float SpottedNo, float Cash)
    {
        //this.gameObject = gameObject;
        //this.ID = ID;
        //this.name = name;
        this.LevelID = LevelID;
        this.hasKeyLocation = hasKeyLocation;
        this.Score = Score;
        this.SpottedPenetlies = SpottedPenetlies;
        this.TotalScore = TotalScore;
        this.SpottedNo = SpottedNo;
        this.Cash = Cash;
     
        //for (int i = 0; i < this.eventID.Length; i++)
        //{
        //    for (int a = 0; a < eventID.Length; a++)
        //    {
        //        this.eventID[i] = eventID[a];
        //    }
        //}


    }
}
