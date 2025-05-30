using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IdleAutoplay : MonoBehaviour
{
    public float forwardSpeed = 3f;
    public float turnSpeed = 200f; // degrees per second
    public float reachDistance = 0.5f;
    public Transform[] waypoints;

    private Rigidbody2D rb;
    private int currentWaypointIndex = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];

        // Direction to target
        Vector2 direction = (target.position - transform.position).normalized;

        // Desired rotation
        float desiredAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float angleDiff = Mathf.DeltaAngle(rb.rotation, desiredAngle);

        // Rotate smoothly toward the target
        float angleStep = turnSpeed * Time.fixedDeltaTime;
        float newAngle = Mathf.MoveTowardsAngle(rb.rotation, desiredAngle, angleStep);
        rb.MoveRotation(newAngle);

        // Move forward in local "up" direction
        rb.velocity = transform.up * forwardSpeed;

        // If close enough, go to next waypoint
        if (Vector2.Distance(transform.position, target.position) < reachDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
