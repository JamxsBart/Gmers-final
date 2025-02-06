using UnityEngine;
using UnityEngine.SceneManagement;  // Add this for scene management

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;

    public HeartManager hm;  // Correct class name
    public KeyManager km;    // Correct class name

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("heart"))
        {
            Destroy(other.gameObject);
            if (hm != null)
            {
                hm.heartCount++;
            }
            else
            {
                Debug.LogWarning("HeartManager not assigned!");
            }
        }
        else if (other.gameObject.CompareTag("key"))
        {
            Destroy(other.gameObject);
            if (km != null)
            {
                km.keyCount++;
            }
            else
            {
                Debug.LogWarning("KeyManager not assigned!");
            }
        }
        else if (other.gameObject.CompareTag("TravelScene2"))
        {
            // Load a new scene (replace "NewSceneName" with your scene name)
            SceneManager.LoadScene("Level1HouseScene");  // Replace "NewSceneName" with your scene's actual name
        }
    }
}
