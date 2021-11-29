    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMenu : MonoBehaviour
{

    public static QuickMenu instance;

    public GameObject QuickMenuUI;  // The entire UI
    public KeyCode QuickMenuButton = KeyCode.Escape;
    // Start is called before the first frame update
    public bool ShowingQuickMenu;


    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Information to Save found");
        }
        instance = this; // creating a static variable. 
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(QuickMenuButton))
        {
            ShowingQuickMenu = !ShowingQuickMenu;

            if (ShowingQuickMenu == true)
            {
                QuickMenuUI.SetActive(!QuickMenuUI.activeSelf);
            }

            if (ShowingQuickMenu == false)
            {
                QuickMenuUI.SetActive(!QuickMenuUI.activeSelf);
            }
        }


    }

    public void Continue()
    {
        ShowingQuickMenu = !ShowingQuickMenu;

        if (ShowingQuickMenu == true)
        {
            QuickMenuUI.SetActive(!QuickMenuUI.activeSelf);
        }

        if (ShowingQuickMenu == false)
        {
            QuickMenuUI.SetActive(!QuickMenuUI.activeSelf);
        }
    }
}
