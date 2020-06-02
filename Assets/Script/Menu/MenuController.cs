using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    private const string MAINMENU = "MainMenu";
    private const string OPTIONMENU = "OptionMenu";
    private const string PLAYMENU = "PlayMenu";
    private const string STORYMENU = "StoryMenu";
    private List<GameObject> menus = new List<GameObject>();
    private static MenuController controller;
    private void Awake()
    {
        if (controller != null)
            Destroy(this);
        else
        {
            controller = this;
            menus.Add(GameObject.Find(MAINMENU));
            menus.Add(GameObject.Find(OPTIONMENU));
            menus.Add(GameObject.Find(PLAYMENU));
            menus.Add(GameObject.Find(STORYMENU));
            Init();
        }
    }

    public static MenuController GetInstance()
    {
        return controller;
    }

    private void Init()
    {
        OnlyActive(MAINMENU);
    }

    public void GotoMainMenu()
    {
        OnlyActive(MAINMENU);
    }

    public void GotoOptionMenu()
    {
        OnlyActive(OPTIONMENU);
    }

    public void GotoPlayMenu()
    {
        OnlyActive(PLAYMENU);
    }

    public void GotoStoryMenu()
    {
        OnlyActive(STORYMENU);
    }

    private void OnlyActive(string menuName)
    {
        foreach (GameObject g in menus)
        {
            if (g.name != menuName)
                g.SetActive(false);
            else
                g.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToLoadMapScene()
    {
        SceneManager.LoadScene(GameManager.LoadMapScene);
    }

    public void GoToStoryScene()
    {
        OnlyActive(STORYMENU);
    }

    public void GoToTutorial()
    {
        GameManager.SetWorldMap("Display");
        SceneManager.LoadScene(GameManager.MainScene);
    }
}
