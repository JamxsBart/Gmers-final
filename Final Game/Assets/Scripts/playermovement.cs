using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool canMove = true;  
    public HeartManager hm;  
    public KeyManager km;   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!canMove) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

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
            SceneManager.LoadScene("Level1HouseScene");
        }
    }
    public void DisableMovement()
    {
        canMove = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }
    public void EnableMovement()
    {
        canMove = true;
        rb.isKinematic = false;
    }
}