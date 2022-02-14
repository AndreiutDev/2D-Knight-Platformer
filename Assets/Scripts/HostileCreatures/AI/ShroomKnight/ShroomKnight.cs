using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomKnight : HostileCreatureGoap
{
    public override void Behaviour()
    {
        
    }
    void Start()
    {
        stamina = 100f;
        health = 50;
        speed = 20;
        strength = 10;
        regenRate = .5f;
        maxStamina = 100f;

        terminalSpeed = speed / 10;
        initialSpeed = (speed / 10) / 2;
        acceleration = (speed / 10) / 4;
    }

    public override void PassiveRegen()
    {
        stamina += regenRate;
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
