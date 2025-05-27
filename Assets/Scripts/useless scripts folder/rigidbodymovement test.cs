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
        carRigid.velocity = MOVEMENT_RGBDY;

    }

    // Called when Left button is pressed
    public void MoveLeft()
    {
        MOVEMENT_RGBDY += (Vector2.left * horizontalSpeed);
    }

    // Called when Right button is pressed
    public void MoveRight()
    {
        MOVEMENT_RGBDY -= (Vector2.left * horizontalSpeed);
    }

    // Called when no button is pressed
    public void StopMoving()
    {
        //setting velocity to 0 to stop the car
        Debug.Log("movement stopped innit");
        MOVEMENT_RGBDY.x = 0;
    }
}