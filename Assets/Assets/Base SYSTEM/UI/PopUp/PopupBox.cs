using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PopupBox : MonoBehaviour
{
    public GameObject PopupImage;

    private PopupBox popupBox;

    //[Header("If Dialog Box not activated elsewhere")]
    //public GameObject DialogBox;
    //public bool ActivateDialogBox;

    private Animator anim;
    private float CountdownTimer = 1.2f;
    private float CountDownTimerMax;
    private bool StartCountdown;
    private bool BoxOpen;


  
    [Header("Button to close box")]
    public KeyCode CloseDialogBox;


    [Header("Pause Timer")]
    private float PauseCountdownTimer = 2f;
    private float PauseCountDownTimerMax;

    public bool hintGiven;

    [Header("UIToturial")]
    public bool isUITutorial;


    // Start is called before the first frame update
    void Start()
    {
        popupBox = this.gameObject.GetComponent<PopupBox>();
        anim = PopupImage.gameObject.GetComponent<Animator>();
        PopupImage.gameObject.SetActive(false);
        CountDownTimerMax = CountdownTimer;
        PauseCountDownTimerMax = CountdownTimer;
        StartCountdown = false;
        popupBox.hintGiven = false;


        if (isUITutorial == true)
        {
            PopupImage.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(CloseDialogBox))
        {
            if (BoxOpen == true)
            {
                PopupImage.gameObject.SetActive(false);
               popupBox.hintGiven = true;
            }
        }

        if (Input.GetKey(CloseDialogBox))
        {
            if (isUITutorial == true)
            {
                PopupImage.gameObject.SetActive(false);
            }
        }

        if (StartCountdown ==true)
        {
            if (CountdownTimer <= 0.1f)
            {
                PopupImage.gameObject.SetActive(false);
                StartCountdown = false;
                CountdownTimer = CountDownTimerMax;
               // popupBox.hintGiven = true;
                BoxOpen = false;
            }

            if (CountdownTimer >= 0.1f)
            {
                //if (ActivateDialogBox == true)
                //{
                //    DialogBox.gameObject.SetActive(false);
                //}
                anim.SetBool("isClosed", true);
                CountdownTimer -= Time.deltaTime;
            }
        }
    }

//    private void OnTriggerEnter(Collider collider)
//    {
//        //collider = Colider of the game object
//        if (collider.gameObject.tag == "Player" && hintGiven == false)
//        {
////            MasterPickupMessage.Instance.turnOnnPickupMessage = true;
//            PopupImage.gameObject.SetActive(true);
//            hintGiven = true;
//            StartCountdown = false;
//        }
//    }

    private void OnTriggerStay(Collider collider)
    {
        //if (ActivateDialogBox == true)
        //{
        //    DialogBox.gameObject.SetActive(true);
        //}

        //collider = Colider of the game object
        if (collider.gameObject.tag == "Player")
        {
        if (popupBox.hintGiven == false)
            {
                //            MasterPickupMessage.Instance.turnOnnPickupMessage = true;
                PopupImage.gameObject.SetActive(true);
                BoxOpen = true;
                anim.SetBool("isClosed", false);
                StartCountdown = false;
            }
         
        }
    }

    private void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {

            StartCountdown = true;
        }

    }

    public void tutorialPopUp()
    {

        if (popupBox.hintGiven == false)
        {
            //            MasterPickupMessage.Instance.turnOnnPickupMessage = true;
            PopupImage.gameObject.SetActive(true);
            BoxOpen = true;
            anim.SetBool("isClosed", false);
            StartCountdown = false;
            PauseGame();


        }



        if (Input.GetKey(CloseDialogBox))
        {
            if (BoxOpen == true)
            {
                PopupImage.gameObject.SetActive(false);
                popupBox.hintGiven = true;
                ContinueGame();
            }
        }

    }

    public void PauseGame()
    {

        if (PauseCountdownTimer <= 0.1f)
        {
          
            Time.timeScale = 0;
        }

        if (PauseCountdownTimer >= 0.1f)
        {
            PauseCountdownTimer -= Time.deltaTime;
        }


      
        //  pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        PauseCountdownTimer = PauseCountDownTimerMax;
        // pausePanel.SetActive(false);
        //enable the scripts again
    }

}
