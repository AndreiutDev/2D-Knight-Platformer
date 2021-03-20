using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom_Behaviour : MonoBehaviour
{
    //Config
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float restTime = 1f;

    //Cached component references
    Rigidbody2D rigidbody2D;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (restTime < 0)
        {
            if (isFacingRight())
            {
                rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
                anim.Play("Shroom_Walking");
            }
            else
            {
                rigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
                anim.Play("Shroom_Walking");
            }
        }
        else
        {
            restTime -= Time.deltaTime;
            rigidbody2D.velocity = new Vector2(0f, 0f);
            anim.Play("Shroom_Idle");
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
