using UnityEngine;
using System.Collections; 

public class EnemyAI : MonoBehaviour
{
    [Header("Settings")]
    public Transform player; // Reference to the player's transform
    public float detectionRadius = 5f; // Detection radius
    public float hoverHeight = 3f; // Distance above the player to hover
    public float moveSpeed = 2f; // Speed of movement
    public float swoopInterval = 3f; // Time between swoops
    public float swoopSpeed = 5f; // Speed during the swoop
    public float swoopHeight = 1f; // Height to swoop down

    private Vector3 startPosition; // Initial position of the enemy
    private Vector3 targetHoverPosition; // Position to hover above the player
    private bool playerDetected = false; // Whether the player is in range
    private bool isSwooping = false; // Whether the enemy is swooping
    private float swoopTimer = 0f; // Timer for swoop intervals

    void Start()
    {
        startPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        if (playerDetected)
        {
            // Move to the hover position above the player
            targetHoverPosition = new Vector3(player.position.x, player.position.y + hoverHeight, transform.position.z);
            if (!isSwooping)
                MoveToPosition(targetHoverPosition);

            // Handle swoop attacks
            swoopTimer += Time.deltaTime;
            if (swoopTimer >= swoopInterval)
            {
                swoopTimer = 0f;
                StartCoroutine(SwoopAttack());
            }
        }
        else
        {
            // Return to the starting position if player is out of range
            MoveToPosition(startPosition);
        }
    }

    private void MoveToPosition(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private IEnumerator SwoopAttack()
    {
        isSwooping = true;

        // Swoop down to the swoop height
        Vector3 swoopPosition = new Vector3(player.position.x, player.position.y + swoopHeight, transform.position.z);
        while (Vector3.Distance(transform.position, swoopPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, swoopPosition, swoopSpeed * Time.deltaTime);
            yield return null;
        }

        // Return to the hover position
        while (Vector3.Distance(transform.position, targetHoverPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetHoverPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isSwooping = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }
}
