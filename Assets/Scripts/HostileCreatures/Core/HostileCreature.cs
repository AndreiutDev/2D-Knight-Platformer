using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileCreature : MonoBehaviour
{
    public Player player;
    //Animator
    [SerializeField] protected HostileCreatureAnimationManager hostileCreatureAnimationManager;
    
    //Config
    [SerializeField] public float moveSpeed = 4f;
    [SerializeField] public float dazzleTime = 0f;
    [SerializeField] public float knockbackTime = 0f;
    [SerializeField] protected Vector3 playerKnockback = new Vector3(3, 0, 0);
    [SerializeField] protected float scale;
    
    //Death
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private Vector3 deathParticlesOffset;
    
    //Hurt
    [SerializeField] protected Vector3 damagePopupOffset;

    //Cached component references
    public Rigidbody2D rigidbody2D;

    [SerializeField]
    protected int health;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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
        rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    }
    public void MoveLeft()
    {
        rigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
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
        if (isPlayerToTheLeft())
        {
            transform.localScale = new Vector2(-2f, 2f);
        }
        else
        {
            transform.localScale = new Vector2(2f, 2f);
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
}
