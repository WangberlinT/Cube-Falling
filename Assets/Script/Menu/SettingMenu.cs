using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    private GameObject settingMenu;

    private void Start()
    {
        settingMenu = GameObject.Find("SettingCavas");
        settingMenu.SetActive(false);
    }

    public void UpdateActive()
    {
        settingMenu.SetActive(!settingMenu.activeSelf);
    }

    public bool GetMenuActive()
    {
        return settingMenu.activeSelf;
    }

    public void Replay()
    {
        Debug.Log("Click");
        GameManager.GetInstance().Replay();
        GameManager.GetInstance().UpdateSettingMenu();
    }

    public void Back()
    {
        GameManager.GetInstance().BackToMenu() ;
    }

    public void Save()
    {
        GameManager.GetInstance().SaveWorld();
        //TODO: 保存提示
        Debug.Log("Save Successfully!");
    }

    public void Quit()
    {
        //TODO: 弹出提示框
        Application.Quit();
    }
}
