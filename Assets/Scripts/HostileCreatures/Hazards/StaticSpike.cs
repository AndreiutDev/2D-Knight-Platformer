using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<HostileCreature>()?.Die();
    }
}
