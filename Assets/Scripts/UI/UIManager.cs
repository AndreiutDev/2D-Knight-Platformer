using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static Player player;
    public Player nonstaticPlayer;
    public Animator nonstaticDeathgroupAnimator;
    public static Animator deathGroupAnimator;

    private static int currentSceneIndex;
    public static UIManager instance;
    void Awake()
    {
        player = nonstaticPlayer;
        deathGroupAnimator = nonstaticDeathgroupAnimator;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static IEnumerator playStartTransition(Animator transitionAnimator)
    {
        yield return new WaitForSeconds(1f);
        transitionAnimator.Play("transition_start");
    }
    public static IEnumerator playEndTransition(Animator transitionAnimator)
    {
        yield return new WaitForSeconds(0.1f);
        transitionAnimator.Play("transition_end");
    }
    public void ShowCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }
    public void HideCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
    public static void onDeathGroupClick()
    {
        player.playerActions.Revive();
    }
}
