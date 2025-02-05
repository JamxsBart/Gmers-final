using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int points = 1;  // Points awarded by the collectible item
    public GameManager gameManager;  // Reference to the GameManager

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called!");  // Log to check if this is getting triggered
        
        if (other.CompareTag("Player"))  // Check if the colliding object is the player
        {
            Debug.Log("Player collected the item!");  // Confirm the player is detected
            gameManager.AddPoints(points);  // Call AddPoints in GameManager
            Destroy(gameObject);  // Destroy the collectible item after collection
        }
    }
}
