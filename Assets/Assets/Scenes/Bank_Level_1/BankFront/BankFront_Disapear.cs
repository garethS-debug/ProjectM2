using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankFront_Disapear : MonoBehaviour
{

    //public Animator anim;
    //private bool Disapear;
    //// Start is called before the first frame update

    //void Start()
    //{
    //    anim = this.gameObject.GetComponent<Animator>();  
    //}




    //private void OnTriggerEnter(Collider collider)
    //{
    //    //collider = Colider of the game object
    //    if ((collider.gameObject.tag == "Player"))
    //    {
    //        if (Disapear == false)
    //        {
    //            anim.SetBool("isDisapear", true);
    //            Disapear = true;
    //            print("Bank Front: disapear");
    //        }



    //        if (Disapear == true)
    //        {
    //            anim.SetBool("isDisapear", false);
    //            Disapear = false;
    //        }


    //    }
    //}

    //private void OnTriggerStay(Collider collider)
    //{
    //    //collider = Colider of the game object
    //    if ((collider.gameObject.tag == "Player"))
    //    {
    //        Disapear = true;
    //        anim.SetBool("isDisapear", true);

    //    }
    //}

    //private void OnTriggerExit(Collider collider)
    //{
    //    //collider = Colider of the game object
    //    if (collider.gameObject.tag == "Player")
    //    {
    //        anim.SetBool("isDisapear", false);
    //        Disapear = false;

    //    }

    //}

    //public Material dissapearMaterial;

    //private float PlayerDistance;
    //private float MaxDistance = 500f;

    //private GameObject player;

    //private void Start()
    //{
    //  //  dissapearMaterial = this.gameObject.GetComponent<Material>();
    //    player = GameObject.FindGameObjectWithTag("Player");
    //   // PlayerDistance = MaxDistance;
      
    //}

    //private void Update()
    //{
    //    PlayerDistance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
    //    PlayerDistance = PlayerDistance / MaxDistance;
    //    dissapearMaterial.SetFloat("Range", PlayerDistance) ;
    //    print("Shader Distance" + PlayerDistance);
    //}
}
