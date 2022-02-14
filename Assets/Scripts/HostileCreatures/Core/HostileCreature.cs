using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileCreature : MonoBehaviour
{
    public HostileCreature self;
    public Player player;
    //Animator
    [SerializeField] public HostileCreatureAnimationManager hostileCreatureAnimationManager;
    
    //Config
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] public float dazzleTime = 0f;
    [SerializeField] public float knockbackTime = 0f;
    [SerializeField] protected Vector3 playerKnockback = new Vector3(3, 0, 0);
    [SerializeField] protected float scale;
    [SerializeField] protected bool isSpriteInverted;
    
    //Death
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private Vector3 deathParticlesOffset;
    
    //Hurt
    [SerializeField] protected Vector3 damagePopupOffset;

    //Cached component references
    public Rigidbody2D rigidbody2D;
    public CapsuleCollider2D hostileEntityCollider;

    [SerializeField]
    protected int health;
    public int damage;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        hostileEntityCollider = GetComponent<CapsuleCollider2D>();
        self = GetComponent<HostileCreature>();
    }
    public abstract void Behaviour();
    public abstract void TakeDamage(int damage);
    public void Die()
    {
        Instantiate(deathParticles, this.transform.position + deathParticlesOffset, Quaternion.identity);
        Destroy(gameObject);
    }
    #region Movement
    public void Move()
    {
        if (isFacingRight())
        {
            MoveRight();
        }
        else if (isFacingLeft())
        {
            MoveLeft();
        }
    }
    public void StopMoving()
    {
        rigidbody2D.velocity = new Vector2(0f, 0f);
    }
    public void MoveRight()
    {
        if (isSpriteInverted)
        {
            rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(moveSpeed, rigidbody2D.velocity.y);
        }
    }
    public void MoveLeft()
    {
        if (isSpriteInverted)
        {
            rigidbody2D.velocity = new Vector2(moveSpeed, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
        }
    }
    public void MoveUp()
    {
        rigidbody2D.velocity = new Vector2(0f, moveSpeed);
    }
    public void MoveDown()
    {
        rigidbody2D.velocity = new Vector2(0f, -moveSpeed);
    }
    public void ChangeWalkingDirection()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)) * 2f, 2f);
    }
    #endregion
    #region WhatDirectionIsThisEntityFacing
    public bool isFacingRight()
    {
        return transform.localScale.x > 0;
    }
    public bool isFacingLeft()
    {
        return transform.localScale.x < 0;
    }
    #endregion
    #region PositionRelativeToThePlayer
    public void ChangeDirectionRelativeToPlayer()
    {
       
        if (isSpriteInverted)
        {
            if (isPlayerToTheLeft())
            {
                transform.localScale = new Vector2(2f, 2f);
            }
            else
            {
                transform.localScale = new Vector2(-2f, 2f);
            }
        }
        else
        {
            if (isPlayerToTheLeft())
            {
                transform.localScale = new Vector2(-2f, 2f);
            }
            else
            {
                transform.localScale = new Vector2(2f, 2f);
            }
        }
    }
    public bool isPlayerToTheLeft()
    {
        if (player.transform.position.x < transform.position.x)
            return true;
        return false;
    }
    public bool isPlayerToTheRight()
    {
        if (player.transform.position.x > transform.position.x)
            return true;
        return false;
    }
    public void GetKnockedBack()
    {
        if (isPlayerToTheLeft())
            rigidbody2D.AddForce(Vector3.Scale(playerKnockback, new Vector3(1, 1, 0)), ForceMode2D.Impulse);
        if (isPlayerToTheRight())
            rigidbody2D.AddForce(Vector3.Scale(playerKnockback, new Vector3(-1, 1, 0)), ForceMode2D.Impulse);
    }
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 10)
        {
            player.playerActions.attackingEnemy = this;
        }
    }
}
