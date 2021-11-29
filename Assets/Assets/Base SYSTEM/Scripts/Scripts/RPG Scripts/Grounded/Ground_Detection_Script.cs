using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Detection_Script : MonoBehaviour
{

    Transform otherTransform;

    GameObject TargetTransform;

    public float offset;
    public LayerMask Groundmask;
    public bool isObjonGround;
    public float distanceToGround;
    public Transform Character;
    public List<Transform> visibleTargets = new List<Transform>();//list of visiable targets

    // Use this for initialization
    void Start()
    {
        //otherTransform = GameObject.Find("Ground").transform;	



    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        //FindVisiableTargets();
        //otherTransform = TargetTransform.transform;

        otherTransform = GameObject.FindWithTag("Ground").transform;
    }


    public void GroundCheck()
    {
        isObjonGround = Physics.Raycast(transform.position, Vector3.down, distanceToGround, Groundmask);

        while (!isObjonGround)
        {
            // transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y - correctionSpeed, transform.localPosition.z);
            Character.transform.position = new Vector3(transform.position.x, otherTransform.position.y, transform.position.z);
            break;
        }

        Debug.Log(Physics.Raycast(transform.position, Vector3.down, distanceToGround, Groundmask));
        Debug.DrawRay(transform.position, Vector3.down, Color.blue);
    }


    //void FindVisiableTargets()
    //{
        //visibleTargets.Clear();
        //Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, distanceToGround, Groundmask); // get an array of all the colliders in FOV

        //for (int i = 0; i < targetsInViewRadius.Length; i++)
        //{
        //    Transform target = targetsInViewRadius[i].transform;
        //    Vector3 dirToTarget = (target.position - transform.position).normalized;
        //    float dstToTarget = Vector3.Distance(transform.position, target.position); // if there is an obsticle between outselves and target

        //            //THERE ARE NO OBSTICLES AND CAN SEE TARGET
        //            visibleTargets.Add(target);
        //    //ATTACK METHOD HERE
        //    TargetTransform = target.gameObject;

        //}



        //}


    }



    






