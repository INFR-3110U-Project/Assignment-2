using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataFileHandler
{
    private string filePath = "";
    private string fileName = "";

    //create file in the right place
    public DataFileHandler(string path, string name)
    {
        filePath = path;
        fileName = name;
    }
    
    //load from file
    public GameData Load()
    {
        //use the provided file path
        string fullPath = Path.Combine(filePath, fileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            //deal with errors
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize data
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.Log("Couldn't read from path: " + fullPath + "\n" + e);
            }
        }

        return loadedData;
    }

    //save to file
    public void Save(GameData data)
    {
        //use the provided file path
        string fullPath = Path.Combine(filePath, fileName);
        try
        {
            //create path if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //change from C# into JSON type
            string dataToSave = JsonUtility.ToJson(data, true);

            //write data into file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter (stream))
                {
                    writer.Write(dataToSave);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Couldn't load this file: " + fullPath + "\n" + e);
        }
    }
}
