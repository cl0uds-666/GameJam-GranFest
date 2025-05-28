using UnityEngine;

public class OilSpill : MonoBehaviour
{
    [SerializeField] private float spinForce = 300f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb)
        {
            float torque = Random.Range(-spinForce, spinForce);
            rb.AddTorque(torque, ForceMode2D.Impulse);
        }
    }
}
