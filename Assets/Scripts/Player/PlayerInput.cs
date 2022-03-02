using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    Player player;

    internal float xAxisMovement;
    internal float yAxisMovement;
    internal bool isJumpPressed => Input.GetKey(KeyCode.Space) == true;
    internal bool isAttackPressed => Input.GetKeyDown(KeyCode.J) == true;
    internal bool isClimbDownPressed => Input.GetKey(KeyCode.S) == true || Input.GetKey(KeyCode.DownArrow);
    internal bool isClimbPressed => Input.GetKeyDown(KeyCode.W) == true || Input.GetKeyDown(KeyCode.UpArrow);
    private void Update()
    {
        xAxisMovement = Input.GetAxis("Horizontal");
        yAxisMovement = Input.GetAxisRaw("Vertical");
    }
}
