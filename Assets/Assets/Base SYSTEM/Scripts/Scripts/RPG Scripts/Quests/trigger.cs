using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public QuestObject QuestObjScript;
    public bool TurnOffOnQuestObj;

    // Start is called before the first frame update
    void Start()
    {

        QuestObjScript = this.gameObject.GetComponent<QuestObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnOffOnQuestObj == true)
        {
            QuestObjScript.enabled = (true);
        }

        if (TurnOffOnQuestObj == false)
        {
            QuestObjScript.enabled = (false);
        }

        //else
        //{
        //    TurnOffOnQuestObj = false;
        //    QuestObjScript.enabled = (false);
        //}
    }
}
