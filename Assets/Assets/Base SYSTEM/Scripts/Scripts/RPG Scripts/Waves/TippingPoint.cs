using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TippingPoint : MonoBehaviour {


    public float minAngle = 3.00f; // greater than +3 
    public float maxAngle = 356.00f; // greater than -4
    public float correctionSpeed = 0.1f;

    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;


    // Use this for initialization
    //public Quaternion originalRotationValue; // declare this as a Quaternion
    //float rotationResetSpeed = 1.0f;
    //------->

    private void Start()
    {
      //  originalRotationValue = transform.rotation; // save the initial rotation
    }

    private void Update()
    {

      //  Quaternion YMaxRotation = 

        eulerAngX = transform.localEulerAngles.x;
        eulerAngY = transform.localEulerAngles.y;
        eulerAngZ = transform.localEulerAngles.z;

        //EUL ANGLE X AXIS --------> 
        //   number (350) is less thn 355      and more than 3
       while (eulerAngX <  maxAngle && eulerAngX > 200) // number is above +3 and below -5
        {
            gameObject.transform.Rotate(correctionSpeed, 0, 0); // this rotates only 1 degree on y axis!

                                                  //    Debug.Log("xmax correcting with +");
                                                  // transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime / rotationResetSpeed);
                                                  //  print(eulerAngX);
            break;  
        }

        //number (6)    is more than 5    and less than 355
        while (eulerAngX > minAngle && eulerAngX < 199)
        {
            gameObject.transform.Rotate(-correctionSpeed, 0, 0); // this rotates only 1 degree on y axis!
                                                  // transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime / rotationResetSpeed);
                                                  ///transform.rotation = Quaternion.Euler(eulerAngX, 0.01f, Time.deltaTime);
            //Debug.Log("xmin correcting with -");
            break;
        }
        // EUL ANGLE X AXIS --------->


        //EUL ANGLE Z AXIS --------> 
        //   number (350) is less thn 355      and more than 3
        while (eulerAngZ < maxAngle && eulerAngZ > 200) // number is above +3 and below -5
        {
            gameObject.transform.Rotate(0, 0, correctionSpeed); // this rotates only 1 degree on y axis!
                                                  // transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime / rotationResetSpeed); ;
//            Debug.Log("zmax correcting with +");
            break;
        }

        //number (6)    is more than 5    and less than 355
        while (eulerAngZ > minAngle && eulerAngZ < 199)
        {
            gameObject.transform.Rotate(0, 0, -correctionSpeed); // this rotates only 1 degree on y axis!
                                                  // transform.rotation = Quaternion.Slerp(transform.rotation, originalRotationValue, Time.deltaTime / rotationResetSpeed);
                                                  // Debug.Log("zmin correcting with -");
            break;
        }
        // EUL ANGLE Z AXIS --------->

        //Y starts at -90 = 270 



        // ----- Y AXIS IS OK TO ROTATE AS THIS IS TURNING THE SHIOP LEFT OR RIGHT 
        /*

        //EUL ANGLE Y AXIS -------->
        //   number (350) is less thn 355      and more than 3
        while (eulerAngY < maxAngle && eulerAngY > 200) // number is above +3 and below -5
        {
            Debug.Log("Adjusting Up to 360");
          transform.localRotation = Quaternion.Euler(0, 90f * Time.deltaTime, 0);
            // Debug.Log("y correcting with +");
            break;
        }

        //while above 3 and below 199
        while (eulerAngY > minAngle && eulerAngY < 199)
        {
            Debug.Log("Adjusting Down to 0");
          //  eulerAngY -= correctionSpeed;
            // Debug.Log("y correcting with -");
            break;
        }
        // EUL ANGLE Y AXIS --------->


        */

    }

    /*
     * 
     *     Vector3 position;
    Quaternion rotation;

    Vector3 startPosition;
    public Quaternion startRotation;

    public float maxXangle = 350; // -10 degrees
    public bool putBack;

    //360 = 0  so - 1 is 359 and + 1 is +1 ? 
    //
    private Transform myLocation;
    Vector3 startPos;
    int waitTime = 0;

    private void Start()
    {
        position = this.transform.position;
        rotation = transform.rotation;
    }

    void Update()
    {
        if (transform.localEulerAngles.x > maxXangle)
        {
            putBack = true;
            StartCoroutine(takeMeHome());
        }
    }


    IEnumerator takeMeHome()
    {
        Debug.Log("take me home tonight");
        yield return new WaitForSeconds(waitTime);
        this.transform.position = startPosition;
        transform.rotation = startRotation;
        putBack = false;
    }

     */



    /*

    public GameObject myObject;
    public int MaxTipPoint;
    public float fixSpeed = 0.1f;


    private void Start()
    {

    }

    private void Update()
    {
        float ang = myObject.transform.rotation.eulerAngles.z;

        if (myObject.transform.rotation.eulerAngles.z < MaxTipPoint)
        {
            
             print(ang);// Do something
            ang += fixSpeed;
           
        }
    }


    */

    /*
     private Vector3 lastFwd;
    private float curAngleX = 0;
    private float rotAngle = 0.01f;


    void Start()
    {
        lastFwd = transform.forward;
    }

    void Update()
    {

        Vector3 curFwd = transform.forward;
        // measure the angle rotated since last frame:
        float ang = Vector3.Angle(curFwd, lastFwd);
        if (ang > rotAngle)
        { // if rotated a significant angle...
          // fix angle sign...
            if (Vector3.Cross(curFwd, lastFwd).x < 0) ang = -ang;
            curAngleX += ang; // accumulate in curAngleX...
            lastFwd = curFwd; // and update lastFwd
        }

        print(curAngleX);
    }

*/
}

