using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Animator nonstaticDeathgroupAnimator;
    public static Animator deathGroupAnimator;

    private static int currentSceneIndex;

    void Awake()
    {
        deathGroupAnimator = nonstaticDeathgroupAnimator;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public static IEnumerator playStartTransition(Animator transitionAnimor)
    {
        yield return new WaitForSeconds(1f);
        transitionAnimor.Play("transition_start");
    }
    public static IEnumerator playEndTransition(Animator transitionAnimor)
    {
        yield return new WaitForSeconds(0.2f);
        transitionAnimor.Play("transition_end");
    }
    public static void onDeathGroupClick()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
