using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileCreatureAnimationManager : MonoBehaviour
{
    [SerializeField] protected HostileCreature hostileCreature;

    protected Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("speed", hostileCreature.moveSpeed);
    }
    public void PlayIdleAnimation()
    {
        animator.Play("idle");
    }
    public void PlayWalkingAnimation()
    {
        animator.Play("walk");
    }
    public void PlayHurtAnimation()
    {
        animator.SetTrigger("hurt");
    }
}
