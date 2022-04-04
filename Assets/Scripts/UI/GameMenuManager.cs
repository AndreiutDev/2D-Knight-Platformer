using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] private CanvasGroup mainMenu;
    bool isMenuActive;

    Animator mainMenuAnimator;
    private void Awake()
    {
        isMenuActive = false;
        mainMenuAnimator = mainMenu.GetComponent<Animator>();
    }
    public void LoadMap()
    {
        ResumeGame();
        ScenesManager.instance.LoadMapScene();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void HideGameMenu()
    {
        isMenuActive = false;
        ResumeGame();
        UIManager.instance.HideCanvasGroup(mainMenu);
        
    }
    public void ShowGameMenu()
    {
        isMenuActive = true;
        resumeButton.Select();
        PauseGame();
        UIManager.instance.ShowCanvasGroup(mainMenu);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuActive)
            {
                Debug.Log("Menu Hidden!");
                HideGameMenu();
            }
            else
            {
                Debug.Log("Menu Shown!");
                ShowGameMenu();
            }
        }
    }
}
