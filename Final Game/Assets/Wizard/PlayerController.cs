using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool isRunning;

    void Start()
    {
        animator = GetComponent<Animator>();

        transform.localScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        isRunning = moveInput != 0;
        animator.SetBool("IsRunning", isRunning);

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
