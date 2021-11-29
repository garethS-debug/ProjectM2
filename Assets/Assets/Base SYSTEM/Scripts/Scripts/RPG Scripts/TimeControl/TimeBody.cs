using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour {

 bool isRewinding = false;

    #region List

    //keeping track of object position
    List<PointInTime> pointsInTime;

    #endregion

    public float recordingTime = 5f;

    Rigidbody ridgb;

    // Use this for initialization
    void Start () 
    {
        //storing the object over time
        pointsInTime = new List<PointInTime>();
        ridgb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update () 
    {
if (Input.GetKeyDown(KeyCode.R))
        {
           StartRewind();

if (Input.GetKeyUp(KeyCode.R))
                StopRewind();
        }

    }

    private void FixedUpdate()
    {
        if (isRewinding)
        
            Rewind();
            else 
                Record();


    }

    void Rewind ()
    {
        if (pointsInTime.Count > 0) // if there are more elements in the list 

        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;// continue rewinding 
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }

    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round (recordingTime /Time.fixedDeltaTime))//how many points we are recording in 1s 
                                                                       // checking if we are going to record more points in time than 5s 
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);//remove the oldest entries when 5s passed. 
        }
        //start insert of record at position 0 in list
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind ()
    {
        isRewinding = true;
        ridgb.isKinematic = true; // Set Kinematic on while rewinding 
    }

    public void StopRewind ()
    {
        isRewinding = false;
        ridgb.isKinematic = false;// Stop Kinematic when not rewinding 
    }
}
