using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour {


    //reference to player
   // private PlayerData _player;


    //private void Awake()
    //{
    //    _player = GameObject.FindObjectOfType<PlayerData>();
    //}

    ////SAVE GAMEFILE
    //public void Save()
    //{
    //    //create a file or open a file to save 
    //    FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.OpenOrCreate); // create or open a file
    //    //Binary Formater  == allows us to write data to file 
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    //serialization method to write to a file.
    //    formatter.Serialize(file, _player.health);


    //    file.Close();
    //}

        //Saving the Game Brackeys
        public static void SavePlayer (PlayerInformation player)
    {
        ////create a file or open a file to save 
        //FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.OpenOrCreate); // create or open a file
        ////Binary Formater  == allows us to write data to file 
        //BinaryFormatter formatter = new BinaryFormatter();
        ////Getting Data
        //PlayerData data = new PlayerData(player);
        ////Getting Saved Information
        //formatter.Serialize(file, data);
        ////Close the file
        //file.Close();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        //Getting Saved Information
        formatter.Serialize(stream, data);
        //Close the file
        stream.Close();

    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.dat";
        //Check if file exists
         if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found" + path);
            return null;
        }
    }

   
}
