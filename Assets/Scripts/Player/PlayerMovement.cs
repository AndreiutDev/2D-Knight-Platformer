using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField] float climbSpeed = 12f;
    float climbCooldownTime = 0.5f;
    float climbCooldownTimer;

    float startGravityScale;
    bool isAttached;

    private void Start()
    {
        startGravityScale = player.playerRigidbody.gravityScale;
    }

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
    public void FreezeMovementOnXAxis()
    {
        player.playerRigidbody.velocity = new Vector2(0f, player.playerRigidbody.velocity.y);
    }
    public void StopMoving()
    {
        player.playerRigidbody.velocity = new Vector2(0f, 0f);
    }

    public void ClimbLadder()
    {
        if (player.playerCollision.isTouchingGround && player.playerInput.isClimbDownPressed)
        {
            Debug.Log("Yes");
            isAttached = false;
        }
        if (climbCooldownTimer >= 0)
        {
            climbCooldownTimer -= Time.deltaTime;
        }
        if (!player.playerCollision.isTouchingClimbingConstruct)
        {
            player.playerRigidbody.gravityScale = startGravityScale;
            return;
        }
        else
        {
            if (player.playerInput.isClimbPressed && !isAttached)
            {
                climbCooldownTimer = climbCooldownTime;
                isAttached = true;
            }
            else if (player.playerInput.isJumpPressed && climbCooldownTimer < 0)
            {
                isAttached = false;
            }
        }
        if (isAttached)
        {
            FreezeMovementOnXAxis();
            Vector2 climbVelocity = new Vector2(player.playerRigidbody.velocity.x, player.playerInput.yAxisMovement * climbSpeed);
            player.playerRigidbody.velocity = climbVelocity;
            player.playerRigidbody.gravityScale = 0f;
        }
        else
        {
            player.playerRigidbody.gravityScale = startGravityScale;
        }
    }
}
