using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGold : MonoBehaviour
{
    public int value;
    public float animationMovingTime;
    public float animationMovingTimeStart;
    public float animationSpeed;
    public int animationDirection;
    public Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
        animationDirection = 1;
    }
    void Update()
    {
        IdleAnimation();
    }
    public void IdleAnimation()
    {
        if (animationMovingTime >= 0)
        {
            transform.position += new Vector3(0, animationSpeed, 0) * Time.deltaTime * animationDirection;
            animationMovingTime -= Time.deltaTime;
        }
        else
        {
            animationDirection = animationDirection * (-1);
            animationMovingTime = animationMovingTimeStart;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player")
        {
            animator.SetTrigger("pickup");
            Player player = other.gameObject.GetComponent<Player>();
            player.playerInventory.gold += value;
            GetComponent<ParticleLifecycle>().die = true;
        }
    }
}
