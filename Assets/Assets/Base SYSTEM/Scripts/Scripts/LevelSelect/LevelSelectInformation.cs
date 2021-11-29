using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectInformation : MonoBehaviour
{
    [Header("Icons")]
    public Image StaffRotaIcon;
    public Image Events;
    public Image Key;

    [Header("Level Information - ID")]
    public int LevelID;

    [Header("Save File")]
    public SaveInformation saveFile;
    // Start is called before the first frame update
    void Start()
    {

        Color GuardRotaIcon = StaffRotaIcon.color;
        GuardRotaIcon.a = 0;
        StaffRotaIcon.color = GuardRotaIcon;

        Color Eventss = Events.color;
        Eventss.a = 0;
        Events.color = Eventss;

        Color Keys = Key.color;
        Keys.a = 0;
        Key.color = Keys;


        foreach (HeistLevelListInfo levelInfo in saveFile.HeistLevel)
        {
            if (levelInfo.LevelID == LevelID)
            {
                

                if (levelInfo.hasKeyLocation == true)
                {
                    Color Keyss = Key.color;
                    Keyss.a = 1;
                    Key.color = Keyss;
                }
            }

            else
            {
                break;
            }
          
        }




    }

}
