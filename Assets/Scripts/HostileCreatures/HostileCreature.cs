﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileCreature : MonoBehaviour
{

    //Config
    [SerializeField] protected float moveSpeed = 4f;
    [SerializeField] protected float dazzleTime = 0.5f;

    [SerializeField] private GameObject deathParticles;
    [SerializeField] private Vector3 deathParticlesOffset;
    [SerializeField] protected Vector3 damagePopupOffset;

    //Cached component references
    protected Rigidbody2D rigidbody2D;
    protected Animator animator;

    [SerializeField]
    protected int health;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("speed", moveSpeed);
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
}
