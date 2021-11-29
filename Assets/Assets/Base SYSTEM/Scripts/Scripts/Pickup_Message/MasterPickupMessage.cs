using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPickupMessage : MonoBehaviour
{

    public static MasterPickupMessage Instance { get; set; }

   

    public bool ActivatePickupMessage;

    private Animator anim;

    private IEnumerator coroutine;

    public bool turnOffPickupMessage;
    public bool turnOnnPickupMessage;

    [Header("Timer")]
    private float Alarmtime = 1.50f;
    private float defaultAlarmTime;

    [Header("Text Box")]
    public GameObject PickupMessageImage;
    public GameObject pickupmessageText;
    public GameObject PickupMessageGO;

    [Header("Image Store")]
    public Sprite MurphyThinking;
    public Sprite MurphyCelebrating;
    public bool murphyCelebratingSprite;
    public bool murphyThinkingSprite;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }



    void Start()
    {
        defaultAlarmTime = Alarmtime;
        anim = PickupMessageGO.gameObject.GetComponent<Animator>();
        PickupMessageGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (turnOnnPickupMessage == true)
        {
            TurnonPickupMessage();
        }

        if (turnOffPickupMessage == true)
        {
            TurnoffPickupMessage();
        }


        if (murphyCelebratingSprite == true)
        {

        }
        if (murphyThinkingSprite == true)
        {

        }
    }

    public void TurnonPickupMessage()
    {
        PickupMessageGO.SetActive(true);
        turnOnnPickupMessage = false;
    }





    public void TurnoffPickupMessage()
    {
        anim.SetBool("isClosed", true);

        if (Alarmtime <= 0)
        {

            anim.SetBool("isClosed", false);
          
            Alarmtime = defaultAlarmTime;
            PickupMessageGO.SetActive(false);
            turnOffPickupMessage = false;
        }


        if (Alarmtime > 0)
        {
            Alarmtime -= Time.deltaTime;
        }


    }
}
