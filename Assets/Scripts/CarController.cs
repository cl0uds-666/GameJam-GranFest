using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float turnSpeed = 3f;

    private float turnDirection = 0f;

    void Update()
    {
        // Constant forward movement (uo screen)
        transform.Translate(Vector3.up * forwardSpeed * Time.deltaTime);

        // Apply horizontal movement
        transform.Rotate(Vector3.forward * -turnDirection * turnSpeed * Time.deltaTime);
    }

    // Called when Left button is pressed
    public void MoveLeft()
    {
        turnDirection = -1f;
    }

    // Called when Right button is pressed
    public void MoveRight()
    {
        turnDirection = 1f;
    }

    // Called when no button is pressed
    public void StopMoving()
    {
        turnDirection = 0f;
    }
}