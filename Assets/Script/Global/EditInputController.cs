using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditInputController : MonoBehaviour,InputController
{
    private static EditInputController instance;
    private EditController controller;
    private GameObject crosshair;
    public static InputController GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        if (instance != null)
            Destroy(this);
        else
        {
            instance = this;
            controller = GameObject.Find("Player").GetComponent<EditController>();
            crosshair = GameObject.Find("Crosshair");
        }
        
    }

    public void StartAll()
    {
        controller.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        crosshair.SetActive(true);
    }

    public void StopAll()
    {
        controller.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        crosshair.SetActive(false);
    }
}
