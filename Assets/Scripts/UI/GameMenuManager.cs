using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenu;
    bool isMenuActive;
    private void Awake()
    {
        isMenuActive = false;
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
        PauseGame();
        UIManager.instance.ShowCanvasGroup(mainMenu);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuActive)
            {
                HideGameMenu();
            }
            else
            {
                ShowGameMenu();
            }
        }
    }
}
