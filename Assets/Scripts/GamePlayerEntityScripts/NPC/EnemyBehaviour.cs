using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private float speed = 3f; // Speed at which the enemy moves
    [SerializeField] private float lungeDistance = 0.5f; // Distance within which the enemy lunges at the player
    [SerializeField] private float lungeForce = 10f; // Force applied for the lunge
    [SerializeField] private float framesUntilLunge = 30; // Frames that player has to remain in lungeDistance to trigger a lunge

    private Rigidbody _rb;
    private bool _isOnPlatform;
    private LayerMask platformLayer; // Dynamically assigned platform layer
    private int _framesOfPlayerInLungeProx = 0;

    private void Awake()
    {
        // Get the Rigidbody component attached to this GameObject
        _rb = GetComponent<Rigidbody>();
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned.");
        }
    }

    private void FixedUpdate()
    {
        if (player == null || platformLayer == 0) return;

        // Calculate the direction to the player
        Vector3 direction = player.position - transform.position;

        // Check if the enemy is within lunge distance
        if (direction.magnitude <= lungeDistance)
        {
            _framesOfPlayerInLungeProx += 1;
            if (_framesOfPlayerInLungeProx >= framesUntilLunge)
            {
                LungeAtPlayer(direction);
                _framesOfPlayerInLungeProx = 0;
            }
        }
        else
        {
            _framesOfPlayerInLungeProx = 0;
        }

        // Normalize the direction vector and move the enemy towards the player
        Vector3 move = direction.normalized * speed * Time.deltaTime;
        Vector3 targetPosition = transform.position + move;

        // Check if the target position is still on the platform
        if (_isOnPlatform && IsPositionOnPlatform(targetPosition))
        {
            _rb.MovePosition(targetPosition);
        }
    }

    private bool IsPositionOnPlatform(Vector3 position)
    {
        // Cast a ray downward from the target position to check if it hits the platform
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, 2f, platformLayer))
        {
            return hit.collider.CompareTag("Platform");
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy has collided with a platform and assign the platform layer mask if not already assigned
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isOnPlatform = true;
            if (platformLayer == 0)
            {
                platformLayer = 1 << collision.gameObject.layer; // Assign the platform layer mask
                Debug.Log($"Assigned platform layer: {LayerMask.LayerToName(collision.gameObject.layer)}");
            }
        }
        else if (!collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Continue to check if the enemy remains on the platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the enemy has exited the platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            _isOnPlatform = false;
            Debug.Log("Enemy has left the platform");
        }
    }

    private void LungeAtPlayer(Vector3 direction)
    {
        if (_isOnPlatform)
        {
            // Normalize the direction and apply a force to lunge towards the player
            Vector3 lungeVector = direction.normalized * lungeForce;
            _rb.AddForce(lungeVector, ForceMode.Impulse);
            Debug.Log($"Enemy {gameObject.name} lunges at the player");
        }
    }

}
