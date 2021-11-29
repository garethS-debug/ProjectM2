using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneEvent : MonoBehaviour
{

    public LootableItem lootableItemScript;
    private Animator anim;
  

    public float RotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lootableItemScript.startEventSequence == true)
        {
            //   transform.Rotate(new Vector3(0, 0, RotationSpeed), Space.Self);
            anim.SetBool("isMoving", true);
        }
    }

    void spawnSpecialEffect()
    {

    }
}
