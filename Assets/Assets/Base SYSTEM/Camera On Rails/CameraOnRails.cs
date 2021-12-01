using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnRails : MonoBehaviour
{
   [HideInInspector] public Transform player;
    public Vector3 offset;
 //   public Vector3 followoffset;

    public float smoothSpeed;

    public static CameraOnRails instance;




    [Header("Focus onTarget")]
    public bool enablePointOfInterest;
   public GameObject objectOfInterestTarget;
    // Maximum turn rate in degrees per second.
    public float turningRate = 30f;
    // Rotation we should blend towards.
    private Quaternion _targetRotation = Quaternion.identity;
    // Call this when you want to turn the object smoothly.
   public Vector3 vector3Reset;

    public Vector3 vector3ResetDEBUG;

    public float snapBackDistance;


    private void Awake()
    {
        instance = this;
       
    }

    public void Start()
    {
        vector3Reset.x = this.transform.eulerAngles.x;
        vector3Reset.y = this.transform.eulerAngles.y;
        vector3Reset.z = this.transform.eulerAngles.z;
    }

    public void SetBlendedEulerAngles(Vector3 angles)
    {
        _targetRotation = Quaternion.Euler(angles);
    }

    void Update ()
    {
        SetBlendedEulerAngles( vector3ResetDEBUG);


        // Vector3 position = transform.position;
        // position.y = (player.position + offset).y;
        // transform.position = position;

        //this.transform.localPosition = player.transform.position + followoffset ;

        float blend = 1f - Mathf.Pow(1f - smoothSpeed, Time.deltaTime * 30f);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, blend);

        if (objectOfInterestTarget != false)
        {


            float dist = Vector3.Distance(player.transform.position, objectOfInterestTarget.transform.position);



            if (enablePointOfInterest == true)
            {

            }

            if (enablePointOfInterest == false)
            {

            }




            if (dist <= snapBackDistance)
            {
                Vector3 relativePos = objectOfInterestTarget.transform.position - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(relativePos);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 1 * Time.deltaTime);
            }

            if (dist >= snapBackDistance)
            {
                SetBlendedEulerAngles(vector3Reset);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);
            }

        }
    
    }



    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "ObjectOfInterest")
        {
            objectOfInterestTarget = other.gameObject;

        }

    }


    private void SetBoolBack()
    {
        enablePointOfInterest = false;
    }


    //private void OnTriggerExit(Collider other)
    //{

    //    if (other.gameObject.tag == "ObjectOfInterest")
    //    {
    //        SetBlendedEulerAngles(vector3Reset);
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, turningRate * Time.deltaTime);
    //    }

    //}


}
