using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    Player player;
    internal bool isTouchingProjectile => player.playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Projectile")) == true || player.playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Projectile")) == true;
    internal bool isTouchingGround => player.playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) == true;
    internal bool isTouchingEnemy => player.playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")) == true;
    internal bool isTouchingHazards => player.playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) == true;
    internal bool isTouchingClimbingConstruct => player.playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) == true;
}
