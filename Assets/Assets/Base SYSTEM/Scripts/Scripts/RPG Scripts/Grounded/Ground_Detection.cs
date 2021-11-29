using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Detection : MonoBehaviour {
    
   

       public float distanceToGround = 0.5f;
       public LayerMask Groundmask;
       public bool physicsLine;
       public float StartingYPOS;
       public float viewRadius;    //viewing radius of player
       public List<Transform> visibleTargets = new List<Transform>();//list of visiable targets
                                                      //public Vector3 currentYPOS;
    public float YTransformPostion;
       public float correctionSpeed = 0.1f;


    public float myTransform;


       // Use this for initialization
    void Start () 
       {

       }

    // Update is called once per frame
    void FixedUpdate () 
    {
       
        GroundCheck();
       // FindVisiableTargets();
    }


    public void GroundCheck()
    {
       physicsLine =  Physics.Raycast(transform.position, Vector3.down, distanceToGround, Groundmask);

        while (!physicsLine)
        {
            print("HIT THE FLOOR!");
           // transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y - correctionSpeed, transform.localPosition.z);
        }

        Debug.Log(Physics.Raycast(transform.position, Vector3.down, distanceToGround, Groundmask));
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
    }

    //void FindVisiableTargets()
    //{
    //    visibleTargets.Clear();

    //    Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, Groundmask); // get an array of all the colliders in FOV

    //    for (int i = 0; i < targetsInViewRadius.Length; i++)
    //    {
    //        Transform target = targetsInViewRadius[i].transform;
    //        Vector3 dirToTarget = (target.position - transform.position).normalized;
    //       // YTransformPostion = target.transform.localPosition.y;

    //        if (!Physics.Raycast(transform.position, Vector3.down, distanceToGround, Groundmask)) //IF RAYCAST IS NOT HITTING TARGET
    //        {
    //            visibleTargets.Add(target);
    //            Debug.Log("CurrentYPOS" + target.transform.localPosition.y);

    //            YTransformPostion = target.transform.localPosition.y;


    //            while (transform.localPosition.y != target.transform.localPosition.y)
    //            {
    //                // startingPOS = target.transform.localPosition.y - Correction;
    //                print("PLEASE FIX Y");
    //                FixYTransform();
    //                break;
    //            }
    //        }
    //    }
    //}

    //void FixYTransform()
    //{
    //    transform.position = new Vector3(transform.position.x, YTransformPostion, transform.position.z);
    //}

}
