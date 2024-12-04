using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the target
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Initialize health to maximum
    }

    // Method to apply damage
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Reduce health
        Debug.Log($"{gameObject.name} took {damageAmount} damage. Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die(); // Trigger death if health is zero
        }
    }

    // Method to handle death
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // Remove the target from the scene
    }
}
