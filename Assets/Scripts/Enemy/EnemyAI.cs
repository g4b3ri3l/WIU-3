using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    protected enum State { Idle, Patrol, Chase, Attack }
    [SerializeField] protected State currentState;

    [SerializeField] protected Transform player;
    [SerializeField] protected float chaseRange, attackRange, patrolSpeed, chaseSpeed;


    protected NavMeshAgent navAgent;

    [SerializeField] protected Transform[] patrolPoints;  // Array of patrol points
    [SerializeField] protected int currentPatrolIndex;
    [SerializeField] protected float idleTime = 3f;  // Time spent in idle state
    [SerializeField] protected float idleTimer = 0f;

    [SerializeField] protected EnemyStats stats;
    //protected Animator animator;

    protected virtual void Start()
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

    protected virtual void Update()
    {
        //navAgent.SetDestination(player.position);

        //FSM
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

    protected virtual void Idle()
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

    protected virtual void Patrol()
    {
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

    protected virtual void Chase()
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

    protected virtual void Attack()
    {
       // animator.SetBool("IsMoving", false);

        // Face the player
        transform.LookAt(player);

        // Transition back to Chase if player moves out of attack range
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = State.Chase;
        }
    }

    // Draw attack and chase range circles in the Scene view
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
