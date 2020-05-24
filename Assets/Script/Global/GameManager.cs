using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GameManager 管理游戏事：
 * 1. 显示debug窗口，暂停，设置等
 */
public class GameManager : MonoBehaviour
{
    GameObject debugScreen;
    World world;
    void Awake()
    {
        debugScreen = GameObject.Find("Debug Screen");
        world = GameObject.Find("World").GetComponent<World>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            world.SaveWorld();
        if (Input.GetKeyDown(KeyCode.F2))
            world.LoadWorld("auto_save2020-05-24-23-46.data");
        if (Input.GetKeyDown(KeyCode.F3))   
            debugScreen.SetActive(!debugScreen.activeSelf);
        if (Input.GetKeyDown(KeyCode.F4))
        {
            //TODO:切换编辑模式
        }
            
    }
}
