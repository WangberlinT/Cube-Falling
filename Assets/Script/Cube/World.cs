using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * World Manager 管理游戏事件：显示debug窗口，暂停，设置等
 */
public class World : MonoBehaviour
{
    GameObject debugScreen;
    void Start()
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
