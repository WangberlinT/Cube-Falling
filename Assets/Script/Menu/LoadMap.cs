using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMap : MonoBehaviour
{
    private string mapSelected;//TODO: 设置默认加载地图
    private static LoadMap instance;

    void Start()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;
            
            string path = SaveSystem.savingDir;
            Debug.Log("Load Map in " + path);
            string[] files = Directory.GetFiles(path, "*"+SaveSystem.POSTFIX);
            Debug.Log("Number: " + files.Length); 
            GameObject container = GameObject.Find("MapContainer");
            GenerateMapElement(files, container);
        }
        
    }

    public static LoadMap GetInstance()
    {
        return instance;
    }

    public void SetMap(string map)
    {
        mapSelected = map;
        Debug.Log(map);
    }

    private void GenerateMapElement(string[] filenames, GameObject container)
    {
        foreach(string name in filenames)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(name);
            new MapElement(filename, container);
            Debug.Log(name);
        }
    }

    public void StartGame()
    {
        GameManager.SetWorldMap(mapSelected);
        SceneManager.LoadScene(GameManager.MainScene);
    }



}
