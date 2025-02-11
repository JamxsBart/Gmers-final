using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public variable to set enemy health in the Inspector
    public float health = 100f;

    // Method to take damage
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Enemy took damage. Current Health: " + health);

        // Check if health is 0 or below
        if (health <= 0)
        {
            Die(); // Call the die method when health reaches 0
        }
    }

    // Method to handle the enemy's death
    private void Die()
    {
        Debug.Log("Enemy has died!");
        // Add any death logic here, such as playing animations, effects, or destroying the object
        Destroy(gameObject); // Destroy the enemy object from the scene
    }
}
