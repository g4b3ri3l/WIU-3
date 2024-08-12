using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase, Attack }
    public State currentState;

    public Transform player;
    public float chaseRange = 5f;
    public float attackRange = 1f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;

    private NavMeshAgent navAgent;
    private Animator animator;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = State.Idle;
        navAgent.updateRotation = false;
        navAgent.updateUpAxis= false;
    }

    void Update()
    {
        navAgent.SetDestination(player.position);

        //switch (currentState)
        //{
        //    case State.Idle:
        //        Idle();
        //        break;
        //    case State.Patrol:
        //        Patrol();
        //        break;
        //    case State.Chase:
        //        Chase();
        //        break;
        //    case State.Attack:
        //        Attack();
        //        break;
        //}
    }

    void Idle()
    {
        animator.SetBool("IsMoving", false);

        // Check if player is within chase range
        if (Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            currentState = State.Chase;
        }
    }

    void Patrol()
    {
        // Patrol logic (moving between waypoints, etc.)
        animator.SetBool("IsMoving", true);

        // Transition to Chase if player is close
        if (Vector3.Distance(transform.position, player.position) < chaseRange)
        {
            currentState = State.Chase;
        }
    }

    void Chase()
    {
        animator.SetBool("IsMoving", true);
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
        animator.SetBool("IsMoving", false);

        // Face the player
        transform.LookAt(player);

        // Attack logic here (e.g., reduce player health)

        // Transition back to Chase if player moves out of attack range
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = State.Chase;
        }
    }
}
