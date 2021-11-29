using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPOS : MonoBehaviour
{

    public bool PleaseResetThisPOS;

    // Update is called once per frame
    void Update()
    {

        if (PleaseResetThisPOS == true)
        {
            this.transform.position = new Vector3(48.24016f, -20f, -147.0188f);
            PleaseResetThisPOS = false;
        }

    }
}
