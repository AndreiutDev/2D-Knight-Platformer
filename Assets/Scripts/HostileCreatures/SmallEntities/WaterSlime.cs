using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlime : HostileCreature, IAttack
{
    private Notice notice;

    public Vector3 maxAttackJumpForce;
    public Vector3 minAttackJumpForce;
    private Vector3 attackJumpForce;

    public float attackCooldownTimer;
    public float attackCooldown;
    public float attackDurationTimer;
    public float attackDuration;

    public float walkTimeTimer;
    public float walkTime;

    void Awake()
    {
        notice = GetComponent<Notice>();
    }
    private void InitAttackJumpForce()
    {
        attackJumpForce.x = Random.Range(minAttackJumpForce.x, maxAttackJumpForce.x);
        attackJumpForce.y = Random.Range(minAttackJumpForce.y, maxAttackJumpForce.y);
        attackJumpForce.z = 0f;
    }
    public void Attack()
    {
        if (attackCooldownTimer <= 0)
        {
            attackDurationTimer = attackDuration;
            InitAttackJumpForce();
            if (isPlayerToTheLeft())
            {
                rigidbody2D.AddForce(Vector3.Scale(attackJumpForce,new Vector3(-1,1,0)), ForceMode2D.Impulse);
            }
            else
            {
                rigidbody2D.AddForce(attackJumpForce, ForceMode2D.Impulse);
            }
            attackCooldownTimer = attackCooldown;
        }
        attackCooldownTimer -= Time.deltaTime;
    }

    public override void Behaviour()
    {
        hostileCreatureAnimationManager.PlayIdleAnimation();
        if (attackDurationTimer <= 0)
        {
            if (walkTimeTimer < 0)
            {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 0));
                walkTimeTimer = Random.Range(walkTime / 2, walkTime);
            }
            if (isFacingLeft())
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
        if (notice.isPlayerInRange)
        { 
            Attack();
        }
        walkTimeTimer -= Time.deltaTime;
        attackDurationTimer -= Time.deltaTime;
    }
    public override void TakeDamage(int damage)
    {
        PopupManager.InstantiateDamagePopup(this.transform, damagePopupOffset, damage);
        hostileCreatureAnimationManager.PlayHurtAnimation();

        GetKnockedBack();
        dazzleTime = 0.5f;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    void FixedUpdate()
    {
        if (dazzleTime <= 0)
        {
            Behaviour();
        }
        else
        {
            dazzleTime -= Time.deltaTime;
        }
    }
}
