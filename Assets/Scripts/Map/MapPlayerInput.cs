using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerInput : MonoBehaviour
{
    internal bool isDownPressed => Input.GetKeyDown(KeyCode.S) == true || Input.GetKey(KeyCode.DownArrow);
    internal bool isUpPressed => Input.GetKeyDown(KeyCode.W) == true || Input.GetKey(KeyCode.UpArrow);
    internal bool isLeftPressed => Input.GetKeyDown(KeyCode.A) == true || Input.GetKey(KeyCode.LeftArrow);
    internal bool isRightPressed => Input.GetKeyDown(KeyCode.D) == true || Input.GetKey(KeyCode.RightArrow);
    internal bool isLoadScenePressed => Input.GetKeyDown(KeyCode.Space) == true;
}
