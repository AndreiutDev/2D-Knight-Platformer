using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Start = 0,
    Map = 1,
    Cave = 2,
}
public static class ScenesExtensions
{
    public static string GetString(this Scenes scene)
    {
        switch (scene)
        {
            case Scenes.Start:
                return "Start";
            case Scenes.Map:
                return "Map";
            case Scenes.Cave:
                return "Cave";
            default:
                throw new System.Exception("Scene does not exist");
        }
    }
}

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(Scenes.Start.GetString());
    }
    public void LoadMapScene()
    {
        SceneManager.LoadScene(Scenes.Map.GetString());
    }
    public void LoadCaveScene()
    {
        SceneManager.LoadScene(Scenes.Cave.GetString());
    }
}