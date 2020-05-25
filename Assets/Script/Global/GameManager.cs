using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * GameManager 管理游戏事：
 * 1. 显示debug窗口，暂停，设置等
 */
public class GameManager : MonoBehaviour
{
    private GameObject debugScreen;
    private World world;
    private GameObject player;
    //Scene Control
    public static int MainScene = 0;
    public static int EditScene = 1;
    public static bool DebugMode = false;

    //Debug
    private DebugScreen screen;

    void Awake()
    {
        debugScreen = GameObject.Find("Debug Screen");
        world = GameObject.Find("World").GetComponent<World>();
        screen = DebugScreen.GetInstance();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetGlobalInput();
    }

    private void GetGlobalInput()
    {
        
        if (Input.GetKeyDown(KeyCode.F1))
            world.SaveWorld();
        if (Input.GetKeyDown(KeyCode.F2))
            world.LoadWorld("auto_save2020-05-25-16-23.data");
        
        if (Input.GetKeyDown(KeyCode.F3))
            debugScreen.SetActive(!debugScreen.activeSelf);
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (SceneManager.GetSceneAt(MainScene).Equals(SceneManager.GetActiveScene()))
                SceneManager.LoadScene(1);
            else if (SceneManager.GetSceneAt(EditScene).Equals(SceneManager.GetActiveScene()))
                SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            //进入debug模式，人物可飞行，按键触发脚下方块陷落
            DebugMode = !DebugMode;
            DebugPlayerBehaviour d = player.GetComponent<DebugPlayerBehaviour>();
            d.enabled = !d.enabled;
        }
        screen.Log("[GameManager]", "DebugMode = " + DebugMode);
    }
}
