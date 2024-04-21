using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavigation : MonoBehaviour
{
    public Transform[] patrolPoints;
    private EnemySight enemySight;
    private NavMeshAgent agent;
    
    private int currentTarget;
    public float navigationUpdateFrequency = 0.15f;
    private bool seenPlayer;

    private void Start() 
    {
        enemySight = GetComponent<EnemySight>();
        agent = GetComponent<NavMeshAgent>();
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
            // All enemies will just be able to know the player's location after seeing them once (because I am a lazy programmer who doesn't deserve any praise)
            if(seenPlayer)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                agent.destination = player.transform.position;
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
                }
            }
            yield return new WaitForSeconds(navigationUpdateFrequency);
        }
    }
    
    private int GetNextPatrolPointIndex(Transform[] patrolPointArray, int currentElement)
    {
        // I somehow learned how ternary expressions work by writing this one line
        return currentTarget + 1 > patrolPointArray.Length ? 0 : currentElement + 1;
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
