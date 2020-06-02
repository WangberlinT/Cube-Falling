using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMap : MonoBehaviour
{
    private string mapSelected;//TODO: 设置默认加载地图
    private InputField mapNameInput;
    private static LoadMap instance;
    private List<MapElement> mapElementList = new List<MapElement>();

    void Start()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;
            mapNameInput = GameObject.Find("MapNameInput").GetComponent<InputField>();
            LoadMapFiles();
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
            mapElementList.Add(new MapElement(filename, container));
            Debug.Log(name);
        }
    }

    private void LoadMapFiles()
    {
        string path = SaveSystem.savingDir;
        Debug.Log("Load Map in " + path);
        string[] files = Directory.GetFiles(path, "*" + SaveSystem.POSTFIX);
        Debug.Log("Number: " + files.Length);
        GameObject container = GameObject.Find("MapContainer");
        //在视窗中创建
        GenerateMapElement(files, container);
    }

    private void ClearMapElements()
    {
        foreach (MapElement me in mapElementList)
            me.DistroyElement();
    }

    public void StartGame()
    {
        GameManager.SetWorldMap(mapSelected);
        GameManager.SetGameMode(GameMode.SinglePlayer);
        SceneManager.LoadScene(GameManager.MainScene);
    }

    public void Edit()
    {
        GameManager.SetWorldMap(mapSelected);
        GameManager.SetGameMode(GameMode.EditMode);
        SceneManager.LoadScene(GameManager.EditScene);
    }

    public void NewMap()
    {
        //TODO: 检查输入
        string mapName = mapNameInput.text;
        if(StringValidation.IsValidFileName(mapName))
        {
            SaveSystem.CreateNewWorld(mapName);
            //重载刷新选择栏
            ClearMapElements();
            LoadMapFiles();
        }
        else
        {
            //TODO:提示输入不合法
            Debug.Log("Invalid Input");
        }
        
    }

    public void DeleteMap()
    {

    }

    //检查文件名合法性
    
    public void Back()
    {
        SceneManager.LoadScene(GameManager.Menu);
    }


}
