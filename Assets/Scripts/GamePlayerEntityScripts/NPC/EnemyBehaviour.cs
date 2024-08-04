using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 3f; // Speed at which the enemy moves
    public float stoppingDistance = 1f; // Distance at which the enemy stops chasing
    public float jumpForce = 1.5f;
    public float backwardForce = 3f; // Backward force
    public float forwardForce = 10f; // Forward force for the jump
    public float waitTime = 1f; // Time to wait before jumping

    private Rigidbody _rb;
    private bool isOnPlatform;

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Calculate the direction to the player
        Vector3 direction = player.position - transform.position;
        //direction.y = 0; // Ensure the enemy stays on the same plane

        // Check if the enemy is within stopping distance
        if (direction.magnitude > stoppingDistance)
        {
            // Normalize the direction vector and move the enemy towards the player
            Vector3 move = direction.normalized * speed * Time.deltaTime;
            _rb.MovePosition(transform.position + move);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy has collided with the platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
            Debug.Log("Enemy is on the platform");
            StartCoroutine(MoveBackwardsAndJump());
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Continue to check if the enemy remains on the platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the enemy has exited the platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
            Debug.Log("Enemy has left the platform");
        }
    }

    private IEnumerator MoveBackwardsAndJump()
    {
        // Move the enemy backward
        Vector3 backwardDirection = -transform.forward;
        _rb.AddForce(backwardDirection * backwardForce, ForceMode.Impulse);

        // Wait for the specified wait time
        yield return new WaitForSeconds(waitTime);

        // Calculate the direction to the player
        Vector3 forwardDirection = transform.forward;
        Vector3 jumpVector = new Vector3(forwardDirection.x, jumpForce, forwardDirection.z);

        // Apply forward and upward force to the Rigidbody to make the enemy jump forward
        _rb.AddForce(jumpVector * forwardForce, ForceMode.Impulse);
    }
}
