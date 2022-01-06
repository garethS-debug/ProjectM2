using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_Data", menuName = "player_Data/Player_Data_SO", order = 1)]
public class PlayerSO : ScriptableObject
{
    [Tooltip("2 = ghost 1= human")]
    public int PlayerCharacterChoise;

    public string PlayerName;

    public bool AutoConnect;

    [Tooltip("1 = multiplayer 2= Single Player")]
    public int SingleOrMultiPlayer;

}
