using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Player player;
    private string currentAnimation;


    private float animationDuration;
    public Animator animator;
    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    float time = 0;
    void FixedUpdate()
    {

    }
}
