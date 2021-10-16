using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public HostileCreature hostileCreature;
    public State currentState;
    private void Update()
    {
        RunStateMachine();
    }
    private void RunStateMachine()
    {
        if (hostileCreature.dazzleTime <= 0)
        {
            State nextState = currentState?.RunCurrentState();
            if (nextState != null)
            {
                SwitchToTheNextState(nextState);
            }
        }
        else
        {
            if (hostileCreature.knockbackTime > 0)
            {
                if(hostileCreature.isFacingLeft())
                {
                    hostileCreature.rigidbody2D.AddForce(new Vector2(0.5f, 0), ForceMode2D.Impulse);
                }
                else if (hostileCreature.isFacingRight())
                {
                    hostileCreature.rigidbody2D.AddForce(new Vector2(-0.5f, 0), ForceMode2D.Impulse);
                }
                hostileCreature.knockbackTime -= Time.deltaTime;
            }
            else
            {
                hostileCreature.StopMoving();
            }
            hostileCreature.dazzleTime -= Time.deltaTime;
        }
       
    }
    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}
