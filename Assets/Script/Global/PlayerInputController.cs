using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : InputController
{
    private PlayerControl playerControl;
    private OrbitCamera orbitCamera;

    private static PlayerInputController instance;
    // Start is called before the first frame update

    public PlayerInputController()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        orbitCamera = GameObject.Find("CameraHolder").GetComponent<OrbitCamera>();
    }

    public void StartAll()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerControl.enabled = true;
        orbitCamera.enabled = true;
    }

    public void StopAll()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerControl.enabled = false;
        orbitCamera.enabled = false;
    }
}
