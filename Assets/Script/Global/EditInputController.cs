using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditInputController : InputController
{
    private EditController controller;
    private GameObject crosshair;

    public EditInputController()
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
