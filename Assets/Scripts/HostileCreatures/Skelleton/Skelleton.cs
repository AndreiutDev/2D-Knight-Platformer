using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelleton : HostileCreature, IAttack
{
    public FlashMaterial flashMaterial;
    public LayerMask whichIsThePlayer;
    public Transform attackPosition;
    public float restTime = 1f;
    public bool isAttacking;

    public void Start()
    {
        flashMaterial = GetComponent<FlashMaterial>();
    }
    public override void TakeDamage(int damage)
    {
        PopupManager.InstantiateDamagePopup(this.transform, damagePopupOffset, damage);
        if (isAttacking == true)
        {
            dazzleTime = 0.05f;
            flashMaterial.Flash(new Color(255, 255, 255), 0.15f);
        }
        else
        {
            dazzleTime = 0.33f;
            hostileCreatureAnimationManager.PlayHurtAnimation();
        }
        
        knockbackTime = 0.15f;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    public override void Behaviour()
    {
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            restTime = 2f;
            transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)) * 2f, 2f);
        }
    }

    public void CheckForInRangeEnemiesAndDealDamage()
    {
        Collider2D[] enemiesInRangeOfTheAttack = Physics2D.OverlapCircleAll(attackPosition.position, 1.2f, whichIsThePlayer);
        if (enemiesInRangeOfTheAttack.Length != 0)
        {
            player.playerActions.isAttacked = true;
        }
        isAttacking = false;
    }
    public void Attack()
    {
        hostileCreatureAnimationManager.animator.SetTrigger("attack");
        Invoke("CheckForInRangeEnemiesAndDealDamage", 0.4f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, 1.2f);
    }
}