using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RollingTyre : MonoBehaviour
{
    [SerializeField] private float tyreSpeed = 3f;
    [SerializeField] private float slowdownDuration = 1f;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        rb.velocity = direction.normalized * tyreSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var controller = other.GetComponent<CarControllerRB>();
        if (controller != null)
        {
            Vector2 toPlayer = other.transform.position - transform.position;
            float dot = Vector2.Dot(toPlayer.normalized, other.transform.up);

            if (dot > 0.5f)
            {
                // Tyre hits front of car TEMPORARY slowdown
                controller.ApplyTemporarySlow(0.5f, 1f);
            }
            else
            {
                // Tyre hits from side PUSH
                Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
                if (playerRb)
                {
                    playerRb.AddForce(rb.velocity.normalized * 5f, ForceMode2D.Impulse);
                }
            }
        }

        Destroy(gameObject);
    }
}
