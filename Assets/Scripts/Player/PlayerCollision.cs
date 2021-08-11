using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    Player player;

    internal bool isTouchingGround;
    internal bool isTouchingEnemy;
    internal bool isTouchingHazards;

    private void Start()
    {
        Debug.Log("Collision Starts!");
    }
    private void Update()
    {
        if (player.playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isTouchingGround = true;
        }
        else
        {
            isTouchingGround = false;
        }

        if (player.playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            isTouchingHazards = true;
        }
        else
        {
            isTouchingHazards = false;
        }

        if (player.playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            isTouchingEnemy = true;
        }
        else
        {
            isTouchingEnemy = false;
        }
    }
}
