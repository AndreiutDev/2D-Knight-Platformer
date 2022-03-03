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
        mainMenuAnimator.Play("GameMenuHide");
    }
    public void ShowGameMenu()
    {
        resumeButton.Select();
        PauseGame();
        mainMenuAnimator.Play("GameMenuShow");
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
