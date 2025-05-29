using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarControllerRB : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private bool canControl = true;
    [SerializeField] public float forwardSpeed = 5f;
    [SerializeField] public float turnSpeed = 200f;

    private float turnDirection = 0f;
    private float speedRestoreTimer = 0f;
    private float originalSpeed;
    private Rigidbody2D rb;

    [Header("Spin")]
    [SerializeField] private float spinTimer = 0f;
    [SerializeField] private float spinDuration = 0.5f;
    [SerializeField] private bool isSpinning = false;
    private Vector2 spinDirection; // New: direction locked during spin

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = forwardSpeed; // store initial speed for resets
    }

    void FixedUpdate()
    {
        if (!canControl) return;


        if (speedRestoreTimer > 0f)
        {
            speedRestoreTimer -= Time.fixedDeltaTime;
            if (speedRestoreTimer <= 0f)
            {
                forwardSpeed = originalSpeed; // restore speed
            }
        }

        if (isSpinning)
        {
            spinTimer -= Time.fixedDeltaTime;
            if (spinTimer <= 0f)
            {
                isSpinning = false;
                rb.angularVelocity = 0f; // stop the spin
            }

            // Maintain original direction while spinning
            rb.velocity = spinDirection * forwardSpeed;
            return; // skip normal movement/steering
        }

        // Normal movement
        rb.velocity = transform.up * forwardSpeed;
        float rotationAmount = -turnDirection * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);
    }

    public void SetControlEnabled(bool enabled)
    {
        canControl = enabled;

        if (!enabled)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public void ApplyTemporarySlow(float newSpeed, float duration)
    {
        forwardSpeed = newSpeed;
        speedRestoreTimer = duration;
    }

    public void ApplySpin(float torque)
    {
        rb.AddTorque(torque, ForceMode2D.Impulse);
        isSpinning = true;
        spinTimer = spinDuration;

        // Lock in current movement direction
        spinDirection = rb.velocity.normalized;
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
