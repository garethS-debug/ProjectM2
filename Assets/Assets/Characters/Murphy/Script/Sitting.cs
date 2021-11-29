using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitting : MonoBehaviour
{

    [Header("InteractionProperties (AutoSet)")]
    [Tooltip("references to player and interaction point")]

    public Transform player;
    public Transform interactionTransform;
    public GameObject PickUpTextBox;

    [Space(10)]

    [Header("TextBox")]
    [Tooltip("What message do you want to send to the text box")]
    public string MessageBoxText; // Text to be placed in text box

    [Space(10)]

    [Header("Interaction")]
    [Tooltip("Whats the 'GoDownstairs' Button")]
    public KeyCode SitDown = KeyCode.L;
    [Tooltip("Whats the floor to load")]
    //  public string FloorToLoad;
    public bool sit;
    public bool imSitting;


    [Header("Camera")]
    [Tooltip("Whats the Camera's new position")]
    public GameObject TheCamera;
    public Transform newCamPos;
   // public GameObject SitCam;

    [Header("Looking")]
    [Tooltip("Where to Look")]
    public Transform PlayerLookFocus;
    public Transform CameraLookFocus;

    [Header("Where to Teleport the PLayer")]
    [Tooltip("Where do you want to releport the player to")]
    public Transform TeleportTo;
    public Transform resetPOS;
    public Vector3 CamResetVector3;
    

    [Header("Reference to other script")]
    static Animator anim;
    private FieldOfView fieldOfView;
    private CameraOnRails cameraOnRails;
    private NewMurphyMovement newMurphyMovement;

    // Start is called before the first frame update
    void Start()
    {
    
      //  SitCam.SetActive(false);

        player = PlayerManager.instance.player.transform;
        interactionTransform = this.gameObject.transform;
        PickUpTextBox = GameObject.FindGameObjectWithTag("ItemPickUpText");
        cameraOnRails = TheCamera.gameObject.GetComponent<CameraOnRails>();
        anim = player.gameObject.GetComponent<Animator>();            // get the animator component attached to enemy
    }

    // Update is called once per frame
    void Update()
    {


        if (sit == true && Input.GetKeyDown(SitDown))// CHANGE THE PICKUP BUTTON HERE

        {
            //Vector3 targetDir = player.transform.position - TheCamera.transform.position; 
            //float angle = Vector3.Angle(targetDir, transform.forward);
            //print(angle);
            //float direction = angle / 360;
            //print(direction);

            // resetPOS = player.transform;
            //camReset = TheCamera.transform;
            //cameraOnRails.enabled = false;

            //    CamResetVector3 = TheCamera.transform.localPosition;


            //TheCamera.SetActive(false);
            //SitCam.SetActive(true);
            newMurphyMovement = player.gameObject.GetComponent<NewMurphyMovement>();
            newMurphyMovement.IsHidden = true;
           // anim.SetFloat("Speed", 0);
            player.transform.position = TeleportTo.transform.position;
            //   TheCamera.transform.position = newCamPos.transform.position;
          

          //  TheCamera.transform.LookAt(CameraLookFocus);
            player.transform.LookAt(PlayerLookFocus);
           //imSitting = true;
            anim.SetBool("isSitting", true);

            if (imSitting == false)
            {
                StartCoroutine(SitDownTimer());
                

            }

        }

        if (Input.GetKeyDown(SitDown) && imSitting == true)
        {
            //TheCamera.transform.position = camReset.position;
            //TheCamera.transform.LookAt(player);
            //TheCamera.transform.rotation = Quaternion.identity;

            //TheCamera.SetActive(true);
            //SitCam.SetActive(false);
            newMurphyMovement.enabled = true;

            anim.SetBool("isSitting", false);
            imSitting = false;
            newMurphyMovement.IsHidden = false;
            //  cameraOnRails.enabled = true;
            // player.transform.position = resetPOS.transform.position;

            //   TheCamera.transform.position = CamResetVector3;

            player.transform.localRotation = resetPOS.transform.localRotation;
        }






    }

    
    #region Colliders Detecting Player 
    //Collision Detection function
    //---------------------->
    private void OnTriggerEnter(Collider collider)
    {
        //collider = Colider of the game object
        if ((collider.gameObject.tag == "Player"))
        {
            //  PickupText.gameObject.SetActive(true);
            PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = MessageBoxText;
            sit = true;

            
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        //collider = Colider of the game object
        if ((collider.gameObject.tag == "Player"))
        {

            if (imSitting == true)
            {
                fieldOfView = collider.gameObject.GetComponent<FieldOfView>();
                fieldOfView.isSitting = true;
            }

            if (imSitting == false)
            {
                fieldOfView = collider.gameObject.GetComponent<FieldOfView>();
                fieldOfView.isSitting = false;
            }

        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //collider = Colider of the game object
        if (collider.gameObject.tag == "Player")
        {

            fieldOfView = collider.gameObject.GetComponent<FieldOfView>();
            fieldOfView.isSitting = false;

            //  PickupText.gameObject.SetActive(false);
            sit = false;
            PickUpTextBox.GetComponent<UnityEngine.UI.Text>().text = "";
        }
    }
    //Collision Detection function
    #endregion

    public IEnumerator SitDownTimer()
    {
        yield return new WaitForSeconds(0.25f);
        imSitting = !imSitting;
        newMurphyMovement.enabled = false;
    }

}
