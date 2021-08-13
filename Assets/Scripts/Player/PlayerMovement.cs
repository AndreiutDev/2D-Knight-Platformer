using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Player player;

    public void MoveOnTheXAxis()
    {
        player.playerRigidbody.velocity = new Vector2(player.playerInput.xAxisMovement * player.playerSpeed, player.playerRigidbody.velocity.y);
    }
    public void MoveOnTheYAxis()
    {
        player.playerRigidbody.velocity = new Vector2(0f, player.jumpForce);
    }
}
