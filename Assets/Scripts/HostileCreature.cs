using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileCreature : MonoBehaviour
{
    public int health;
    public float speed;
    public abstract void Behaviour();
    public void TakeDamage(int damage)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        health -= damage;
        Debug.Log("Damage Taken!");
    }

}
