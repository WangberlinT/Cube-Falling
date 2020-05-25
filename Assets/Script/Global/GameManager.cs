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
    //Scene Control
    public static int MainScene = 0;
    public static int EditScene = 1;

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
            if (SceneManager.GetSceneAt(MainScene).Equals(SceneManager.GetActiveScene()))
                SceneManager.LoadScene(1);
            else if (SceneManager.GetSceneAt(EditScene).Equals(SceneManager.GetActiveScene()))
                SceneManager.LoadScene(0);
        }
            
    }
}
