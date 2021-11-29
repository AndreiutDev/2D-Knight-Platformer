using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public Transform attackPosition;
    public float attackRange;

    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
}
