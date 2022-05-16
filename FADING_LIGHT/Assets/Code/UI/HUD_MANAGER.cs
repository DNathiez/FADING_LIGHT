using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD_MANAGER : MonoBehaviour
{
    private PLAYER_MANAGER player;

    [Header("Player State Interface")]
    public Image healthBar;
    public Image manaBar;

    [Header("Target State Interface")] 
    public GameObject targetDataInterface;
    public Image t_healthBar;
    public Image t_manaBar;

    private void Awake()
    {
        player = FindObjectOfType<PLAYER_MANAGER>();
    }

    private void Update()
    {
        healthBar.fillAmount = (float)player.currentHealth / (float)player.maxHealth;
        manaBar.fillAmount = (float) player.currentMana / (float) player.maxMana;

        if (player.currentTarget != null && player.currentTarget.CompareTag("Targetable"))
        {
            ENEMY enemy = player.currentTarget.GetComponent<ENEMY>();
            PNJ ally = player.currentTarget.GetComponent<PNJ>();
            
            if (enemy)
            {
                targetDataInterface.SetActive(true);
                t_healthBar.fillAmount = (float) enemy.currentHealth / (float) enemy.maxHealth;
                t_manaBar.fillAmount = (float) enemy.currentMana / (float) enemy.maxMana;
            }

            if (ally)
            {
                targetDataInterface.SetActive(true);
                t_healthBar.fillAmount = (float) ally.currentHealth / (float) ally.maxHealth;
                t_manaBar.fillAmount = (float) ally.currentMana / (float) ally.maxMana;
            }
            
        }
        else
        {
            targetDataInterface.SetActive(false);
        }
    }
}
