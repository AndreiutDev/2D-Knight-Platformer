using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieShroom : HostileCreature
{
    //Config
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float restTime = 1f;
    [SerializeField] float hurtDuration = 0.5f;

    //Cached component references
    Rigidbody2D rigidbody2D;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public override void TakeDamage(int damage)
    {
        animator.SetTrigger("hurt");
        hurtDuration = 0.6f;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        health -= damage;
        Debug.Log("Damage Taken!");
    }
    public override void Behaviour()
    {
        if (restTime < 0)
        {
            if (isFacingRight())
            {
                rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
                animator.Play("Shroom_Walking");
            }
            else
            {
                rigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
                animator.Play("Shroom_Walking");
            }
        }
        else
        {
            restTime -= Time.deltaTime;
            rigidbody2D.velocity = new Vector2(0f, 0f);
            animator.Play("Shroom_Idle");
        }
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetTrigger("hurt");
        }
    }
    void Update()
    {
        if (hurtDuration <= 0)
        {
            Behaviour();
        }
        else
        {
            hurtDuration -= Time.deltaTime;
        }
    }

    bool isFacingRight()
    {
        return transform.localScale.x > 0;
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
