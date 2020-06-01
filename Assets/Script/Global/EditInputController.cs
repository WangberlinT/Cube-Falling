using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditInputController : InputController
{
    private static EditInputController instance;
    private EditController controller;
    private GameObject crosshair;
    public static InputController GetInstance()
    {
        if (instance == null)
            instance = new EditInputController();
        return instance;
    }

    private EditInputController()
    {
        controller = GameObject.Find("Player").GetComponent<EditController>();
        crosshair = GameObject.Find("Crosshair");
    }

    public void StartAll()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller.enabled = true;
        crosshair.SetActive(true);
    }

    public void StopAll()
    {
        controller.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        crosshair.SetActive(false);
    }
}
