using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayer : MonoBehaviour
{
    public MapPoint[] MapPointList;
    public MapPoint currentMapPoint;
    public MapPlayerInput mapPlayerInput;


    [SerializeField] private float speed;

    private void Awake()
    {
        int currentMapIndex = PlayerPrefs.GetInt("ActiveMapPoint");
        currentMapPoint = MapPointList[currentMapIndex];
        mapPlayerInput = GetComponent<MapPlayerInput>();
    }
    private void Start()
    {
        currentMapPoint.MovePlayerToThisMapPoint();
    }
    public void Update()
    {
        float step = speed * Time.deltaTime;
        if (mapPlayerInput.isDownPressed)
        {
            currentMapPoint.MoveBottom(step);
        }
        else if(mapPlayerInput.isUpPressed)
        {
            currentMapPoint.MoveTop(step);
        }
        else if (mapPlayerInput.isLeftPressed)
        {
            currentMapPoint.MoveLeft(step);
        }
        else if (mapPlayerInput.isRightPressed)
        {
            currentMapPoint.MoveRight(step);
        }
        if (mapPlayerInput.isLoadScenePressed)
        {
            int index = 0;
            foreach (MapPoint mapPoint in MapPointList)
            {
                if (currentMapPoint == mapPoint)
                {
                    Debug.LogError(index);
                    PlayerPrefs.SetInt("ActiveMapPoint", index);
                }
                index++;
            }
            currentMapPoint.LoadScene();
        }
    }
}
