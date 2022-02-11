using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomKnightAttackAction : GoapAction
{
	private bool attacked = false;

	public ShroomKnightAttackAction()
	{
		AddEffect("damagePlayer", true);
		cost = 100f;
	}

	public override void ResetAction()
	{
		attacked = false;
		target = null;
	}

	public override bool IsDone()
	{
		return attacked;
	}

	public override bool RequiresInRange()
	{
		return true;
	}

	public override bool CheckProceduralPreconditions(GameObject agent)
	{
		target = GameObject.Find("Player");
		return target != null;
	}

	public override bool Perform(GameObject agent)
	{
		return true;
	}
}
