using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarControllerRB : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float turnSpeed = 200f;

    private float turnDirection = 0f;
    private Rigidbody2D rb;

    [Header("Spin")]
    [SerializeField] private float spinTimer = 0f;
    [SerializeField] private float spinDuration = 0.5f;
    [SerializeField] private bool isSpinning = false;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isSpinning)
        {
            spinTimer -= Time.fixedDeltaTime;
            if (spinTimer <= 0f)
            {
                isSpinning = false;
                rb.angularVelocity = 0f; // stop the spin
            }
            rb.velocity = transform.up * forwardSpeed * 0.5f; // reduced speed while spinning
            return; // skip normal turning
        }

        // Normal movement
        rb.velocity = transform.up * forwardSpeed;
        float rotationAmount = -turnDirection * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);
    }

    public void ApplySpin(float torque)
    {
        rb.AddTorque(torque, ForceMode2D.Impulse);
        isSpinning = true;
        spinTimer = spinDuration;
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