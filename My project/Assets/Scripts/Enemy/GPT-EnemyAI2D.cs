using System.Collections;
using UnityEngine;

public class EnemyAI2D : MonoBehaviour
{
    public Transform[] patrolPoints;       // Patrol points for the enemy
    public float detectionRange = 5f;      // Range within which the enemy detects the player
    public float chaseRange = 7f;          // Range within which the enemy will keep chasing the player
    public Transform player;               // Reference to the player object
    public float speed = 2f;               // Movement speed of the enemy

    private Rigidbody2D rb;
    private int currentPatrolIndex;
    private Vector2 startPosition;
    private bool playerDetected = false;

    private enum EnemyState { Patrolling, Chasing, Returning }
    private EnemyState currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        currentPatrolIndex = 0;
        currentState = EnemyState.Patrolling;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                if (distanceToPlayer <= detectionRange)
                {
                    playerDetected = true;
                    currentState = EnemyState.Chasing;
                }
                break;

            case EnemyState.Chasing:
                ChasePlayer();
                if (distanceToPlayer > chaseRange)
                {
                    playerDetected = false;
                    currentState = EnemyState.Returning;
                }
                break;

            case EnemyState.Returning:
                ReturnToStart();
                if (Vector2.Distance(transform.position, startPosition) < 0.2f)
                {
                    currentState = EnemyState.Patrolling;
                }
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Vector2 targetPosition = patrolPoints[currentPatrolIndex].position;
        MoveTowards(targetPosition);

        // Check if reached the patrol point
        if (Vector2.Distance(transform.position, targetPosition) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        if (playerDetected)
        {
            MoveTowards(player.position);
        }
    }

    void ReturnToStart()
    {
        MoveTowards(startPosition);
    }

    void MoveTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
}
