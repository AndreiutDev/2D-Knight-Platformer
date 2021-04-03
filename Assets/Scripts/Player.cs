using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float groundHeight = 0;
    //Config
    [SerializeField] float playerSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Vector2 deathKick = new Vector2(0.1f, 0.1f);

    //State
    bool isAlive = true;
    bool canJump = false;

    //Cached component references
    private Animator playerAnimator;
    private Rigidbody2D playerRigidbody;
    private CapsuleCollider2D playerBodyCollider;
    private BoxCollider2D playerFeetCollider;

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
        Die();
    }

    public void Run()
    {
        float controls = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controls * playerSpeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;

        bool hasHorizontalSpeed = (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon);
        playerAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }
    private void Die()
    {
        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")) || playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            playerAnimator.SetTrigger("Dying");
            isAlive = false;

            playerAnimator.SetBool("isJumping", false);
            
            deathKick.x = deathKick.x * Mathf.Sign(transform.localScale.x) * (-1);
            playerRigidbody.velocity = deathKick;
        }
    }
    public void Jump()
    {
        
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        { 
            canJump=true;
            groundHeight = playerRigidbody.velocity.y; 
            playerAnimator.SetBool("isJumping", false);
        }
        else
        {

            playerAnimator.SetBool("isJumping", true);
        }
        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            canJump = false;

            playerRigidbody.velocity = new Vector2(0f, jumpForce);
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
}
