using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeShroom : HostileCreature
{
    private BoxCollider2D boxCollider2D;
    [SerializeField] private float flyTimerStart;
    private float flyTimer;
    [SerializeField] private float jumpPower;
    public int direction;

    void Start()
    {
        direction = 1;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    public override void Behaviour()
    {
        if (direction == 1)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
        if (flyTimer <= 0)
        {
            direction = direction * (-1);
            flyTimer = flyTimerStart;
        }
        flyTimer -= Time.deltaTime;
    }
    public override void TakeDamage(int damage)
    {
        PopupManager.InstantiateDamagePopup(this.transform, damagePopupOffset, damage);

        dazzleTime = 0.33f;
        hostileCreatureAnimationManager.PlayHurtAnimation();

        knockbackTime = 0.15f;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        Behaviour();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this.transform.position.y + " : " + collision.gameObject.transform.position.y);
        if (this.transform.position.y <= collision.gameObject.transform.position.y)
        {
            collision.gameObject.GetComponent<Player>().playerRigidbody.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
        }
    }
}