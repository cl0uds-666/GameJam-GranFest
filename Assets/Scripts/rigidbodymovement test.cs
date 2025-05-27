using UnityEngine;

public class rigidbodymovementtest : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 3f;

    private Rigidbody2D carRigid;

    private float horizontalInput = 0f;

    public Vector2 MOVEMENT_RGBDY;

    public void Start()
    {
        carRigid = gameObject.GetComponent<Rigidbody2D>();
        //carRigid.AddForce(Vector2.up * forwardSpeed * Time.deltaTime);
        MOVEMENT_RGBDY.y = forwardSpeed;
    }
    void Update()
    {
        // Constant forward movement (uo screen)
        //transform.Translate(Vector3.up * forwardSpeed * Time.deltaTime);

        // Apply horizontal movement
        //transform.Translate(Vector3.right * horizontalInput * horizontalSpeed * Time.deltaTime);

        //carRigid.velocity += (Vector2.up * forwardSpeed * Time.deltaTime);
        carRigid.velocity = MOVEMENT_RGBDY;
        //Debug.Log("velocity is" + carRigid.velocity);
    }

    // Called when Left button is pressed
    public void MoveLeft()
    {
        //horizontalInput = -1f;
        //just adding velocity to the left based on the movement speed
        //carRigid.velocity += (Vector2.left * horizontalSpeed);
        MOVEMENT_RGBDY += (Vector2.left * horizontalSpeed);
    }

    // Called when Right button is pressed
    public void MoveRight()
    {
        //just adding velocity to the right based on the movement speed
        //carRigid.velocity += (Vector2.right * horizontalSpeed);
        MOVEMENT_RGBDY -= (Vector2.left * horizontalSpeed);
    }

    // Called when no button is pressed
    public void StopMoving()
    {
        //setting velocity to 0 to stop the car
        Debug.Log("movement stopped innit");
        MOVEMENT_RGBDY.x = 0;
        //horizontalInput = 0f;
    }
}