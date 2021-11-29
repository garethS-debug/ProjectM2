using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AddBoxCollider : MonoBehaviour {

    public GameObject targetObject;
    static Animator anim;
    private bool BoxCol;


    // Use this for initialization
    void Start () 
    {
        //Vector3 newScale = this.gameObject.transform.localScale;

        BoxCol = false;
       

    }

    // Update is called once per frame
    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Y) && BoxCol == false)
        {
            BoxCollider();

        }

       

    }


    public void BoxCollider ()
    {
        BoxCol = true;
        BoxCollider _bc = (BoxCollider)targetObject.gameObject.AddComponent(typeof(BoxCollider));
        //_bc.center = updateTarget;
        _bc.isTrigger = true;
        Destroy(_bc, 1);
        //Vector3 newRotation = gameObject.transform.rotation.eulerAngles;
        Invoke("SetBoolBack", 1);
    }

    public void SetBoolBack ()
    {
        BoxCol = false;
    }
}
