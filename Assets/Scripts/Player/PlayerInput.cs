using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    Player player;

    internal float xAxisMovement;
    internal bool isJumpPressed;
    internal bool isAttackPressed;
    public void Update()
    {
        xAxisMovement = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
        else
        {
            isJumpPressed = false;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            isAttackPressed = true;
        }
        else
        {
            isAttackPressed = false;
        }
    }
}
