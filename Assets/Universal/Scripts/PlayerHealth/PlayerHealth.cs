using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health = 100;
    private int maxHealth; 
    
    [HideInInspector]
    public bool enforceMaxHealth = true;

    private void Start()
    {
        maxHealth = health;
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

    public void DamagePlayer(int damageDealt)
    {
        health -= damageDealt;
    }

    public void DamagePlayerRandom(int minimumDamage, int maximumDamage)
    {
        int randomizedDamage = Random.Range(minimumDamage, maximumDamage);
        health -= randomizedDamage;
    }

    public void HealPlayer(int healthHealed)
    {
        health += healthHealed;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }
}
