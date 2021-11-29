using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : State
{
    public HostileCreatureAnimationManager animationManager;
    public Skelleton skelleton;
    public ChaseState chaseState;
    public float evadeTimer;
    void Evade()
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
    }
    public override State RunCurrentState()
    {
        Evade();  
        if (evadeTimer <= 0)
        {
            skelleton.moveSpeed -= 3.5f;
            return chaseState;
        }
        return this;
    }
}
