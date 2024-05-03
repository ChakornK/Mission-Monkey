using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemySight))]

public class EnemyNavigation : MonoBehaviour
{
    public Transform[] patrolPoints;
    private EnemySight enemySight;
    private NavMeshAgent agent;
    private EnemyEffects enemyEffects;
    
    private int currentTarget;
    private bool seenPlayer;
    
    [FormerlySerializedAs("chanceOfIdleSoundEffectPlaying")] [Tooltip("The code will pick a number between 1 and the number you specify. if the random number generator picks the max of the number you have specified, it will play a sound effect")]
    public int idleSfxFrequency = 25;
    
    public float navigationUpdateFrequency = 0.15f;
    
    private void Start() 
    {
        enemySight = GetComponent<EnemySight>();
        agent = GetComponent<NavMeshAgent>();
        enemyEffects = GetComponent<EnemyEffects>();
        StartCoroutine(Navigate());
    }

    private void Update()
    {
        Vector3 currentTargetPosition = GetCurrentTargetPosition();
        Quaternion targetRotation = Quaternion.LookRotation(currentTargetPosition - transform.position);
        float rotationSpeed =  0.2f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator Navigate()
    {
        while(true)
        {
            // Set up random number generator value to determine if sound effect is to be played 
            bool doesSfxPlay = Random.Range(1, idleSfxFrequency) == idleSfxFrequency;
            // All enemies will just be able to know the player's location after seeing them once (because I am a lazy programmer who doesn't deserve any praise)
            if(seenPlayer)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                agent.destination = player.transform.position;
                if (doesSfxPlay)
                {
                    enemyEffects.playIdleSfx(true);
                }
            }

            else
            {
                // Patrol point navigation if there are any
                if(patrolPoints != null)
                {
                    if(agent.remainingDistance <= agent.stoppingDistance)
                    {
                        // Debug.Log(gameObject.name + " reached its destination. going to next destination");
                        currentTarget = GetNextPatrolPointIndex(patrolPoints, currentTarget);
                    }    
                    agent.destination = patrolPoints[currentTarget].position;
                }

                // Check if the AI has noticed the player 
                if(enemySight.IsPlayerVisible())
                {
                    // Debug.Log(gameObject.name + " has found the player!");
                    seenPlayer = true;
                    enemyEffects.playPlayerFound();
                }
                
                if (doesSfxPlay)
                {
                    enemyEffects.playIdleSfx(false);
                }
            }
            yield return new WaitForSeconds(navigationUpdateFrequency);
        }
    }
    
    private int GetNextPatrolPointIndex(Transform[] patrolPointArray, int currentElement)
    {
        if(currentTarget + 1 > patrolPointArray.Length - 1)
        {
            return 0;   // Start back at beginning of array
        }
        return currentElement + 1;
    }

    private Vector3 GetCurrentTargetPosition()
    {
        return seenPlayer ? GameObject.FindGameObjectWithTag("Player").transform.position : patrolPoints[currentTarget].position;
    }

    public bool HasNoticedPlayer()
    {
        return seenPlayer;
    }
}
