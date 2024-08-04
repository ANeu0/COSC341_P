using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public string targetTag = "Target"; // The tag of the objects the arrow should point to
    public Transform mainObject; // The main object the arrow hovers over
    public float hoverHeight = 2.0f; // Height above the main object

    private Transform closestTarget;

    void Update()
    {
        // Find the closest target with the specified tag
        FindClosestTarget();

        // Hover over the main object
        HoverOverMainObject();

        // Point towards the closest target
        if (closestTarget != null)
        {
            PointTowardsTarget();
        }
    }

    void FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        float closestDistance = Mathf.Infinity;
        closestTarget = null;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(mainObject.position, target.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target.transform;
            }
        }
    }

    void HoverOverMainObject()
    {
        if (mainObject != null)
        {
            Vector3 hoverPosition = mainObject.position + Vector3.up * hoverHeight;
            transform.position = hoverPosition;
        }
    }

    void PointTowardsTarget()
    {
        Vector3 directionToTarget = closestTarget.position - transform.position;
        //directionToTarget.x = 0; // Keep the arrow horizontal
        transform.rotation = Quaternion.LookRotation(directionToTarget);
    }
}