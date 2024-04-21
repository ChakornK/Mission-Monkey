using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private UpdateHealthUI healthUI;
    private int health = 100;
    
    [HideInInspector]
    public bool enforceMaxHealth = true;

    private void Start()
    {
        healthUI = GetComponent<UpdateHealthUI>();
    }

    private void Update()
    {
        if (enforceMaxHealth)
        {
            health = Mathf.Clamp(health, 0, 100);
        }

        if (health <= 0)
        {
            GetComponent<PlayerDeathController>().OnPlayerDeath();
        }
    }
    
    public void ModifyPlayerHealth(int modifier, bool damagePlayer = true)
    {
        switch (damagePlayer)
        {
            case true:
                DamagePlayer(modifier);
                break;
            case false:
                HealPlayer(modifier);
                break;
        }
    }

    private void DamagePlayer(int damageAmount)
    {
        if (health - damageAmount <= 0)
        {
            health = 0; 
            GetComponent<PlayerDeathController>().OnPlayerDeath();
        }
        else
        {
            health -= damageAmount;
        }
        
        healthUI.OnHealthModification();
    }

    private void HealPlayer(int healAmount)
    {
        if (health + healAmount >= 100) health = 100;
        else health += healAmount;
    }


    public int GetHealth()
    {
        return health;
    }
    
}
