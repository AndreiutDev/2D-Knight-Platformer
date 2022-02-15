using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShroomKnight : HostileCreatureGoap, IAttack, IEvade
{
	public FlashMaterial flashMaterial;
	public LayerMask whichIsThePlayer;
	public Transform attackPosition;
	public float restTime = 1f;
	public bool isAttacking;


	void Start () {
		stamina = 100f;
		speed = 20;
		strength = 10;
		regenRate = 25f;
		maxStamina = 100f;
		animator = GetComponent<Animator>();
	}
	public override void passiveRegen(){
		stamina += regenRate * Time.deltaTime;
	}
	public override HashSet<KeyValuePair<string, object>> createGoalState(){
		HashSet<KeyValuePair<string, object>> goal = new HashSet<KeyValuePair<string, object>> ();
		goal.Add(new KeyValuePair<string, object> ("damagePlayer", true));
		goal.Add(new KeyValuePair<string, object> ("stayAlive", true));
		return goal;
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
	public void CheckForInRangeEnemiesAndDealDamage()
	{
		Collider2D[] enemiesInRangeOfTheAttack = Physics2D.OverlapCircleAll(attackPosition.position, 2.5f, whichIsThePlayer);
		if (enemiesInRangeOfTheAttack.Length != 0)
		{
			player.playerActions.isAttacked = true;
		}
	}
	public void Attack()
	{
		hostileCreatureAnimationManager.animator.SetTrigger("attack");
		Invoke("CheckForInRangeEnemiesAndDealDamage", 0.4f);
	}
	public void Evade()
	{
		hostileCreatureAnimationManager.PlayWalkingAnimation();
		if (isFacingRight())
		{
			MoveLeft();
		}
		else if (isFacingLeft())
		{
			MoveRight();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("OOOOFIEEES" + this.transform.position.y + " : " + collision.gameObject.transform.position.y);
		if (this.transform.position.y <= collision.gameObject.transform.position.y)
		{
			collision.gameObject.GetComponent<Player>().playerRigidbody.AddForce(new Vector2(0f, 15f), ForceMode2D.Impulse);
		}
	}
}
