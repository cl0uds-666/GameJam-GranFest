using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarControllerRB : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float turnSpeed = 200f;

    private float turnDirection = 0f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Forward movement
        rb.velocity = transform.up * forwardSpeed;

        // Rotation
        float rotationAmount = -turnDirection * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);
    }

    public void TurnLeft()
    {
        Debug.Log("TurningLeft");
        turnDirection = -1f;
    }

    public void TurnRight()
    {
        Debug.Log("TurningRight");
        turnDirection = 1f;
    }

    public void StopTurning()
    {
        turnDirection = 0f;
    }
}