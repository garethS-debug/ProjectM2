using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AddBoxColliderShield : MonoBehaviour {

    public GameObject targetObject;
    static Animator anim;
    private bool BoxCol;


    // Use this for initialization
    void Start () 
    {
        //Vector3 newScale = this.gameObject.transform.localScale;
        BoxCollider _bc = (BoxCollider)targetObject.gameObject.AddComponent(typeof(BoxCollider));
        //_bc.isTrigger = true;
    }

    // Update is called once per frame
    void Update () 
    {
       
         
    }





}
