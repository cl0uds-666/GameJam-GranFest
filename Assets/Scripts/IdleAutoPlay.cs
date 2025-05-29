using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class IdleAutoplay : MonoBehaviour
{
    public float forwardSpeed = 3f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * forwardSpeed;
    }
}
