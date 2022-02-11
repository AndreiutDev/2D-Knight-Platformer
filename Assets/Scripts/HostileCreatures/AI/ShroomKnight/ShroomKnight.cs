using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomKnight : HostileCreatureGoap
{
    public override void Behaviour()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        PopupManager.InstantiateDamagePopup(this.transform, damagePopupOffset, damage);
        //hostileCreatureAnimationManager.PlayHurtAnimation();

        dazzleTime = 0.3f;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public override HashSet<KeyValuePair<string, object>> CreateGoalState()
    {
        HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
        goal.Add(new KeyValuePair<string, object>("damagePlayer", true));
        goal.Add(new KeyValuePair<string, object>("stayAlive", true));
        return goal;
    }
}
