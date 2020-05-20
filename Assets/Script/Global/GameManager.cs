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
    void Awake()
    {
        debugScreen = GameObject.Find("Debug Screen");
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F3))
            debugScreen.SetActive(!debugScreen.activeSelf);
    }
}
