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
    private void Start()
    {
        Debug.Log("Player Starts inputing!");
    }
    public void Update()
    {
        xAxisMovement = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space))
        {
            isJumpPressed = true;
        }
        else
        {
            isJumpPressed = false;
        }
        if (Input.GetKey(KeyCode.G))
        {
            isAttackPressed = true;
        }
        else
        {
            isAttackPressed = false;
        }
    }
}
