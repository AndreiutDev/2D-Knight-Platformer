using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //references to sub player components
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
    internal Animator playerAnimator;
    internal Rigidbody2D playerRigidbody;
    internal CapsuleCollider2D playerBodyCollider;
    internal BoxCollider2D playerFeetCollider;

    //Stats

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
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
}