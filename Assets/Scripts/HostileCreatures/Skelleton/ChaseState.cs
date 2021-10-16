using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public Skelleton skelleton;
    public HostileCreatureAnimationManager animationManager;
    public Notice notice;
    public AttackState attackState;
    public PatrolState patrolState;


    public float attackDurationStart;
    public float attackCooldownStart;
    public float attackCooldown;

    public float minDistance;
    public float chaseSpeedBoost;
    public override State RunCurrentState()
    {
        Debug.Log("Chase");
        
        if (Mathf.Abs(skelleton.transform.position.x - skelleton.player.transform.position.x) > minDistance)
        {
            skelleton.Move();
        }
        else
        {
            skelleton.StopMoving();
        }
        animationManager.PlayWalkingAnimation();
        skelleton.ChangeDirectionRelativeToPlayer();

        if (notice.isPlayerInAttackRange && attackCooldown <= 0)
        {
            skelleton.isAttacking = true;
            attackCooldown = attackCooldownStart;
            attackState.attackDuration = attackDurationStart;
            skelleton.Attack();
            return attackState;
        }
        if (notice.isPlayerInRange == false)
        {
            skelleton.moveSpeed -= chaseSpeedBoost;
            return patrolState;
        }
        attackCooldown -= Time.deltaTime;
        return this;
    }
}
