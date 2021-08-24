﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    Player player;

    internal float timeBetweenAttack;
    [SerializeField]
    internal float startTimeBetweenAttacks = 1f;

    public LayerMask whichAreTheEnemies;

    public int damage;

    public void Attack()
    {
        if (player.isAlive)
        {
            if (timeBetweenAttack <= 0)
            {
                if (player.playerInput.isAttackPressed)
                {
                    player.playerWeapon.animator.SetTrigger("Attack_1");
                    Collider2D[] enemiesInRangeOfTheAttack = Physics2D.OverlapCircleAll(player.playerWeapon.attackPosition.position, player.playerWeapon.attackRange, whichAreTheEnemies);
                    for (int i = 0; i < enemiesInRangeOfTheAttack.Length; i++)
                    {
                        StartCoroutine(enemiesInRangeOfTheAttack[i].GetComponent<HostileCreature>().InvokeTakeDamageWithDelay(damage));
                    }
                    timeBetweenAttack = startTimeBetweenAttacks;
                }
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime;
            }
        }
    }
    public void Hurt()
    {

    }
    public void Run()
    {
        player.playerMovement.MoveOnTheXAxis();

        bool hasHorizontalSpeed = (Mathf.Abs(player.playerRigidbody.velocity.x) > Mathf.Epsilon);
        player.playerAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }
    public void Jump()
    {
        if (player.playerInput.isJumpPressed && player.playerCollision.isTouchingGround)
        {
            player.playerMovement.MoveOnTheYAxis();
        }
        if (player.playerCollision.isTouchingGround)
        {
           
            player.playerAnimator.SetBool("isJumping", false);
        }
        else
        {
            player.playerAnimator.SetBool("isJumping", true);
        }
    }
    public void Die()
    {
        if (player.playerCollision.isTouchingEnemy || player.playerCollision.isTouchingHazards)
        {
            player.playerWeapon.animator.SetTrigger("Death");
            player.playerAnimator.SetTrigger("Dying");
            player.isAlive = false;

            player.playerAnimator.SetBool("isJumping", false);

            player.deathKick.x = player.deathKick.x * Mathf.Sign(transform.localScale.x) * (-1);
            player.playerRigidbody.velocity = player.deathKick;
           
        }
    }
    public void FlipPlayer()
    {
        bool hasHorizontalSpeed = (Mathf.Abs(player.playerRigidbody.velocity.x) > Mathf.Epsilon);
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(player.playerRigidbody.velocity.x) * 2f, 2f);
        }
    }
    public bool IsPlayerFacingRight()
    {
        if (transform.localScale.x > 0)
            return true;
        return false;
    }
    public bool IsPlayerFacingLeft()
    {
        if (transform.localScale.x < 0)
            return false;
        return true;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.playerWeapon.attackPosition.position, player.playerWeapon.attackRange);
    }
}