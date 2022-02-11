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
    public void MoveOnTheYAxis(float jumpCoefficient)
    {
        player.playerRigidbody.velocity = new Vector2(player.playerInput.xAxisMovement * player.playerSpeed, player.jumpForce * jumpCoefficient);
    }
    public void MoveSlowOnTheXAxis()
    {
        player.playerRigidbody.velocity = new Vector2((player.playerInput.xAxisMovement * player.playerSpeed)/3, player.playerRigidbody.velocity.y);
    }
    public void StopMoving()
    {
        player.playerRigidbody.velocity = new Vector2(0f, 0f);
    }
}
