using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeaHorseAI : EnemyAI
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
    [SerializeField] private SeaHorse enemy;
    //protected Animator animator;

    public float detectionRange = 5f;  // Range at which bullets are detected
    public float dodgeSpeed = 5f;  // Speed of dodging
    public float dodgeDuration = 0.5f;  // Time spent dodging
    public float shootInterval = 1f;  // Time between each shot
    public Transform bulletPrefab;  // Reference to bullet prefab
    public Transform shootPoint;  // Where the bullets will be shot from

    private bool isDodging = false;  // Flag to check if enemy is dodging
    private float shootTimer = 0f;  // Timer for shooting


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

        enemy = GetComponent<SeaHorse>();
        player = GameObject.Find("Player").transform;
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
            if(patrolPoints.Length > 0)
            {

                currState = State.Patrol;
                idleTimer = 0f;
            }
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

        // Handle attacking behavior
        HandleShooting();

        // Check for incoming bullets
        DetectIncomingBullets();

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

    private void DetectIncomingBullets()
    {
        // Cast a circle to detect incoming bullets (you can adjust this to your needs)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Bullet"))  // Assuming player's bullet has this tag
            {
                // Calculate direction of the bullet and enemy
                Vector2 directionToBullet = hit.transform.position - transform.position;

                // If bullet is close and moving towards the enemy, start dodging
                if (!isDodging && Vector2.Dot(directionToBullet, hit.GetComponent<Rigidbody2D>().velocity.normalized) > 0.5f)
                {
                    StartCoroutine(Dodge(directionToBullet));
                }
            }
        }
    }

    private IEnumerator Dodge(Vector2 directionToBullet)
    {
        isDodging = true;

        // Determine dodge direction (left or right)
        Vector2 dodgeDirection = Vector2.Perpendicular(directionToBullet).normalized;

        // Start moving in the dodge direction
        Vector2 startPosition = transform.position;
        Vector2 dodgeTarget = startPosition + dodgeDirection * dodgeSpeed * dodgeDuration;

        float dodgeTime = 0f;

        while (dodgeTime < dodgeDuration)
        {
            transform.position = Vector2.Lerp(startPosition, dodgeTarget, dodgeTime / dodgeDuration);
            dodgeTime += Time.deltaTime;
            yield return null;
        }

        isDodging = false;
    }

    private void HandleShooting()
    {
        if (isDodging) return;  // Do not shoot while dodging

        shootTimer += Time.deltaTime;

        // Shoot at the player if the timer exceeds the interval
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        // Instantiate bullet and set its direction towards the player
        Transform bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = (player.position - shootPoint.position).normalized * 20f;  // Adjust bullet speed here
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
