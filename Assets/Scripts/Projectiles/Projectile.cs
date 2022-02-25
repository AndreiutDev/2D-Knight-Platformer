using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public GameObject destroyParticles;
    public Vector3 particlesOffset;
    public float projectileSpeed;

    public virtual void ProjectileBehaviour()
    {
        transform.position += new Vector3(projectileSpeed, 0) * Time.deltaTime;
    }
    private void Update()
    {
        ProjectileBehaviour();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(destroyParticles, this.transform.position + particlesOffset, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(destroyParticles, this.transform.position + particlesOffset, Quaternion.identity);
        Destroy(gameObject);
    }
}