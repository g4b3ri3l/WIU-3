using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyAI : EnemyAI
{
    [SerializeField] private State currState;

    [SerializeField] private Transform player;
    [SerializeField] private float chaseRange, attackRange, patrolSpeed, chaseSpeed;


    private NavMeshAgent navAgent;

    [SerializeField] private Transform[] patrolPoints;  // Array of patrol points
    [SerializeField] private int currentPatrolIndex;
    [SerializeField] private float idleTime = 3f;  // Time spent in idle state
    [SerializeField] private float idleTimer = 0f;

    [SerializeField] private EnemyStats testStats;
    [SerializeField] private TestEnemy enemy;
    //protected Animator animator;

    protected override void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        // animator = GetComponent<Animator>();
        currState = State.Idle;
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
        currentPatrolIndex = 0;

        chaseRange = testStats.chaseRange;
        chaseSpeed = testStats.chaseSpeed;
        patrolSpeed = testStats.patrolSpeed;
        attackRange = testStats.attackRange;

        enemy = GetComponent<TestEnemy>();
    }

    protected override void Update()
    {
        //navAgent.SetDestination(player.position);

        //FSM
        switch (currState)
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
            case State.Dead:
                Dead();
                break;
        }
    }

    protected override void Idle()
    {
        //animator.SetBool("IsMoving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleTime)
        {
            currState = State.Patrol;
            idleTimer = 0f;
        }

        // Check if player is within chase range
        if (Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            currState = State.Chase;
        }

        if (enemy.health <= 0)
        {
            currState = State.Dead;
        }
    }

    protected override void Patrol()
    {
        // animator.SetBool("IsMoving", true);

        navAgent.speed = patrolSpeed;

        // Move towards the current patrol point
        navAgent.SetDestination(patrolPoints[currentPatrolIndex].position);

        // If the enemy reaches the patrol point, switch to the next one
        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            currState = State.Idle;  // Switch back to Idle after reaching a patrol point
        }


        // Transition to Chase if player is close
        if (Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            currState = State.Chase;
        }

        if (enemy.health <= 0)
        {
            currState = State.Dead;
        }
    }

    protected override void Chase()
    {
        // animator.SetBool("IsMoving", true);
        navAgent.speed = chaseSpeed;

        // Move towards the player
        navAgent.SetDestination(player.position);

        // Transition to Attack if within attack range
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            currState = State.Attack;
        }
        // Transition back to Idle if player is too far
        else if (Vector3.Distance(transform.position, player.position) > chaseRange)
        {
            currState = State.Idle;
        }

        if (enemy.health <= 0)
        {
            currState = State.Dead;
        }
    }

    protected override void Attack()
    {
        // animator.SetBool("IsMoving", false);

        // Face the player

        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1f);

        // Transition back to Chase if player moves out of attack range
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currState = State.Chase;
        }

        if (enemy.health <= 0)
        {
            currState = State.Dead;
        }

    }

    protected void Dead()
    {
        enemy.Die();
    }

    // Draw attack and chase range circles in the Scene view
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
