using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    private GameObject settingMenu;
    private GameObject promptText;

    private void Start()
    {
        settingMenu = GameObject.Find("SettingCavas");
        promptText = GameObject.Find("Win");
        if(promptText != null)
            promptText.SetActive(false);
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
        promptText.SetActive(false);
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
        promptText.GetComponent<Text>().text = "Win";
        promptText.SetActive(true);
    }

    public void FailPrompt()
    {
        Debug.Log("Fail");
        promptText.GetComponent<Text>().text = "Fail";
        promptText.SetActive(true);
    }
}
