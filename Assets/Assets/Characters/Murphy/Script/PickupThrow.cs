using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickupThrow : MonoBehaviour
{
    public static PickupThrow Instance { get; set; }

    [Header("Pick Up OBJ ")]
    [Tooltip("rSet by FOV")]
    public GameObject PickUpObj;
    public bool isPickedUp;
    public bool weCanPickUpObj;


    [Header("HideBody")]
    public GameObject bodyHideLOC;
    public bool weCanDumpBody;
    public GameObject bodyToHide;
    public GameObject leftHand;
    public GameObject rightHand;



    [Header("Pick Up Location")]
    [Tooltip("Location of Pick up")]
    public Transform ItemPickUpLOC;
    public Transform BodyPickupLOC;

    [Header("Rigidbody")]
    public Rigidbody rb;

    [Header("Buttons")]
    public KeyCode PickUpButton = KeyCode.P;
    public KeyCode ThrowButton = KeyCode.L;

    [Header("Throw Force")]
    [Tooltip("Throw force")]
    public int ThrowForce;


    public bool throwingOBJ;

    [Header("Material")]
    public Material MaterialSwap;
    public Material CostumeMaterial;
    public Material OriginalMaterial;
    private DistractableOBJ distractableOBJ;

    [Header("Prompt")]
    public GameObject Prompt;
    private GameObject PromptInstance;
    public List<GameObject> PrompList = new List<GameObject>();
    public bool stopSpawn = false;
    //   private TextMeshProUGUI m_TextComponent;
    //   private DistractableOBJ objectInfo;
    public bool itemPickupDestroyPrompt;

    [Header("Effects")]
    public ParticleSystem PickupVFX;
    private bool hasPickupEffect;

    //Anim
    private Animator anim;
    private NewMurphyMovement newMurphyMovement;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        //else if (Instance != this)
        //{
        //    Destroy(gameObject);
        //}
    }


    public void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        newMurphyMovement = gameObject.GetComponent<NewMurphyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
     

        if (Input.GetKeyDown(PickUpButton) && throwingOBJ == false)
        {
            if (weCanPickUpObj == true)
            {


                isPickedUp = !isPickedUp;

            }

            if (weCanDumpBody)
            {
                newMurphyMovement.letsHidetheBody = true;
           
            }

            //    break;
        }

    

        if (isPickedUp == true)
        {
            DestroyPrompt();

            if (PickUpObj != null)
            {
          

                if (PickUpObj.gameObject.tag == "PickupBody")
                {
                    bodyToHide = PickUpObj.gameObject;

                    PickUpObj.transform.position = BodyPickupLOC.transform.position;
                    PickUpObj.transform.rotation = BodyPickupLOC.transform.rotation;

                    anim.SetBool("isDrag", true);
                    newMurphyMovement.isDragging = true;
                    
                }


                    if (PickUpObj.gameObject.tag == "PickUpOBJ" || PickUpObj.gameObject.tag == "Distraction")
                {

                    PickUpObj.transform.position = ItemPickUpLOC.transform.position;
                    PickUpObj.transform.rotation = ItemPickUpLOC.transform.rotation;

                    distractableOBJ = PickUpObj.gameObject.GetComponent<DistractableOBJ>();
                    distractableOBJ.TurnUndistractable = false;

                }
            }

          //  isPickedUp =false;

        }

        if (isPickedUp == false)
        {
            if (anim!=null)
            {
                anim.SetBool("isDrag", false);
            }

            if (newMurphyMovement != null)
            {
                newMurphyMovement.isDragging = false;
            }
         
        }
        


        if (Input.GetKey(ThrowButton) )//&& Input.GetKey(PickUpButton))
        {
            if (PickUpObj != null)
            {
                isPickedUp = false;
                throwingOBJ = true;
                //  objInMotion = true;
            }
        }

        if (Input.GetKeyUp(ThrowButton))
        {
            
            throwingOBJ = false;
        
        }

        if (throwingOBJ == true)
        {
            if (PickUpObj != null)
            {
                    PickUpObj.gameObject.tag = "Distraction";
                    PickUpObj.gameObject.layer = 15; // distraction layer
                    rb.AddForce(transform.forward * ThrowForce );
            }
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUpOBJ" || other.gameObject.tag == "Distraction" )
        {
            weCanPickUpObj = true;

            PickUpObj = other.gameObject;
            rb = PickUpObj.GetComponent<Rigidbody>();

            if (other.gameObject.GetComponent<MeshRenderer>() != false)
            {
                OriginalMaterial = other.gameObject.GetComponent<MeshRenderer>().material;
            }
            // objectInfo = other.gameObject.GetComponent<DistractableOBJ>();

            distractableOBJ = PickUpObj.gameObject.GetComponent<DistractableOBJ>();

            if (Prompt != null)
            {
               
                if (stopSpawn == false)
                {
                        PromptInstance = Instantiate(Prompt, new Vector3(other.transform.position.x, other.transform.position.y + 2.5f, other.transform.position.z), Prompt.transform.rotation);
                    //PromptInstance.gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>().text;
                      //  m_TextComponent = PromptInstance.gameObject.GetComponent<TextMeshProUGUI>();

                    //    m_TextComponent.text = objectInfo.PromptText;


                   
                    PrompList.Add(PromptInstance);


                    //foreach (GameObject prompt in PrompList)
                    //{
                    //    prompt.transform.LookAt(PlayerManager.instance.camera.transform);
                    //}

                    stopSpawn = true;
                }
                
            }


        }

        if (other.gameObject.tag == "InventoryItem")
        {
            weCanPickUpObj = true; 

            OriginalMaterial = other.gameObject.GetComponent<MeshRenderer>().material;

            if (Prompt != null)
            {

                if (stopSpawn == false)
                {
                    PromptInstance = Instantiate(Prompt, new Vector3(other.transform.position.x, other.transform.position.y + 2.5f, other.transform.position.z), Prompt.transform.rotation);
                    //PromptInstance.gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>().text;
                    //  m_TextComponent = PromptInstance.gameObject.GetComponent<TextMeshProUGUI>();

                    //    m_TextComponent.text = objectInfo.PromptText;



                    PrompList.Add(PromptInstance);

                    //foreach (GameObject prompt in PrompList)
                    //{
                    //    prompt.transform.LookAt(PlayerManager.instance.camera.transform);
                    //}

                    stopSpawn = true;
                }

            }
        }


        if (other.gameObject.tag == "HideBody")
        {
            bodyHideLOC = other.gameObject;

            if (other.gameObject.GetComponent<MeshRenderer>() != false)
            {
                OriginalMaterial = other.gameObject.GetComponent<MeshRenderer>().material;
            }

            if (Prompt != null)
            {

                if (stopSpawn == false)
                {
                    PromptInstance = Instantiate(Prompt, new Vector3(other.transform.position.x, other.transform.position.y + 2.5f, other.transform.position.z), Prompt.transform.rotation);
                    //PromptInstance.gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>().text;
                    //  m_TextComponent = PromptInstance.gameObject.GetComponent<TextMeshProUGUI>();

                    //    m_TextComponent.text = objectInfo.PromptText;



                    PrompList.Add(PromptInstance);

                    //foreach (GameObject prompt in PrompList)
                    //{
                    //    prompt.transform.LookAt(PlayerManager.instance.camera.transform);
                    //}

                    stopSpawn = true;
                }

            }

        }

            if (other.gameObject.tag == "Door")
        {

            if (other.gameObject.GetComponent<DoorChecker>() != null )
            {
                print("I am a door");

                other.gameObject.GetComponent<DoorChecker>().CheckForRequirement();

                if (other.gameObject.GetComponent<DoorChecker>().isEventCompleted == false)
                {
                    if (Prompt != null)
                    {
                        if (stopSpawn == false)
                        {
                            PromptInstance = Instantiate(Prompt, new Vector3(other.transform.position.x, other.transform.position.y + 2.5f, other.transform.position.z), Prompt.transform.rotation);
                            //PromptInstance.gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>().text;
                            //  m_TextComponent = PromptInstance.gameObject.GetComponent<TextMeshProUGUI>();

                            //    m_TextComponent.text = objectInfo.PromptText;

                            PrompList.Add(PromptInstance);

                            //foreach (GameObject prompt in PrompList)
                            //{
                            //    prompt.transform.LookAt(PlayerManager.instance.camera.transform);
                            //}

                            stopSpawn = true;
                        }

                    }
                }
            }
         
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PickUpOBJ" || other.gameObject.tag == "Distraction" )
        {
            weCanPickUpObj = true;
            PickUpObj = other.gameObject;
            rb = PickUpObj.GetComponent<Rigidbody>();
            if (other.gameObject.GetComponent<MeshRenderer>() != false)
            {
                other.gameObject.GetComponent<MeshRenderer>().material = MaterialSwap;
            }
            
        }

        if (other.gameObject.tag == "HideBody")
        {
            if (other.gameObject.GetComponent<MeshRenderer>() != false)
            {
              //  other.gameObject.GetComponent<MeshRenderer>().material = MaterialSwap;
            }

            if (newMurphyMovement != null)
            {
                if (newMurphyMovement.isDragging == true)
                {

                    weCanDumpBody = true;
                    
                    
                    //print("Lets Drop this off");

                }
            }



        }


            if ( other.gameObject.tag == "PickupBody")
        {
            weCanPickUpObj = true;

            PickUpObj = other.gameObject;
            rb = PickUpObj.GetComponent<Rigidbody>();


            if (Prompt != null)
            {

                if (stopSpawn == false)
                {
                    PromptInstance = Instantiate(Prompt, new Vector3(other.transform.position.x, other.transform.position.y + 2.5f, other.transform.position.z), Prompt.transform.rotation);
                    //PromptInstance.gameObject.GetComponentsInChildren<TMPro.TextMeshProUGUI>().text;
                    //  m_TextComponent = PromptInstance.gameObject.GetComponent<TextMeshProUGUI>();

                    //    m_TextComponent.text = objectInfo.PromptText;



                    PrompList.Add(PromptInstance);


                    //foreach (GameObject prompt in PrompList)
                    //{
                    //    prompt.transform.LookAt(PlayerManager.instance.camera.transform);
                    //}

                    stopSpawn = true;
                }

            }
        }




        if (other.gameObject.tag == "InventoryItem")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = CostumeMaterial;
        }

        //if (other.gameObject.tag == "InventoryItem")
        //{
        //    other.gameObject.GetComponent<MeshRenderer>().material = MaterialSwap;
        //}

    }

    private void OnTriggerExit(Collider other)
    {


        if (other.gameObject.tag == "PickUpOBJ" || other.gameObject.tag == "Distraction" )
        {
            weCanPickUpObj = false;

            other.gameObject.GetComponent<MeshRenderer>().material = OriginalMaterial;

            DestroyPrompt();

            distractableOBJ.TurnOriginalMaterial = true;
        }


        if (other.gameObject.tag == "HideBody")
        {
            if (other.gameObject.GetComponent<MeshRenderer>() != false)
            {
                other.gameObject.GetComponent<MeshRenderer>().material = OriginalMaterial;
            }
            DestroyPrompt();

        }

        if (other.gameObject.tag == "InventoryItem")
        {
            other.gameObject.GetComponent<MeshRenderer>().material = OriginalMaterial;

            DestroyPrompt();
        }

        if ( other.gameObject.tag == "PickupBody")
        {
            weCanPickUpObj = false;

            DestroyPrompt();
        }


        if (other.gameObject.tag == "Door")
        {

            DestroyPrompt();
        }

        // Destroy(PromptInstance);


        PickUpObj = null;
        
    }

    public void DestroyPrompt()
    {
        
        if (PrompList != null)
        {

            for (var i = PrompList.Count - 1; i > -1; i--)
            {
                if (PrompList[i] == null)
                    PrompList.RemoveAt(i);
            }
            foreach (GameObject prompt in PrompList)
            {
                //  print("prompt list Loop and destroy");
                //  print(prompt.gameObject.name);
                //if (PrompList.Count > 1)
                //{
                //    PrompList.Remove(prompt);

                //}
                
                if (prompt != null)
                {
                    Destroy(prompt);
                    stopSpawn = false;
                }
              
            }

            

        }
       
    }

    public void PickupEffect()
    {
        if (hasPickupEffect != true)
        {
            PickupVFX.Play();
            hasPickupEffect = true;
        }
    }
}
