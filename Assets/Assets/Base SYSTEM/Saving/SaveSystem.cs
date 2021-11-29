using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem 
{

    public static void SaveInformation (InformationToSave informationToSave)
    {
        //Creating the file
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);


        //Add information to file.
        SavedData data = new SavedData(informationToSave);

        //Write to file
        formatter.Serialize(stream, data);

        //Close File
        stream.Close();
    }







    public static SavedData LoadInformation ()
    {

        string path = Application.persistentDataPath + "/player.fun";
        //check if file exisits.
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

           SavedData data = formatter.Deserialize(stream) as SavedData;

            //remember to close stream otherwise errors. 
            stream.Close();

            return data;

        } else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }

    }
   
}
