using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public HostileCreature hostileCreature;
    public HostileCreatureAnimationManager animationManager;
    public Notice notice;
    public ChaseState chaseState;
    
    public float restTime;
    public float restTimer;

    public float walkTime;
    public float walkTimer;
    public float chaseSpeedBoost;
    void Patrol()
    {
        if (restTimer < 0)
        {
            if (walkTimer > 0)
            {
                hostileCreature.Move();
                walkTimer -= Time.deltaTime;
                animationManager.PlayWalkingAnimation();
            }
            if (walkTimer < 0)
            {
                hostileCreature.transform.localScale = new Vector2(-(Mathf.Sign(hostileCreature.rigidbody2D.velocity.x)) * 2f, 2f);
                restTimer = restTime;
                walkTimer = Random.Range(walkTime / 2, walkTime);
            }
        }
        else
        {
            restTimer -= Time.deltaTime;
            hostileCreature.StopMoving();
            animationManager.PlayIdleAnimation();
        }
    }
    public override State RunCurrentState()
    {
        Patrol();
        if (notice.isPlayerInRange)
        {
            hostileCreature.moveSpeed += chaseSpeedBoost;
            return chaseState;
        }
        return this;
    }
}
