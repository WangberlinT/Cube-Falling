﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
    MenuController controller;
    void Start()
    {
        controller = MenuController.GetInstance();
    }

    public void LoadEdit()
    {
        controller.GoToLoadMapScene();
    }

    public void Story()
    {
        controller.GoToStoryScene();
    }

    public void Back()
    {
        controller.GotoMainMenu();
    }

    public void Tutorial()
    {
        controller.GoToTutorial();
    }

    
}
