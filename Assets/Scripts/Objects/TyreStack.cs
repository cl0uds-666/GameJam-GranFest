using UnityEngine;

public class TyreStack : MonoBehaviour
{
    [SerializeField] private GameObject rollingTyrePrefab;
    [SerializeField] private int tyreCount = 4;
    [SerializeField] private float tyreForce = 3f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb) rb.velocity *= 0.5f;

        for (int i = 0; i < tyreCount; i++)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            GameObject tyre = Instantiate(rollingTyrePrefab, transform.position, Quaternion.identity);
            Rigidbody2D tyreRb = tyre.GetComponent<Rigidbody2D>();
            if (tyreRb) tyreRb.AddForce(direction * tyreForce, ForceMode2D.Impulse);
        }

        Destroy(gameObject); 
    }
}
