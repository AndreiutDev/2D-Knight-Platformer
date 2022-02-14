using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class HostileCreatureGoap : HostileCreature, IGoap
{
	public HostileCreature shroomKnight;
	public float minDistance;

	public int strength;
	public int speed;
	public float stamina;
	public float regenRate;
	protected float terminalSpeed;
	protected float initialSpeed;
	protected float acceleration;
	protected float minDist = 1.5f;
	protected float aggroDist = 5f;
	protected bool loop = false;
	protected float maxStamina;

	public void Awake()
	{
		shroomKnight = this;
	}
	public HashSet<KeyValuePair<string, object>> GetWorldState()
	{
		HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
		worldData.Add(new KeyValuePair<string, object>("damagePlayer", false));
		worldData.Add(new KeyValuePair<string, object>("evadePlayer", false));
		return worldData;
	}
	public abstract void PassiveRegen();
	public abstract HashSet<KeyValuePair<string, object>> CreateGoalState();

	public void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal)
	{

	}

	public void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GoapAction> action)
	{

	}

	public void ActionsFinished()
	{

	}

    public void PlanAborted(GoapAction aborter)
	{

	}
	public void setSpeed(float val)
	{
		terminalSpeed = val / 10;
		initialSpeed = (val / 10) / 2;
		acceleration = (val / 10) / 4;
		return;
	}
	public virtual bool MoveAgent(GoapAction nextAction)
	{
		float dist = Vector3.Distance(transform.position, nextAction.target.transform.position);
		if (dist < aggroDist)
		{
			Vector3 moveDirection = player.transform.position - transform.position;

			setSpeed(speed);

			if (initialSpeed < terminalSpeed)
			{
				initialSpeed += acceleration;
			}

			Vector3 newPosition = moveDirection * initialSpeed * Time.deltaTime;
			transform.position += newPosition;
		}
		if (dist <= minDist)
		{
			nextAction.SetInRange(true);
			return true;
		}
		else
		{
			return false;
		}
	}
}
