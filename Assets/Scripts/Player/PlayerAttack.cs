using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerWeapon playerWeapon;

    internal float timeBetweenAttack;
    internal float startTimeBetweenAttacks;
    float animationDuration;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whichAreTheEnemies;
    public int damage;

    private void Update()
    {
        if (timeBetweenAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerWeapon.animator.SetTrigger("Attack_1");
                Collider2D[] enemiesInRangeOfTheAttack = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whichAreTheEnemies);
                for(int i = 0; i < enemiesInRangeOfTheAttack.Length; i++)
                {
                    StartCoroutine(enemiesInRangeOfTheAttack[i].GetComponent<HostileCreature>().InvokeTakeDamageWithDelay(damage));
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
