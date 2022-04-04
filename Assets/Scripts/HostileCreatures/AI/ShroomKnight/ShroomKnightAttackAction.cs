using UnityEngine;
using System.Collections;

public class ShroomKnightAttackAction : GOAPAction {

	private bool attacked = false;

	public ShroomKnightAttackAction()
	{
		addEffect("damagePlayer", true);
		cost = 100f;
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
		ShroomKnight shroomKnight = agent.GetComponent<ShroomKnight>();
		if (shroomKnight.stamina >= (cost))
		{
			shroomKnight.hostileCreatureAnimationManager.animator.SetTrigger("attack");
			shroomKnight.attackDurationTimer = shroomKnight.attackDuration;
			shroomKnight.attackOverchargeDurationTimer = shroomKnight.attackOverchargeDuration;
			shroomKnight.Attack();
			int damage = shroomKnight.strength;
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
