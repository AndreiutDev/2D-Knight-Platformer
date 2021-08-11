using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //references to sub player components
    [SerializeField]
    internal PlayerInput playerInput;

    [SerializeField]
    internal PlayerMovement playerMovement;

    [SerializeField]
    internal PlayerCollision playerCollision;

    [SerializeField]
    internal PlayerAttack playerAttack;

    float groundHeight = 0;
    //Config
    [SerializeField] internal float playerSpeed = 0;
    [SerializeField] internal float jumpForce = 0;
    [SerializeField] Vector2 deathKick = new Vector2(0.1f, 0.1f);

    //State
    bool isAlive = true;
    bool canJump = false;

    //Cached component references
    internal Animator playerAnimator;
    internal Rigidbody2D playerRigidbody;
    internal CapsuleCollider2D playerBodyCollider;
    internal BoxCollider2D playerFeetCollider;

    //Initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        Jump();
        FlipPlayer();
        //Die();
    }

    private void Die()
    {
        if (playerCollision.isTouchingEnemy || playerCollision.isTouchingHazards)
        {
            playerAnimator.SetTrigger("Dying");
            isAlive = false;

            playerAnimator.SetBool("isJumping", false);
            
            deathKick.x = deathKick.x * Mathf.Sign(transform.localScale.x) * (-1);
            playerRigidbody.velocity = deathKick;
        }
    }
    public void Run()
    {
        playerMovement.MoveOnTheXAxis();

        bool hasHorizontalSpeed = (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon);
        playerAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }
    public void Jump()
    {
        if (playerCollision.isTouchingGround) 
        { 
            canJump = true;
            groundHeight = playerRigidbody.velocity.y; 
            playerAnimator.SetBool("isJumping", false);
        }
        else
        {
            playerAnimator.SetBool("isJumping", true);
        }
        if (playerInput.isJumpPressed && canJump == true)
        {
            playerMovement.MoveOnTheYAxis();
            canJump = false;
        }
    }
    public void FlipPlayer()
    {
        bool hasHorizontalSpeed = (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon);
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x) * 2f, 2f);
        }
    }
    public bool IsPlayerFacingRight()
    {
        if (transform.localScale.x > 0)
            return true;
        return false;
    }
    public bool IsPlayerFacingLeft()
    {
        if (transform.localScale.x < 0)
            return false;
        return true;
    }
}