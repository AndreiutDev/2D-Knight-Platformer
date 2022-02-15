using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class HostileCreatureGoap : HostileCreature, IGOAP {

	public Animator animator;
	public BoxCollider2D boxCollider;

	public int strength;
	public int speed;
	public float stamina;
	public float regenRate = 100f;
	protected float terminalSpeed;
	protected float initialSpeed;
	protected float acceleration;
	protected float minDist = 2f;
	protected float chaseDistance = 10f;
	protected bool loop = false;
	protected float maxStamina;

	public float attackDuration;
	public float attackDurationTimer;

	public float attackOverchargeDuration;
	public float attackOverchargeDurationTimer;

	public float evadeTimer;
	public float evadeDuration;
	public float evadeDistance;
	private bool evaded;

	void RegenerateStamina()
    {
		if (stamina <= maxStamina)
		{
			Invoke("passiveRegen", 1.0f);
		}
		else
		{
			stamina = maxStamina;
		}
	}
	void AttackTimersHandler()
    {
		if (attackDurationTimer >= 0)
		{
			attackDurationTimer -= Time.deltaTime;
		}
		if (attackOverchargeDurationTimer >= 0)
		{
			attackOverchargeDurationTimer -= Time.deltaTime;
		}
	}
	void Chase()
    {
		float dist = Vector3.Distance(transform.position, player.transform.position);

		if (dist < chaseDistance && attackOverchargeDurationTimer <= 0)
		{
			evaded = false;
			if (Mathf.Abs(transform.position.x - player.transform.position.x) > minDist)
			{
				Move();
				ChangeDirectionRelativeToPlayer();
				if (attackDurationTimer <= 0 && dist > minDist)
				{
					hostileCreatureAnimationManager.PlayWalkingAnimation();
					moveSpeed = 3f;
				}
				else
				{
					moveSpeed = 0;
				}
			}
			else
			{
				StopMoving();
				if (attackDurationTimer <= 0 && dist <= minDist)
				{
					hostileCreatureAnimationManager.PlayIdleAnimation();
				}
			}
		}
		else
        {
			if (attackDurationTimer <= 0)
			{
				hostileCreatureAnimationManager.PlayIdleAnimation();
			}
		}
	}
	public virtual void Update()
	{
		RegenerateStamina();
		AttackTimersHandler();
		Chase();
	}

	public abstract void passiveRegen();

	public HashSet<KeyValuePair<string, object>> getWorldState(){
		HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>> ();
		worldData.Add(new KeyValuePair<string, object> ("damagePlayer", false));
		worldData.Add(new KeyValuePair<string, object> ("evadePlayer", false));
		return worldData;
	}

	public abstract HashSet<KeyValuePair<string, object>> createGoalState ();

	public void planFailed (HashSet<KeyValuePair<string, object>> failedGoal){
		
	}

	public void planFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> action){

	}

	public void actionsFinished(){
		
	}

	public void planAborted(GOAPAction aborter){

	}

	public void setSpeed(float val){
		terminalSpeed = val / 10;
		initialSpeed = (val / 10) / 2;
		acceleration = (val / 10) / 4;
		return;
	}

	public virtual bool moveAgent(GOAPAction nextAction){
		float dist = Vector3.Distance(transform.position, nextAction.target.transform.position);
		if (dist <= minDist)
		{
			nextAction.setInRange(true);
			return true;
		}
		else
		{
			return false;
		}
	}
}
