using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    private GameObject settingMenu;
    private GameObject winText;

    private void Start()
    {
        settingMenu = GameObject.Find("SettingCavas");
        winText = GameObject.Find("Win");
        winText.SetActive(false);
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

    public void WinPrompt()
    {
        Debug.Log("Win");
        winText.SetActive(true);
    }
}
