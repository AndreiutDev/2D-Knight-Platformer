using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notice : MonoBehaviour
{
    public Vector3 offset;
    public bool isPlayerInRange;
    public bool isPlayerInAttackRange;
    public bool isPlayerInTransitionAction;
    public LayerMask playerLayer;

    public float noticeRange;
    public float attackRange;
    public float transitionActionRange;

    public void IsPlayerInBetweenNoticeAndAttackActionRangeRange()
    {
        Collider2D[] playerInRange = Physics2D.OverlapCircleAll(this.transform.position + offset, transitionActionRange, playerLayer);
        if (playerInRange.Length > 0)
            isPlayerInTransitionAction = true;
        else
            isPlayerInTransitionAction = false;
    }
    public void IsPlayerInNoticeRange()
    {
        Collider2D[] playerInRange = Physics2D.OverlapCircleAll(this.transform.position + offset, noticeRange, playerLayer);
        if (playerInRange.Length > 0)
            isPlayerInRange = true;
        else
            isPlayerInRange = false;
    }
    public void IsPlayerInAttackRange()
    {
        Collider2D[] playerInRange = Physics2D.OverlapCircleAll(this.transform.position + offset, attackRange, playerLayer);
        if (playerInRange.Length > 0)
            isPlayerInAttackRange = true;
        else
            isPlayerInAttackRange = false;
    }
    private void Update()
    {
        IsPlayerInNoticeRange();
        IsPlayerInAttackRange();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position + offset, noticeRange);
    }
}
