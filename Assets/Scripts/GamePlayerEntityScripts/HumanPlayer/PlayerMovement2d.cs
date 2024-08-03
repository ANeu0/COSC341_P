using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SphereMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Speed of sphere movement
    public float jumpForce = 8.0f; // Force of the jump
    private Rigidbody rb;
    private bool isGrounded = true; // Check if sphere is grounded

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if sphere collides with ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
