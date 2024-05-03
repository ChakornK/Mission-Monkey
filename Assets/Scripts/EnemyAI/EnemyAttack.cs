using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyNavigation))]
[RequireComponent(typeof(EnemySight))]
[RequireComponent(typeof(EnemyEffects))]
public class EnemyAttack : MonoBehaviour
{
    public GameObject attackPoint;
    
    private NavMeshAgent agent;
    private EnemyNavigation enemyNavigation;
    private EnemySight enemySight;
    private EnemyEffects enemyEffects;
    
    public int minDamage, maxDamage;
    public float timeBetweenAttacks, shootingDistance;
    
    [Tooltip("If set to false, enemy will attempt to attack the player with a raycast/direct hit attack")]
    public bool enableAreaOfEffectAttacks;
    
    public bool enableDebug;

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        enemyNavigation = GetComponent<EnemyNavigation>();
        enemySight = GetComponent<EnemySight>();
        enemyEffects = GetComponent<EnemyEffects>();
        
        StartCoroutine(EnemyAttackHandler());   // This entire AI system is built off coroutines lol
    }

    
    private IEnumerator EnemyAttackHandler()
    {
        // (Refactor this later)
        // Co-routine first checks if the agent is within range of the player, then checks if the agent has noticed the player, then checks if the player is visible
        // If all conditions are met, shoot at the player for a pre-determined amount of damage
        
        while(true)
        {
            if(agent.remainingDistance <= shootingDistance && enemyNavigation.HasNoticedPlayer() && enemySight.IsPlayerVisible())
            {
                switch (enableAreaOfEffectAttacks)
                {
                    case true:
                        DirectHitAttack();
                        break;
                    case false:
                        AreaOfEffectAttack();
                        break;
                }
            }
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    private void DirectHitAttack()
    {
        // Fire a raycast towards the player and check if it hits, then do the damage stuff if it hits
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Do some Vector3 math that I don't understand (I fear the math that I'll have to learn in the future)
        Vector3 raycastOrigin = attackPoint.transform.position;
        Vector3 directionToTarget = (player.transform.position - raycastOrigin).normalized;
        
        if (enableDebug)
        {
            Debug.DrawRay(raycastOrigin, directionToTarget);
        }
        
        if(Physics.Raycast(raycastOrigin, directionToTarget, out RaycastHit hit))
        { 
            // The player GameObject SHOULD have PlayerHealth on it no matter what. If it doesn't then that's not a good thing
            int randomizedDamageAmount = Random.Range(minDamage, maxDamage);
            hit.collider.GetComponent<PlayerHealth>().ModifyPlayerHealth(randomizedDamageAmount);
        }
    }

    private void AreaOfEffectAttack()
    {
        // TODO: AoE Attacks
        // Would love to get this implemented right now but it involves me doing silly ai accuracy stuff and I really don't want to deal with it on a time crunch for the
        // School assignment bit of this update cycle, so I'll code it in later
    }
}
