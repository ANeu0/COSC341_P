using Assets.Scripts;
using System;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{

    public float rotationSpeed = 100f;
    public float bounceSpeed = 2f;
    public float bounceHeight = 0.5f;
    public new Renderer renderer;

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
        var newEvent = DateTime.UtcNow;
        if (other.CompareTag("Player"))
        {
            Vector3 objectSize = renderer.bounds.size;

            // Extract individual dimensions
            float width = objectSize.x;
            float height = objectSize.y;
            float depth = objectSize.z;
            //GameManager.Instance.AddScore(1);
            Data data = new Data(name: this.gameObject.name,
                                technique: GameMain.Technique,
                                lastTimeTriggered: GameMain.LastCoinEvent == DateTime.MinValue ? GameMain.GameStartedAt : GameMain.LastCoinEvent,
                                timeTriggered: newEvent,
                                positionX: transform.position.x.ToString(),
                                positionY: transform.position.y.ToString(),
                                positionZ: transform.position.z.ToString(),
                                width: width.ToString(),
                                height: height.ToString(),
                                depth: depth.ToString(),
                                amplitude: Vector3.Distance(startPosition, new Vector3(0, 0, 0)).ToString()
                                );
            CSVHelper.WriteObjToCSV(data, GameMain.DataFileLocation);
            GameMain.LastCoinEvent = newEvent;
            Debug.Log("Coin collided with " + other.gameObject.name);
            Destroy(gameObject); // Destroy the coin object
        }
    }
}
