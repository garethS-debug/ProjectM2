using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObj : MonoBehaviour {

    public GameObject Obj;
    public GameObject Bone;

    // Use this for initialization
    void Start () {




    }
	
	// Update is called once per frame
	void Update () {
        Obj.transform.parent = Bone.transform;

    }
}
