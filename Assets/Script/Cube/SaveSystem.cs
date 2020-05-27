using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
public static class SaveSystem
{
    public static string savingDir = Application.persistentDataPath+"/";
    public static string POSTFIX = ".data";
    //默认地图为defautlt.meta 玩家不可见，在生成新地图时作为模板
    private static string DEFAULT_MAP = "default.meta";
    public static void AutoSaveWorld(World world)
    {
        DateTime date = DateTime.Now;
        WorldData data = new WorldData(world);
        string fileName = "auto_save" + date.ToString("yyyy-MM-dd-HH-mm") + POSTFIX;
        string path = savingDir + fileName;
        
        Save(path, data);
        Debug.Log("Save world" + fileName + " successfully in " + savingDir);
    }

    public static void SaveWorld(World world, string name)
    {
        string path = savingDir + name + POSTFIX;
        Save(path, new WorldData(world));
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
        return LoadWorldData(path);
    }

    private static WorldData LoadWorldData(string path)
    {
        if (File.Exists(path))
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

    private static WorldData LoadDefaultWorld()
    {
        return LoadWorldData(savingDir + DEFAULT_MAP);
    }

    public static void CreateNewWorld(string worldName)
    {
        //TODO: 检查重复和覆盖
        WorldData data = LoadDefaultWorld();
        string path = savingDir + worldName + POSTFIX;
        Save(path, data);
    }

     
}
