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
		target = GameObject.Find("knight");
		return target != null;
	}

	public override bool Perform(GameObject agent)
	{
		ShroomKnight shroomKnight = agent.GetComponent<ShroomKnight>();
		Debug.Log("ATTACK: " + attacked);
		if (shroomKnight.stamina >= (cost))
		{
			Debug.Log("ShroomKnight attack!");
			shroomKnight.hostileCreatureAnimationManager.animator.SetTrigger("attack");
			shroomKnight.player.playerActions.Hurt();
			shroomKnight.stamina -= cost;

			attacked = true;
			return false;
		}
		else
		{
			return true;
		}
	}
}
