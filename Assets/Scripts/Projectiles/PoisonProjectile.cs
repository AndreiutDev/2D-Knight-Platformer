using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonProjectile : Projectile
{
    bool IsTouchingGround => this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")) == true;

    public Vector3 PlayerLocation;
    public Vector3 DirectionVector;
    private void Awake()
    {
        PlayerLocation = GameObject.Find("Player").GetComponent<Transform>().position;
        DirectionVector = (PlayerLocation - transform.position).normalized;
    }
    public override void ProjectileBehaviour()
    {
        if (IsTouchingGround)
        {
            Instantiate(destroyParticles, this.transform.position + particlesOffset, Quaternion.identity);
            Destroy(gameObject);
        }
        transform.rotation = Quaternion.LookRotation(Vector3.forward, DirectionVector);
        transform.position += DirectionVector * projectileSpeed * Time.deltaTime;
    }
}
