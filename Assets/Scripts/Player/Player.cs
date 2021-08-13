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
    internal PlayerBehaviour playerBehaviour;

    internal float groundHeight = 0;
    //Config
    [SerializeField] internal float playerSpeed = 0;
    [SerializeField] internal float jumpForce = 0;
    [SerializeField] internal  Vector2 deathKick = new Vector2(0.1f, 0.1f);

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

        playerBehaviour.Run();
        playerBehaviour.Jump();
        playerBehaviour.FlipPlayer();
        playerBehaviour.Die();
        playerBehaviour.Attack();
    }
}