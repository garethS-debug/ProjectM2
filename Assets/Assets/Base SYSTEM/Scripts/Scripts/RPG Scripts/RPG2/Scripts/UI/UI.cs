using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {

    public GameObject MessagePanel;

public void  OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);

        //set other message text here
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }
}
