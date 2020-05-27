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
        GameManager.GetInstance().Replay();
        GameManager.GetInstance().UpdateSettingMenu();
    }
}
