using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomKnightEvadeAction : GOAPAction
{
	private bool evaded = false;

	public ShroomKnightEvadeAction()
	{
		addEffect("damagePlayer", true);
		cost = 0f;
	}

	public override void reset()
	{
		evaded = false;
		target = null;
	}

	public override bool isDone()
	{
		return evaded;
	}

	public override bool requiresInRange()
	{
		return false;
	}
	public override bool checkProceduralPrecondition(GameObject agent)
	{
		ShroomKnight shroomKnight = agent.GetComponent<ShroomKnight>();
		if (shroomKnight.distanceRelativeToThePlayer <= shroomKnight.evadeDistance)
		{
			return true;
		}
		return false;
	}
	public override bool perform(GameObject agent)
	{
		ShroomKnight shroomKnight = agent.GetComponent<ShroomKnight>();
		
		if (shroomKnight.evadeCooldownTimer <= 0)
		{
			shroomKnight.evadeCooldownTimer = shroomKnight.evadeCooldownDuration;
			Debug.Log("Evading!");

			evaded = true;
			return false;
		}
		else
		{
			return true;
		}
	}
}
