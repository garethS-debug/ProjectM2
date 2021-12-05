using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


//script for moving up ladder
public class MoveDownLadder2 : MonoBehaviour
{
    //GameObject
    public GameObject TopSpawnTarget;       //this is th spawn target for the top of the ladder
    public GameObject BottomSpwnTrgt;
    //public GameObject ladder;           //Reference to the gameobject
    public GameObject PLayer;
    public GameObject TopLadder;
    public GameObject BottomLadder;

    //transform
    public Transform FaceTarget;
   // public Transform BottomLadderTarget;

    //Anim
    Animator anim;

    // Movement Bool
    public bool LetsGetMovingDown;
    public bool nowyoucangodown;
    public bool nowyoucanchangeyourmind;

    //float
    public float moveSpeed = 2.4f;
   // public float backwardsMovement = 1;

    //Path Follow
    public PathCreator pathcreator;
    public float distanceTraveled;
    private float startingDistance = 0.0f;
    public float MaxDistance = 3.0f;
    public float distancefromtarget;


    // Start is called before the first frame update
    void Start()
    {
       // PLayer = MurphyPlayerManager.instance.player;
        distanceTraveled = startingDistance;
        anim = PLayer.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (distancefromtarget >= 1.0f)
        {
            nowyoucanchangeyourmind = true;

        }

        if (LetsGetMovingDown == true)
        {

            distancefromtarget = Vector3.Distance(PLayer.transform.position, transform.position);
            print(distancefromtarget);

            if (distancefromtarget >= MaxDistance  )
            {
                LetsGetMovingDown = false;
                nowyoucangodown = false;
                nowyoucanchangeyourmind = false;


                PLayer.transform.position = TopSpawnTarget.transform.position;
                        
                        // Making sure  player is zerod
                        PLayer.transform.rotation  = Quaternion.Euler(0, 0, 0);
                        
                      
                       
                        // BottomLadder.gameObject.SetActive(true);
                       
                        StartCoroutine("DisableLadder");

                   
                        pathcreator.gameObject.SetActive(false);

                         print("MaxTop Reached");
                        distanceTraveled = startingDistance;
                        PLayer.gameObject.GetComponent<DuckMovement>().enabled = true;
                         TopLadder.gameObject.SetActive(false);
            }           

            //if (distancefromtarget >= MaxDistance && this.gameObject.tag == "TopLadder")
            //{
            //    PLayer.transform.position = BottomSpwnTrgt.transform.position;
            //    // TopLadder.gameObject.SetActive(false);
            //    LetsGetMovingDown = false;
            //    pathcreator.gameObject.SetActive(false);
            //    print("MaxBottom Reached");
            //    distanceTraveled = startingDistance;
            //    PLayer.gameObject.GetComponent<DuckMovement>().enabled = true;
            //}


            //WASD & Arrow Keys
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                anim.SetFloat("Speed", 0.5f);
                anim.SetBool("isWalking", true);
                nowyoucangodown = true;
                while (distancefromtarget <= MaxDistance)
                {
                    distanceTraveled -= moveSpeed * Time.deltaTime;
                    PLayer.transform.position = pathcreator.path.GetPointAtDistance(distanceTraveled);
                    // PLayer.transform.rotation = pathcreator.path.GetRotationAtDistance(distanceTraveled);
                    break;
                }
                // PLayer.transform.position = Vector3.MoveTowards(PLayer.transform.position, BottomLadderTarget.position, moveSpeed * Time.smoothDeltaTime);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
            {
                anim.SetFloat("Speed", 0.0f);
                anim.SetBool("isWalking", false);
                distanceTraveled += 0.0f;
                //ADD SLOW DOWN ANUME
                //anim.
                // PLayer.transform.position = Vector3.MoveTowards(transform.position, BottomLadderTarget.position, -1 * backwardsMovement * Time.smoothDeltaTime);
            }


            if (nowyoucangodown == true)
            {
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    anim.SetFloat("Speed", 0.5f);
                    anim.SetBool("isWalking", true);

                    while (distancefromtarget <= MaxDistance)
                    {
                        distanceTraveled += moveSpeed * Time.deltaTime;
                        PLayer.transform.position = pathcreator.path.GetPointAtDistance(distanceTraveled);
                        // PLayer.transform.rotation = pathcreator.path.GetRotationAtDistance(distanceTraveled);
                        break;
                    }
                    // PLayer.transform.position = Vector3.MoveTowards(PLayer.transform.position, BottomLadderTarget.position, moveSpeed * Time.smoothDeltaTime);
                }

                if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
                {
                    anim.SetFloat("Speed", 0.0f);
                    anim.SetBool("isWalking", false);
                    distanceTraveled += 0;
                    //ADD SLOW DOWN ANUME
                    //anim.
                    // PLayer.transform.position = Vector3.MoveTowards(transform.position, BottomLadderTarget.position, -1 * backwardsMovement * Time.smoothDeltaTime);
                }

            }

           




        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player" && this.gameObject.tag == "TopLadder" && LetsGetMovingDown == false)
        //{
        //    //Player Has Entered the ladder trigger
        //        Debug.Log("Player entered top ladder trigger");
        //        //Making Sure Player Looks at Ladder
        //        PLayer.transform.LookAt(FaceTarget);
        //        PLayer.transform.position = TopSpawnTarget.transform.position;
        //        //Disabling Traditional Movement
        //        other.gameObject.GetComponent<DuckMovement>().enabled = false;
        //        LetsGetMovingDown = true;
        //      //  BottomLadder.gameObject.SetActive(false);
        //}

        if (other.gameObject.tag == "Player" && LetsGetMovingDown == false)
        {
            anim.SetFloat("Speed", 0.0f);
            anim.SetBool("isBackward", false);

            //Player Has Entered the ladder trigger
            Debug.Log("Player entered bottom ladder trigger");
            //Making Sure Player Looks at Ladder
            PLayer.transform.LookAt(FaceTarget);
            PLayer.transform.position = BottomSpwnTrgt.transform.position;
            //Disabling Traditional Movement
            other.gameObject.GetComponent<DuckMovement>().enabled = false;
            LetsGetMovingDown = true;
            //TopLadder.gameObject.SetActive(false);

        }


        else if (other.gameObject.tag == "Player" && nowyoucanchangeyourmind == true)
        {
            //Player Has gove down the ladder. 
            PLayer.transform.position = BottomSpwnTrgt.transform.position;
            nowyoucanchangeyourmind = false;
            // Making sure  player is zerod
            PLayer.transform.rotation = Quaternion.Euler(0, 0, 0);
            BottomLadder.gameObject.SetActive(false);
            LetsGetMovingDown = false;
            nowyoucangodown = false;
            pathcreator.gameObject.SetActive(false);
            print("Decided Against that Reached");
            distanceTraveled = startingDistance;
            PLayer.gameObject.GetComponent<DuckMovement>().enabled = true;

        }

        // if (other.gameObject.tag == "Player" && this.gameObject.tag == "TopLadder" && LetsGetMovingDown == true)
        //{
        //        LetsGetMovingDown = false;
        //        pathcreator.gameObject.SetActive(false);
        //        print("MaxTop Reached");
        //        distanceTraveled = startingDistance;
        //        PLayer.transform.position = TopSpawnTarget.transform.position;
        //        PLayer.gameObject.GetComponent<DuckMovement>().enabled = true;

        //}


        // if (other.gameObject.tag == "Player" && this.gameObject.tag == "BottomLadder" && LetsGetMovingDown == true)
        //{


        //        print("MaxBottom Reached");
        //        distanceTraveled = startingDistance;
        //        PLayer.transform.position = BottomSpwnTrgt.transform.position;
        //        PLayer.gameObject.GetComponent<DuckMovement>().enabled = true;
        //        LetsGetMovingDown = false;
        //}

    }


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player" && this.gameObject.tag == "BottomLadder" && LetsGetMovingDown == true)
    //    {

    //      //  StartCoroutine("DisableLadder");
    //    }

    //}


        private IEnumerator DisableLadder()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            this.gameObject.SetActive(false);
            //yield return new WaitForSeconds(1f);
            //TopLadder.gameObject.SetActive(true);

        }
    }
}
