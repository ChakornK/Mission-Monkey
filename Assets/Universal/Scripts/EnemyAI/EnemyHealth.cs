using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;    // Can be changed in the editor
    
    // This stuff should be pretty straightforward for anyone to understand so the "great commenting of April 2024" won't do much here
    public void DamageAI(int damage)
    {
        if(health - damage <= 0) Destroy(gameObject);
        else health -= damage;
    }
    
    // TODO: Switch to RandomAIDamage later for most weapons
    public void RandomAIDamage(int minDamage, int maxDamage)
    {
        int damageAmount = Random.Range(minDamage, maxDamage);
        
        if (health - damageAmount <= 0) Destroy(gameObject);
        else health -= damageAmount;
    }
}
