using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBoxColliderNPC : MonoBehaviour {

    public GameObject targetObject;
    static Animator anim;
   


    // Use this for initialization
    void Start()
    {
        BoxCollider _bc = (BoxCollider)targetObject.gameObject.AddComponent(typeof(BoxCollider));
        _bc.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {


    }

}
