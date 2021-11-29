using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldOfView : MonoBehaviour {

    public static FieldOfView fieldOfView;

    [Header("ViewRadius")]
    [Tooltip("Viewing Radious")]
    public float viewRadius;    //viewing radius of player
    [Range(0,360)]              //clampting to 360  
    public float viewAngle;     //viewing angle

    [Header("View Target Mask")]
    public LayerMask targetMask;    //layer mask for target
    public LayerMask obstacleMask;  //Layer ask for obsticle

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();//list of visiable targets
                                //targeting system target


    public static bool enemyhitDetected = false;
    public static bool playerhitDetected = false;

    public bool isSitting;
   

    private float distance;
 

   public static bool ActivateQuestObject = false;

    //DIALOG FUNCTION
    public Image Dialog_Panel;


    //Stored Dialog and Quest Information. ---------------> 
    public static int questID;
    public static DialogMenu storedNPCDialog;            //Test
    public static DialogMenu dialogStandardDialog;      //QUEST STANDARD DIALOG
    public static DialogMenu dialogQuestAvailable;     //QUEST AVAILABLE DIALOG
    public static DialogMenu dialogQuestRunning;       //QUEST RUNNING DIALOG
    public static DialogMenu dialogQuestFinished;      //QUEST FINISHED DIALOG
    public static DialogMenu defaultDialog;            //Default DIALOG

    public static bool RecievableQuestDialog;          //recievable quest bool from quest obj
    public static bool AcceptedQuestDialog;            //accepted quest bool from quest obj
    public static bool CompletedQuestDialog;            //completed quest bool from quest obj
    public static bool StartQuestFromDialog;            //start quest bool from quest obj
    public static bool CompleteQuestFromDialog;         //completed quest bool from quest obj
    public static bool withinattackDistance;

    [Header("stealth Kills")]
    public float StealthKillDistance;


    [Header("References to Other Scripts")]
    [Tooltip("Reference to the other scripts")]
    //Reference to other script
    public CharacterStats EnemyStats;
    public trigger triggerObj;
    public GameObject EnemyInSight;
    public NewMurphyMovement newMurphyMovement;
    public PickupThrow pickupThrow;

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
        playerhitDetected = false;
        enemyhitDetected = false;
        newMurphyMovement = this.gameObject.GetComponent<NewMurphyMovement>();
        pickupThrow = this.gameObject.GetComponent<PickupThrow>();
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

       // newMurphyMovement.EnemyStealthKillGO = null;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask); // get an array of all the Enemy colliders in FOV

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
          

            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
//            print(dirToTarget);
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) //check if target falls in view angle
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position); // if there is an obsticle between outselves and target
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) // if we dont collider with anything & Target is NPC
                {
                    visibleTargets.Add(target);

                 

                    //THERE ARE NO OBSTICLES AND CAN SEE TARGET NOW
                    //CHECK TO SEE IF TARGET IS NPC OR ENEMY TAG

                    if (targetsInViewRadius[i].gameObject.tag == "NPC")
                    {
                       // Debug.Log("YOU HIT ME PREPARE TO DIE WITH WORDS");
                        Dialog_Panel.gameObject.SetActive(true);//set dialog panel to open
                                                                //  DialogSystem.dialogSystem.defaultDialog = targetsInViewRadius[i].gameObject.GetComponent<DialogNPCStore>().defaultDialog;

                        //IDENTIFY WHAT NPC WE ARE TALKING TO.
                        //PASS NECESSARY INFORMATION TO DialogSystem ---> DialogChange()
                       // print(targetsInViewRadius[i].gameObject.GetComponent<DialogNPCStore>().NPCID);          //THIS IS THE NPC ID. 
                        questID = targetsInViewRadius[i].gameObject.GetComponent<DialogNPCStore>().NPCID;    //THIS IS NPC ID
                        storedNPCDialog = targetsInViewRadius[i].gameObject.GetComponent<DialogNPCStore>().storedNPCDialog; //THIS IS NPC DIALOG TEST
                        dialogStandardDialog = targetsInViewRadius[i].gameObject.GetComponent<DialogNPCStore>().dialogStandardDialog;     //QUEST STANDARD DIALOG
                        dialogQuestAvailable = targetsInViewRadius[i].gameObject.GetComponent<DialogNPCStore>().dialogQuestAvailable;     //QUEST AVAILABLE DIALOG
                        dialogQuestRunning = targetsInViewRadius[i].gameObject.GetComponent<DialogNPCStore>().dialogQuestRunning;      //QUEST RUNNING DIALOG
                        dialogQuestFinished = targetsInViewRadius[i].gameObject.GetComponent<DialogNPCStore>().dialogQuestFinished;    //QUEST FINISHED DIALOG
                        defaultDialog = targetsInViewRadius[i].gameObject.GetComponent<DialogNPCStore>().defaultDialog;       //Default DIALOG

                        //QuestObjScript = targetsInViewRadius[i].gameObject.GetComponent<QuestObject>();
                        //QuestObjScript.enabled = true;

                        //  RecievableQuestDialog = targetsInViewRadius[i].gameObject.GetComponent<QuestObject>().RecievableQuestDialog;//recievable Quest dialog bool
                        //  AcceptedQuestDialog = targetsInViewRadius[i].gameObject.GetComponent<QuestObject>().AcceptedQuestDialog; //accepted Quest dialog bool
                        //  CompletedQuestDialog = targetsInViewRadius[i].gameObject.GetComponent<QuestObject>().CompletedQuestDialog;    //completed quest dialog bool
                        //  StartQuestFromDialog = targetsInViewRadius[i].gameObject.GetComponent<QuestObject>().StartQuestFromDialog;            //start quest bool from quest obj
                        // CompleteQuestFromDialog = targetsInViewRadius[i].gameObject.GetComponent<QuestObject>().CompleteQuestFromDialog;       //completed quest bool from quest obj

                        //IF CHECK TO SEE IF THE NPC IS ENEMY OR NPC 
                        //IF STATEMET IS NEW SO IF BUG DETECTED REMOVE THIS. 
                        //if (targetsInViewRadius[i].gameObject.tag == "NPC")
                        //{


                        //     QuestObject QuestObjectScript = targetsInViewRadius[i].gameObject.GetComponent<QuestObject>();
                        //     QuestObjectScript.enabled = true;
                        //    // 

                        //   // print("enable script obj");

                            if (targetsInViewRadius[i].gameObject.GetComponent<QuestObject>() != null)
                            {

                            triggerObj = targetsInViewRadius[i].gameObject.GetComponent<trigger>();
                            triggerObj.TurnOffOnQuestObj = true;
                          //   print("ENABLE QUEST");

                            // targetsInViewRadius[i].gameObject.GetComponent<QuestObject>().enabled = true;

                            }

                            else 
                        { triggerObj.TurnOffOnQuestObj = false; }
                       


                        //}

                    }



                    if (targetsInViewRadius[i].gameObject.tag == "Enemy" || targetsInViewRadius[i].gameObject.tag == "Police")
                    {
                        Debug.Log("Murphy:  I CAN SEE GUARD");
                         Interacting.CombatInteracting = true;
                        EnemyStats = target.GetComponent<CharacterStats>();

                        float dist = Vector3.Distance(target.position, this.transform.position);

                        if (dist <= StealthKillDistance)
                        {
                           newMurphyMovement.EnemyStealthKillGO = target.gameObject;
                        }
                      


                    }

                    if (targetsInViewRadius[i].gameObject.tag == "Staff")
                    {
                        Debug.Log("Murphy: I CAN SEE STAFF");

                        newMurphyMovement.EnemyStealthKillGO = target.gameObject;


                    }

                    if (targetsInViewRadius[i].gameObject.tag == "Civilian")
                    {
                        Debug.Log("Murphy: I CAN SEE Civilian");


                        float dist = Vector3.Distance(target.position, this.transform.position);
                        if (dist <= StealthKillDistance)
                        {
                            newMurphyMovement.EnemyStealthKillGO = target.gameObject;
                        }

                    }

                    //if (targetsInViewRadius[i].gameObject.tag == "PickUpOBJ")
                    //{
                    //    print("I can see Spine");
                    //    pickupThrow.PickUpObj = targetsInViewRadius[i].gameObject.gameObject;
                    //}



                    //------------->ATTACK METHOD HERE
                    //  Interacting.CombatInteracting = true;
                    //------------->





                }




            }
            else
            {
                Interacting.CombatInteracting = false;
                Dialog_Panel.gameObject.SetActive(false);//set panel to closed
               ActivateQuestObject = false;
               
                //questObj = targetsInViewRadius[i].gameObject.GetComponent<QuestObject>();
                //questObj.initiateQuestScript = false;

                //  targetsInViewRadius[i].gameObject.GetComponent<QuestObject>().enabled = false;

                if (DialogSystem._isDialoguePlaying == true)
                {
                  //  Debug.Log("Resetting the Dialog now Master");
                 DialogSystem.DialogReset = true;

                }

                //IF CHECK TO SEE IF THE NPC IS ENEMY OR NPC 
                //IF STATEMET IS NEW SO IF BUG DETECTED REMOVE THIS. 
                //if (targetsInViewRadius[i].gameObject.tag == "NPC")
                //{


                //     QuestObject QuestObjectScript = targetsInViewRadius[i].gameObject.GetComponent<QuestObject>();
                //     QuestObjectScript.enabled = true;
                //    // print("enable script obj");

                //   // print("enable script obj");

                //    //if (targetsInViewRadius[i].gameObject.GetComponent("QuestObject"))
                //    //{
                //    //    print("enable script obj");
                //    //    QuestObjScript.enabled = true;
                //    //}

                //}
               


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
