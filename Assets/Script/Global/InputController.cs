using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController
{
    private PlayerControl playerControl;
    private OrbitCamera orbitCamera;

    private static InputController instance;
    // Start is called before the first frame update
    public static InputController GetInstance()
    {
        if (instance == null)
        {
            instance = new InputController();
            return instance;
        }
        else
            return instance;
    }

    private InputController()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        orbitCamera = GameObject.Find("Main Camera").GetComponent<OrbitCamera>();
    }

    public void StartAll()
    {
        playerControl.enabled = true;
        orbitCamera.enabled = true;
    }

    public void StopAll()
    {
        playerControl.enabled = false;
        orbitCamera.enabled = false;
    }
}
