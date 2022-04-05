using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonProjectile : Projectile
{
    bool IsTouchingGround => this.GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground")) == true;

    private RipplePostProcessor ripplePostProcessor;

    public Vector3 PlayerLocation;
    public Vector3 DirectionVector;
    private void Awake()
    {
        ripplePostProcessor = GameObject.Find("Main Camera").GetComponent<RipplePostProcessor>();
        PlayerLocation = GameObject.Find("Player").GetComponent<Transform>().position;
        DirectionVector = (PlayerLocation - transform.position).normalized;
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        if (player != null)
        {
            player.playerActions.isAttacked = true;
            player.playerActions.Hurt();
        }
        ripplePostProcessor.RippleEffect();
        Instantiate(destroyParticles, this.transform.position + particlesOffset, Quaternion.identity);
        Destroy(this.gameObject);
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
