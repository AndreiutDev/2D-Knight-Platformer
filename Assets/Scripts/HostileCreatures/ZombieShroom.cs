using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieShroom : HostileCreature
{

    [SerializeField] float restTime = 1f;

    public override void TakeDamage(int damage)
    {
        PopupManager.InstantiateDamagePopup(this.transform, damagePopupOffset, damage);
        hostileCreatureAnimationManager.PlayHurtAnimation();

        GetKnockedBack();
        dazzleTime = 0.3f;

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
            hostileCreatureAnimationManager.PlayWalkingAnimation();
        }
        else
        {
            restTime -= Time.deltaTime;
            StopMoving();
            hostileCreatureAnimationManager.PlayIdleAnimation();
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
            restTime = 2f;
            transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)) * 1.5f, 1.5f);
        }
    }
}