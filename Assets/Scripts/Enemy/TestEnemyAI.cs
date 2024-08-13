using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyAI : EnemyAI
{
    [SerializeField] private State testCurrentState;

    [SerializeField] private Transform testPlayer;
    [SerializeField] private float testChaseRange, testAttackRange, testPatrolSpeed, testChaseSpeed;


    private NavMeshAgent testNavAgent;

    [SerializeField] private Transform[] testPatrolPoints;  // Array of patrol points
    [SerializeField] private int testCurrentPatrolIndex;
    [SerializeField] private float testIdleTime = 3f;  // Time spent in idle state
    [SerializeField] private float testIdleTimer = 0f;

    [SerializeField] private EnemyStats testStats;
    [SerializeField] private TestEnemy enemy;
    //protected Animator animator;

    protected override void Start()
    {
        testNavAgent = GetComponent<NavMeshAgent>();
        // animator = GetComponent<Animator>();
        testCurrentState = State.Idle;
        testNavAgent.updateRotation = false;
        testNavAgent.updateUpAxis = false;
        testCurrentPatrolIndex = 0;

        testChaseRange = testStats.chaseRange;
        testChaseSpeed = testStats.chaseSpeed;
        testPatrolSpeed = testStats.patrolSpeed;
        testAttackRange = testStats.attackRange;

        enemy = GetComponent<TestEnemy>();
    }

    protected override void Update()
    {
        //navAgent.SetDestination(testPlayer.position);

        //FSM
        switch (testCurrentState)
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
        testIdleTimer += Time.deltaTime;

        if (testIdleTimer >= testIdleTime)
        {
            testCurrentState = State.Patrol;
            testIdleTimer = 0f;
        }

        // Check if testPlayer is within chase range
        if (Vector3.Distance(transform.position, testPlayer.position) < testChaseRange)
        {
            testCurrentState = State.Chase;
        }

        if (enemy.testCurrentHealth <= 0)
        {
            testCurrentState = State.Dead;
        }
    }

    protected override void Patrol()
    {
        // animator.SetBool("IsMoving", true);

        testNavAgent.speed = testPatrolSpeed;

        // Move towards the current patrol point
        testNavAgent.SetDestination(testPatrolPoints[testCurrentPatrolIndex].position);

        // If the enemy reaches the patrol point, switch to the next one
        if (Vector3.Distance(transform.position, testPatrolPoints[testCurrentPatrolIndex].position) < 2f)
        {
            testCurrentPatrolIndex = (testCurrentPatrolIndex + 1) % testPatrolPoints.Length;
            testCurrentState = State.Idle;  // Switch back to Idle after reaching a patrol point
        }


        // Transition to Chase if testPlayer is close
        if (Vector3.Distance(transform.position, testPlayer.position) < testChaseRange)
        {
            testCurrentState = State.Chase;
        }

        if (enemy.testCurrentHealth <= 0)
        {
            testCurrentState = State.Dead;
        }
    }

    protected override void Chase()
    {
        // animator.SetBool("IsMoving", true);
        testNavAgent.speed = testChaseSpeed;

        // Move towards the testPlayer
        testNavAgent.SetDestination(testPlayer.position);

        // Transition to Attack if within attack range
        if (Vector3.Distance(transform.position, testPlayer.position) < testAttackRange)
        {
            testCurrentState = State.Attack;
        }
        // Transition back to Idle if testPlayer is too far
        else if (Vector3.Distance(transform.position, testPlayer.position) > testChaseRange)
        {
            testCurrentState = State.Idle;
        }

        if (enemy.testCurrentHealth <= 0)
        {
            testCurrentState = State.Dead;
        }
    }

    protected override void Attack()
    {
        // animator.SetBool("IsMoving", false);

        // Face the testPlayer

        Vector3 dir = testPlayer.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1f);

        // Transition back to Chase if testPlayer moves out of attack range
        if (Vector3.Distance(transform.position, testPlayer.position) > testAttackRange)
        {
            testCurrentState = State.Chase;
        }

        if (enemy.testCurrentHealth <= 0)
        {
            testCurrentState = State.Dead;
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
        Gizmos.DrawWireSphere(transform.position, testChaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, testAttackRange);
    }
}
