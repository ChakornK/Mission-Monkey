using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AiNavAndFov : MonoBehaviour
{
    private GameObject Ai;
    public GameObject Player;
    private NavMeshAgent Agent;
    public int SightRadius, DistanceToPlayer, DefAgentSpeed;
    private int CurrentPatrolNav, DistanceToPatrolPos;
    public bool CanSeePlayer;
    public LayerMask ObstructionMask, PlayerMask;
    public Vector3[] PatrolPositions;
    private Transform PlayerPosition;



    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerPosition = Player.transform;
        DistanceToPlayer = Mathf.FloorToInt(Vector3.Distance(Ai.transform.position, Player.transform.position));
        CurrentPatrolNav = PatrolPositions.Length;
        DistanceToPatrolPos = Mathf.FloorToInt(Agent.remainingDistance);
        Patrol();
    }

    public void Patrol()
    {
        if (Agent.speed == 0) Agent.speed = DefAgentSpeed;
        Agent.SetDestination(PatrolPositions[CurrentPatrolNav]);
        if (CurrentPatrolNav >= PatrolPositions.Length)
        {
            CurrentPatrolNav = 0;
        }
        if (DistanceToPatrolPos == 0)
        {
            CurrentPatrolNav++;
        }
    }
    public void ChasePlayer()
    {
        if (Agent.speed == 0) Agent.speed = DefAgentSpeed;
        Agent.SetDestination(Player.transform.position);
    }
    public void AttackPlayer()
    {
        Agent.speed = 0;

    }

    private IEnumerator FieldOfViewRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }
    public void FieldOfViewCheck()
    {

    }
}