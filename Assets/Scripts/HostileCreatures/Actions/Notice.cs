using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notice : MonoBehaviour
{
    public bool isPlayerInRange;
    public LayerMask playerLayer;

    public float noticeRange;

    public void IsPlayerInNoticeRange()
    {
        Collider2D[] playerInRange = Physics2D.OverlapCircleAll(this.transform.position, noticeRange, playerLayer);
        if (playerInRange.Length > 0)
            isPlayerInRange = true;
        else
            isPlayerInRange = false;
    }
    private void Update()
    {
        IsPlayerInNoticeRange();
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, noticeRange);
    }
}
