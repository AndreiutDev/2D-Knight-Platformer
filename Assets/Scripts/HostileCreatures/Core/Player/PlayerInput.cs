using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    Player player;

    internal float xAxisMovement;
    internal bool isJumpPressed => Input.GetKey(KeyCode.Space) == true;
    internal bool isAttackPressed => Input.GetKeyDown(KeyCode.J) == true;

    private void Update()
    {
        xAxisMovement = Input.GetAxis("Horizontal");
    }
}
