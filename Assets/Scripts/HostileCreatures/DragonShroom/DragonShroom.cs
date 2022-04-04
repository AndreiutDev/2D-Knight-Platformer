using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonShroom : HostileCreatureGoap, IAttack
{
	public FlashMaterial flashMaterial;
	public LayerMask whichIsThePlayer;
	public Transform projectileSpawnPosition;
	public Projectile poisonProjectile;
	public float restTime = 1f;
	public bool isAttacking;
	public Notice Notice;

	void Start()
	{
		Notice = GetComponent<Notice>();
		stamina = 120f;
		speed = 20;
		strength = 10;
		regenRate = 18f;
		maxStamina = 120f;
		animator = GetComponent<Animator>();
	}
	public override void passiveRegen()
	{
		stamina += regenRate * Time.deltaTime;
	}
	public override HashSet<KeyValuePair<string, object>> createGoalState()
	{
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>>();
		goal.Add(new KeyValuePair<string, object>("damagePlayer", true));
		goal.Add(new KeyValuePair<string, object>("stayAlive", true));
		return goal;
	}
	void Update()
	{
		RegenerateStamina();
	}
	public override void Behaviour()
	{
	
	}
	public override void TakeDamage(int damage)
	{
		PopupManager.InstantiateDamagePopup(this.transform, damagePopupOffset, damage);
		flashMaterial.Flash(new Color(255, 255, 255), 0.15f);
		health -= damage;
		if (health <= 0)
		{
			Die();
		}
	}
	public void Attack()
	{
		StartCoroutine(AttackRoutine());
	}
	public IEnumerator AttackRoutine()
    {
		Projectile firstProjectile = Instantiate(poisonProjectile, projectileSpawnPosition.position, Quaternion.identity);
		yield return new WaitForSeconds(0.15f);
		Projectile secondProjectile = Instantiate(poisonProjectile, projectileSpawnPosition.position, Quaternion.identity);
		secondProjectile.transform.localScale = new Vector3(1.2f, 1.2f, 0f);
		yield return new WaitForSeconds(0.25f);
		Projectile thirdProjectile = Instantiate(poisonProjectile, projectileSpawnPosition.position, Quaternion.identity);
		thirdProjectile.transform.localScale = new Vector3(1.4f, 1.4f, 0f);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.GetComponent<Player>() != null)
		{
			if (this.transform.position.y <= collision.gameObject.transform.position.y)
			{
				collision.gameObject.GetComponent<Player>().playerRigidbody.AddForce(new Vector2(0f, 15f), ForceMode2D.Impulse);
			}
		}
	}
}
