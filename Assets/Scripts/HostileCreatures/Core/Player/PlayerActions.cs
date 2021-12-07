using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    Player player;

    internal float timeBetweenAttack;
    [SerializeField]
    internal float startTimeBetweenAttacks = 1f;
    public bool isAttacked = false;
    public float attackDuration;
    public float startAttackDuration;
    public HostileCreature attackingEnemy;

    [SerializeField]
    internal PlayerTakeDamage playerTakeDamage;

    internal float jumpTimer;
    [SerializeField]
    internal float startJumpTimer = 1f;
    internal bool isJumping;

    [SerializeField]
    internal float hurtTime;
    [SerializeField]
    internal float hurtTimer;

    public float hangTime;
    private float hangCounter;

    public float jumpBufferLength;
    private float jumpBufferCount;

    public HostileCreature enemyWhoAttacked;

    public LayerMask whichAreTheEnemies;
    
    public int damage;
    public void CheckForInRangeEnemiesAndDealDamage()
    {
        Collider2D[] enemiesInRangeOfTheAttack = Physics2D.OverlapCircleAll(player.playerWeapon.attackPosition.position, player.playerWeapon.attackRange, whichAreTheEnemies);
        for (int i = 0; i < enemiesInRangeOfTheAttack.Length; i++)
        {
            enemiesInRangeOfTheAttack[i].GetComponent<HostileCreature>().TakeDamage(damage);
        }
    }
    public void Attack()
    {
        if (player.isAlive)
        {
            if (timeBetweenAttack <= 0)
            {
                if (player.playerInput.isAttackPressed)
                {
                    attackDuration = startAttackDuration;
                    player.playerWeapon.animator.SetTrigger("Attack_1");
                    Invoke("CheckForInRangeEnemiesAndDealDamage", 0.15f);
                    timeBetweenAttack = startTimeBetweenAttacks;
                }
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime;
            }
        }
        if (attackDuration >= 0)
        {
            attackDuration -= Time.deltaTime;
        }
    }
    public void Hurt()
    {
        if (player.playerCollision.isTouchingEnemy || player.playerCollision.isTouchingHazards || isAttacked)
        {
            
            if (player.immunity.isImmune == false)
            {
                playerTakeDamage.TakeDamage(1);
                player.immunity.GainImmunity();
                hurtTimer = hurtTime;
            }
            isAttacked = false;
        }
    }
    public void Run()
    {
        if (attackDuration <= 0)
        {
            player.playerMovement.MoveOnTheXAxis();
        }
        else if (player.playerCollision.isTouchingGround == true)
        {
            player.playerMovement.MoveSlowOnTheXAxis();
        }
        bool hasHorizontalSpeed = (Mathf.Abs(player.playerRigidbody.velocity.x) > Mathf.Epsilon);
        player.playerAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }
    public void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else 
        {
            jumpBufferCount -= Time.deltaTime;
        }

        if (player.playerCollision.isTouchingGround)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        if (jumpBufferCount >= 0 && hangCounter > 0)
        {
            player.playerRigidbody.velocity = new Vector2(player.playerRigidbody.velocity.x, player.jumpForce);
            jumpBufferCount = 0;
        }
        if (Input.GetButtonUp("Jump") && player.playerRigidbody.velocity.y > 0)
        {
            player.playerRigidbody.velocity = new Vector2(player.playerRigidbody.velocity.x, player.playerRigidbody.velocity.y*0.7f);
        }
        if (player.playerCollision.isTouchingGround)
        {
            player.playerAnimator.SetBool("isJumping", false);
        }
        else
        {
            player.playerAnimator.SetBool("isJumping", true);
        }
        /*if (player.playerCollision.isTouchingGround)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        if (player.playerInput.isJumpPressed && hangCounter >= 0)
        {
            isJumping = true;
            jumpTimer = startJumpTimer;
            player.playerMovement.MoveOnTheYAxis(1f);
        }
        if (player.playerInput.isJumpPressed && isJumping == true)
        {
            if (jumpTimer > 0)
            {
                player.playerMovement.MoveOnTheYAxis(0.5f);
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        */
    }
    public void Die()
    {
        if (player.health <= 0)
        {
            player.playerWeapon.animator.SetTrigger("death");
            player.playerAnimator.SetTrigger("Dying");
            player.isAlive = false;

            player.playerAnimator.SetBool("isJumping", false);

            player.deathKick.x = player.deathKick.x * Mathf.Sign(transform.localScale.x) * (-1);
            player.playerRigidbody.velocity = player.deathKick;
            StartCoroutine(UIManager.playStartTransition(UIManager.deathGroupAnimator));
        }
    }
    public void FlipPlayer()
    {
        bool hasHorizontalSpeed = (Mathf.Abs(player.playerRigidbody.velocity.x) > Mathf.Epsilon);
        if (hasHorizontalSpeed && attackDuration <= 0)
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
