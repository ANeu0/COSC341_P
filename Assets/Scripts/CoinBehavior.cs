using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float bounceSpeed = 2f;
    public float bounceHeight = 0.5f;

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position of the coin
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotate the coin around its Y axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Make the coin bounce up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(1);
            Debug.Log("Coin collided with " + other.gameObject.name);
            Destroy(gameObject); // Destroy the coin object
        }
    }
}
