using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase, Attack }
    [SerializeField] private State currentState;

    [SerializeField] private Transform player;
    [SerializeField] private float chaseRange, attackRange, patrolSpeed, chaseSpeed; 


    private NavMeshAgent navAgent;

    [SerializeField] private Transform[] patrolPoints;  // Array of patrol points
    [SerializeField] private int currentPatrolIndex;
    [SerializeField] private float idleTime = 3f;  // Time spent in idle state
    [SerializeField] private float idleTimer = 0f;

    [SerializeField] private EnemyStats stats;
    //private Animator animator;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
       // animator = GetComponent<Animator>();
        currentState = State.Idle;
        navAgent.updateRotation = false;
        navAgent.updateUpAxis= false;
        currentPatrolIndex = 0;

        chaseRange = stats.chaseRange;
        chaseSpeed = stats.chaseSpeed;
        patrolSpeed = stats.patrolSpeed;
        attackRange = stats.attackRange;
    }

    void Update()
    {
        //navAgent.SetDestination(player.position);

        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }

    void Idle()
    {
        //animator.SetBool("IsMoving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleTime)
        {
            currentState = State.Patrol;
            idleTimer = 0f;
        }

        // Check if player is within chase range
        if (Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            currentState = State.Chase;
        }
    }

    void Patrol()
    {
        // Patrol logic (moving between waypoints, etc.)
        // animator.SetBool("IsMoving", true);

        navAgent.speed = patrolSpeed;

        // Move towards the current patrol point
        navAgent.SetDestination(patrolPoints[currentPatrolIndex].position);

        // If the enemy reaches the patrol point, switch to the next one
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            currentState = State.Idle;  // Switch back to Idle after reaching a patrol point
        }


        // Transition to Chase if player is close
        if (Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            currentState = State.Chase;
        }
    }

    void Chase()
    {
       // animator.SetBool("IsMoving", true);
        navAgent.speed = chaseSpeed;

        // Move towards the player
        navAgent.SetDestination(player.position);

        // Transition to Attack if within attack range
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            currentState = State.Attack;
        }
        // Transition back to Idle if player is too far
        else if (Vector3.Distance(transform.position, player.position) > chaseRange)
        {
            currentState = State.Idle;
        }
    }

    void Attack()
    {
       // animator.SetBool("IsMoving", false);

        // Face the player
        transform.LookAt(player);

        // Attack logic here (e.g., reduce player health)

        // Transition back to Chase if player moves out of attack range
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = State.Chase;
        }
    }

    // Draw attack and chase range circles in the Scene view
    void OnDrawGizmos()
    {
        // Set Gizmo color for chase range (e.g., blue)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Set Gizmo color for attack range (e.g., red)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
