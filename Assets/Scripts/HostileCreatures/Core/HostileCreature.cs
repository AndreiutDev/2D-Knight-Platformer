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
    [SerializeField] protected float dazzleTime = 0.5f;
    [SerializeField] protected Vector3 playerKnockback = new Vector3(3, 0, 0);
    [SerializeField] protected float scale;
    
    //Death
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private Vector3 deathParticlesOffset;
    
    //Hurt
    [SerializeField] protected Vector3 damagePopupOffset;

    //Cached component references
    protected Rigidbody2D rigidbody2D;

    [SerializeField]
    protected int health;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
 
    public abstract void Behaviour();
    public abstract void TakeDamage(int damage);
    protected void Die()
    {
        Instantiate(deathParticles, this.transform.position + deathParticlesOffset, Quaternion.identity);
        Destroy(gameObject);
    }
    #region Movement
    protected void StopMoving()
    {
        rigidbody2D.velocity = new Vector2(0f, 0f);
    }
    protected void MoveRight()
    {
        rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    }
    protected void MoveLeft()
    {
        rigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
    }
    protected void ChangeWalkingDirection()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)) * 2f, 2f);
    }
    #endregion
    #region WhatDirectionIsThisEntityFacing
    protected bool isFacingRight()
    {
        return transform.localScale.x > 0;
    }
    protected bool isFacingLeft()
    {
        return transform.localScale.x < 0;
    }
    #endregion
    #region PositionRelativeToThePlayer
    protected bool isPlayerToTheLeft()
    {
        if (player.transform.position.x < transform.position.x)
            return true;
        return false;
    }
    protected bool isPlayerToTheRight()
    {
        if (player.transform.position.x > transform.position.x)
            return true;
        return false;
    }
    protected void GetKnockedBack()
    {
        if (isPlayerToTheLeft())
            rigidbody2D.AddForce(Vector3.Scale(playerKnockback, new Vector3(1, 1, 0)), ForceMode2D.Impulse);
        if (isPlayerToTheRight())
            rigidbody2D.AddForce(Vector3.Scale(playerKnockback, new Vector3(-1, 1, 0)), ForceMode2D.Impulse);
    }
    #endregion
}
