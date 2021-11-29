using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{

    private bool AlarmHasBeenRaised;
    public GameObject AlertedGuardRallyPoint;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Alarm.Instance.falseAlarm == true)
        {
            AlarmHasBeenRaised = false;
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
            Alarm.Instance.CameraCheckCostume();

            if (Alarm.Instance.fooledByCostume == false )
            {
                Alarm.Instance.RemoveCostumeProtection();

                if (Alarm.Instance.RaiseAlarm == false)
                {
                    Alarm.Instance.RaiseAlarm = true;
                }
              

                Objective.instance.UpdateScorePenelty(10.0f); //Penelty Points;

                //if (AlarmHasBeenRaised == true)
                //{
                    Alarm.Instance.endCallForHelp = false;
                 //   Alarm.Instance.CallForHelp(AlertedGuardRallyPoint);
                //}
            }
        }
    }

    //---------------------->
    private void OnTriggerStay(Collider collider)
    {
        //collider = Colider of the game object
        if ((collider.gameObject.tag == "Player"))
        {
            Alarm.Instance.CameraCheckCostume();

            if (Alarm.Instance.fooledByCostume == false)
            {
                Alarm.Instance.RaiseAlarm = true;
            }


        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //collider = Colider of the game object
        if (Alarm.Instance.RaiseAlarm == true)
        {
            AlarmHasBeenRaised = true;
        }
    }


    //Collision Detection function
    #endregion
}
