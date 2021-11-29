using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_FOV : MonoBehaviour {


    public float viewRadius;    //viewing radius of player
    [Range(0, 360)]              //clampting to 360  
    public float viewAngle;     //viewing angle
    public LayerMask targetMask;    //layer mask for target
    public LayerMask obstacleMask;  //Layer ask for obsticle
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();//list of visiable targets

    public Transform player; // exposed variable in inspector.
    private float distance;
    public static bool DialogReady;
    public static bool DialogReset;
    public float DetectionRange = 5f;

   

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
        DialogReady = false;

    }

    public void Update()
    {
        #region LOS
        Vector3 direction = player.position - this.transform.position; // getting direction to calculate angle between enemy & playr
        float angle = Vector3.Angle(direction, this.transform.forward);
        #endregion

        if (Vector3.Distance(player.position, this.transform.position) > DetectionRange && DialogSystem._isDialoguePlaying == true)
        {
            // if the distance between the player and the skeleton position is more than 10
            DialogReady = false;    //Set panel to close
            DialogReset = true;     //reset the text
           // Debug.Log("dialog Pause Bool");
        }


    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisiableTargets();
        }
    }




    void FindVisiableTargets()
    {
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask); // get an array of all the colliders in FOV

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) //check if target falls in view angle
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); // if there is an obsticle between outselves and target

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) // if we dont collider with anything
                {
                    if (Vector3.Distance(player.position, this.transform.position) < DetectionRange)
                    {
                        // if the distance between the player and the skeleton position is less than 10 
                        //THERE ARE NO OBSTICLES AND CAN SEE TARGET
                        visibleTargets.Add(target);
                        DialogReady = true; //DIALOG METHOD HERE. 

                    }
                    else
                    {
                        DialogReady = false; //DIALOG METHOD HERE. See Dialog C#
                    }

                }


            }
            else
            {
                DialogReady = false;
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) //if not global angle
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        //converting the agles to unity angles
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
