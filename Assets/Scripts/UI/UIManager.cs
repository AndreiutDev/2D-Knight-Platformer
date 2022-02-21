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

    void Awake()
    {
        player = nonstaticPlayer;
        deathGroupAnimator = nonstaticDeathgroupAnimator;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
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
    public static void onDeathGroupClick()
    {
        player.playerActions.Revive();
    }
}
