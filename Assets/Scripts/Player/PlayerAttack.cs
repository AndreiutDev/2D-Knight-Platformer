using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttack;
    public float startTimeBetweenAttacks;

    public Transform attackPosition;
    public float attackRange;
    public LayerMask whichAreTheEnemies;
    public int damage;

    private void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetKey(KeyCode.C))
            {
                Debug.Log("Attacked!");
                Collider2D[] enemiesInRangeOfTheAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whichAreTheEnemies);
                Debug.Log(enemiesInRangeOfTheAttack);
                for(int i = 0; i < enemiesInRangeOfTheAttack.Length; i++)
                {
                    enemiesInRangeOfTheAttack[i].GetComponent<HostileCreature>().TakeDamage(damage);
                }
            }
            timeBetweenAttack = startTimeBetweenAttacks;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }
}
