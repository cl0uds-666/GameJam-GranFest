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
    private Vector2 spinDirection;

    [Header("Grass Slowdown")]
    [SerializeField] private LayerMask grassLayer; 
    [SerializeField] private float grassSlowMultiplier = 0.6f; 
    [SerializeField] private float overlapRadius = 0.3f;

    [Header("Turning Sprites")]
    [SerializeField] private Sprite straightSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;

    [Header("assorted avery things")]
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private bool isCollidingWithTrigger;
    [SerializeField] private AudioManager AudioManager;

    private SpriteRenderer spriteRenderer;

    [Header("Grass")]

    [SerializeField] private int playerIndex = 0;
    private float grassScoreTimer = 0f;
    private ScoreManager scoreManager;



    private void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        MainCamera = GameObject.Find("Main Camera");
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = forwardSpeed;
        scoreManager = FindFirstObjectByType<ScoreManager>();


        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on child of " + name);
        }
    }


    void FixedUpdate()
    {
        if (!canControl) return;

        float currentSpeed = forwardSpeed;

        // Check if overlapping grass
        bool onGrass = Physics2D.OverlapCircle(transform.position, overlapRadius, grassLayer);
        if (onGrass)
        {
            currentSpeed *= grassSlowMultiplier;

            // Tick down score
            grassScoreTimer += Time.fixedDeltaTime;
            if (grassScoreTimer >= 0.5f) // Deduct 1 point every 0.5s = 2 points per sec
            {
                if (scoreManager != null)
                {
                    scoreManager.DeductScore(playerIndex, 1);
                }
                grassScoreTimer = 0f;
            }
        }
        else
        {
            grassScoreTimer = 0f;
        }


        if (isSpinning)
        {
            spinTimer -= Time.fixedDeltaTime;
            if (spinTimer <= 0f)
            {
                isSpinning = false;
                rb.angularVelocity = 0f;
            }

            rb.velocity = spinDirection * currentSpeed;
            return;
        }

        rb.velocity = transform.up * currentSpeed;
        float rotationAmount = -turnDirection * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);


        if (isCollidingWithTrigger)
        {
            MainCamera.GetComponent<CameraMovement>().CameraTurn(90, gameObject);
            isCollidingWithTrigger = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if it's a camera turn object
        if (collision.CompareTag("CameraTurn"))
        {
            //finds the degrees to turn from the gameobject of this collider, then puts that into the camera turn function
            MainCamera.GetComponent<CameraMovement>().CameraTurn(collision.gameObject.GetComponent<TurnInformation>().TurnDegrees, gameObject);
            isCollidingWithTrigger = true;
        }
        //getting the degrees the camera has to turn from the information script
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //on collision plays sound effect from car that got bumped into
        AudioManager.SFXSource.PlayOneShot(AudioManager.Bump);
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
        spinDirection = rb.velocity.normalized;
    }

    private void UpdateSprite(Sprite sprite)
    {
        if (spriteRenderer != null && sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }



    public void TurnLeft()
    {
        Debug.Log("TurningLeft");
        turnDirection = -1f;
        UpdateSprite(leftSprite);
    }

    public void TurnRight()
    {
        Debug.Log("TurningRight");
        turnDirection = 1f;
        UpdateSprite(rightSprite);
    }

    public void StopTurning()
    {
        turnDirection = 0f;
        UpdateSprite(straightSprite);
    }


}
