using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileCreature : MonoBehaviour
{
    [SerializeField]
    private GameObject deathParticles;
    [SerializeField]
    private Vector3 deathParticlesOffset;

    public int health;
    public abstract void Behaviour();
    public abstract void TakeDamage(int damage);
    public void Die()
    {
        Instantiate(deathParticles, this.transform.position + deathParticlesOffset, Quaternion.identity);
        Destroy(gameObject);
    }
}
