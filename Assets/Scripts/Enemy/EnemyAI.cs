using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    protected enum State { Idle, Patrol, Chase, Attack, Dead }
    protected State BasecurrentState;

    protected Transform Baseplayer;
    protected float BasechaseRange, BaseattackRange, BasepatrolSpeed, BasechaseSpeed;


    protected NavMeshAgent BasenavAgent;

    protected Transform[] BasepatrolPoints;  // Array of patrol points
    protected int BasecurrentPatrolIndex;
    protected float BaseidleTime = 3f;  // Time spent in idle state
    protected float BaseidleTimer = 0f;

    protected EnemyStats Basestats;
    //protected Animator animator;

    protected virtual void Start()
    {
        BasenavAgent = GetComponent<NavMeshAgent>();
        // animator = GetComponent<Animator>();
        BasecurrentState = State.Idle;
        BasenavAgent.updateRotation = false;
        BasenavAgent.updateUpAxis= false;
        BasecurrentPatrolIndex = 0;

        BasechaseRange = Basestats.chaseRange;
        BasechaseSpeed = Basestats.chaseSpeed;
        BasepatrolSpeed = Basestats.patrolSpeed;
        BaseattackRange = Basestats.attackRange;
    }

    protected virtual void Update()
    {
        //navAgent.SetDestination(player.position);

        //FSM
        switch (BasecurrentState)
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
        BaseidleTimer += Time.deltaTime;

        if (BaseidleTimer >= BaseidleTime)
        {
            BasecurrentState = State.Patrol;
            BaseidleTimer = 0f;
        }

        // Check if player is within chase range
        if (Vector3.Distance(transform.position, Baseplayer.position) < BasechaseRange)
        {
            BasecurrentState = State.Chase;
        }
    }

    protected virtual void Patrol()
    {
        // animator.SetBool("IsMoving", true);

        BasenavAgent.speed = BasepatrolSpeed;

        // Move towards the current patrol point
        BasenavAgent.SetDestination(BasepatrolPoints[BasecurrentPatrolIndex].position);

        // If the enemy reaches the patrol point, switch to the next one
        if (Vector3.Distance(transform.position, BasepatrolPoints[BasecurrentPatrolIndex].position) < 2f)
        {
            BasecurrentPatrolIndex = (BasecurrentPatrolIndex + 1) % BasepatrolPoints.Length;
            BasecurrentState = State.Idle;  // Switch back to Idle after reaching a patrol point
        }


        // Transition to Chase if player is close
        if (Vector3.Distance(transform.position, Baseplayer.position) < BasechaseRange)
        {
            BasecurrentState = State.Chase;
        }
    }

    protected virtual void Chase()
    {
        // animator.SetBool("IsMoving", true);
        BasenavAgent.speed = BasechaseSpeed;

        // Move towards the player
        BasenavAgent.SetDestination(Baseplayer.position);

        // Transition to Attack if within attack range
        if (Vector3.Distance(transform.position, Baseplayer.position) < BaseattackRange)
        {
            BasecurrentState = State.Attack;
        }
        // Transition back to Idle if player is too far
        else if (Vector3.Distance(transform.position, Baseplayer.position) > BasechaseRange)
        {
            BasecurrentState = State.Idle;
        }
    }

    protected virtual void Attack()
    {
       // animator.SetBool("IsMoving", false);

        // Face the player
        transform.LookAt(Baseplayer);

        // Transition back to Chase if player moves out of attack range
        if (Vector3.Distance(transform.position, Baseplayer.position) > BaseattackRange)
        {
            BasecurrentState = State.Chase;
        }
    }

    // Draw attack and chase range circles in the Scene view
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BasechaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BaseattackRange);
    }
}
