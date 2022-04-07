using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    [SerializeField] public int index;
    [SerializeField] public Transform rightPoint;
    [SerializeField] public Transform leftPoint;
    [SerializeField] public Transform topPoint;
    [SerializeField] public Transform bottomPoint;
    [SerializeField] GameObject mapPlayerGameObject;
    MapPlayer mapPlayer;

    private void Awake()
    {
        mapPlayerGameObject = GameObject.Find("MapPlayer");
        mapPlayer = mapPlayerGameObject.GetComponent<MapPlayer>();
    }
    public void MovePlayerToThisMapPoint()
    {
        mapPlayer.transform.position = transform.position;
    }
    public void MoveRight(float step)
    {
        if (rightPoint != null)
        {
            mapPlayer.transform.position = rightPoint.transform.position;
            mapPlayer.currentMapPoint = rightPoint.GetComponent<MapPoint>();
        }
    }
    public void MoveLeft(float step)
    {
        if (leftPoint != null)
        {
            mapPlayer.transform.position = leftPoint.transform.position;
            mapPlayer.currentMapPoint = leftPoint.GetComponent<MapPoint>();
        }
    }
    public void MoveBottom(float step)
    {
        if (bottomPoint != null)
        {
            mapPlayer.transform.position = bottomPoint.transform.position;
            mapPlayer.currentMapPoint = bottomPoint.GetComponent<MapPoint>();
        }
    }
    public void MoveTop(float step)
    {
        if (topPoint != null)
        {
            mapPlayer.transform.position = topPoint.transform.position;
            mapPlayer.currentMapPoint = topPoint.GetComponent<MapPoint>();
        }
    }
    public void LoadScene()
    {
        if (index == 0)
        {
            ScenesManager.instance.LoadStartScene();
        }
        else if (index == 1)
        {
            ScenesManager.instance.LoadMapScene();
        }
        else if (index == 2)
        {
            ScenesManager.instance.LoadCaveScene();
        }
    }
}
