using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD_MANAGER : MonoBehaviour
{
    public static HUD_MANAGER instance;
    
    private CHARACTER_CONTROLLER player;

    [Header("Player State Interface")]
    public Image healthBar;
    public Image manaBar;

    [Header("Target State Interface")] 
    public GameObject targetDataInterface;
    public Image t_healthBar;
    public Image t_manaBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        player = FindObjectOfType<CHARACTER_CONTROLLER>();
    }

    public void UpdateTargetHUD()
    {
        if (player.target != null && player.target.CompareTag("Targetable"))
        {
            ENEMY enemy = player.target.GetComponent<ENEMY>();
            PNJ ally = player.target.GetComponent<PNJ>();
            
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
