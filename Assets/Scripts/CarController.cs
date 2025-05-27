using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 3f;

    private float horizontalInput = 0f;

    void Update()
    {
        // Constant forward movement (uo screen)
        //transform.Translate(Vector3.up * forwardSpeed * Time.deltaTime);

        // Apply horizontal movement
        transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);
    }

    // Called when Left button is pressed
    public void MoveLeft()
    {
        horizontalInput = -1f;
    }

    // Called when Right button is pressed
    public void MoveRight()
    {
        horizontalInput = 1f;
    }

    // Called when no button is pressed
    public void StopMoving()
    {
        horizontalInput = 0f;
    }
}