using UnityEngine;

public class RollingTyre : MonoBehaviour
{
    [SerializeField] private float hitPushForce = 4f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        Vector2 relativeVelocity = GetComponent<Rigidbody2D>().velocity;
        Vector2 contactDir = collision.contacts[0].normal;

        Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
        if (!playerRb) return;

        float angle = Vector2.Angle(contactDir, Vector2.up);

        if (angle < 60f) // Front collision
        {
            playerRb.velocity *= 0.5f;
        }
        else // Side hit
        {
            playerRb.AddForce(relativeVelocity.normalized * hitPushForce, ForceMode2D.Impulse);
        }

        Destroy(gameObject); // tyres disappear after impact
    }
}
