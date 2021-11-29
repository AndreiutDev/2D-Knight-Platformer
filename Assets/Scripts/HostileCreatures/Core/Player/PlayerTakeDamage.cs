using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public float knockbackTime;
    public float startKnockbackTime;
    public FlashMaterial playerFlashMaterial;
    public FlashMaterial weaponFlashMaterial;
    public Player player;
    public void TakeDamage(int damage)
    {
        player.health = player.health - damage;
        if (player.health <= 0)
        {
            player.health = 0;
        }
        playerFlashMaterial.Flash(new Color(255, 255, 255), 0.15f);

        weaponFlashMaterial.Flash(new Color(255, 255, 255), 0.15f);

        PopupManager.InstantiateEnemyDamagePopup(this.transform, new Vector3(0, 1f, 0), damage);

        //knockbackTime = startKnockbackTime;
    }
    void Update()
    {
       //if (knockbackTime >= 0)
       //{
       //    knockbackTime -= Time.deltaTime;
       //}
    }
}
