using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SawFishAI : EnemyAI
{
    [SerializeField] private State currentState;

    [SerializeField] private Transform Player;
    [SerializeField] private float ChaseRange, AttackRange, PatrolSpeed, ChaseSpeed;


    private NavMeshAgent NavAgent;

    [SerializeField] private Transform[] PatrolPoints;  // Array of patrol points
    [SerializeField] private int CurrentPatrolIndex;
    [SerializeField] private float IdleTime = 3f;  // Time spent in idle state
    [SerializeField] private float IdleTimer = 0f;

    [SerializeField] private EnemyStats Stats;
    [SerializeField] private SawFish enemy;
    //protected Animator animator;

    protected override void Start()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        // animator = GetComponent<Animator>();
        currentState = State.Idle;
        NavAgent.updateRotation = false;
        NavAgent.updateUpAxis = false;
        CurrentPatrolIndex = 0;

        ChaseRange = Stats.chaseRange;
        ChaseSpeed = Stats.chaseSpeed;
        PatrolSpeed = Stats.patrolSpeed;
        AttackRange = Stats.attackRange;

        enemy = GetComponent<SawFish>();
        Player = GameObject.Find("Player").transform;

    }

    protected override void Update()
    {
        //navAgent.SetDestination(testPlayer.position);

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
            case State.Dead:
                Dead();
                break;
        }
    }

    protected override void Idle()
    {
        //animator.SetBool("IsMoving", false);
        IdleTimer += Time.deltaTime;

        if (IdleTimer >= IdleTime)
        {
            currentState = State.Patrol;
            IdleTimer = 0f;
        }

        // Check if testPlayer is within chase range
        if (Vector3.Distance(transform.position, Player.position) < ChaseRange)
        {
            currentState = State.Chase;
        }

        if (enemy.currHealth <= 0)
        {
            currentState = State.Dead;
        }
    }

    protected override void Patrol()
    {
        // animator.SetBool("IsMoving", true);

        NavAgent.speed = PatrolSpeed;

        // Move towards the current patrol point
        NavAgent.SetDestination(PatrolPoints[CurrentPatrolIndex].position);

        // If the enemy reaches the patrol point, switch to the next one
        if (Vector3.Distance(transform.position, PatrolPoints[CurrentPatrolIndex].position) < 2f)
        {
            CurrentPatrolIndex = (CurrentPatrolIndex + 1) % PatrolPoints.Length;
            currentState = State.Idle;  // Switch back to Idle after reaching a patrol point
        }


        // Transition to Chase if testPlayer is close
        if (Vector3.Distance(transform.position, Player.position) < ChaseRange)
        {
            currentState = State.Chase;
        }

        if (enemy.currHealth <= 0)
        {
            currentState = State.Dead;
        }
    }

    protected override void Chase()
    {
        // animator.SetBool("IsMoving", true);
        NavAgent.speed = ChaseSpeed;

        // Move towards the testPlayer
        NavAgent.SetDestination(Player.position);

        // Transition to Attack if within attack range
        if (Vector3.Distance(transform.position, Player.position) < AttackRange)
        {
            currentState = State.Attack;
        }
        // Transition back to Idle if testPlayer is too far
        else if (Vector3.Distance(transform.position, Player.position) > ChaseRange)
        {
            currentState = State.Idle;
        }

        if (enemy.currHealth <= 0)
        {
            currentState = State.Dead;
        }
    }

    protected override void Attack()
    {
        // animator.SetBool("IsMoving", false);

        // Face the testPlayer

        Vector3 dir = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1f);

        // Transition back to Chase if testPlayer moves out of attack range
        if (Vector3.Distance(transform.position, Player.position) > ChaseRange)
        {
            currentState = State.Chase;
        }

        if (enemy.currHealth <= 0)
        {
            currentState = State.Dead;
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
        Gizmos.DrawWireSphere(transform.position, ChaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
