using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    MenuController controller;
    void Start()
    {
        controller = MenuController.GetInstance();
    }

    public void LoadMap()
    {
        controller.GoToLoadMapScene();
    }

    public void Story()
    {
        controller.GoToStoryScene();
    }

    public void Edit()
    {
        controller.GoToEditScene();
    }

    public void Back()
    {
        controller.GotoMainMenu();
    }

    
}
