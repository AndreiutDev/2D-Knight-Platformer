using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : State
{
    public HostileCreatureAnimationManager animationManager;
    public Skelleton skelleton;
    public ChaseState chaseState;
    public float evadeTimer;
    public override State RunCurrentState()
    {
        evadeTimer -= Time.deltaTime;
        animationManager.PlayWalkingAnimation();
        if (skelleton.isFacingRight())
        {
            skelleton.MoveLeft();
        }
        else if (skelleton.isFacingLeft())
        {
            skelleton.MoveRight();
        }
        if (evadeTimer <= 0)
        {
            skelleton.moveSpeed -= 3.5f;
            return chaseState;
        }
        return this;
    }
}
