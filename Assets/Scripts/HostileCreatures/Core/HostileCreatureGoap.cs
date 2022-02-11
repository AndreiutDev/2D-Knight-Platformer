using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class HostileCreatureGoap : HostileCreature, IGoap
{
	public HashSet<KeyValuePair<string, object>> GetWorldState()
	{
		HashSet<KeyValuePair<string, object>> worldData = new HashSet<KeyValuePair<string, object>>();
		worldData.Add(new KeyValuePair<string, object>("damagePlayer", false));
		worldData.Add(new KeyValuePair<string, object>("evadePlayer", false));
		return worldData;
	}

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


	public virtual bool MoveAgent(GoapAction nextAction)
	{
		return false;
	}
}
