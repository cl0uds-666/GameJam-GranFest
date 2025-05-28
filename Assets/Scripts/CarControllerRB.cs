using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarControllerRB : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float forwardSpeed = 5f;
    [SerializeField] public float turnSpeed = 200f;

    [Header("Camera :3")]
    [SerializeField] public GameObject MainCamera;

    private float turnDirection = 0f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
    }

    void FixedUpdate()
    {
        // Forward movement
        rb.velocity = transform.up * forwardSpeed;

        // Rotation
        float rotationAmount = -turnDirection * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);
    }

    //on trigger call camera turn from camera movement script
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainCamera.GetComponent<CameraMovement>().CameraTurn(90);
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