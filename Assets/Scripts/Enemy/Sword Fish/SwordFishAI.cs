using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SwordFishAI : EnemyAI
{
    [SerializeField] private State currentState;
    
    [SerializeField] private Transform player;
    private float chaseRange, attackRange, patrolSpeed, chaseSpeed;
    
    
    private NavMeshAgent navAgent;
    
    [SerializeField] private Transform[] patrolPoints;  // Array of patrol points
    [SerializeField] private int currentPatrolIndex = 0;
    [SerializeField] private float idleTime = 3f;  // Time spent in idle state
    [SerializeField] private float idleTimer = 0f;

    [SerializeField] private EnemyStats swordFishStats;
    //protected Animator animator;
    [SerializeField] private SwordFish swordFish;

    [SerializeField] private float chargeSpeed = 10f; // Speed during the charge
    [SerializeField] private float chargeDuration = 1.5f; // Duration of the charge
    [SerializeField] private float chargeCooldown = 2f; // Time between charges
    [SerializeField] private float chargePreparationTime = 0.5f; // Time before charging

    private bool isCharging = false;
    private bool isCooldown = false;
    private Vector2 chargeDirection;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    protected override void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        // animator = GetComponent<Animator>();
        currentState = State.Idle;
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;

        chaseRange = swordFishStats.chaseRange;
        chaseSpeed = swordFishStats.chaseSpeed;
        patrolSpeed = swordFishStats.patrolSpeed;
        attackRange = swordFishStats.attackRange;
    }

    protected override void Update()
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
            case State.Dead:
                Dead();
                break;
        }
    }

    protected override void Idle()
    {
        //animator.SetBool("IsMoving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleTime && !isCharging && !isCooldown)
        {
            currentState = State.Patrol;
            idleTimer = 0f;
        }

        // Check if player is within chase range
        if (!isCooldown && !isCharging && Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            currentState = State.Chase;
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
            currentState = State.Idle;  // Switch back to Idle after reaching a patrol point
        }


        // Transition to Chase if player is close
        if (Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            currentState = State.Chase;
        }
    }

    protected override void Chase()
    {
        // animator.SetBool("IsMoving", true);
        // Make sure the NavMeshAgent is enabled before setting the destination
        if (!navAgent.enabled)
        {
            navAgent.enabled = true;
        }

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

    protected override void Attack()
    {
        // animator.SetBool("IsMoving", false);

        navAgent.enabled = false;


        StartCoroutine(PrepareCharge());
            
    
        // Transition back to Chase if player moves out of attack range
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = State.Chase;
        }
    }

    private IEnumerator PrepareCharge()
    {
        isCharging = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            navAgent.enabled = false;

            // Calculate direction towards player
            chargeDirection = (player.transform.position - transform.position).normalized;

            // Wait for charge preparation time
            yield return new WaitForSeconds(chargePreparationTime);

            // Perform the charge
            Debug.Log("Starting Charge");
            StartCoroutine(Charge());
        }
        else
        {
            isCharging = false; // Stop charging if no player is found
        }
    }

    private IEnumerator Charge()
    {
        Debug.Log("Charging");
        float chargeTime = 0f;
        navAgent.enabled = false;

        rb.velocity = chargeDirection * chargeSpeed;

        while (chargeTime < chargeDuration)
        {
            chargeTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero; // Stop movement after charging
        isCharging = false;

        // Add a delay after the charge to prevent immediate chasing
        yield return new WaitForSeconds(1f);

        StartCoroutine(Cooldown());
        currentState = State.Idle;

    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(chargeCooldown);
        isCooldown = false;

        navAgent.enabled = true;
    }
    protected void Dead()
    {
        swordFish.Die();
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
