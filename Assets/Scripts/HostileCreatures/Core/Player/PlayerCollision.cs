using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    Player player;

    internal bool isTouchingGround => player.playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) == true;
    internal bool isTouchingEnemy => player.playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")) == true;
    internal bool isTouchingHazards => player.playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) == true;
}
