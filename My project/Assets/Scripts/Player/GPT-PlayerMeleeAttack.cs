using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public float attackRange = 1.5f; // Radius of the attack arc
    public float attackAngle = 90f; // Angle of the attack arc
    public LayerMask targetLayer; // Layers that can be hit
    public Transform attackOrigin; // Position where the attack originates
    public float attackCooldown = 0.5f; // Time between attacks
    public float attackDamage = 25f; // Damage dealt to each target

    private float lastAttackTime;

    void Update()
    {
        // Check for left mouse button input
        if (Input.GetMouseButtonDown(0) && Time.time >= lastAttackTime + attackCooldown)
        {
            PerformMeleeAttack();
            lastAttackTime = Time.time;
        }
    }

    private void PerformMeleeAttack()
    {
        // Calculate the attack direction based on mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure we're only dealing with 2D
        Vector3 attackDirection = (mousePosition - attackOrigin.position).normalized;

        // Get all colliders within the attack range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackOrigin.position, attackRange, targetLayer);

        foreach (var collider in hitColliders)
        {
            // Calculate the direction to the target
            Vector3 targetDirection = (collider.transform.position - attackOrigin.position).normalized;

            // Check if the target is within the attack angle
            if (Vector3.Angle(attackDirection, targetDirection) <= attackAngle / 2f)
            {
                // Apply damage if the target has a Health component
                Health targetHealth = collider.GetComponent<Health>();
                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(attackDamage);
                }
            }
        }

        // Visualize the attack arc for debugging (optional)
        Debug.DrawRay(attackOrigin.position, attackDirection * attackRange, Color.red, 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackOrigin == null)
            return;

        // Draw the attack range in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRange);

        // Draw the attack arc
        Gizmos.color = Color.red;
        Vector3 leftLimit = Quaternion.Euler(0, 0, -attackAngle / 2) * transform.right * attackRange;
        Vector3 rightLimit = Quaternion.Euler(0, 0, attackAngle / 2) * transform.right * attackRange;

        Gizmos.DrawRay(attackOrigin.position, leftLimit);
        Gizmos.DrawRay(attackOrigin.position, rightLimit);
    }
}
