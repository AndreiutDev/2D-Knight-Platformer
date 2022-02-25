using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistPriestAttackAction : GOAPAction
{
	private bool attacked = false;

	public CultistPriestAttackAction()
	{
		addEffect("damagePlayer", true);
		cost = 50f;
	}

	public override void reset()
	{
		attacked = false;
		target = null;
	}

	public override bool isDone()
	{
		return attacked;
	}

	public override bool requiresInRange()
	{
		return false;
	}

	public override bool checkProceduralPrecondition(GameObject agent)
	{
		target = GameObject.Find("Player");
		return target != null;
	}
	public override bool perform(GameObject agent)
	{
		
		CultistPriest cultistPriest = agent.GetComponent<CultistPriest>();
		if (cultistPriest.stamina >= (cost))
		{
			cultistPriest.Attack();
			Debug.Log("Attack!!!");
			cultistPriest.stamina -= cost;
			attacked = true;
			return false;
		}
		else
		{
			return true;
		}
	}
}
