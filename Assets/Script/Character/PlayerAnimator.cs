using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator animator;
    private DebugScreen screen;
    private const string TAG = "[Animator]";
    

    void Start()
    {
        animator = GetComponent<Animator>();
        screen = DebugScreen.GetInstance();
    }

    public void Jump()
    {
        screen.Log(TAG, "Jumping");
    }

    public void Run()
    {
        screen.Log(TAG, "Running");
        animator.SetBool("isRunning",true);
    }

    public void Idle()
    {
        screen.Log(TAG, "Stopping");
        animator.SetBool("isRunning", false);
        //TODO: Jump to Idle
    }

    public void Die()
    {
        screen.Log(TAG, "Died");
        //TODO: Die
        animator.SetBool("isDie", true);
    }

}
