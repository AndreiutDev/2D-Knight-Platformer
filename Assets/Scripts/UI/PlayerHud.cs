using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI healthText;
    public Image healthBar;
    void Update()
    {
        goldText.text = player.playerInventory.gold + "";
        healthText.text = player.health + "HP";
        healthBar.fillAmount = (float)(player.health) / (float)(player.maxHealth);
    }
}
