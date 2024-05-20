using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData; // Reference to the EnemyData ScriptableObject

    private Transform player; // Reference to the player
    private Rigidbody2D rb2d; // Reference to the enemy's Rigidbody2D
    private Vector2 originalPosition; // Store the original position of the enemy
    private bool playerDetected; // Track if the player is detected
    private bool isReturningToOrigin; // Track if the enemy is returning to the origin

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        originalPosition = transform.position; // Store the original position
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Ensure the player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        // Check the distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= enemyData.detectionRadius)
        {
            playerDetected = true;
            isReturningToOrigin = false;

            // Move towards the player if outside the attack distance
            if (distanceToPlayer > enemyData.attackDistance)
            {
                MoveTowardsPlayer();
            }
            else
            {
                // Attack the player if within attack distance
                AttackPlayer();
            }
        }
        else
        {
            playerDetected = false;

            // Return to origin if player is not detected
            if (!isReturningToOrigin)
            {
                StartCoroutine(ReturnToOrigin());
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb2d.velocity = direction * enemyData.moveSpeed;
    }

    private void AttackPlayer()
    {
        rb2d.velocity = Vector2.zero; // Stop moving

        // Here you would trigger an attack animation or behavior
        Debug.Log("Enemy attacks the player!");
    }

    private IEnumerator ReturnToOrigin()
    {
        isReturningToOrigin = true;

        while (Vector2.Distance(transform.position, originalPosition) > 0.1f)
        {
            Vector2 direction = (originalPosition - (Vector2)transform.position).normalized;
            rb2d.velocity = direction * enemyData.moveSpeed;
            yield return null;
        }

        rb2d.velocity = Vector2.zero;
        isReturningToOrigin = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // You can handle interactions here if needed
            Debug.Log("Player detected by the enemy!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player lost by the enemy!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (enemyData == null) return;

        // Visualize the detection radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.detectionRadius);

        // Visualize the attack distance in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyData.attackDistance);
    }
}
