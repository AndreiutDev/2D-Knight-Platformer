using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public Notice notice;
    public ChaseState chaseState;
    public EvadeState evadeState;
    public Skelleton skelleton;
    public float attackDuration;
    public bool hasAttacked = false;
    public override State RunCurrentState()
    {
        Debug.Log("attacking");
        skelleton.StopMoving();
        attackDuration -= Time.deltaTime;
        if (attackDuration <= 0)
        {
            skelleton.moveSpeed += 3.5f;
            evadeState.evadeTimer = Random.Range(0.2f, 0.5f);
            return evadeState;
        }
        return this;
    }
}
