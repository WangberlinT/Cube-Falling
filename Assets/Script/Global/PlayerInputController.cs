using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController :MonoBehaviour, InputController
{
    private PlayerControl playerControl;
    private OrbitCamera orbitCamera;

    private static PlayerInputController instance;
    // Start is called before the first frame update
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
            playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
            orbitCamera = GameObject.Find("Main Camera").GetComponent<OrbitCamera>();
        }
        
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
