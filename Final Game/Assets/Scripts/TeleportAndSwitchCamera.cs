using System.Collections;
using UnityEngine;

public class TeleportAndSwitchCamera : MonoBehaviour
{
    public Transform teleportLocation;
    public Camera newCamera;
    public float disableControlDuration = 2f;
    public float destroyAfterSeconds = 16f;
    private Camera mainCamera;
    private PlayerMovement playerMovement;
    public GameObject panel; // Reference to the panel you want to activate/deactivate

    void Start()
    {
        mainCamera = Camera.main;
        if (panel != null)
        {
            panel.SetActive(false); // Ensure the panel is deactivated at the start
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                StartCoroutine(TeleportPlayer(other.gameObject));
            }

            StartCoroutine(DestroyTriggerAfterTime());
        }
    }

    IEnumerator TeleportPlayer(GameObject player)
    {
        playerMovement.DisableMovement();

        // Activate the panel
        if (panel != null)
        {
            panel.SetActive(true);
        }

        if (newCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            newCamera.gameObject.SetActive(true);
        }

        player.transform.position = teleportLocation.position;

        yield return new WaitForSeconds(disableControlDuration);

        if (newCamera != null)
        {
            newCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
        }

        // Deactivate the panel after the teleportation and control re-enable
        if (panel != null)
        {
            panel.SetActive(false);
        }

        playerMovement.EnableMovement();
    }

    IEnumerator DestroyTriggerAfterTime()
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        Destroy(gameObject);
    }
}
