using UnityEngine;

public class BehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Transform orientationTransform;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isGameOver;
    private bool canDoubleJump;
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private int points;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Player starting up");
    }

    private void Update()
    {
        if (isGameOver) return;

        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis(HorizontalAxis);
        float moveVertical = Input.GetAxis(VerticalAxis);

        Vector3 forward = orientationTransform.forward;
        Vector3 right = orientationTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * baseSpeed * Time.deltaTime;
        rb.AddForce(movement);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                Jump();
                canDoubleJump = false;
            }
        }
    }

    private void Jump()
    {
        Vector3 jumpVector = new Vector3(0, jumpForce, 0);
        rb.AddForce(jumpVector, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            canDoubleJump = false;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player ded");
            isGameOver = true;
        }
    }
}