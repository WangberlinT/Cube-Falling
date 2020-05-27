using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    MenuController controller;
    private void Start()
    {
        controller = MenuController.GetInstance();
    }

    public void PlayGame()
    {
        controller.GotoPlayMenu();
    }
    public void OptionButton()
    {
        controller.GotoOptionMenu();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
