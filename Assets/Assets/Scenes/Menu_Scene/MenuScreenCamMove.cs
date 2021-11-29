using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreenCamMove : MonoBehaviour
{

    public float speed;
    public float rotationSpeed;



   // public Transform StartTartget;
   // public Transform EndTartget;
   

    public float min = 2f;
    public float max = 3f;

    // Start is called before the first frame update
    void Start()    
    {
       // min = transform.position.x;
      //  max = transform.position.x + 3;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = Vector3.MoveTowards(transform.position, StartTartget.position, speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * speed, max - min) + min);


         transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));

        if (transform.rotation.eulerAngles.y > 10)
        {
            //  rotationSpeed = -0.2f;
            rotationSpeed = -Mathf.Abs(rotationSpeed);
        }

        if (transform.rotation.eulerAngles.y < 0.1)
        {
            rotationSpeed = Mathf.Abs(rotationSpeed);
        }

        print(transform.rotation.eulerAngles.y);

        //    transform.localEulerAngles = new Vector3(Mathf.Clamp(Camera.main.transform.localEulerAngles.x, -viewRange, viewRange), 0, 0);



    }
}
