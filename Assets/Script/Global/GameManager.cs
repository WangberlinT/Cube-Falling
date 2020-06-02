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
    private SettingMenu settingMenu;
    //Scene Control
    public static int Menu = 0;
    public static int LoadMapScene = 1;
    public static int MainScene = 2;
    public static int EditScene = 3;
    public static int StoryScene = 4;
    public static bool DebugMode = false;
    private static string mapSelected;//TODO: set 默认地图
    private static GameManager instance;
    private InputController inputController;
    private static GameMode gameMode;
    private static bool gameover = false;
   

    //Debug
    private DebugScreen screen;

    void Start()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;
            debugScreen = GameObject.Find("Debug Screen");
            debugScreen.SetActive(false);
            world = GameObject.Find("World").GetComponent<World>();
            screen = DebugScreen.GetInstance();
            player = GameObject.Find("Player");
            settingMenu = GetComponent<SettingMenu>();
            if (MainScene == SceneManager.GetActiveScene().buildIndex)
            {
                inputController = new PlayerInputController();
                screen.Log("[InputController]", "Play");
            } 
            else
            {
                screen.Log("[InputController]", "Edit");
                inputController = new EditInputController();
            }
                
            inputController.StartAll();
            WorldInit();
        }
        
    }

    void Update()
    {
        GetGlobalInput();
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public static void SetGameMode(GameMode mode)
    {
        gameMode = mode;
    }

    public static GameMode GetGameMode()
    {
        return gameMode;
    }


    public static void SetWorldMap(string map)
    {
        mapSelected = map;
    }
    public void Replay()
    {
        gameover = false;
        world.Replay();
        UpdateSettingMenu();
    }

    public void Win()
    {
        if(!gameover)
        {
            gameover = true;
            settingMenu.WinPrompt();
            UpdateSettingMenu();
        }
        
    }

    public void Fail()
    {
        if(!gameover)
        {
            gameover = true;
            settingMenu.FailPrompt();
            UpdateSettingMenu();
        }
        
    }

    public void SaveWorld()
    {
        world.SaveWorld();
    }

    private void WorldInit()
    {
        gameover = false;
        if(mapSelected != null)
            world.LoadWorld(mapSelected);
        //else load default
    }

    private void GetGlobalInput()
    {
        
        if (Input.GetKeyDown(KeyCode.F1))
            world.SaveWorld();
        if (Input.GetKeyDown(KeyCode.F2))
            world.LoadWorld("auto_save2020-05-25-16-23");
        
        if (Input.GetKeyDown(KeyCode.F3))
            debugScreen.SetActive(!debugScreen.activeSelf);
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (MainScene == SceneManager.GetActiveScene().buildIndex)
                SceneManager.LoadScene(EditScene);
            else if (EditScene == SceneManager.GetActiveScene().buildIndex)
                SceneManager.LoadScene(MainScene);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            //进入debug模式，人物可飞行，按键触发脚下方块陷落
            DebugMode = !DebugMode;
            DebugPlayerBehaviour d = player.GetComponent<DebugPlayerBehaviour>();
            d.enabled = !d.enabled;
        }
        screen.Log("[GameManager]", "DebugMode = " + DebugMode);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateSettingMenu();
        }
    }

    public void UpdateSettingMenu()
    {

        //打开设置提示栏
        settingMenu.UpdateActive();
        //禁用所有游戏控制
        if (settingMenu.GetMenuActive())
            inputController.StopAll();
        else
            inputController.StartAll();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(Menu);
    }
}

public enum GameMode
{
    SinglePlayer,EditMode
}
