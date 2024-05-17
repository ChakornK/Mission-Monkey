using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(EnemyEffects))]
[RequireComponent(typeof(EnemyNavigation))]
[RequireComponent(typeof(EnemySight))]
[RequireComponent(typeof(NavMeshAgent))]

public abstract class EnemyAttacksBase : MonoBehaviour
{
    public GameObject firePoint;
    protected GameObject player; // Scripts need to reference the player GameObject when they are calculating attacks
    
    private EnemySight enemySight;
    private EnemyNavigation enemyNavigation;
    private EnemyEffects enemyEffects;
    private NavMeshAgent enemyAgent;

    [Tooltip("If you want the damage to be fixed, set minDamage and maxDamage to the same values")]
    public int minDamage, maxDamage;
    public float attackingRange, timeBetweenAttacks;
    
    public bool enableDebug;
    
    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyEffects = GetComponent<EnemyEffects>();
        enemyNavigation = GetComponent<EnemyNavigation>();
        enemySight = GetComponent<EnemySight>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        StartCoroutine(performEnemyAttack());
    }

    private IEnumerator performEnemyAttack()
    {
        while (true)
        {
            // If the agents remaining distance to the target is less than or equal to the shooting range AND the player has been spotted AND the player is still visible, attack the player
            if (enemyAgent.remainingDistance <= attackingRange && enemyNavigation.HasNoticedPlayer() && enemySight.IsPlayerVisible())
            {
                // Actual attack handling will be put in a separate method because it's easier to modify it on children of this class imo
                enemyAttack();
            }
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    // For now, attacks will target the player directly, because I'm lazy to implement a random attack system
    protected virtual void enemyAttack()
    {
        enemyEffects.playAttackSfx();
    }

    protected virtual void enemyHitPlayerActions(PlayerHealth playerHealth)
    {
        int currentDamage = Random.Range(minDamage, maxDamage);
        playerHealth.ModifyPlayerHealth(currentDamage);
    }
}
