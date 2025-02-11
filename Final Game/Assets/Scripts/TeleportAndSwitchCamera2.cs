using System.Collections;
using UnityEngine;
using TMPro;  // TextMesh Pro namespace for UI

public class TeleportAndSwitchCamera2 : MonoBehaviour
{
    public Transform teleportLocation;  // The teleport destination
    public Camera newCamera;            // The new camera to switch to
    private Camera mainCamera;          // The main camera reference
    private PlayerMovement playerMovement; // Player movement reference
    public GameObject panel;            // Reference to the UI panel with input field
    public TMP_InputField inputField;   // Reference to the TMP input field
    public string correctInput = "teleport"; // Correct input that will destroy the enemy
    public GameObject enemy;            // Reference to the enemy that will be destroyed
    public playerhealth playerHealth;   // Reference to the player's health system
    public AudioSource audioSource;     // Reference to the AudioSource to play sound
    public AudioClip teleportationAudio; // Audio clip to play after teleportation

    private bool isTeleporting = false; // To prevent multiple teleportations

    void Start()
    {
        mainCamera = Camera.main;
        if (panel != null)
        {
            panel.SetActive(false); // Initially, hide the input panel
        }

        // Check if AudioSource and AudioClip are assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned in the inspector.");
        }

        if (teleportationAudio == null)
        {
            Debug.LogError("Teleportation AudioClip is not assigned in the inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            playerHealth = other.GetComponent<playerhealth>(); // Assuming HealthSystem is attached to the player

            if (playerMovement != null && playerHealth != null)
            {
                StartCoroutine(TeleportPlayer(other.gameObject));
            }
        }
    }

    IEnumerator TeleportPlayer(GameObject player)
    {
        isTeleporting = true; // Prevent teleporting multiple times

        // Disable player movement while teleporting
        playerMovement.DisableMovement();

        // Activate the input panel
        if (panel != null)
        {
            panel.SetActive(true);
        }

        // Switch to the new camera
        if (newCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            newCamera.gameObject.SetActive(true);
        }

        // Teleport the player to the destination
        player.transform.position = teleportLocation.position;

        // Play the teleportation audio immediately after teleportation
        if (audioSource != null && teleportationAudio != null)
        {
            Debug.Log("Playing teleportation audio...");
            audioSource.PlayOneShot(teleportationAudio); // Play the audio
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip not set!");
        }

        // Wait until the player presses "Enter"
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); // Wait for the Enter key

        // Check if the input is correct
        if (inputField.text.ToLower() == correctInput.ToLower())
        {
            Debug.Log("Correct input! Destroying enemy and teleportation block...");
            Destroy(enemy);  // Destroy the enemy if the input is correct
            Destroy(gameObject); // Destroy the teleportation block (trigger object)
            inputField.text = "";  // Clear the input field
            panel.SetActive(false); // Hide the input panel

            // Switch back to the main camera
            if (newCamera != null)
            {
                newCamera.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(true);
            }

            playerMovement.EnableMovement(); // Re-enable player movement
        }
        else
        {
            Debug.Log("Incorrect input! Player loses health...");

            // Correct the way we decrease the health
            playerHealth.DecreaseHealth(25); // Call the DecreaseHealth method to subtract health

            inputField.text = "";  // Clear the input field

            // Check if the player has lost all health
            if (playerHealth.health <= 0)
            {
                Debug.Log("Player has no health left! Game over...");
                // Add game over logic here, like disabling controls, showing a game over screen, etc.
            }
            else
            {
                Debug.Log("Incorrect input. Try again!");
            }

            // Wait a moment before allowing another input attempt
            yield return new WaitForSeconds(1f);

            // Allow the player to retry
            StartCoroutine(TeleportPlayer(player)); // Restart the process
        }

        isTeleporting = false; // Allow teleportation again after the process
    }
}
