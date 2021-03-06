using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public GameObject destroyParticles;
    public Vector3 particlesOffset;
    public float negativeProjectileSpeed;
    public float positiveProjectileSpeed;
    public float projectileSpeed;

    public virtual void ProjectileBehaviour()
    {
        transform.position += new Vector3(projectileSpeed, 0) * Time.deltaTime;
    }
    private void Update()
    {
        ProjectileBehaviour();
    }
    public void SetProjectileDirectionToRight()
    {
        projectileSpeed = positiveProjectileSpeed;
    }
    public void SetProjectileDirectionToLeft()
    {
        transform.localScale = new Vector3(-2, transform.localScale.y, 0);
        projectileSpeed = negativeProjectileSpeed;
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        HostileCreature hostileCreature = collision.collider.GetComponent<HostileCreature>();
        if (player != null)
        {
            player.playerActions.isAttacked = true;
            player.playerActions.Hurt();
        }

        Instantiate(destroyParticles, this.transform.position + particlesOffset, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(destroyParticles, this.transform.position + particlesOffset, Quaternion.identity);
        Destroy(gameObject);
    }
}