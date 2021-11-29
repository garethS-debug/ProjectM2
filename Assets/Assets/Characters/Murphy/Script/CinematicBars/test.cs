using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public bool testing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    if (testing == true)
        {
            if (CinematicBars.Instance != null)
           {
                CinematicBars.Instance.ShowBars();
            }
           

        }

    if (testing == false)
        {
            if (CinematicBars.Instance != null)
            {
                CinematicBars.Instance.HideBars();
            }
        }


    }
}
