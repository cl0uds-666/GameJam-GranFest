using UnityEngine;

public class TyreStack : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.5f;             // How much to slow the player
    [SerializeField] private float slowDuration = 1f;                 // Duration of the slow effect
    [SerializeField] private int numberOfTyres = 6;                   // How many rolling tyres to spawn
    [SerializeField] private float tyreSpreadForce = 3f;              // Speed at which tyres spread out
    [SerializeField] private GameObject rollingTyrePrefab;            // Rolling tyre prefab

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        CarControllerRB controller = other.GetComponent<CarControllerRB>();
        if (controller != null)
        {
            float slowedSpeed = Mathf.Max(0.1f, controller.forwardSpeed * slowMultiplier); // avoid 0 or negative speed
            controller.ApplyTemporarySlow(slowedSpeed, slowDuration);
        }

        // Spawn tyres in radial directions
        if (rollingTyrePrefab != null)
        {
            for (int i = 0; i < numberOfTyres; i++)
            {
                float angle = (360f / numberOfTyres) * i;
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                GameObject tyre = Instantiate(rollingTyrePrefab, transform.position, Quaternion.identity);
                RollingTyre rollingTyre = tyre.GetComponent<RollingTyre>();
                if (rollingTyre != null)
                {
                    rollingTyre.Launch(direction.normalized * tyreSpreadForce);
                }
            }
        }

        Destroy(gameObject);
    }
}
