using UnityEngine;
using System.Collections;

public class DragonShroomAttackAction : GOAPAction
{

	private bool attacked = false;

	public DragonShroomAttackAction()
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
		DragonShroom dragonShroom = agent.GetComponent<DragonShroom>();
		if (dragonShroom.stamina >= (cost) && dragonShroom.Notice.isPlayerInAttackRange == true)
		{
			dragonShroom.Attack();
			dragonShroom.stamina -= cost;
			attacked = true;
			return false;
		}
		else
		{
			return true;
		}
	}
}