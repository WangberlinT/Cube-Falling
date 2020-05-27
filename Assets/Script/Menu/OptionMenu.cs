using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    private MenuController controller;

    private void Start()
    {
        controller = MenuController.GetInstance();
    }

    public void Back()
    {
        controller.GotoMainMenu();
    }
}
