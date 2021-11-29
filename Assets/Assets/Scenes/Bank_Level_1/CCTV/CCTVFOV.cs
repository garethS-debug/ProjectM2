using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVFOV : MonoBehaviour
{
    [Header("CCTV GAME OBJ")]
    public GameObject CCTVBone;

    [Header("Alarm")]
    private bool AlarmHasBeenRaised;
    public GameObject AlertedGuardRallyPoint;
    private bool checkcostume;
    public bool playerInFOV;
     private bool CallForHelp;
    public bool testScoreBool;
    public float CallPoliceDistance;
  

    [Header("Last Known LOC")]
    public List<Vector3> LastKnownFOVLOC = new List<Vector3>();
    [HideInInspector] public Vector3 LastKnownLocation;

    [Header("FOV")]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

  

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

      

    }



    private void Update()
    {

        FindVisibleTargets();


    //    this.transform.eulerAngles = new Vector3(CCTVBone.transform.localEulerAngles.x, CCTVBone.transform.localEulerAngles.y, CCTVBone.transform.rotation.z);

        if (playerInFOV == true && testScoreBool == false) 
        {
            print("only 1");
            //if (Alarm.Instance.RaiseAlarm == false)
            //{
                checkcostume = true;
            //}

            if (Alarm.Instance.RaiseAlarm == true)
            {
                Alarm.Instance.PlayerinCCTVFOV = true;
              //  Alarm.Instance.callThePolice = true;
            }

            if (Alarm.Instance.RaiseAlarm == false)
            {
                Alarm.Instance.callThePolice = true;
            }
    
            Objective.instance.UpdateScorePenelty(10.0f);
            testScoreBool = true;
        }


        if (playerInFOV == false && testScoreBool == true)
        {
            checkcostume = false;
            testScoreBool = false;
            if (Alarm.Instance.RaiseAlarm == true)
            {
                Alarm.Instance.PlayerinCCTVFOV = false;
            }

            if (Alarm.Instance.callThePolice == true)
            {
                Alarm.Instance.callThePolice = false;
            }
        }

    }


    void LateUpdate()
    {

        DrawFieldOfView();

        if (Alarm.Instance.falseAlarm == true)
        {
            AlarmHasBeenRaised = false;
        }

        if (checkcostume == true)
        {
            CostumeCheck();
        }

       

    }

    //public void FindVisibleTargets()
    //{
    //    checkcostume = false;
    //    playerInFOV = false;
    //    visibleTargets.Clear();


    //    Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

    //    for (int i = 0; i < targetsInViewRadius.Length; i++)
    //    {
    //        Transform target = targetsInViewRadius[i].transform;
    //        Vector3 dirToTarget = (target.position - transform.position).normalized;

    //        if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
    //        {
    //            float dstToTarget = Vector3.Distance(transform.position, target.position);

    //            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
    //            {

    //                    playerInFOV = true;
    //                    visibleTargets.Add(target);
    //                    checkcostume = true;


    //            }
    //        }
    //    }
    //}

    void FindVisibleTargets()
    {
        playerInFOV = false;
  

        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    
                        playerInFOV = true;
                        visibleTargets.Add(target);
                        LastKnownFOVLOC.Add(target.transform.position);

                        if (LastKnownFOVLOC.Count >= 3)
                        {
                            LastKnownFOVLOC.RemoveRange(0, 2);
                        }
                    
                    

                  
                }
            }
        }
    }


    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }

            }


            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }


    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }

    public void CostumeCheck()
    {
       
            //ADDED ALARM SCRIPT
            Alarm.Instance.CameraCheckCostume();

            if (Alarm.Instance.fooledByCostume == false)
            {

            if (PlayerManager.instance.player.GetComponent<NewMurphyMovement>().IsHidden == false)
            {

                Alarm.Instance.RemoveCostumeProtection();

                //IF ALARM IS NOT ALREADY RAISED
                if (Alarm.Instance.RaiseAlarm == false)
                {
                    Alarm.Instance.RaiseAlarm = true;
                   
                    if (Objective.instance.levelInformation.neverSeenOnCCTV == false)
                    {
                        Objective.instance.levelInformation.neverSeenOnCCTV = true;
                    }
                    //  Alarm.Instance.callThePolice = true;
                    //   Alarm.Instance.PlayerinCCTVFOV = true;
                }

              

                //     //IF ALARM IS ALREADY RAISED
                //if (Alarm.Instance.RaiseAlarm == true)
                //{

                //    //PLAYER BECOMES IN FOV 
                //    Alarm.Instance.PlayerinCCTVFOV = true;
                //}






                if (LastKnownFOVLOC.Count >= 1)
                {
                    int ListNumber = LastKnownFOVLOC.Count;
                    LastKnownLocation = LastKnownFOVLOC[ListNumber - 1];
                }

                    
                    Alarm.Instance.CallForHelp(LastKnownLocation);
                    Alarm.Instance.CallPoliceDistance = CallPoliceDistance;
                
                  //  Alarm.Instance.callThePolice = true;

                

                //  Objective.instance.UpdateScore(10.0f); //Penelty Points;

                //if (AlarmHasBeenRaised == true)
                //{

                //}
                return;
            }

        }
    }

}

