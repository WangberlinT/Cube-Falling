using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryControl : MonoBehaviour
{
    public Canvas Win_Canvas;
    public Canvas Fail_Canvas;
    void Awake()
    {
        //Win_Canvas.enabled = false;
        Fail_Canvas.enabled = false;
    }

    public void win(){
        Win_Canvas.enabled = true;
    }

    public void fail(){
        Fail_Canvas.enabled = true;
    }
}
