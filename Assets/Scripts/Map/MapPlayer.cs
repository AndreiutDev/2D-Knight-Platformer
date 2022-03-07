using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayer : MonoBehaviour
{
    public MapPoint currentMapPoint;
    public MapPlayerInput mapPlayerInput;

    [SerializeField] private float speed;

    private void Awake()
    {
        mapPlayerInput = GetComponent<MapPlayerInput>();
    }
    public void Update()
    {
        float step = speed * Time.deltaTime;
        if (mapPlayerInput.isDownPressed)
        {
            Debug.Log("Bottom pressed");
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
            currentMapPoint.LoadScene();
        }
    }
}
