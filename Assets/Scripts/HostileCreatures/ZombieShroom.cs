using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieShroom : HostileCreature
{

    [SerializeField] float restTime = 1f;

    public override void TakeDamage(int damage)
    {
        PopupManager.InstantiateDamagePopup(this.transform, damagePopupOffset, damage);
        animator.SetTrigger("hurt");
                                                                                                                                                                                                                                                
        dazzleTime = 0.8f;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    public override void Behaviour()
    {
        if (restTime < 0)
        {
            if (isFacingRight())
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
            animator.Play("Shroom_Walking");
        }
        else
        {
            restTime -= Time.deltaTime;
            rigidbody2D.velocity = new Vector2(0f, 0f);
            animator.Play("Shroom_Idle");
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
            StopMoving();
            restTime -= Time.deltaTime;
            dazzleTime -= Time.deltaTime;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            restTime = 3f;
            transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)) * 1.5f, 1.5f);
        }
    }
}
