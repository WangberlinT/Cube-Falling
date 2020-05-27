using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
public static class SaveSystem
{
    public static string savingDir = Application.persistentDataPath+"/";
    public static string POSTFIX = ".data";
    public static void SaveWorld(World world)
    {
        DateTime date = DateTime.Now;
        WorldData data = new WorldData(world);
        string fileName = "auto_save" + date.ToString("yyyy-MM-dd-HH-mm") + POSTFIX;
        string path = savingDir + fileName;
        
        Save(path, data);
        Debug.Log("Save world" + fileName + " successfully in " + savingDir);
    }

    private static void Save(string path,object data)
    {
        System.IO.FileInfo file = new System.IO.FileInfo(path);
        file.Directory.Create();
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static WorldData LoadWorld(string worldName)
    {
        string path = savingDir + worldName + POSTFIX;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            WorldData data = formatter.Deserialize(stream) as WorldData;
            return data;
        }
        else
        {
            throw new IOException("No such file");
        }
    }

     
}
