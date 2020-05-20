using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  在游戏内显示的Debug窗口，按F3显示
 *  其他类通过GetInstance获取DebugScreen实例
 */
public class DebugScreen : MonoBehaviour
{
    private Text text;
    private float frameRate = 0;
    private float timer = 0;
    private static DebugScreen instance;
    //<TAG,Message> debug 信息显示存储
    private Dictionary<string, string> logRecord = new Dictionary<string, string>();
    private const string DEBUGINIT = "[Debug] Init Successfully\n";
    void Awake()
    {
        text = GetComponent<Text>();
        instance = GetComponent<DebugScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        ClearScreen();
        FPSUpdate();
        MessageUpdate();
    }

    private void MessageUpdate()
    {
        PrintLn(frameRate + " fps");
        foreach (KeyValuePair<string, string> p in logRecord)
        {
            PrintLn(p.Key + " " + p.Value);
        }
    }

    private void FPSUpdate()
    {
        if (timer > 1f)
        {
            frameRate = (int)(1f / Time.unscaledDeltaTime);
            timer = 0;
        }
        else
        {
            timer += Time.unscaledDeltaTime;
        }
    }

    private void ClearScreen()
    {
        text.text = DEBUGINIT;
    }

    private void PrintLn(string line)
    {
        text.text += line + "\n";
    }

    public void Log(string tag, string message)
    {
        logRecord[tag] = message;
    }

    public static DebugScreen GetInstance()
    {
        return instance;
    }
}
