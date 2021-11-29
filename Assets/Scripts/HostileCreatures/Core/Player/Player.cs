using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //references to sub player components
    [SerializeField]
    public PlayerInventory playerInventory;

    [SerializeField]
    public Immunity immunity;

    [SerializeField]
    internal PlayerInput playerInput;

    [SerializeField]
    internal PlayerMovement playerMovement;

    [SerializeField]
    internal PlayerCollision playerCollision;

    [SerializeField]
    internal PlayerActions playerActions;

    internal float groundHeight = 0;
    
    public int health;
    public int maxHealth;

    //Config
    [SerializeField] internal float playerSpeed = 0;
    [SerializeField] internal float jumpForce = 0;
    [SerializeField] internal  Vector2 deathKick = new Vector2(0.1f, 0.1f);
    [SerializeField] internal Vector2 knockbackPower = new Vector2(0.1f, 0.1f);

    [SerializeField]
    internal PlayerWeapon playerWeapon;

    //State
    internal bool isAlive = true;
    internal bool canJump = false;

    //Cached component references
    public Animator playerAnimator;
    internal Rigidbody2D playerRigidbody;
    internal CapsuleCollider2D playerBodyCollider;
    internal BoxCollider2D playerFeetCollider;

    //Stats

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
    
        if (!isAlive) { return; }

        playerActions.Run();
        playerActions.Jump();
        playerActions.FlipPlayer();
        playerActions.Die();
        playerActions.Attack();
        playerActions.Hurt();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "MovingGround")
        {
            transform.parent.position = other.transform.position;
            Debug.Log("We are on a moving platform");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingGround")
        {
            transform.parent = null;
        }
    }
}